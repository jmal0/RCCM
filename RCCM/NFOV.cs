using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;

using FlyCapture2Managed;
using FlyCapture2Managed.Gui;

namespace RCCM
{
    /// <summary>
    /// Class that handles connecting to and operating NFOV camera (BlackFly
    /// </summary>
    public class NFOV
    {
        enum AviType
        {
            Uncompressed,
            Mjpg,
            H264
        }
        /// <summary>
        /// Height in pixels of image
        /// </summary>
        static int IMG_HEIGHT = 2048;
        /// <summary>
        /// Width in pixels of image
        /// </summary>
        static int IMG_WIDTH = 2448;

        protected uint serial;
        protected ManagedCameraBase camera;
        protected ManagedImage rawImage;
        protected ManagedImage processedImage;
        protected bool grabImages;
        protected AutoResetEvent grabThreadExited;
        protected BackgroundWorker grabThread;

        /// <summary>
        /// Camera microns / pixel calibration
        /// </summary>
        public double Scale { get; set; }
        /// <summary>
        /// Height in mm of image
        /// </summary>
        public double Height
        {
            get { return this.Scale * NFOV.IMG_HEIGHT; }
        }        
        /// <summary>
        /// Height in mm of image
        /// </summary>
        public double Width
        {
            get { return this.Scale * NFOV.IMG_WIDTH; }
        }
        /// <summary>
        /// Flag indicating if camera is recording video
        /// </summary>
        public bool Recording { get; set; }

        /// <summary>
        /// Create a NFOV camera from its serial number and apply the specified calibration
        /// </summary>
        /// <param name="serial">Serial number of the camera</param>
        /// <param name="pix2um">Calibration, microns/pixels</param>
        public NFOV(uint serial, double pix2um)
        {
            this.serial = serial;
            this.Scale = pix2um;
            this.Recording = false;
            // Initialize camera object. Connection occurs when initialize() is called
            this.camera = new ManagedGigECamera();
            // Initialize images
            this.rawImage = new ManagedImage();
            this.processedImage = new ManagedImage();
            // Event that occurs when grab thead is exited
            this.grabThreadExited = new AutoResetEvent(false);            
        }

        /// <summary>
        /// Attempt to connect to camera
        /// </summary>
        /// <returns>True if connection is successful</returns>
        public bool initialize()
        {
            ManagedBusManager busMgr = new ManagedBusManager();
            try
            {
                ManagedPGRGuid guid = busMgr.GetCameraFromSerialNumber(this.serial);
                this.camera.Connect(guid);
            }
            catch (Exception ex)
            {
                // Connection unsuccessful
                return false;
            }
            // Set embedded timestamp to on
            EmbeddedImageInfo embeddedInfo = this.camera.GetEmbeddedImageInfo();
            embeddedInfo.timestamp.onOff = true;
            this.camera.SetEmbeddedImageInfo(embeddedInfo);
            // Start live capture
            this.camera.StartCapture();
            this.grabImages = true;
            this.StartGrabLoop();
            return true;
        }

        /// <summary>
        /// Stop live capture and release camera
        /// </summary>
        public void disconnect()
        {
            try
            {
                if (this.camera.IsConnected())
                {
                    this.stop();
                    this.camera.Disconnect();
                }
            }
            catch (FC2Exception ex)
            {
                // Nothing to do here
            }
            catch (NullReferenceException ex)
            {
                // Nothing to do here
            }
        }

        /// <summary>
        /// Start live streaming images from camera
        /// </summary>
        public void start()
        {
            this.camera.StartCapture();
            this.grabImages = true;

            StartGrabLoop();
        }

        /// <summary>
        /// Stop live streaming images
        /// </summary>
        public void stop()
        {
            this.grabImages = false;

            try
            {
                if (this.camera.IsConnected())
                {
                    this.camera.StopCapture();
                }
            }
            catch (FC2Exception ex)
            {
                Logger.Out("Failed to stop camera: " + ex.Message);
            }
            catch (NullReferenceException)
            {
                Logger.Out("Camera is null");
            }
        }

        /// <summary>
        /// Initializes background thread for capturing live images
        /// </summary>
        private void StartGrabLoop()
        {
            this.grabThread = new BackgroundWorker();
            this.grabThread.DoWork += new DoWorkEventHandler(GrabLoop);
            this.grabThread.WorkerReportsProgress = true;
            this.grabThread.RunWorkerAsync();
        }

