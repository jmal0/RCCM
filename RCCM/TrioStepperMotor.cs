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
            { "enabled", "AXIS_ENABLE" },
            { "microstep per mm", "UNITS" },
            { "velocity", "SPEED" },
            { "jog speed", "JOGSPEED" },
            { "acceleration", "ACCEL" },
            { "deceleration", "DECEL" },
            { "low position limit", "AXIS_RS_LIMIT" },
            { "high position limit", "AXIS_FS_LIMIT" },
            { "home", "" }
        };

        private TrioController controller;
        private short axisNum;

        public TrioStepperMotor(TrioController controller, short axisNum)
        {
            this.controller = controller;
            this.axisNum = axisNum;
            this.Jogging = false;
        }

        public override void Jog(bool fwd)
        {
            if (this.GetProperty("enabled") != 0 && !this.Jogging)
            {
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
            }
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
        /// Set the command position of the motor. After calling this method, getPos() will return cmd
        /// </summary>
        /// <param name="cmd">New command position</param>
        /// <returns>The new commanded position</returns>
        override public double SetPos(double cmd)
        {
            if (this.GetProperty("enabled") == 0)
            {
                return this.commandPos;
            }
            // Coerce position to be within travel range
            cmd = Math.Max(this.settings["low position limit"], cmd);
            cmd = Math.Min(this.settings["high position limit"], cmd);
            this.commandPos = cmd;
            this.controller.MoveAbs(this.axisNum, cmd);
            return cmd;
        }

        /// <summary>
        /// Set the command position of the motor. After calling this method, getPos() will return cmd
        /// </summary>
        /// <param name="dist">New command position</param>
        /// <returns>The previous commanded position</returns>
        override public double MoveRel(double dist)
        {
            if (this.GetProperty("enabled") == 0)
            {
                return this.commandPos;
            }
            double prev = this.commandPos;
            double cmd = this.GetPos() + dist;
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
        public override double GetPos()
        {
            return this.controller.GetAxisProperty("MPOS", this.axisNum);
        }

        public override void Zero()
        {
            this.controller.Zero(this.axisNum);
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
                return 0;
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
            if (property == "feedback")
            {
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
    }
}
