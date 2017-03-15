using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RCCM
{
    public class TrioStepperZMotor : Motor
    {
        public static long UPDATE_PERIOD = (long)Program.Settings.json["z position update period"];
        public static double alpha = (double)Program.Settings.json["height reading filter constant"];

        protected TrioController controller;
        protected short axisNum;
        protected Func<double> height;
        protected double commandHeight;
        protected double fiteredHeight;
        protected BackgroundWorker bw;
        protected AutoResetEvent adjustThreadExited;
        protected bool adjust;
        protected bool adjustThreadPaused;

        public TrioStepperZMotor(TrioController controller, short axisNum, Func<double> heightFunc)
        {
            this.controller = controller;
            this.axisNum = axisNum;
            this.Jogging = false;
            this.height = heightFunc;
            this.commandHeight = this.height();
            this.fiteredHeight = this.commandHeight;
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
                    double newHeight = this.height();
                    this.fiteredHeight = TrioStepperZMotor.alpha * newHeight + 
                                         (1 - TrioStepperZMotor.alpha) * this.fiteredHeight;
                    double actuatorPos = this.controller.GetAxisProperty("MPOS", this.axisNum);
                    Console.WriteLine(actuatorPos + " " + this.commandHeight + " " + this.fiteredHeight);
                    double newPos = actuatorPos + this.commandHeight - this.fiteredHeight;
                    newPos = Math.Max(this.settings["low position limit"], newPos);
                    newPos = Math.Min(this.settings["high position limit"], newPos);
                    if (!this.controller.isMoving(this.axisNum) && Math.Abs(newPos - actuatorPos) > 0.5)
                    {
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
            Console.WriteLine(this.axisNum);
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
            string variable = TrioStepperMotor.TRIO_PROPERTY_MAP[property];
            return this.controller.GetAxisProperty(variable, this.axisNum);
        }

        public override bool SetProperty(string property, double value)
        {
            if (property == "home")
            {
                this.settings["home"] = value;
                return true;
            }
            string variable = TrioStepperMotor.TRIO_PROPERTY_MAP[property];
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
                Console.WriteLine("jogging");
                this.Jogging = true;
            }
        }

        public override void JogStop()
        {
            if (this.GetProperty("enabled") != 0 && this.Jogging)
            {
                this.controller.JogStop(this.axisNum);
                Console.WriteLine("jog stop stepper");
                this.Jogging = false;
                this.adjustThreadPaused = false;
            }
        }

        public override double GetActuatorPos()
        {
            return this.controller.GetAxisProperty("MPOS", this.axisNum);
        }

        public override void Terminate()
        {
            this.adjust = false;
        }
    }
}
