﻿using System;
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

        protected ManagedCameraBase m_camera;
        protected ManagedImage m_rawImage;
        protected ManagedImage m_processedImage;
        protected bool m_grabImages;
        protected AutoResetEvent m_grabThreadExited;
        protected BackgroundWorker m_grabThread;

        /// <summary>
        /// Camera microns / pixel calibration
        /// </summary>
        public double Scale { get; set; }

        static int IMG_HEIGHT = 2048;
        public double Height
        {
            get { return this.Scale * NFOV.IMG_HEIGHT; }
        }
        static int IMG_WIDTH = 2448;
        public double Width
        {
            get { return this.Scale * NFOV.IMG_WIDTH; }
        }

        protected bool recording;

        public NFOV(double pix2um)
        {
            m_rawImage = new ManagedImage();
            m_processedImage = new ManagedImage();
            
            m_grabThreadExited = new AutoResetEvent(false);

            this.Scale = pix2um;
            recording = false;
        }

        public bool initialize(ManagedPGRGuid[] selectedGuids)
        {
            if (selectedGuids.Length == 0)
            {
                return false;
            }

            ManagedPGRGuid guidToUse = selectedGuids[0];

            ManagedBusManager busMgr = new ManagedBusManager();
            InterfaceType ifType = busMgr.GetInterfaceTypeFromGuid(guidToUse);
            m_camera = new ManagedGigECamera();

            // Connect to the first selected GUID
            m_camera.Connect(guidToUse);

            CameraControlDialog camCtlDlg = new CameraControlDialog();
            camCtlDlg.Connect(m_camera);

            CameraInfo camInfo = m_camera.GetCameraInfo();

            // Set embedded timestamp to on
            EmbeddedImageInfo embeddedInfo = m_camera.GetEmbeddedImageInfo();
            embeddedInfo.timestamp.onOff = true;
            m_camera.SetEmbeddedImageInfo(embeddedInfo);

            m_camera.StartCapture();

            m_grabImages = true;

            StartGrabLoop();
            return true;
        }

        public void disconnect()
        {
            try
            {
                this.stop();
                m_camera.Disconnect();
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

        public void start()
        {
            m_camera.StartCapture();

            m_grabImages = true;

            StartGrabLoop();
        }

        public void stop()
        {
            m_grabImages = false;

            try
            {
                m_camera.StopCapture();
            }
            catch (FC2Exception ex)
            {
                Console.WriteLine("Failed to stop camera: " + ex.Message);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Camera is null");
            }
        }
        /*
        private void UpdateUI(object sender, ProgressChangedEventArgs e)
        {
            this.parentForm.UpdateUI(this.m_processedImage.bitmap);
        }
        */
        private void StartGrabLoop()
        {
            m_grabThread = new BackgroundWorker();
            //m_grabThread.ProgressChanged += new ProgressChangedEventHandler(UpdateUI);
            m_grabThread.DoWork += new DoWorkEventHandler(GrabLoop);
            m_grabThread.WorkerReportsProgress = true;
            m_grabThread.RunWorkerAsync();
        }

        private void GrabLoop(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (m_grabImages)
            {
                try
                {
                    m_camera.RetrieveBuffer(m_rawImage);
                }
                catch (FC2Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    continue;
                }

                lock (this)
                {
                    m_rawImage.Convert(PixelFormat.PixelFormatBgr, m_processedImage);
                }

                worker.ReportProgress(0);
            }

            m_grabThreadExited.Set();
        }

        public void showPropertiesDlg()
        {
            CameraControlDialog camCtlDlg = new CameraControlDialog();
            camCtlDlg.Show();
        }

        public void snap(string filename)
        {
            ManagedImage rawImage = new ManagedImage();
            // Retrieve an image
            m_camera.RetrieveBuffer(rawImage);

            // Create a converted image
            ManagedImage convertedImage = new ManagedImage();

            // Convert the raw image
            rawImage.Convert(PixelFormat.PixelFormatBgr, convertedImage);

            // Get the Bitmap object. Bitmaps are only valid if the
            // pixel format of the ManagedImage is RGB or RGBU.
            System.Drawing.Bitmap bitmap = convertedImage.bitmap;

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

                Console.WriteLine("Appending {0} images to AVI file {1}...", imageList.Count, aviFileName);

                for (int imageCnt = 0; imageCnt < imageList.Count; imageCnt++)
                {
                    // Append the image to AVI file
                    aviRecorder.AVIAppend(imageList[imageCnt]);

                    Console.WriteLine("Appended image {0}", imageCnt);
                }

                aviRecorder.AVIClose();
            }
        }

        public void record(string aviFileName)
        {
            List<ManagedImage> imageList = new List<ManagedImage>();
            ManagedImage rawImage = new ManagedImage();

            // Record until state of 'recording' is set to false
            this.recording = true;
            int i = 0; // There is a limit to how many images can be buffered. Do not exceed this limit
            while (i < 100 && this.recording)
            {
                m_camera.RetrieveBuffer(rawImage);
                ManagedImage tempImage = new ManagedImage(rawImage);
                imageList.Add(tempImage);
                i++;
            }

            // Check if the camera supports the FRAME_RATE property
            CameraPropertyInfo propInfo = m_camera.GetPropertyInfo(PropertyType.FrameRate);

            float frameRateToUse = 15.0F;
            if (propInfo.present == true)
            {
                // Get the frame rate
                CameraProperty prop = m_camera.GetProperty(PropertyType.FrameRate);
                frameRateToUse = prop.absValue;
            }

            SaveAviHelper(AviType.Uncompressed, ref imageList, aviFileName, frameRateToUse);
        }

        public System.Drawing.Bitmap getLiveImage()
        {
            return this.m_processedImage.bitmap;
        }

        public bool isRecording()
        {
            return this.recording;
        }

        public void setRecord(bool rec)
        {
            this.recording = rec;
        }
    }
}
