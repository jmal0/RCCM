using System;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

namespace RCCM
{
    /// <summary>
    /// Class for operating Gardasoft controller and focusing both optotune liquid lenses
    /// </summary>
    public class NFOVLensController
    {
        /// <summary>
        /// Gardasoft command string for getting current input voltage
        /// </summary>
        public static string GET_VOLTAGE_CMD = "AN3";
        /// <summary>
        /// Gardasoft command string for getting current status (including output focal power)
        /// </summary>
        public static string GET_STATUS_CMD = "ST";
        /// <summary>
        /// Gardasoft command string for setting output focal power
        /// </summary>
        public static string SET_FOCALPOWER_CMD = "RS1";
        /// <summary>
        /// Regex for parsing get status command response for input voltage
        /// </summary>
        public static Regex PARSE_GET = new Regex(@"Ig([0-9]+)");
        /// <summary>
        /// Regex for parsing status command for output focal power
        /// </summary>
        public static Regex PARSE_GETOUTPUT = new Regex(@"AF(\s)+([0-9.-]+)");
        /// <summary>
        /// Setting for period between focal power updates
        /// </summary>
        public static long UPDATE_PERIOD = (long)Program.Settings.json["distance sensor"]["focus update period"];
        /// <summary>
        /// Minimum height value to send when reading is out of range
        /// </summary>
        public static double MIN_HEIGHT = (double)Program.Settings.json["distance sensor"]["min height"];
        /// <summary>
        /// Maximum height value to send when reading is out of range
        /// </summary>
        public static double MAX_HEIGHT = (double)Program.Settings.json["distance sensor"]["max height"];
        /// <summary>
        /// Exponential moving average filter constant applied to distance reading
        /// </summary>
        public static double alpha = (double)Program.Settings.json["distance sensor"]["height reading filter constant"];
        /// <summary>
        /// Gardasoft controller manager for detecting lens controllers
        /// </summary>
        protected ControllerManager manager;
        /// <summary>
        /// Controller interface for NFOV 1
        /// </summary>
        public IController NFOV1Controller { get; private set; }
        /// <summary>
        /// Controller interface for NFOV 2
        /// </summary>
        public IController NFOV2Controller { get; private set; }
        /// <summary>
        /// Adjustment offset to add to output focal power for NFOV 1
        /// </summary>
        public double FocusOffset1 { get; set; }
        /// <summary>
        /// Adjustment offset to add to output focal power for NFOV 2
        /// </summary>
        public double FocusOffset2 { get; set; }
        /// <summary>
        /// Slope, y-intercept of conversion from input voltage to distance (NFOV 1)
        /// </summary>
        public double[] conversion1 { get; private set; }
        /// <summary>
        /// Slope, y-intercept of conversion from input voltage to distance (NFOV 2)
        /// </summary>
        public double[] conversion2 { get; private set; }
        /// <summary>
        /// Array of input voltage, output focal power pairs to interpolate for NFOV 1
        /// </summary>
        public double[,] NFOV1Calibration { get; private set; }
        /// <summary>
        /// Array of input voltage, output focal power pairs to interpolate for NFOV 2
        /// </summary>
        public double[,] NFOV2Calibration { get; private set; }
        /// <summary>
        /// Current height reading for NFOV 1
        /// </summary>
        public double Height1 { get; private set; }
        /// <summary>
        /// Current height reading for NFOV 2
        /// </summary>
        public double Height2 { get; private set; }
        /// <summary>
        /// Z motor for NFOV 1
        /// </summary>
        public TrioStepperZMotor Motor1 { get; set; }
        /// <summary>
        /// Z motor for NFOV 2
        /// </summary>
        public TrioStepperZMotor Motor2 { get; set; }
        /// <summary>
        /// Flag to indicate if input power should be read
        /// </summary>
        protected bool read;
        /// <summary>
        /// Flag to indicate to background thread if it should autofocus NFOV 1
        /// </summary>
        protected bool readThread1Paused;
        /// <summary>
        /// Flag to indicate to background thread if it should autofocus NFOV 2
        /// </summary>
        protected bool readThread2Paused;
        /// <summary>
        /// Background worker for focusing NFOV 1
        /// </summary>
        protected BackgroundWorker bw1;
        /// <summary>
        /// Background worker for focusing NFOV 2
        /// </summary>
        protected BackgroundWorker bw2;
        /// <summary>
        /// Event handler for when NFOV 1 background thread exits
        /// </summary>
        protected AutoResetEvent readHeight1ThreadExited;
        /// <summary>
        /// Event handler for when NFOV 2 background thread exits
        /// </summary>
        protected AutoResetEvent readHeight2ThreadExited;

