using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    class VirtualMotor : Motor
    {
        /// <summary>
        /// Get motor position. For a virtual motor, this is simply the last commanded position
        /// </summary>
        /// <returns>Current position</returns>
        public override double getPos()
        {
            return this.commandPos;
        }

        /// <summary>
        /// Set the command position of the motor. After calling this method, getPos() will return cmd
        /// </summary>
        /// <param name="cmd">New command position</param>
        /// <returns>The previous commanded position</returns>
        override public double setPos(double cmd)
        {
            double prev = this.commandPos;
            // Check that position is within range
            if (cmd >= this.settings["low position limit"] && cmd <= this.settings["high position limit"])
            {
                this.commandPos = cmd;
            }
            return prev;
        }

        /// <summary>
        /// Move the motor a specified distance
        /// </summary>
        /// <param name="dist">Distance to move</param>
        /// <returns>The previous commanded position</returns>
        override public double moveRel(double dist)
        {
            double prev = this.commandPos;
            // Check that position is within range
            double cmd = this.commandPos + dist;
            if (cmd >= this.settings["low position limit"] && cmd <= this.settings["high position limit"])
            {
                this.commandPos = cmd;
            }
            return prev;
        }

        /// <summary>
        /// Initialize status variables of superclass Motor
        /// </summary>
        /// <returns>Initialization status of motor</returns>
        public override bool initialize()
        {
            this.homed = true;
            return this.homed;
        }

        public override Dictionary<string, double> getAllProperties()
        {
            return settings;
        }

        // TODO: utilize maybe?
        /*
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
        */
    }
}
