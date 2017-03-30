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
        public static uint PACKET_SIZE = 4000;
        public static uint PACKET_DELAY = 6000;
        public static uint CHANNEL = 0;

        enum AviType
        {
            Uncompressed,
            Mjpg,
            H264
        }
        /// <summary>
        /// Height in pixels of image
        /// </summary>
        public uint PixelHeight
        {
            get { return this.pixelHeight; }
            set { this.pixelHeight = Math.Min(2048, value); }
        }
        protected uint pixelHeight;
        /// <summary>
        /// Width in pixels of image
        /// </summary>
        public uint PixelWidth
        {
            get { return this.pixelWidth; }
            set { this.pixelWidth = Math.Min(2448, value); }
        }
        protected uint pixelWidth;

        protected uint serial;
        protected ManagedGigECamera camera;
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
            //get { return this.Scale / 1000.0 * NFOV.IMG_HEIGHT; }
            get { return this.Scale / 1000.0 * this.PixelHeight; }
        }        
        /// <summary>
        /// Height in mm of image
        /// </summary>
        public double Width
        {
            //get { return this.Scale / 1000.0 * NFOV.IMG_WIDTH; }
            get { return this.Scale / 1000.0 * this.PixelWidth; }
        }
        /// <summary>
        /// Flag indicating if camera is recording video
        /// </summary>
        public bool Recording { get; set; }
        /// <summary>
        /// Filename that currently recording video will save to
        /// </summary>
        protected string videoFileName;

        /// <summary>
        /// Create a NFOV camera from its serial number and apply the specified calibration
        /// </summary>
        /// <param name="serial">Serial number of the camera</param>
        /// <param name="pix2um">Calibration, microns/pixels</param>
        public NFOV(uint serial, double pix2um, uint h, uint w)
        {
            this.serial = serial;
            this.Scale = pix2um;
            this.Recording = false;
            // Initialize camera object. Connection occurs when initialize() is called
            this.camera = new ManagedGigECamera();
            // Save image dimensions to be used
            this.PixelHeight = h;
            this.PixelWidth = w;
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
                GigEImageSettings config = this.camera.GetGigEImageSettings();
                config.height = this.PixelHeight;
                config.width = this.PixelWidth;
                config.offsetX = (2448 - this.PixelWidth) / 2;
                config.offsetY = (2048 - this.PixelHeight) / 2;
                this.camera.SetGigEImageSettings(config);
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
            this.Connected = true;
            this.Start();
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
                    GigEStreamChannel info = this.camera.GetGigEStreamChannelInfo(NFOV.CHANNEL);
                    info.packetSize = NFOV.PACKET_SIZE;
                    info.interPacketDelay = NFOV.PACKET_DELAY;
                    info.destinationIpAddress = this.camera.GetCameraInfo().ipAddress;
                    this.camera.SetGigEStreamChannelInfo(NFOV.CHANNEL, info);

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
            List<ManagedImage> imageList = new List<ManagedImage>();
            int i = 0;

            while (this.grabImages)
            {
                try
                {
                    this.camera.RetrieveBuffer(this.rawImage);

                    if (this.Recording)
                    {
                        if (i == 0)
                        {
                            // Check if the camera supports the FRAME_RATE property
                            CameraPropertyInfo propInfo = this.camera.GetPropertyInfo(PropertyType.FrameRate);

                            float frameRateToUse = 15.0F;
                            if (propInfo.present == true)
                            {
                                // Get the frame rate
                                CameraProperty prop = this.camera.GetProperty(PropertyType.FrameRate);
                                frameRateToUse = prop.absValue;
                            }

                            // Start AVI writer
                            BackgroundWorker bwVideoWriter = new BackgroundWorker();
                            bwVideoWriter.DoWork += delegate (object s, DoWorkEventArgs e2)
                            {
                                this.SaveAviHelper(AviType.Mjpg, ref imageList, this.videoFileName, frameRateToUse);
                            };
                            bwVideoWriter.RunWorkerAsync();
                        }

                        ManagedImage tempImage = new ManagedImage(this.ProcessedImage);
                        imageList.Add(tempImage);
                        i++;
                    }
                    else if (i > 0)
                    {
                        i = 0;
                        this.Recording = false;
                    }
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
            camCtlDlg.Connect(this.camera);
            camCtlDlg.ShowModal();
            GigEImageSettings config = this.camera.GetGigEImageSettings();
            this.PixelWidth = config.width;
            this.PixelHeight = config.height;
        }
        
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
                            option.quality = 90;
                            aviRecorder.AVIOpen(aviFileName, option);
                        }
                        break;
                }

                Logger.Out(string.Format("Appending images to AVI file {0}...", aviFileName));

                int imageCnt = 0;
                while (this.Recording)
                {
                    if (imageCnt < imageList.Count - 1)
                    {
                        aviRecorder.AVIAppend(imageList[imageCnt]);
                        imageList[imageCnt].Dispose();
                        imageCnt++;
                        Logger.Out(string.Format("Appended image {0}", imageCnt));
                    }
                }
                aviRecorder.AVIAppend(imageList[imageList.Count - 1]);
                aviRecorder.AVIClose();
            }
        }

        public void Record(string aviFileName)
        {
            this.Recording = true;
            this.videoFileName = aviFileName;
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
