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
    public class TrioStepperZMotor : Motor
    {
        public static Dictionary<string, string> TRIO_PROPERTY_MAP = new Dictionary<string, string>{
            { "enabled", "AXIS_ENABLE" },
            { "microstep per mm", "UNITS" },
            { "velocity", "SPEED" },
            { "jog speed", "JOGSPEED" },
            { "acceleration", "ACCEL" },
            { "deceleration", "DECEL" },
            { "low position limit", "AXIS_RS_LIMIT" },
            { "high position limit", "AXIS_FS_LIMIT" },
            { "home", "" },
            { "feedback", "" }
        };
        public static long UPDATE_PERIOD = (long)Program.Settings.json["distance sensor"]["z position update period"];
        public static double ERROR = (double)Program.Settings.json["distance sensor"]["max height error"];
        public static double PGAIN = 0.5;

        protected TrioController controller;
        protected short axisNum;
        protected Func<double> height;
        protected Func<double> minPosition;
        protected double commandHeight;
        protected BackgroundWorker bw;
        protected AutoResetEvent adjustThreadExited;
        protected bool adjust;
        protected bool adjustThreadPaused;

        public TrioStepperZMotor(TrioController controller, short axisNum, RCCMSystem rccm, RCCMStage stage)
        {
            this.controller = controller;
            this.axisNum = axisNum;
            this.Jogging = false;
            if (stage == RCCMStage.RCCM1)
            {
                this.height = delegate () { return rccm.LensController.Height1; };
            }
            else
            {
                this.height = delegate () { return rccm.LensController.Height2; };
            }
            this.minPosition = delegate ()
            {
                PointF pos = rccm.GetNFOVLocation(stage, CoordinateSystem.Local);
                return rccm.GetPanelDistance(pos.X, pos.Y);
            };
            this.commandHeight = this.height();
            this.bw = new BackgroundWorker();
            this.bw.DoWork += new DoWorkEventHandler(this.heightAdjustLoop);
            this.adjustThreadExited = new AutoResetEvent(false);
            this.adjust = true;
            this.adjustThreadPaused = false;
            this.bw.RunWorkerAsync();
        }

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

        public override double GetPos()
        {
            return this.height();
        }

        public override double SetPos(double cmdHeight)
        {
            if (this.GetProperty("enabled") == 0)
            {
                return this.GetPos();
            }
            double prevHeight = this.GetPos();
            double actuatorPos = this.controller.GetAxisProperty("MPOS", this.axisNum);
            double cmd = actuatorPos + cmdHeight - prevHeight;
            // Check that position is within range
            if (cmd >= this.settings["low position limit"] && cmd <= this.settings["high position limit"])
            {
                this.commandHeight = cmdHeight;
                this.commandPos = cmd;
            }
            return prevHeight;
        }

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

        public override bool Initialize()
        {
            if (this.GetProperty("enabled") == 0)
            {
                return false;
            }
            return true;
        }

        public override Dictionary<string, double> GetAllProperties()
        {
            Dictionary<string, double> properties = new Dictionary<string, double>(TrioController.AX_PROPERTIES.Length);
            foreach (string property in TrioController.AX_PROPERTIES)
            {
                properties[property] = this.controller.GetAxisProperty(property, this.axisNum);
            }
            return properties;
        }

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

        public override bool SetProperty(string property, double value)
        {
            if (property == "home")
            {
                this.settings["home"] = value;
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

        public override void WaitForEndOfMove()
        {
            this.controller.WaitForEndOfMove(this.axisNum);
        }

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

        public override double GetActuatorPos()
        {
            return this.controller.GetAxisProperty("MPOS", this.axisNum);
        }

        public void Pause()
        {
            Logger.Out("Pausing z motor adjustment axis " + this.axisNum);
            this.adjustThreadPaused = true;
        }

        public void Resume()
        {
            Logger.Out("Unpausing z motor adjustment axis " + this.axisNum);
            this.adjustThreadPaused = false;
        }

        public override void Terminate()
        {
            this.adjust = false;
            this.adjustThreadExited.WaitOne(10 * (int)TrioStepperZMotor.UPDATE_PERIOD);
        }
    }
}
