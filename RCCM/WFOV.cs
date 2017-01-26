using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TIS.Imaging;
using TIS.Imaging.VCDHelpers;

namespace RCCM
{
    public class WFOV
    {
        protected TIS.Imaging.ICImagingControl ic;
        protected VCDSimpleProperty VCDProp;
        protected VCDPropertyItem focus = null;
        protected bool available;
        protected bool recording;

        public WFOV(TIS.Imaging.ICImagingControl ui_ic, string configFile)
        {
            this.ic = ui_ic;
            try
            {
                this.ic.LoadDeviceStateFromFile(configFile, true);
                this.available = true;
            }
            catch (TIS.Imaging.ICException err)
            {
                System.Windows.Forms.MessageBox.Show("Error occurred while initializing WFOV camera. WFOV will be unavailable.\n\n" + err.ToString());
                this.available = false;
            }
            catch (System.IO.IOException err)
            {
                System.Windows.Forms.MessageBox.Show("WFOV configuration file missing or invalid.");
                this.available = false;
            }
            this.recording = false;
        }

        public void initialize()
        {
            if (this.ic.DeviceValid)
            {
                this.ic.LivePrepare();

                this.VCDProp = VCDSimpleModule.GetSimplePropertyContainer(this.ic.VCDPropertyItems);
                this.focus = this.ic.VCDPropertyItems.FindItem(VCDIDs.VCDElement_OnePush);
            }
        }

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

        public void stop()
        {
            if (this.ic.LiveVideoRunning)
            {
                this.ic.LiveSuspend();
            }
        }

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

        public void record(string filename)
        {
            if (this.ic.DeviceValid)
            {
                if (this.recording == false)
                {
                    string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt.fff}", DateTime.Now);
                    this.ic.AviStartCapture(filename, this.ic.AviCompressors[0]);
                    this.recording = true;
                }
                else
                {
                    this.ic.AviStopCapture();
                    this.ic.LiveStart();
                    this.recording = false;
                }
            }
        }

        public void stopRecord()
        {
            this.ic.AviStopCapture();
            this.recording = false;
            if (this.ic.DeviceValid)
            {
                this.ic.LiveStart();
            }            
        }

        public void editProperties()
        {
            this.ic.ShowPropertyDialog();
        }

        public int getPropertyMin(string prop)
        {
            return VCDProp.RangeMin(prop);
        }

        public int getPropertyMax(string prop)
        {
            return VCDProp.RangeMax(prop);
        }

        public int getPropertyValue(string prop)
        {
            return VCDProp.RangeValue[prop];
        }

        public void autoFocus()
        {
            VCDProp.OnePush(VCDIDs.VCDID_Focus);
        }

        public void setZoom(int val)
        {
            VCDProp.RangeValue[VCDIDs.VCDID_Zoom] = val;
        }

        public void setFocus(int val)
        {
            VCDProp.RangeValue[VCDIDs.VCDID_Focus] = val;
        }

        public bool isAvailable()
        {
            return this.available;
        }
    }
}
