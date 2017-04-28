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
    public class NFOV : ICamera
    {
        /// <summary>
        /// Bytes to send in a single packet. Configured for jumbo packets
        /// </summary>
        public static uint PACKET_SIZE = 4000;
        /// <summary>
        /// Added delay between packets to prevent dropped frames
        /// </summary>
        public static uint PACKET_DELAY = 6000;
        /// <summary>
        /// Added delay between packets to prevent dropped frames
        /// </summary>
        public static uint CHANNEL = 0;

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
        /// <summary>
        /// Serial number of camera
        /// </summary>
        protected uint serial;
        /// <summary>
        /// Camera object
        /// </summary>
        protected ManagedGigECamera camera;
        /// <summary>
        /// Binary image from camera buffer
        /// </summary>
        protected ManagedImage rawImage;
        /// <summary>
        /// Converted image
        /// </summary>
        public ManagedImage ProcessedImage { get; private set; }
        /// <summary>
        /// Flag to direct grab thread to get the live image
        /// </summary>
        protected bool grabImages;
        /// <summary>
        /// 
        /// </summary>
        protected AutoResetEvent grabThreadExited;
        /// <summary>
        /// Background worker for grabbing images from camera
        /// </summary>
        protected BackgroundWorker grabThread;

        /// <summary>
        /// Indicates if camera is connected
        /// </summary>
        public bool Connected { get; protected set; }
        /// <summary>
        /// Camera microns / pixel calibration
        /// </summary>
        public double Scale { get; protected set; }
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
        /// Height at which calibration was made
        /// </summary>
        public double CalibrationHeight { get; protected set; }

        /// <summary>
        /// Create a NFOV camera from its serial number and apply the specified calibration
        /// </summary>
        /// <param name="name">Name of camera in settings</param>
        public NFOV(string name)
        {
            this.serial = (uint)Program.Settings.json[name]["camera serial"];
            this.Scale = (double)Program.Settings.json[name]["microns / pixel"];
            this.CalibrationHeight = (double)Program.Settings.json[name]["calibration height"];
            this.Recording = false;
            // Initialize camera object. Connection occurs when initialize() is called
            this.camera = new ManagedGigECamera();
            // Save image dimensions to be used
            this.PixelHeight = (uint)Program.Settings.json[name]["height"];
            this.PixelWidth = (uint)Program.Settings.json[name]["width"];
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
        private void GrabLoop(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            List<ManagedImage> imageList = new List<ManagedImage>();
            int i = 0;

            while (this.grabImages)
            {
                try
                {
                    // Get live image
                    this.camera.RetrieveBuffer(this.rawImage);
                    // Capture frame if recording
                    if (this.Recording)
                    {
                        // Initialize video recording
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
                                this.SaveAviHelper(ref imageList, this.videoFileName, frameRateToUse);
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
                // Lock camera while saving processed image for access on other threads
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
                System.Drawing.Bitmap bitmap = this.GetLiveImage();
                // Save the image
                bitmap.Save(filename);
            }
        }

        /// <summary>
        /// Helper function for creating video file
        /// </summary>
        /// <param name="imageList">Reference to list where frames are being added</param>
        /// <param name="aviFileName">Path to .avi file where video will save</param>
        /// <param name="frameRate">Video framerate to use</param>
        private void SaveAviHelper(ref List<ManagedImage> imageList, string aviFileName, float frameRate)
        {
            using (ManagedAVIRecorder aviRecorder = new ManagedAVIRecorder())
            {
                // Set MJPG codec options
                MJPGOption option = new MJPGOption();
                option.frameRate = frameRate;
                option.quality = 90;

                // Write frames to video as they are being added
                int imageCnt = 0;
                while (this.Recording)
                {
                    // Don't write latest frame as it is incomplete
                    if (imageCnt < imageList.Count - 1)
                    {
                        aviRecorder.AVIAppend(imageList[imageCnt]);
                        imageList[imageCnt].Dispose(); // Dispose to save RAM
                        imageCnt++;
                    }
                }
                // Write remaining frames to file
                for (; imageCnt < imageList.Count; imageCnt++)
                {
                    aviRecorder.AVIAppend(imageList[imageCnt]);
                }
                // Close file
                aviRecorder.AVIClose();
            }
        }

        /// <summary>
        /// Begins recording by signalling to grab loop start of recording
        /// </summary>
        /// <param name="aviFileName">Path to .avi file where video will save</param>
        public void Record(string aviFileName)
        {
            this.Recording = true;
            this.videoFileName = aviFileName;
        }

        /// <summary>
        /// Stops recording by indicating to grab loop that recording is done
        /// </summary>
        public void StopRecord()
        {
            this.Recording = false;
        }
        
        /// <summary>
        /// Set image scale and save current height
        /// </summary>
        /// <param name="rccm"></param>
        /// <param name="scale">New calibration</param>
        public void SetScale(RCCMSystem rccm, double scale)
        {
            // Get z motor for this stage
            Motor z = this == rccm.NFOV1 ? rccm.motors["fine 1 Z"] : rccm.motors["fine 2 Z"];
            this.CalibrationHeight = z.GetPos();
            
            string camera = this == rccm.NFOV1 ? "nfov 1" : "nfov 2";
            Program.Settings.json[camera]["calibration height"] = this.CalibrationHeight;

            this.Scale = scale;
        }

        /// <summary>
        /// Check if measurement conditions match calibration conditions
        /// </summary>
        /// <param name="rccm"></param>
        /// <returns>True if measurement conditions match calibration</returns>
        public bool CheckFOV(RCCMSystem rccm)
        {
            // Get z motor for this stage
            Motor z = this == rccm.NFOV1 ? rccm.motors["fine 1 Z"] : rccm.motors["fine 2 Z"];
            // Get curernt height
            double h = z.GetPos();
            // Check that calibration height and current height are within a tolerance
            return Math.Abs(this.CalibrationHeight - h) < 3*TrioStepperZMotor.ERROR;
        }

        /// <summary>
        /// Return current live image
        /// </summary>
        /// <returns>Live image as a bitmap</returns>
        public System.Drawing.Bitmap GetLiveImage()
        {
            lock (this)
            {
                return new System.Drawing.Bitmap(this.ProcessedImage.bitmap);
            }            
        }
    }
}