        /// <summary>
        /// Initialize autofocusing of NFOV lens controllers
        /// </summary>
        /// <param name="nfov1Serial">Serial number of NFOV 1 gardasoft controller</param>
        /// <param name="nfov2Serial">Serial number of NFOV 2 gardasoft controller</param>
        /// <param name="conversion1">Conversion loaded from settings for NFOV 1</param>
        /// <param name="conversion2">Conversion loaded from settings for NFOV 2</param>
        /// <param name="calibration1">Lens calibration loaded from settings for NFOV 1</param>
        /// <param name="calibration2">Lens calibration loaded from settings for NFOV 2</param>
        public NFOVLensController(int nfov1Serial, int nfov2Serial, double[] conversion1, double[] conversion2, double[,] calibration1, double[,] calibration2)
        {
            this.manager = ControllerManager.Instance();
            this.manager.DiscoverControllers();

            this.Height1 = 0;
            this.Height2 = 0;
            this.FocusOffset1 = 0;
            this.FocusOffset2 = 0;

            // Create NFOV 1 controller
            try
            {
                this.conversion1 = conversion1;
                this.NFOV1Calibration = calibration1;
                this.NFOV1Controller = this.manager.ControllerFromSerialNumber(nfov1Serial);
                if (this.NFOV1Controller == null)
                {
                    MessageBox.Show("NFOV 1 Lens controller disconnected or invalid.");
                }
                else
                {
                    this.ApplyCalibration(calibration1, RCCMStage.RCCM1);
                    this.NFOV1Controller.ConnectionStatusChanged += new EventHandler<ControllerConnectionStatusChangedEventArgs>(this.nfov1ConnectionChanged);
                }
            }
            catch (GardasoftException err)
            {
                MessageBox.Show("Error connencting to NFOV 1 Lens controller. Error meassage:\n\n" + err.ToString());
            }
            // Create NFOV 2 controller
            try
            {
                this.conversion2 = conversion2;
                this.NFOV2Calibration = calibration2;
                this.NFOV2Controller = this.manager.ControllerFromSerialNumber(nfov2Serial);
                if (this.NFOV2Controller == null)
                {
                    MessageBox.Show("NFOV 2 Lens controller disconnected or invalid.");
                }
                else
                {
                    this.ApplyCalibration(calibration2, RCCMStage.RCCM2);
                    this.NFOV2Controller.ConnectionStatusChanged += new EventHandler<ControllerConnectionStatusChangedEventArgs>(this.nfov2ConnectionChanged);
                }
            }
            catch (GardasoftException err)
            {
                MessageBox.Show("Error connencting to NFOV 2 Lens controller. Error meassage:\n\n" + err.ToString());
            }
            // Start background threads
            this.bw1 = new BackgroundWorker();
            this.bw2 = new BackgroundWorker();
            this.bw1.DoWork += new DoWorkEventHandler(this.readHeight1);
            this.bw2.DoWork += new DoWorkEventHandler(this.readHeight2);
            this.readHeight1ThreadExited = new AutoResetEvent(false);
            this.readHeight2ThreadExited = new AutoResetEvent(false);
            this.read = true;
            this.readThread1Paused = false;
            this.readThread2Paused = false;
            this.bw1.RunWorkerAsync();
            this.bw2.RunWorkerAsync();
        }

        /// <summary>
        /// Background thread function for autofocusing NFOV 1
        /// </summary>
        private void readHeight1(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            while (this.read)
            {
                stopwatch.Start();
                if (!this.readThread1Paused && this.NFOV1Controller != null && this.NFOV1Controller.ConnectionStatus == ControllerConnectionStatus.Healthy)
                {
                    double input = this.GetReading(RCCMStage.RCCM1);
                    if (input != -1)
                    {
                        double pow = NFOVLensController.PwlInterp(this.NFOV1Calibration, input);
                        pow += this.FocusOffset1;
                        this.SetFocalPower(pow, RCCMStage.RCCM1);
                        this.Height1 = alpha * this.ToHeight1(input) + (1 - alpha) * this.Height1;
                    }                    
                }
                stopwatch.Stop();
                Thread.Sleep((int)Math.Max(0, NFOVLensController.UPDATE_PERIOD - stopwatch.ElapsedMilliseconds));
            }
            this.readHeight1ThreadExited.Set();
        }
        
