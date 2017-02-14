﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public static string GET_STATUS_CMD = "ST";
        public static string SETPOINT_CMD = "RA1";
        public static string SET_NPOINTS_CMD = "RA1";
        public static string SET_FOCALPOWER_CMD = "RS1";
        public static string SAVE_CALIBRATION_CMD = "AW";

        public static Regex PARSE_GET = new Regex(@"Ig([0-9]+)");
        public static Regex PARSE_GETOUTPUT = new Regex(@"AF(\s)+([0-9.-]+)");

        public static int UPDATE_PERIOD = 50;

        protected ControllerManager manager;
        public IController NFOV1Controller { get; private set; }
        public IController NFOV2Controller { get; private set; }

        public double[,] NFOV1Calibration { get; private set; }
        public double[,] NFOV2Calibration { get; private set; }

        protected Timer focusTimer;

        public NFOVLensController(int nfov1Serial, int nfov2Serial, double[,] calibration1, double[,] calibration2)
        {
            this.manager = ControllerManager.Instance();
            this.manager.DiscoverControllers();
            try
            {
                this.NFOV1Controller = this.manager.ControllerFromSerialNumber(nfov1Serial);
                if (this.NFOV1Controller == null)
                {
                    System.Windows.Forms.MessageBox.Show("NFOV 1 Lens controller disconnected or invalid.");
                }
                else
                {
                    this.applyCalibration(calibration1, RCCMStage.RCCM1);
                }
            }
            catch (GardasoftException err)
            {
                System.Windows.Forms.MessageBox.Show("Error connencting to NFOV 1 Lens controller. Error meassage:\n\n" + err.ToString());
            }
            try
            {
                this.NFOV2Controller = this.manager.ControllerFromSerialNumber(nfov2Serial);
                if (this.NFOV2Controller == null)
                {
                    System.Windows.Forms.MessageBox.Show("NFOV 2 Lens controller disconnected or invalid.");
                }
                else
                {
                    this.applyCalibration(calibration2, RCCMStage.RCCM2);
                }
            }
            catch (GardasoftException err)
            {
                System.Windows.Forms.MessageBox.Show("Error connencting to NFOV 2 Lens controller. Error meassage:\n\n" + err.ToString());
            }

            // Create timer to call countLoop periodically
            this.focusTimer = new Timer();
            this.focusTimer.Enabled = false;
            this.focusTimer.Interval = NFOVLensController.UPDATE_PERIOD;
            this.focusTimer.Tick += new EventHandler(focusLoop);
        }
        
        private void focusLoop(object sender, EventArgs e)
        {
            if (this.NFOV1Controller.ConnectionStatus == ControllerConnectionStatus.Healthy)
            {
                double input = this.getReading(RCCMStage.RCCM1);
                double pow = 0;
                int i;
                int n = this.NFOV1Calibration.GetLength(0);
                for (i = 0; i < n && input < this.NFOV1Calibration[i, 0]; i++) ;
                if (i >= n - 1)
                {
                    pow = this.NFOV1Calibration[n - 1, 1];
                }
                else if (i == 0)
                {
                    pow = this.NFOV1Calibration[0, 1];
                }
                else
                {
                    double m = (this.NFOV1Calibration[i + 1, 1] - this.NFOV1Calibration[i, 1])
                               / (this.NFOV1Calibration[i + 1, 0] - this.NFOV1Calibration[i, 0]);
                    double x = input - this.NFOV1Calibration[i, 0];
                    double b = this.NFOV1Calibration[i, 1];
                    pow = m*x + b;
                }
                this.setFocalPower(pow, RCCMStage.RCCM1);
            }
            if (this.NFOV2Controller.ConnectionStatus == ControllerConnectionStatus.Healthy)
            {
                double input = this.getReading(RCCMStage.RCCM2);
                double pow = 0;
                int i;
                int n = this.NFOV2Calibration.GetLength(0);
                for (i = 0; i < n && input < this.NFOV2Calibration[i, 0]; i++) ;
                if (i >= n - 1)
                {
                    pow = this.NFOV2Calibration[n - 1, 1];
                }
                else if (i == 0)
                {
                    pow = this.NFOV2Calibration[0, 1];
                }
                else
                {
                    double m = (this.NFOV2Calibration[i + 1, 1] - this.NFOV2Calibration[i, 1])
                               / (this.NFOV2Calibration[i + 1, 0] - this.NFOV2Calibration[i, 0]);
                    double x = input - this.NFOV2Calibration[i, 0];
                    double b = this.NFOV2Calibration[i, 1];
                    pow = m * x + b;
                }
                this.setFocalPower(pow, RCCMStage.RCCM2);
            }
        }

        public double getReading(RCCMStage stage)
        {
            // Get controller based on specified stage
            IController controller = stage == RCCMStage.RCCM1 ? this.NFOV1Controller : this.NFOV2Controller;

            // Send get input command and pull reading from resulting string
            try
            {
                string response = controller.SendCommand(NFOVLensController.GET_VOLTAGE_CMD);
                Match m = NFOVLensController.PARSE_GET.Match(response);
                // Convert reading to distance
                return (0.51 * Double.Parse(m.Value.Substring(2)) - 59.9) / 100.0;
            }
            catch
            {
                return -1;
            }
        }

        public double getHeight(RCCMStage stage)
        {
            // Get controller based on specified stage
            IController controller = stage == RCCMStage.RCCM1 ? this.NFOV1Controller : this.NFOV2Controller;

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

        public double getFocalPower(RCCMStage stage)
        {
            // Get controller based on specified stage
            IController controller = stage == RCCMStage.RCCM1 ? this.NFOV1Controller : this.NFOV2Controller;

            // Send get input command and pull reading from resulting string
            string response;
            try
            {
                response = controller.SendCommand(NFOVLensController.GET_STATUS_CMD);
                Console.WriteLine(response.Substring(0, 1000));
            }
            catch
            {
                return -10000;
            }
            Match m = NFOVLensController.PARSE_GETOUTPUT.Match(response.Substring(0,1024));
            Console.WriteLine("Match: " + m.Value);
            // Convert reading to distance
            return Double.Parse(m.Value.Substring(3));            
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
            IController controller = stage == RCCMStage.RCCM1 ? this.NFOV1Controller : this.NFOV2Controller;

            // Set number of points in calibration
            try
            {
                controller.SendCommand(string.Format("{0},{1}", NFOVLensController.SET_NPOINTS_CMD, rows));
                // Apply each calibration point
                for (int i = 0; i < rows; i++)
                {
                    string response = controller.SendCommand(string.Format("{0},{1},{2},{3}", NFOVLensController.SETPOINT_CMD, i, data[i, 0], data[i, 1]));
                }
                controller.SendCommand(string.Format("{0}", NFOVLensController.SAVE_CALIBRATION_CMD));
                this.NFOV1Calibration = data;
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Sends command to specified controller to set a constant focal power value
        /// </summary>
        /// <param name="power">Focal power to use, in diopters</param>
        /// <param name="stage">Stage indicating which controller to send the command to</param>
        /// <returns></returns>
        public bool setFocalPower(double power, RCCMStage stage)
        {
            IController controller = stage == RCCMStage.RCCM1 ? this.NFOV1Controller : this.NFOV2Controller;
            try
            {
                controller.SendCommand(string.Format("{0},{1}", NFOVLensController.SET_FOCALPOWER_CMD, power));
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static double ToHeight(double inputVoltage)
        {
            return 44.4444 * inputVoltage + 5.5555; 
        }
    }
}
