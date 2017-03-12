using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;

using FlyCapture2Managed;
using FlyCapture2Managed.Gui;
using System.Windows.Forms;

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
        protected ManagedCamera camera;
        protected ManagedImage rawImage;
        public ManagedImage ProcessedImage { get; private set; }
        protected bool grabImages;
        protected AutoResetEvent grabThreadExited;
        protected BackgroundWorker grabThread;

        /// <summary>
        /// Indicates if camera is connected
        /// </summary>
        public bool Connected { get; private set; }
        /// <summary>
        /// Camera microns / pixel calibration
        /// </summary>
        public double Scale { get; set; }
        /// <summary>
        /// Height in mm of image
        /// </summary>
        public double Height
        {
            get { return this.Scale / 1000.0 * NFOV.IMG_HEIGHT; }
        }        
        /// <summary>
        /// Height in mm of image
        /// </summary>
        public double Width
        {
            get { return this.Scale / 1000.0 * NFOV.IMG_WIDTH; }
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
            this.camera = new ManagedCamera();
            // Initialize images
            this.rawImage = new ManagedImage();
            this.ProcessedImage = new ManagedImage();
            // Event that occurs when grab thead is exited
            this.grabThreadExited = new AutoResetEvent(false);            
        }

        /// <summary>
        /// Attempt to connect to camera
        /// </summary>
        /// <returns>True if connection is successful</returns>
        public bool Initialize()
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
                Logger.Out(ex.ToString());
                this.Connected = false;
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
            this.Connected = true;
            return true;
        }

        /// <summary>
        /// Stop live capture and release camera
        /// </summary>
        public void Disconnect()
        {
            try
            {
                if (this.camera.IsConnected())
                {
                    this.Stop();
                    this.camera.Disconnect();
                }
            }
            catch (FC2Exception ex)
            {
                // Nothing to do here
                Logger.Out(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                // Nothing to do here
                Logger.Out(ex.ToString());
            }
        }

        /// <summary>
        /// Start live streaming images from camera
        /// </summary>
        public void Start()
        {
            try
            {
                if (this.Connected)
                {
                    this.camera.StartCapture();
                    this.grabImages = true;
                    StartGrabLoop();
                }
            }
            catch (FC2Exception ex)
            {
                this.Connected = false;
                MessageBox.Show("Failed to start camera");
                Logger.Out("Failed to start camera: " + ex.Message);
            }
        }

        /// <summary>
        /// Stop live streaming images
        /// </summary>
        public void Stop()
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
                    /*
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
                    */
                }
                catch (FC2Exception ex)
                {
                    Logger.Out("Error: " + ex.Message);
                    continue;
                }

                lock (this)
                {
                    this.rawImage.Convert(PixelFormat.PixelFormatMono8, this.ProcessedImage);
                }

                worker.ReportProgress(0);
            }

            this.grabThreadExited.Set();
        }

        /// <summary>
        /// Open dialog box for setting camera properties
        /// </summary>
        public void ShowPropertiesDlg()
        {
            CameraControlDialog camCtlDlg = new CameraControlDialog();
            camCtlDlg.Show();
        }

        /*
        public void setMode()
        {
            const Mode Format7Mode = Mode.Mode0;
            const PixelFormat Format7PixelFormat = PixelFormat.PixelFormatMono8;
            // Query for available Format 7 modes
            bool supported = false;
            Format7Info fmt7Info = this.camera.GetFormat7Info(Format7Mode, ref supported);

            Format7ImageSettings fmt7ImageSettings = new Format7ImageSettings();
            fmt7ImageSettings.mode = Format7Mode;
            fmt7ImageSettings.width = fmt7Info.maxWidth;
            fmt7ImageSettings.height = fmt7Info.maxHeight;
            fmt7ImageSettings.offsetX = 0;
            fmt7ImageSettings.offsetY = 0;
            fmt7ImageSettings.pixelFormat = Format7PixelFormat;

            bool settingsValid = false;
            Format7PacketInfo fmt7PacketInfo = this.camera.ValidateFormat7Settings(
                fmt7ImageSettings,
                ref settingsValid);
            
            this.camera.SetFormat7Configuration(
               fmt7ImageSettings,
               fmt7PacketInfo.recommendedBytesPerPacket);
        }
        */

        /// <summary>
        /// Save live image to file
        /// </summary>
        /// <param name="filename">Full path where image will be saved</param>
        public void Snap(string filename)
        {
            if (this.Connected)
            {
                // Get the Bitmap object. Bitmaps are only valid if the
                // pixel format of the ManagedImage is RGB or RGBU.
                System.Drawing.Bitmap bitmap = this.ProcessedImage.bitmap;
                // Save the image
                bitmap.Save(filename);
            }
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

        public void Record(string aviFileName)
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
        public System.Drawing.Bitmap GetLiveImage()
        {
            return this.ProcessedImage.bitmap;
        }
    }
}
