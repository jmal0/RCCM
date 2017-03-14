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
    public class VirtualZMotor : Motor
    {
        public static long UPDATE_PERIOD = 1000;

        protected Func<double> height;
        protected double commandHeight;
        protected double actuatorPos;
        protected BackgroundWorker bw;
        protected AutoResetEvent adjustThreadExited;
        protected bool adjust;
        protected bool adjustThreadPaused;

        public VirtualZMotor(Func<double> heightFunc)
        {
            this.height = heightFunc;
            this.commandHeight = this.height();
            this.actuatorPos = 0;
            this.Jogging = false;
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
                    Console.WriteLine(this.commandPos + " " + this.actuatorPos + " " + this.height());
                    double newPos = this.actuatorPos + this.commandHeight - this.height();
                    newPos = Math.Max(this.settings["low position limit"], newPos);
                    newPos = Math.Min(this.settings["high position limit"], newPos);
                    this.actuatorPos = newPos;
                    this.commandPos = newPos;
                }
                stopwatch.Stop();
                Thread.Sleep((int)Math.Max(0, VirtualZMotor.UPDATE_PERIOD - stopwatch.ElapsedMilliseconds));
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
            double cmd = this.actuatorPos + cmdHeight - prevHeight;
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
            // Check that position is within range
            double cmd = this.actuatorPos + (this.commandHeight + dist) - prevHeight;
            if (cmd >= this.settings["low position limit"] && cmd <= this.settings["high position limit"])
            {
                this.commandHeight = this.commandHeight + dist;
                this.commandPos = cmd;
            }
            return prevHeight;
        }
        
        public override bool Initialize()
        {
            this.homed = true;
            return true;
        }

        public override Dictionary<string, double> GetAllProperties()
        {
            return settings;
        }

        public override void WaitForEndOfMove()
        {
            return;
        }

        public override void Jog(bool fwd)
        {
            if (!this.Jogging)
            {
                Console.WriteLine("jogging");
                this.Jogging = true;
            }
        }

        public override void JogStop()
        {
            if (this.Jogging)
            {
                Console.WriteLine("jog stop");
                this.Jogging = false;
            }
        }
    }
}
