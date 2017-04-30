using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    /// <summary>
    /// Virtual representation of motor for use when motor is not connected
    /// </summary>
    class VirtualMotor : Motor
    {
        /// <summary>
        /// Get motor position. For a virtual motor, this is simply the last commanded position
        /// </summary>
        /// <returns>Current position</returns>
        public override double GetPos()
        {
            return this.commandPos;
        }

        /// <summary>
        /// Set the command position of the motor. After calling this method, getPos() will return cmd
        /// </summary>
        /// <param name="cmd">New command position</param>
        /// <returns>The new commanded position</returns>
        public override double SetPos(double cmd)
        {
            if (this.GetProperty("enabled") == 0)
            {
                return this.commandPos;
            }
            // Coerce position to be within travel range
            cmd = Math.Max(this.settings["low position limit"], cmd);
            cmd = Math.Min(this.settings["high position limit"], cmd);
            this.commandPos = cmd;
            return cmd;
        }

        /// <summary>
        /// Move the motor a specified distance
        /// </summary>
        /// <param name="dist">Distance to move</param>
        /// <returns>The previous commanded position</returns>
        public override double MoveRel(double dist)
        {
            if (this.GetProperty("enabled") == 0)
            {
                return this.commandPos;
            }
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
        public override bool Initialize()
        {
            if (this.GetProperty("enabled") == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get all property value pairs for this motor in a dictionary
        /// </summary>
        /// <returns>Dictionary of property value pairs</returns>
        public override Dictionary<string, double> GetAllProperties()
        {
            return settings;
        }

        /// <summary>
        /// Since move is applied instantaneously, no waiting necessary
        /// </summary>
        public override void WaitForEndOfMove()
        {
            return;
        }

        /// <summary>
        /// Pretend to jog this motor lol
        /// </summary>
        /// <param name="fwd">Direction of jog - forward if true</param>
        public override void Jog(bool fwd)
        {
            if (!this.Jogging)
            {
                Logger.Out("jogging");
                this.Jogging = true;
            }
        }

        /// <summary>
        /// Pretend to stop jogging this motor lol
        /// </summary>
        public override void JogStop()
        {
            if (this.Jogging)
            {
                Logger.Out("jog stop");
                this.Jogging = false;
            }            
        }
    }
}
