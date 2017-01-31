using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODBUSRTULib;

namespace RCCM
{
    class StepperMotor : Motor
    {
        ISerialCtrl driver;
        
        public StepperMotor()
        {
            this.driver = new SerialCtrl();
            this.driver.OpenPort();
            this.driver.DefineMoveProfile(0,0,0,0,0,0,0,0,0,0);
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
        /// Get motor position. For a virtual motor, this is simply the last commanded position
        /// </summary>
        /// <returns>Current position</returns>
        public override double getPos()
        {
            return this.commandPos;
        }

        /// <summary>
        /// Initialize status variables of superclass Motor
        /// </summary>
        /// <returns>Previous status of motor</returns>
        public override string initialize()
        {
            this.homed = true;
            string previous = this.status;
            this.status = "OK";
            return previous;
        }
    }
}
