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
using System.Text.RegularExpressions;

namespace RCCM
{
    public class NFOVLensController
    {
        public static string GET_VOLTAGE_CMD = "AN3";
        public static string SETPOINT_CMD = "RA1,0,";
        public static string SET_NPOINTS_CMD = "RA1,";

        public static Regex PARSE_GET = new Regex(@"Ig([0-9]+)");

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

        public double getHeight(RCCMStage stage)
        {
            // Get controller based on specified stage
            IController controller = stage == RCCMStage.RCCM1 ? this.nfov1Controller : this.nfov2Controller;

            // Send get input command and pull reading from resulting string
            try
            {
                string response = controller.SendCommand(NFOVLensController.GET_VOLTAGE_CMD);
                Match m = NFOVLensController.PARSE_GET.Match(response);
                // Convert reading to distance
                return 0.226667 * Double.Parse(m.Value.Substring(2)) - 21.0667;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Applies a calibration to the lens. This scales the focal power based off the distance sensor reading according to a piecewise linear interpolant
        /// </summary>        
        /// <param name="data">2D array of calibration data. Column 1 contains input voltage, column 2 contains output voltage</param>
        /// <param name="stage">Parent stage of the lens to indicate which lens to calibrate</param>
        /// <returns></returns>
        public bool applyCalibration(double[,] data, RCCMStage stage)
        {
            int rows = data.GetLength(0);
            int cols = data.GetLength(1);

            // Data must be formatted into rows of input voltage - output voltage pairs
            if (cols != 2)
            {
                return false;
            }
            
            // Get controller based on specified stage
            IController controller = stage == RCCMStage.RCCM1 ? this.nfov1Controller : this.nfov2Controller;

            // Set number of points in calibration
            controller.SendCommand(string.Format("{0},{1}", NFOVLensController.SET_NPOINTS_CMD, rows));
            // Apply each calibration point
            for (int i = 0; i < rows; i++)
            {
                this.nfov1Controller.SendCommand(string.Format("{0},{1},{2},{3}", NFOVLensController.SETPOINT_CMD, i, data[i,0], data[i,1]));
            }
            return true;
        }
    }
}
