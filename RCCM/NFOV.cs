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
    public class NFOV
    {
        private Form1 parentForm;
        private FlyCapture2Managed.Gui.CameraControlDialog m_camCtlDlg;
        private ManagedCameraBase m_camera = null;
        private ManagedImage m_rawImage;
        private ManagedImage m_processedImage;
        private bool m_grabImages;
        private AutoResetEvent m_grabThreadExited;
        private BackgroundWorker m_grabThread;

        public NFOV(Form1 parent)
        {
            parentForm = parent;

            m_rawImage = new ManagedImage();
            m_processedImage = new ManagedImage();
            m_camCtlDlg = new CameraControlDialog();

            m_grabThreadExited = new AutoResetEvent(false);
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

            m_camCtlDlg.Connect(m_camera);

            CameraInfo camInfo = m_camera.GetCameraInfo();
            PrintCameraInfo(m_camera.GetCameraInfo());

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

        private void UpdateUI(object sender, ProgressChangedEventArgs e)
        {
            this.parentForm.UpdateUI(this.m_processedImage.bitmap);
        }

        private void StartGrabLoop()
        {
            m_grabThread = new BackgroundWorker();
            m_grabThread.ProgressChanged += new ProgressChangedEventHandler(UpdateUI);
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

        static void PrintCameraInfo(CameraInfo camInfo)
        {
            StringBuilder newStr = new StringBuilder();
            newStr.Append("\n*** CAMERA INFORMATION ***\n");
            newStr.AppendFormat("Serial number - {0}\n", camInfo.serialNumber);
            newStr.AppendFormat("Camera model - {0}\n", camInfo.modelName);
            newStr.AppendFormat("Camera vendor - {0}\n", camInfo.vendorName);
            newStr.AppendFormat("Sensor - {0}\n", camInfo.sensorInfo);
            newStr.AppendFormat("Resolution - {0}\n", camInfo.sensorResolution);
            newStr.AppendFormat("Firmware version - {0}\n", camInfo.firmwareVersion);
            newStr.AppendFormat("Firmware build time - {0}\n", camInfo.firmwareBuildTime);
            newStr.AppendFormat("GigE version - {0}.{1}\n", camInfo.gigEMajorVersion, camInfo.gigEMinorVersion);
            newStr.AppendFormat("User defined name - {0}\n", camInfo.userDefinedName);
            newStr.AppendFormat("XML URL 1 - {0}\n", camInfo.xmlURL1);
            newStr.AppendFormat("XML URL 2 - {0}\n", camInfo.xmlURL2);
            newStr.AppendFormat("MAC address - {0}\n", camInfo.macAddress.ToString());
            newStr.AppendFormat("IP address - {0}\n", camInfo.ipAddress.ToString());
            newStr.AppendFormat("Subnet mask - {0}\n", camInfo.subnetMask.ToString());
            newStr.AppendFormat("Default gateway - {0}\n", camInfo.defaultGateway.ToString());

            Console.WriteLine(newStr);
        }
        /*
        void RunSingleCamera(ManagedPGRGuid guid)
        {
            const int NumImages = 10;

            ManagedGigECamera cam = new ManagedGigECamera();

            // Connect to a camera
            cam.Connect(guid);

            // Get the camera information
            CameraInfo camInfo = cam.GetCameraInfo();
            PrintCameraInfo(camInfo);

            // Set camera stream information
            GigEImageSettingsInfo imageSettingsInfo = cam.GetGigEImageSettingsInfo();

            GigEImageSettings imageSettings = new GigEImageSettings();
            imageSettings.offsetX = 0;
            imageSettings.offsetY = 0;
            imageSettings.height = imageSettingsInfo.maxHeight;
            imageSettings.width = imageSettingsInfo.maxWidth;
            imageSettings.pixelFormat = PixelFormat.PixelFormatMono8;

            cam.SetGigEImageSettings(imageSettings);

            // Get embedded image info from camera
            EmbeddedImageInfo embeddedInfo = cam.GetEmbeddedImageInfo();

            // Enable timestamp collection
            if (embeddedInfo.timestamp.available == true)
            {
                embeddedInfo.timestamp.onOff = true;
            }

            // Set embedded image info to camera
            cam.SetEmbeddedImageInfo(embeddedInfo);

            // Start capturing images
            cam.StartCapture();

            ManagedImage rawImage = new ManagedImage();
            for (int imageCnt = 0; imageCnt < NumImages; imageCnt++)
            {
                // Retrieve an image
                cam.RetrieveBuffer(rawImage);

                // Get the timestamp
                TimeStamp timeStamp = rawImage.timeStamp;

                Console.WriteLine(
                   "Grabbed image {0} - {1} {2} {3}",
                   imageCnt,
                   timeStamp.cycleSeconds,
                   timeStamp.cycleCount,
                   timeStamp.cycleOffset);

                // Create a converted image
                ManagedImage convertedImage = new ManagedImage();

                // Convert the raw image
                rawImage.Convert(PixelFormat.PixelFormatBgr, convertedImage);

                // Create a unique filename
                string filename = String.Format(
                   "GigEGrabEx_CSharp-{0}-{1}.bmp",
                   camInfo.serialNumber,
                   imageCnt);

                // Get the Bitmap object. Bitmaps are only valid if the
                // pixel format of the ManagedImage is RGB or RGBU.
                System.Drawing.Bitmap bitmap = convertedImage.bitmap;

                // Save the image
                bitmap.Save(filename);
            }

            // Stop capturing images
            cam.StopCapture();

            // Disconnect the camera
            cam.Disconnect();
        }
        */
    }
}
