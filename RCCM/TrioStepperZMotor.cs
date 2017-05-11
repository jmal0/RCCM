using Gardasoft.Controller.API.EventsArgs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RCCM
{
    /// <summary>
    /// Actuator controlled through trio controller that adjusts its position based on distance sensor input
    /// </summary>
    public class TrioStepperZMotor : Motor
    {
        /// <summary>
        /// Dictionary mapping settings property names to their corresponding Trio axis property
        /// </summary>
        public static Dictionary<string, string> TRIO_PROPERTY_MAP = new Dictionary<string, string>{
            { "enabled", "AXIS_ENABLE" },
            { "microstep per unit", "UNITS" },
            { "velocity", "SPEED" },
            { "jog speed", "JOGSPEED" },
            { "acceleration", "ACCEL" },
            { "deceleration", "DECEL" },
            { "low position limit", "AXIS_RS_LIMIT" },
            { "high position limit", "AXIS_FS_LIMIT" },
            { "home", "" },
            { "feedback", "" }
        };
        /// <summary>
        /// Time in milliseconds between commands sent to controller
        /// </summary>
        public static long UPDATE_PERIOD = (long)Program.Settings.json["distance sensor"]["z position update period"];
        /// <summary>
        /// Maximum allowable error between command height and measured height before axis is adjusted
        /// </summary>
        public static double ERROR = (double)Program.Settings.json["distance sensor"]["max height error"];
        /// <summary>
        /// Gain multiplying position error to determine much correction actuator should do
        /// </summary>
        public static double PGAIN = 0.5;
        /// <summary>
        /// RCCM Trio controller object
        /// </summary>
        protected TrioController controller;
        /// <summary>
        /// Number of port where this axis is connected to Trio controller
        /// </summary>
        protected short axisNum;
        /// <summary>
        /// Function reference for getting height of actuator above panel
        /// </summary>
        protected Func<double> height;
        /// <summary>
        /// Function reference for computing lowest position that axis should move to from current position on panel
        /// </summary>
        protected Func<double> minPosition;
        /// <summary>
        /// Current desired height above panel
        /// </summary>
        protected double commandHeight;
        /// <summary>
        /// Background worker for sending movement commands
        /// </summary>
        protected BackgroundWorker bw;
        /// <summary>
        /// Event for when background thread exits
        /// </summary>
        protected AutoResetEvent adjustThreadExited;
        /// <summary>
        /// Flag indicating if background thread should continue running
        /// </summary>
        protected bool adjust;
        /// <summary>
        /// Flag indicating if actuator should be actively trying to match height to command height 
        /// </summary>
        protected bool adjustThreadPaused;

        /// <summary>
        /// Create a controlled z actuator
        /// </summary>
        /// <param name="controller">RCCM Trio controller object</param>
        /// <param name="axisNum">Number of port where axis is connected to Trio controller</param>
        /// <param name="rccm">The RCCM object</param>
        /// <param name="stage">Enum value of the set of fine actuators containing this actuator</param>
        public TrioStepperZMotor(TrioController controller, short axisNum, RCCMSystem rccm, RCCMStage stage)
        {
            this.controller = controller;
            this.axisNum = axisNum;
            this.Jogging = false;
            // Create height function reference from lens controller height property
            if (stage == RCCMStage.RCCM1)
            {
                this.height = delegate () { return rccm.LensController.Height1; };
            }
            else
            {
                this.height = delegate () { return rccm.LensController.Height2; };
            }
            // Create function reference that computes height of panel at current location
            this.minPosition = delegate ()
            {
                PointF pos = rccm.GetNFOVLocation(stage, CoordinateSystem.Local);
                return rccm.GetPanelDistance(pos.X, pos.Y);
            };
            this.commandHeight = this.height();
            // Start background thread
            this.bw = new BackgroundWorker();
            this.bw.DoWork += new DoWorkEventHandler(this.heightAdjustLoop);
            this.adjustThreadExited = new AutoResetEvent(false);
            this.adjust = true;
            this.adjustThreadPaused = false;
            this.bw.RunWorkerAsync();
        }

        /// <summary>
        /// Background thread function that reads height and sends position commands
        /// </summary>
        private void heightAdjustLoop(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            while (this.adjust)
            {
                stopwatch.Start();
                if (!this.adjustThreadPaused)
                {
                    double h = this.height();
                    if (h == NFOVLensController.MIN_HEIGHT || h == NFOVLensController.MAX_HEIGHT)
                    {
                        continue;
                    }
                    double actuatorPos = this.controller.GetAxisProperty("MPOS", this.axisNum);
                    double err = this.commandHeight - h;
                    // When feedback is 0, operate in closed loop mode
                    if (!this.controller.isMoving(this.axisNum) && this.GetProperty("feedback") == 0)
                    {
                        double newPos = this.commandHeight;
                        double minPos = this.minPosition();
                        newPos = Math.Max(this.settings["low position limit"], newPos);
                        newPos = Math.Min(this.settings["high position limit"], newPos);
                        newPos = Math.Max(minPos, newPos);
                        this.controller.MoveAbs(this.axisNum, newPos);
                    }
                    // Else, adjust based on distance sensor reading
                    else if (!this.controller.isMoving(this.axisNum) && Math.Abs(err) > TrioStepperZMotor.ERROR)
                    {
                        double newPos = actuatorPos + TrioStepperZMotor.PGAIN * err;
                        double minPos = this.minPosition();
                        newPos = Math.Max(this.settings["low position limit"], newPos);
                        newPos = Math.Min(this.settings["high position limit"], newPos);
                        newPos = Math.Max(minPos, newPos);
                        this.controller.MoveAbs(this.axisNum, newPos);
                    }
                }
                stopwatch.Stop();
                Thread.Sleep((int)Math.Max(0, TrioStepperZMotor.UPDATE_PERIOD - stopwatch.ElapsedMilliseconds));
            }
            this.adjustThreadExited.Set();
        }

        /// <summary>
        /// Get actuator height above panel
        /// </summary>
        /// <returns>Current height of actuator above panel</returns>
        public override double GetPos()
        {
            return this.height();
        }

        /// <summary>
        /// Set current position of actuator (independent of feedback property value)
        /// </summary>
        /// <param name="cmdHeight">New command position of actuator</param>
        /// <returns>Coerced command position</returns>
        public override double SetPos(double cmdHeight)
        {
            if (this.GetProperty("enabled") == 0)
            {
                return this.GetPos();
            }
            double prevHeight = this.GetPos();
            double actuatorPos = this.controller.GetAxisProperty("MPOS", this.axisNum);
            double cmd = actuatorPos + cmdHeight - prevHeight;
            // Coerce position to be within travel range
            cmd = Math.Max(this.settings["low position limit"], cmd);
            cmd = Math.Min(this.settings["high position limit"], cmd);
            this.commandPos = cmd;
            return cmd;
        }

        /// <summary>
        /// Move command position a specified distance from current
        /// </summary>
        /// <param name="dist">Distance to move</param>
        /// <returns>Last command position</returns>
        public override double MoveRel(double dist)
        {
            if (this.GetProperty("enabled") == 0)
            {
                return this.GetPos();
            }
            double prevHeight = this.GetPos();
            double actuatorPos = this.controller.GetAxisProperty("MPOS", this.axisNum);
            // Check that position is within range
            double cmd = actuatorPos + (this.commandHeight + dist) - prevHeight;
            if (cmd >= this.settings["low position limit"] && cmd <= this.settings["high position limit"])
            {
                this.commandHeight = this.commandHeight + dist;
                this.commandPos = cmd;
            }
            return prevHeight;
        }

        /// <summary>
        /// Does nothing
        /// </summary>
        /// <returns>True if actuator is enabled</returns>
        public override bool Initialize()
        {
            if (this.GetProperty("enabled") == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get all property values in a dictionary
        /// </summary>
        /// <returns>Dictionary of property name, value pairs</returns>
        public override Dictionary<string, double> GetAllProperties()
        {
            Dictionary<string, double> properties = new Dictionary<string, double>(TrioController.AX_PROPERTIES.Length);
            foreach (string property in TrioController.AX_PROPERTIES)
            {
                properties[property] = this.controller.GetAxisProperty(property, this.axisNum);
            }
            return properties;
        }

        /// <summary>
        /// Get a specified property value
        /// </summary>
        /// <param name="property">Name of property</param>
        /// <returns>Current property value</returns>
        public override double GetProperty(string property)
        {
            if (property == "home")
            {
                return this.settings["home"];
            }
            if (property == "feedback")
            {
                return this.settings["feedback"];
            }
            string variable = TrioStepperZMotor.TRIO_PROPERTY_MAP[property];
            return this.controller.GetAxisProperty(variable, this.axisNum);
        }

        /// <summary>
        /// Set a property value
        /// </summary>
        /// <param name="property">Property name</param>
        /// <param name="value">New property value</param>
        /// <returns>True if property was set succesfully</returns>
        public override bool SetProperty(string property, double value)
        {
            if (property == "home")
            {
                this.settings["home"] = value;
                return true;
            }
            if (property == "feedback")
            {
                this.settings["feedback"] = value;
                return true;
            }
            string variable = TrioStepperZMotor.TRIO_PROPERTY_MAP[property];
            Logger.Out(variable + " " + value);
            this.settings[property] = value;
            return this.controller.SetAxisProperty(variable, value, this.axisNum);
        }

        /// <summary>
        /// Blocking function that completes when actuator motion ends
        /// </summary>
        public override void WaitForEndOfMove()
        {
            this.controller.WaitForEndOfMove(this.axisNum);
        }
        
        /// <summary>
        /// Pauses active adjustment of actuator and begins moving axis continuously
        /// </summary>
        /// <param name="fwd">Flag indicating direction of move</param>
        public override void Jog(bool fwd)
        {
            if (this.GetProperty("enabled") != 0 && !this.Jogging)
            {
                this.adjustThreadPaused = true;
                this.controller.Jog(fwd, this.axisNum);
                Logger.Out("jogging axis " + this.axisNum);
                this.Jogging = true;
            }
        }

        /// <summary>
        /// Stop jogging actuator and resume adjustment thread
        /// </summary>
        public override void JogStop()
        {
            if (this.GetProperty("enabled") != 0 && this.Jogging)
            {
                this.controller.JogStop(this.axisNum);
                Logger.Out("stopping jog axis " + this.axisNum);
                this.Jogging = false;
                this.adjustThreadPaused = false;
            }
        }

        /// <summary>
        /// Set current position of actuator as 0 and clear any errors
        /// </summary>
        public override void Zero()
        {
            this.controller.Zero(this.axisNum);
        }

        /// <summary>
        /// Define current actuator position as a new numeric position
        /// </summary>
        /// <param name="value">Corrected current actuator position</param>
        public override void FixPosition(double value)
        {
            this.controller.FixPosition(value, this.axisNum);
        }

        /// <summary>
        /// Get position of actuator from end of travel
        /// </summary>
        /// <returns>Current actuator position</returns>
        public override double GetActuatorPos()
        {
            return this.controller.GetAxisProperty("MPOS", this.axisNum);
        }

        /// <summary>
        /// Pause active height adjustment
        /// </summary>
        public void Pause()
        {
            Logger.Out("Pausing z motor adjustment axis " + this.axisNum);
            this.adjustThreadPaused = true;
        }

        /// <summary>
        /// Unpause active height adjustment
        /// </summary>
        public void Resume()
        {
            Logger.Out("Unpausing z motor adjustment axis " + this.axisNum);
            this.adjustThreadPaused = false;
        }

        /// <summary>
        /// Stop background adjustment thread
        /// </summary>
        public override void Terminate()
        {
            this.adjust = false;
            this.adjustThreadExited.WaitOne(10 * (int)TrioStepperZMotor.UPDATE_PERIOD);
        }
    }
}
