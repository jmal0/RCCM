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
        /// <summary>
        /// List of motor setting names that all motor implementations must include
        /// </summary>
        public static string[] MOTOR_SETTINGS = { "enabled", "microstep per mm", "velocity", "jog speed", "acceleration", "deceleration", "low position limit", "high position limit", "home", "feedback" };
        /// <summary>
        /// Current goal position of actuator
        /// </summary>
        protected double commandPos = 0;
        /// <summary>
        /// Maintained list of motion settings and limits
        /// </summary>
        protected Dictionary<string, double> settings;
        /// <summary>
        /// A flag to indicate if the motor is being jogged
        /// </summary>
        public bool Jogging { get; protected set; }

        /// <summary>
        /// Initialize variables associated with base motor class
        /// </summary>
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
        
        /// <summary>
        /// Get current position of actuator
        /// </summary>
        /// <returns>Current position of actuator</returns>
        abstract public double GetPos();
        
        /// <summary>
        /// Set new command position of actuator
        /// </summary>
        /// <param name="cmd">New goal position to send to actuator</param>
        /// <returns>Goal position that was set, coerced if input was out of travel range</returns>
        abstract public double SetPos(double cmd);

        /// <summary>
        /// Move actuator specified distance from current position
        /// </summary>
        /// <param name="dist">Distance to move</param>
        /// <returns>Previous commanded position</returns>
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

        /// <summary>
        /// Define current actuator position as a new numeric position
        /// </summary>
        /// <param name="value">Corrected current actuator position</param>
        public virtual void FixPosition(double value)
        {
            this.commandPos = value;
        }
       
        /// <summary>
        /// Move to user defined home position
        /// </summary>
        public void GotoHome()
        {
            this.SetPos(this.settings["home"]);
        }

        /// <summary>
        /// Perform any required initialization
        /// </summary>
        /// <returns>True if initialization was successful</returns>
        abstract public bool Initialize();

        /// <summary>
        /// Get all motor settings in a property value pair dictionary
        /// </summary>
        /// <returns>Dictionary of properties and values</returns>
        abstract public Dictionary<string, double> GetAllProperties();

        /// <summary>
        /// Blocking function that runs until current actuator move completes
        /// </summary>
        abstract public void WaitForEndOfMove();

        /// <summary>
        /// Begin continuously moving actuator at jog speed
        /// </summary>
        /// <param name="fwd">Direction of jog - forward if true</param>
        abstract public void Jog(bool fwd);

        /// <summary>
        /// Stop jogging
        /// </summary>
        abstract public void JogStop();

        /// <summary>
        /// Must return actual position of actuator from end of travel. Important for Z actuators
        /// </summary>
        /// <returns>Position of actuator from end of travel</returns>
        public virtual double GetActuatorPos()
        {
            return this.GetPos();
        }

        /// <summary>
        /// Define current actuator position as zero
        /// </summary>
        public virtual void Zero()
        {
            this.commandPos = 0;
        }

        /// <summary>
        /// Perform any required action to disconnect from motor
        /// </summary>
        public virtual void Terminate() { }
    }
}
