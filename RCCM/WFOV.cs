﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TIS.Imaging;
using TIS.Imaging.VCDHelpers;

namespace RCCM
{
    /// <summary>
    /// Class representing DMK Z12G445 camera for the RCCM WFOV
    /// </summary>
    public class WFOV
    {
        #region Properties

        /// <summary>
        /// Flag to indicate connection status of WFOV camera
        /// </summary>
        public bool Available { get; private set; }
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
            get { return this.VCDProp.RangeMin(VCDIDs.VCDID_Focus); }
        }
        /// <summary>
        /// Maximum zoom level of the camera
        /// </summary>
        public int ZoomMax
        {
            get { return this.VCDProp.RangeMax(VCDIDs.VCDID_Focus); }
        }

        #endregion

        #region Instance Variables

        /// <summary>
        /// Imaging user control for displaying the live image
        /// </summary>
        protected TIS.Imaging.ICImagingControl ic;
        /// <summary>
        /// Camera properties accessor
        /// </summary>
        protected VCDSimpleProperty VCDProp;
        /// <summary>
        /// Flag to indicate if a video is being recorded
        /// </summary>
        protected bool recording;
        
        #endregion

        /// <summary>
        /// Constructor for WFOV camera
        /// </summary>
        /// <param name="ui_ic">User control for displaying live image</param>
        /// <param name="configFile">Configuration xml file from which settings will be loaded</param>
        public WFOV(TIS.Imaging.ICImagingControl ui_ic, string configFile)
        {
            this.ic = ui_ic;
            try
            {
                this.ic.LoadDeviceStateFromFile(configFile, true);
                this.Available = true;
            }
            catch (TIS.Imaging.ICException err)
            {
                System.Windows.Forms.MessageBox.Show("Error occurred while initializing WFOV camera. WFOV will be unavailable.\n\n" + err.ToString());
                this.Available = false;
            }
            catch (System.IO.IOException err)
            {
                System.Windows.Forms.MessageBox.Show("WFOV configuration file missing or invalid.");
                this.Available = false;
            }
            this.recording = false;
        }

        #region Methods

        /// <summary>
        /// Connect to camera. Will fail if configuration file referred to invalid or disconnected camera
        /// </summary>
        public void initialize()
        {
            if (this.ic.DeviceValid)
            {
                this.ic.LivePrepare();

                this.VCDProp = VCDSimpleModule.GetSimplePropertyContainer(this.ic.VCDPropertyItems);
            }
        }

        /// <summary>
        /// Begin displaying live image
        /// </summary>
        public void start()
        {
            if (!this.ic.LiveVideoRunning)
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
        }

        /// <summary>
        /// Stop displaying live image. Will also cease video recording, if active
        /// </summary>
        public void stop()
        {
            if (this.ic.LiveVideoRunning)
            {
                if (this.recording)
                {
                    this.stopRecord();
                }
                this.ic.LiveSuspend();
            }
        }

        /// <summary>
        /// Capture live image to file
        /// </summary>
        /// <param name="filename">Filename to save image to. Should have .png extension</param>
        public void snapImage(string filename)
        {
            try
            {
                this.ic.MemorySnapImage();
                this.ic.MemorySaveImage(filename);
            }
            catch (TIS.Imaging.ICException err)
            {
                throw err;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Start recording video to specified path
        /// </summary>
        /// <param name="filename"></param>
        public void record(string filename)
        {
            if (this.ic.DeviceValid)
            {
                if (this.recording == false)
                {
                    this.ic.AviStartCapture(filename, this.ic.AviCompressors[0]);
                    this.recording = true;
                }
            }
        }

        /// <summary>
        /// Stop recording video. Will resume live display after stopping recording
        /// </summary>
        public void stopRecord()
        {
            this.ic.AviStopCapture();
            this.recording = false;
            if (this.ic.DeviceValid)
            {
                this.ic.LiveStart();
            }            
        }

        /// <summary>
        /// Show device property dialog
        /// <warning>Will cause device to crash if "Cancel" button is pressed from property dialog</warning>
        /// </summary>
        public void editProperties()
        {
            this.ic.ShowPropertyDialog();
        }

        /// <summary>
        /// Activate built-in camera autofocus. Requires 2 second sleep to allow autofocus to complete
        /// </summary>
        /// <returns>New focus level</returns>
        public int autoFocus()
        {
            VCDProp.OnePush(VCDIDs.VCDID_Focus);
            // Stupid work-around recommended by TIS:
            // Wait two seconds and assume autofocus will complete by then
            System.Threading.Thread.Sleep(2000);
            return VCDProp.RangeValue[VCDIDs.VCDID_Focus];
        }

        #endregion
    }
}