        /// <summary>
        /// Background worker function for getting live images
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrabLoop(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            List<ManagedImage> imageList = new List<ManagedImage>(100);
            int i = 0;

            while (this.grabImages)
            {
                try
                {
                    this.camera.RetrieveBuffer(this.rawImage);

                    if (this.Recording && i < 100)
                    {
                        ManagedImage tempImage = new ManagedImage(rawImage);
                        imageList.Add(tempImage);
                        i++;
                    }
                    else if (i > 0)
                    {
                        i = 0;

                        // Check if the camera supports the FRAME_RATE property
                        CameraPropertyInfo propInfo = this.camera.GetPropertyInfo(PropertyType.FrameRate);

                        float frameRateToUse = 15.0F;
                        if (propInfo.present == true)
                        {
                            // Get the frame rate
                            CameraProperty prop = this.camera.GetProperty(PropertyType.FrameRate);
                            frameRateToUse = prop.absValue;
                        }

                        this.SaveAviHelper(AviType.H264, ref imageList, "test.avi", frameRateToUse);
                        imageList.Clear();
                    }
                }
                catch (FC2Exception ex)
                {
                    Logger.Out("Error: " + ex.Message);
                    continue;
                }

                lock (this)
                {
                    this.rawImage.Convert(PixelFormat.PixelFormatBgr, this.processedImage);
                }

                worker.ReportProgress(0);
            }

            this.grabThreadExited.Set();
        }

        /// <summary>
        /// Open dialog box for setting camera properties
        /// </summary>
        public void showPropertiesDlg()
        {
            CameraControlDialog camCtlDlg = new CameraControlDialog();
            camCtlDlg.Show();
        }

        /// <summary>
        /// Save live image to file
        /// </summary>
        /// <param name="filename">Full path where image will be saved</param>
        public void snap(string filename)
        {
            // Get the Bitmap object. Bitmaps are only valid if the
            // pixel format of the ManagedImage is RGB or RGBU.
            System.Drawing.Bitmap bitmap = processedImage.bitmap;
            // Save the image
            bitmap.Save(filename);
        }

        private void SaveAviHelper(AviType aviType, ref List<ManagedImage> imageList, string aviFileName, float frameRate)
        {
            using (ManagedAVIRecorder aviRecorder = new ManagedAVIRecorder())
            {
                switch (aviType)
                {
                    case AviType.Uncompressed:
                        {
                            AviOption option = new AviOption();
                            option.frameRate = frameRate;
                            aviRecorder.AVIOpen(aviFileName, option);
                        }
                        break;

                    case AviType.Mjpg:
                        {
                            MJPGOption option = new MJPGOption();
                            option.frameRate = frameRate;
                            option.quality = 75;
                            aviRecorder.AVIOpen(aviFileName, option);
                        }
                        break;

                    case AviType.H264:
                        {
                            H264Option option = new H264Option();
                            option.frameRate = frameRate;
                            option.bitrate = 1000000;
                            option.height = Convert.ToInt32(imageList[0].rows);
                            option.width = Convert.ToInt32(imageList[0].cols);
                            aviRecorder.AVIOpen(aviFileName, option);
                        }
                        break;
                }

                Logger.Out(string.Format("Appending {0} images to AVI file {1}...", imageList.Count, aviFileName));

                for (int imageCnt = 0; imageCnt < imageList.Count; imageCnt++)
                {
                    // Append the image to AVI file
                    aviRecorder.AVIAppend(imageList[imageCnt]);

                    Logger.Out(string.Format("Appended image {0}", imageCnt));
                }

                aviRecorder.AVIClose();
            }
        }

        public void record(string aviFileName)
        {
            /*
            // Check if the camera supports the FRAME_RATE property
            CameraPropertyInfo propInfo = this.camera.GetPropertyInfo(PropertyType.FrameRate);

            float frameRateToUse = 15.0F;
            if (propInfo.present == true)
            {
                // Get the frame rate
                CameraProperty prop = this.camera.GetProperty(PropertyType.FrameRate);
                frameRateToUse = prop.absValue;
            }

            SaveAviHelper(AviType.H264, ref imageList, aviFileName, frameRateToUse);
            */
        }

        /// <summary>
        /// Return current live image
        /// </summary>
        /// <returns>Live image as a bitmap</returns>
        public System.Drawing.Bitmap getLiveImage()
        {
            return this.processedImage.bitmap;
        }
    }
}
