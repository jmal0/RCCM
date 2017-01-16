using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    class VirtualMotor : Motor
    {
        protected double pos;

        override public double setPos(double cmd)
        {
            double prev = this.commandPos;
            this.commandPos = cmd;
            return prev;
        }

        
        override public double getPos()
        {
            return this.pos;
        }

        private double update(double dt)
        {
            double v = this.settings["velocity"];
            if (this.pos < this.commandPos)
            {
                this.pos = Math.Min(this.pos + v * dt, this.commandPos);
                return this.pos;
            }
            if (this.pos > this.commandPos)
            {
                this.pos = Math.Max(this.pos - v * dt, this.commandPos);
                return this.pos;
            }
            return this.commandPos;
        }
    }
}