        /// <summary>
        /// Background thread function for autofocusing NFOV 2
        /// </summary>
        private void readHeight2(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            while (this.read)
            {
                stopwatch.Start();
                if (!this.readThread2Paused && this.NFOV2Controller != null && this.NFOV2Controller.ConnectionStatus == ControllerConnectionStatus.Healthy)
                {
                    double input = this.GetReading(RCCMStage.RCCM2);
                    if (input != -1)
                    {
                        double pow = NFOVLensController.PwlInterp(this.NFOV2Calibration, input);
                        pow += this.FocusOffset2;
                        this.SetFocalPower(pow, RCCMStage.RCCM2);
                        this.Height2 = alpha * this.ToHeight2(input) + (1 - alpha) * this.Height2;
                    }
                }
                stopwatch.Stop();
                Thread.Sleep((int)Math.Max(0, NFOVLensController.UPDATE_PERIOD - stopwatch.ElapsedMilliseconds));
            }
            this.readHeight2ThreadExited.Set();
        }

        /// <summary>
        /// Get current input voltage to distanc sensor of specified NFOV
        /// </summary>
        /// <param name="stage">Enum value of desired lens</param>
        /// <returns>Current input voltaget</returns>
        public double GetReading(RCCMStage stage)
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

        /// <summary>
        /// Get current height of specified NFOV
        /// </summary>
        /// <param name="stage">Enum value of desired stage</param>
        /// <returns>Current focal height in user units</returns>
        public double GetHeight(RCCMStage stage)
        {
            // Get controller based on specified stage
            IController controller = stage == RCCMStage.RCCM1 ? this.NFOV1Controller : this.NFOV2Controller;

            // Send get input command and pull reading from resulting string
            try
            {
                double input = this.GetReading(stage);
                return stage == RCCMStage.RCCM1 ? this.ToHeight1(input) : this.ToHeight2(input);
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Get current focal power of specified NFOV lens
        /// </summary>
        /// <param name="stage">Enum value of desired lens</param>
        /// <returns>Current focal power output</returns>
        public double GetFocalPower(RCCMStage stage)
        {
            // Get controller based on specified stage
            IController controller = stage == RCCMStage.RCCM1 ? this.NFOV1Controller : this.NFOV2Controller;

            // Send get input command and pull reading from resulting string
            string response;
            try
            {
                response = controller.SendCommand(NFOVLensController.GET_STATUS_CMD);
            }
            catch
            {
                return -10000;
            }
            Match m = NFOVLensController.PARSE_GETOUTPUT.Match(response.Substring(0,1024));
            // Convert reading to distance
            return Double.Parse(m.Value.Substring(3));            
        }

        /// <summary>
        /// Applies a calibration to the lens. This scales the focal power based off the distance sensor reading according to a piecewise linear interpolant
        /// </summary>        
        /// <param name="data">2D array of calibration data. Column 1 contains input voltage, column 2 contains output voltage</param>
        /// <param name="stage">Parent stage of the lens to indicate which lens to calibrate</param>
        /// <returns>True if calibration was valid</returns>
        public bool ApplyCalibration(double[,] data, RCCMStage stage)
        {
            int rows = data.GetLength(0);
            int cols = data.GetLength(1);

            // Data must be formatted into rows of input voltage - output voltage pairs
            if (cols != 2)
            {
                return false;
            }

            // Get controller based on specified stage
            IController controller;
            if (stage == RCCMStage.RCCM1)
            {
                controller = this.NFOV1Controller;
                this.NFOV1Calibration = data;
            }
            else
            {
                controller = this.NFOV2Controller;
                this.NFOV2Calibration = data;
            }
            return true;
        }

        /// <summary>
        /// Sends command to specified controller to set a constant focal power value
        /// </summary>
        /// <param name="power">Focal power to use, in diopters</param>
        /// <param name="stage">Stage indicating which controller to send the command to</param>
        /// <returns>True if command succeded</returns>
        public bool SetFocalPower(double power, RCCMStage stage)
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

        /// <summary>
        /// Convert input voltage to distance for NFOV 1
        /// </summary>
        /// <param name="inputVoltage">Input voltage from distance sensor on NFOV 1</param>
        /// <returns>Height corresponding to input</returns>
        public double ToHeight1(double inputVoltage)
        {
            double h = this.conversion1[0] * inputVoltage + this.conversion1[1];
            h = Math.Min(h, NFOVLensController.MAX_HEIGHT);
            h = Math.Max(h, NFOVLensController.MIN_HEIGHT);
            return h;
        }

        /// <summary>
        /// Convert input voltage to distance for NFOV 2
        /// </summary>
        /// <param name="inputVoltage">Input voltage from distance sensor on NFOV 2</param>
        /// <returns>Height corresponding to input</returns>
        public double ToHeight2(double inputVoltage)
        {
            double h = this.conversion2[0] * inputVoltage + this.conversion2[1];
            h = Math.Min(h, NFOVLensController.MAX_HEIGHT);
            h = Math.Max(h, NFOVLensController.MIN_HEIGHT);
            return h;
        }

        /// <summary>
        /// Pause autofocus loop for specified stage
        /// </summary>
        /// <param name="stage">Enum value corresponding to desired stage</param>
        public void PauseFocusing(RCCMStage stage)
        {
            if (stage == RCCMStage.RCCM1)
            {
                this.readThread1Paused = true;
            }
            else
            {
                this.readThread2Paused = true;
            }
        }

        /// <summary>
        /// Unpause autofocus loop for specified stage
        /// </summary>
        /// <param name="stage">Enum value corresponding to desired stage</param>
        public void ResumeFocusing(RCCMStage stage)
        {
            if (stage == RCCMStage.RCCM1)
            {
                this.readThread1Paused = false;
            }
            else
            {
                this.readThread2Paused = false;
            }
        }

        /// <summary>
        /// Helper function for interpolating calibration
        /// </summary>
        /// <param name="data">Array of x, y pairs</param>
        /// <param name="val">X value to be interpolated</param>
        /// <returns>Interpolated Y value</returns>
        public static double PwlInterp(double[,] data, double val)
        {
            int i = 0;
            int n = data.GetLength(0);
            
            while (i < n && val > data[i, 0])
            {
                i++;
            }
            if (i == n)
            {
                return data[n - 1, 1];
            }
            if (i == 0)
            {
                return data[0, 1];
            }

            double m = (data[i, 1] - data[i - 1, 1])
                       / (data[i, 0] - data[i - 1, 0]);
            double x = val - data[i - 1, 0];
            double b = data[i - 1, 1];
            return m * x + b;
        }
        
        /// <summary>
        /// Save calibrations to settings
        /// </summary>
        public void Save()
        {
            Program.Settings.json["nfov 1"]["conversion"] = JArray.FromObject(this.conversion1);
            Program.Settings.json["nfov 2"]["conversion"] = JArray.FromObject(this.conversion2);
            Program.Settings.json["nfov 1"]["calibration"] = JArray.FromObject(this.NFOV1Calibration);
            Program.Settings.json["nfov 2"]["calibration"] = JArray.FromObject(this.NFOV2Calibration);
            Program.Settings.save();
        }

        /// <summary>
        /// Event handler for loss of connection to NFOV 1 lens. I don't think this works
        /// </summary>
        private void nfov1ConnectionChanged(object sender, ControllerConnectionStatusChangedEventArgs e)
        {
            Console.WriteLine("something happened");
            if (e.ConnectionStatus == ControllerConnectionStatus.Fault)
            {
                Console.WriteLine("disconnected");
                if (this.Motor1 != null)
                {
                    Console.WriteLine("pausing from lens");
                    this.Motor1.Pause();
                }
                this.readThread1Paused = true;
            }
            else if (e.ConnectionStatus == ControllerConnectionStatus.Healthy)
            {
                if (this.Motor1 != null)
                {
                    this.Motor1.Resume();
                }
                this.readThread1Paused = false;
            }
        }

        /// <summary>
        /// Event handler for loss of connection to NFOV 2 lens. I don't think this works
        /// </summary>
        private void nfov2ConnectionChanged(object sender, ControllerConnectionStatusChangedEventArgs e)
        {
            Console.WriteLine("something happened2");
            if (e.ConnectionStatus == ControllerConnectionStatus.Fault)
            {
                if (this.Motor2 != null)
                {
                    this.Motor2.Pause();
                }
                this.readThread2Paused = true;
            }
            else if (e.ConnectionStatus == ControllerConnectionStatus.Healthy)
            {
                if (this.Motor2 != null)
                {
                    this.Motor2.Resume();
                }
                this.readThread2Paused = false;
            }
        }

        /// <summary>
        /// Stops the focusing thread. Should be called when exiting program
        /// </summary>
        public void Stop()
        {
            this.read = false;
        }
    }
}
