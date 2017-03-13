using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    /// <summary>
    /// Abstract representation of stepper motor. Defines the minimum functions and variables needed to define the motor
    /// </summary>
    abstract public class Motor
    {
        public static string[] MOTOR_SETTINGS = { "enabled", "microstep per mm", "velocity", "jog speed", "acceleration", "deceleration", "low position limit", "high position limit", "home" };
        protected double commandPos = 0;
        protected bool homed = false;
        // Maintained list of motion settings and limits
        protected Dictionary<string, double> settings;
        public bool Jogging { get; protected set; }

        public Motor()
        {
            this.settings = new Dictionary<string, double>();
            this.Jogging = false;

            // Create required keys for settings
            foreach (string setting in Motor.MOTOR_SETTINGS)
            {
                this.settings[setting] = 0;
            }
        }

        abstract public double GetPos();
        abstract public double SetPos(double cmd);
        abstract public double MoveRel(double dist);

        /// <summary>
        /// Set a motor property
        /// </summary>
        /// <param name="property">Property name</param>
        /// <param name="value">Property value</param>
        /// <returns>True if property set is successful</returns>
        public virtual bool SetProperty(string property, double value)
        {
            if (this.settings.ContainsKey(property))
            {
                this.settings[property] = value;
                return true;
            }
            throw new ArgumentException("Invalid property name");
        }

        /// <summary>
        /// Gets a motor property value
        /// </summary>
        /// <param name="property">Name of the property</param>
        /// <returns>The value of the specified property</returns>
        public virtual double GetProperty(string property)
        {
            return this.settings[property];
        }
        
        public void GotoHome()
        {
            this.SetPos(this.settings["home"]);
        }

        abstract public bool Initialize();

        abstract public Dictionary<string, double> GetAllProperties();

        abstract public void WaitForEndOfMove();

        abstract public void Jog(bool fwd);

        abstract public void JogStop();
    }
}
