using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrioPCLib;

namespace RCCM
{
    public class TrioStepperMotor : Motor
    {
        public static Dictionary<string, string> TRIO_PROPERTY_MAP = new Dictionary<string, string>{
            { "microstep per mm", "UNITS" },
            { "velocity", "SPEED" },
            { "acceleration", "ACCEL" },
            { "deceleration", "DECEL" },
            { "low position limit", "AXIS_RS_LIMIT" },
            { "high position limit", "AXIS_FS_LIMIT" }
        };

        private TrioController controller;
        private short axisNum;

        private bool Jogging = false;

        public TrioStepperMotor(TrioController controller, short axisNum)
        {
            this.controller = controller;
            this.axisNum = axisNum;
        }

        public bool JogStart(bool fwd)
        {
            if (!this.Jogging)
            {
                return this.controller.Jog(fwd, this.axisNum);
            }
            return true;
        }

        public bool JogStop()
        {
            if (this.Jogging)
            {
                return this.controller.JogStop(this.axisNum);
            }
            return true;
        }

        /// <summary>
        /// Initialize status variables of superclass Motor
        /// </summary>
        /// <returns>Initialization status of motor</returns>
        public override bool initialize()
        {
            return true;
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
                double pos = cmd;
                this.commandPos = cmd;
                this.controller.MoveAbs(this.axisNum, pos);
            }
            return prev;
        }

        /// <summary>
        /// Set the command position of the motor. After calling this method, getPos() will return cmd
        /// </summary>
        /// <param name="dist">New command position</param>
        /// <returns>The previous commanded position</returns>
        override public double moveRel(double dist)
        {
            double prev = this.commandPos;
            double cmd = this.getPos() + dist;
            // Check that position is within range
            if (cmd >= this.settings["low position limit"] && cmd <= this.settings["high position limit"])
            {
                double pos = cmd;
                this.commandPos = cmd;
                this.controller.MoveRel(this.axisNum, dist);
            }
            return prev;
        }

        /// <summary>
        /// Get motor position. This is accessed using the MPOS axis property
        /// </summary>
        /// <returns>Current position</returns>
        public override double getPos()
        {
            return this.controller.GetAxisProperty("MPOS", this.axisNum);
        }

        public override Dictionary<string, double> getAllProperties()
        {
            Console.WriteLine(this.axisNum);
            Dictionary<string, double> properties = new Dictionary<string, double>(TrioController.AX_PROPERTIES.Length);
            foreach (string property in TrioController.AX_PROPERTIES)
            {
                properties[property] = this.controller.GetAxisProperty(property, this.axisNum);
            }
            return properties;
        }

        public override double getProperty(string property)
        {
            string variable = TrioStepperMotor.TRIO_PROPERTY_MAP[property];
            return this.controller.GetAxisProperty(variable, this.axisNum);
        }

        public override bool setProperty(string property, double value)
        {
            string variable = TrioStepperMotor.TRIO_PROPERTY_MAP[property];
            Console.WriteLine(variable + " " + value);
            this.settings[property] = value;
            return this.controller.SetAxisProperty(variable, value, this.axisNum);
        }
    }
}
