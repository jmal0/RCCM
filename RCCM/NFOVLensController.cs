using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gardasoft.Controller.API.Exceptions;
using Gardasoft.Controller.API.Interfaces;
using Gardasoft.Controller.API.Managers;
using Gardasoft.Controller.API.Model;
using Gardasoft.Controller.API.EventsArgs;

namespace RCCM
{
    public class NFOVLensController
    {
        protected ControllerManager manager;
        IController nfov1Controller;
        IController nfov2Controller;

        public NFOVLensController(int nfov1Serial, int nfov2Serial)
        {
            this.manager = ControllerManager.Instance();
            this.manager.DiscoverControllers();
            try
            {
                this.nfov1Controller = this.manager.ControllerFromSerialNumber(nfov1Serial);
                if (this.nfov1Controller == null)
                {
                    System.Windows.Forms.MessageBox.Show("NFOV 1 Lens controller disconnected or invalid.");
                }
            }
            catch (GardasoftException err)
            {
                System.Windows.Forms.MessageBox.Show("Error connencting to NFOV 1 Lens controller. Error meassage:\n\n" + err.ToString());
            }
            try
            {
                this.nfov2Controller = this.manager.ControllerFromSerialNumber(nfov2Serial);
                if (this.nfov2Controller == null)
                {
                    System.Windows.Forms.MessageBox.Show("NFOV 2 Lens controller disconnected or invalid.");
                }
            }
            catch (GardasoftException err)
            {
                System.Windows.Forms.MessageBox.Show("Error connencting to NFOV 2 Lens controller. Error meassage:\n\n" + err.ToString());
            }
        }
    }
}
