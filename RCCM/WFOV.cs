using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    class WFOV
    {
        protected TIS.Imaging.ICImagingControl ic;

        public WFOV(TIS.Imaging.ICImagingControl ui_ic, string configFile)
        {
            this.ic = ui_ic;
            this.ic.LoadDeviceStateFromFile(configFile, true);
            this.ic.LivePrepare();
        }

        public void initialize()
        {

        }

        public void start()
        {
            try
            {
                this.ic.LiveStart();
                this.ic.LiveDisplayHeight = this.ic.ImageHeight / 2;
                this.ic.LiveDisplayWidth = this.ic.ImageWidth / 2;
                this.ic.ScrollbarsEnabled = true;
            }
            catch (TIS.Imaging.ICException err)
            {
                throw err;
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
            this.ic.MemorySaveImage(filename);
        }

        public void editProperties()
        {
            this.ic.ShowPropertyDialog();
        }
    }
}
