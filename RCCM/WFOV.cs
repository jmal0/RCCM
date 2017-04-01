using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TIS.Imaging;
using TIS.Imaging.VCDHelpers;

namespace RCCM
{
    /// <summary>
    /// Class representing DMK Z12G445 camera for the RCCM WFOV
    /// </summary>
    public class WFOV : ICamera
    {
        /// <summary>
        /// Height in pixels of image
        /// </summary>
        public static int IMG_HEIGHT = 960;
        /// <summary>
        /// Width in pixels of image
        /// </summary>
        public static int IMG_WIDTH = 1280;

        #region Properties

        /// <summary>
        /// Flag to indicate connection status of WFOV camera
        /// </summary>
        public bool Available { get; private set; }
        /// <summary>
        /// Camera microns / pixel calibration
        /// </summary>
        public double Scale { get; set; }
        /// <summary>
        /// Height in mm of image
        /// </summary>
        public double Height
        {
            get { return this.Scale / 1000.0 * WFOV.IMG_HEIGHT; }
        }
        /// <summary>
        /// Height in mm of image
        /// </summary>
        public double Width
        {
            get { return this.Scale / 1000.0 * WFOV.IMG_WIDTH; }
        }
        /// <summary>
        /// Zoom level of the camera, an integer between 0 and 100
        /// </summary>
        public int Zoom
        {
            get { return VCDProp.RangeValue[VCDIDs.VCDID_Zoom]; }
            set { VCDProp.RangeValue[VCDIDs.VCDID_Zoom] = value; }
        }
        /// <summary>
        /// Focal distance of the camera, roughly equating to distance in mm
        /// </summary>
        public int Focus
        {
            get { return VCDProp.RangeValue[VCDIDs.VCDID_Focus]; }
            set { VCDProp.RangeValue[VCDIDs.VCDID_Focus] = value; }
        }
        /// <summary>
        /// Minimum focal distance of the camera
        /// </summary>
        public int FocusMin
        {
            get { return this.VCDProp.RangeMin(VCDIDs.VCDID_Focus); }
        }
        /// <summary>
        /// Maximum focal distance of the camera
        /// </summary>
        public int FocusMax
        {
            get { return this.VCDProp.RangeMax(VCDIDs.VCDID_Focus); }
        }
        /// <summary>
        /// Minimum zoom level of the camera
        /// </summary>
        public int ZoomMin
        {
            get { return this.VCDProp.RangeMin(VCDIDs.VCDID_Zoom); }
        }
        /// <summary>
        /// Maximum zoom level of the camera
        /// </summary>
        public int ZoomMax
        {
            get { return this.VCDProp.RangeMax(VCDIDs.VCDID_Zoom); }
        }

        #endregion

        #region Instance Variables

        /// <summary>
        /// Imaging user control for displaying the live image
        /// </summary>
        protected ICImagingControl ic;
        /// <summary>
        /// Camera properties accessor
        /// </summary>
        protected VCDSimpleProperty VCDProp;
        /// <summary>
        /// Flag to indicate if a video is being recorded
        /// </summary>
        public bool Recording { get; private set; }
        /// <summary>
        /// File path to configuration file from which camera is initialized
        /// </summary>
        public string configFile { get; set; }
        
        #endregion

        /// <summary>
        /// Create WFOV camera from configuration file
        /// </summary>
        /// <param name="configFile">Configuration xml file from which settings will be loaded</param>
        public WFOV(string configFile, double pix2um)
        {
            this.configFile = configFile;
            this.Scale = pix2um;
            this.Recording = false;
        }

        #region Methods

        /// <summary>
        /// Connect to camera. Will fail if configuration file referred to invalid or disconnected camera
        /// </summary>
        /// <returns>True if initialization is successful</returns>
        public bool Initialize(ICImagingControl ic)
        {
            this.ic = ic;
            try
            {
                this.ic.LoadDeviceStateFromFile(this.configFile, true);
                this.Available = true;
            }
            catch (TIS.Imaging.ICException err)
            {
                System.Windows.Forms.MessageBox.Show("Error occurred while initializing WFOV camera. WFOV will be unavailable.\n\n" + err.ToString());
                Logger.Out(err.ToString());
                this.Available = false;
                return false;
            }
            catch (System.IO.IOException err)
            {
                System.Windows.Forms.MessageBox.Show("WFOV configuration file missing or invalid.");
                Logger.Out(err.ToString());
                this.Available = false;
                return false;
            }

            if (this.ic.DeviceValid)
            {
                this.ic.LivePrepare();
                this.VCDProp = VCDSimpleModule.GetSimplePropertyContainer(this.ic.VCDPropertyItems);
                this.ic.DeviceLost += this.handleDisconnect;
                return true;
            }
            System.Windows.Forms.MessageBox.Show("Failed to initialize WFOV camera");
            return false;
        }

        private void handleDisconnect(object sender, ICImagingControl.DeviceLostEventArgs e)
        {
            this.Available = false;
        }

        /// <summary>
        /// Begin displaying live image
        /// </summary>
        public void Start()
        {
            if (this.Available && this.ic.DeviceValid && !this.ic.LiveVideoRunning)
            {
                // Device suspended, start it
                try
                {
                    this.ic.LiveStart();
                    this.ic.LiveDisplayDefault = false;
                    this.ic.LiveDisplayHeight = this.ic.ImageHeight / 2;
                    this.ic.LiveDisplayWidth = this.ic.ImageWidth / 2;
                    this.ic.ScrollbarsEnabled = true;
                }
                catch (TIS.Imaging.ICException err)
                {
                    throw err;
                }
            }
            else
            {
                MessageBox.Show("Camera disconnected");
            }
        }

        /// <summary>
        /// Stop displaying live image. Will also cease video recording, if active
        /// </summary>
        public void Stop()
        {
            if (this.Available && this.ic.LiveVideoRunning && this.ic.DeviceValid)
            {
                if (this.Recording)
                {
                    this.StopRecord();
                }
                this.ic.LiveSuspend();
                this.ic.SaveDeviceStateToFile(this.configFile);
            }
            else
            {
                MessageBox.Show("Camera disconnected");
            }
        }

        /// <summary>
        /// Capture live image to file
        /// </summary>
        /// <param name="filename">Filename to save image to. Should have .png extension</param>
        public void Snap(string filename)
        {
            try
            {
                if (this.Available && this.ic.DeviceValid)
                {
                    this.ic.MemorySnapImage();
                    this.ic.MemorySaveImage(filename);
                }
                else
                {
                    MessageBox.Show("Camera disconnected");
                }               
            }
            catch (TIS.Imaging.ICException err)
            {
                Logger.Out(err.ToString());
            }
            catch (Exception err)
            {
                Logger.Out(err.ToString());
            }
        }

        /// <summary>
        /// Start recording video to specified path
        /// </summary>
        /// <param name="filename"></param>
        public void Record(string filename)
        {
            if (this.Available && this.ic.LiveVideoRunning && this.ic.DeviceValid)
            {
                if (this.Recording == false)
                {
                    this.ic.AviStartCapture(filename, this.ic.AviCompressors[0]);
                    this.Recording = true;
                }
            }
            else
            {
                MessageBox.Show("Camera disconnected");
            }
        }

        /// <summary>
        /// Stop recording video. Will resume live display after stopping recording
        /// </summary>
        public void StopRecord()
        {
            if (this.Available && this.ic.DeviceValid)
            {
                this.ic.AviStopCapture();
                this.Recording = false;
                if (this.ic.LiveVideoRunning)
                {
                    this.ic.LiveStart();
                }
            }
            else
            {
                MessageBox.Show("Camera disconnected");
            }
        }

        /// <summary>
        /// Show device property dialog
        /// <warning>Will cause device to crash if "Cancel" button is pressed from property dialog</warning>
        /// </summary>
        public void EditProperties()
        {
            if (this.Available && this.ic.DeviceValid)
            {
                this.ic.ShowPropertyDialog();
            }
            else
            {
                MessageBox.Show("Camera disconnected");
            }
        }

        /// <summary>
        /// Activate built-in camera autofocus. Requires 2 second sleep to allow autofocus to complete
        /// </summary>
        /// <returns>New focus level</returns>
        public int AutoFocus()
        {
            if (this.Available && this.ic.DeviceValid)
            {
                VCDProp.OnePush(VCDIDs.VCDID_Focus);
                // Stupid work-around recommended by TIS:
                // Wait two seconds and assume autofocus will complete by then
                System.Threading.Thread.Sleep(2000);
                return VCDProp.RangeValue[VCDIDs.VCDID_Focus];
            }
            else
            {
                MessageBox.Show("Camera disconnected");
                return 0;
            }
        }

        #endregion
    }
}
