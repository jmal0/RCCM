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
        // Step position 
        protected double commandPos = 0;
        protected bool homed = false;
        // Maintained list of motion settings and limits
        protected Dictionary<string, double> settings;

        public Motor()
        {
            this.settings = new Dictionary<string, double>();

            // Create required keys for settings
            this.settings["velocity"] = 0;
            this.settings["acceleration"] = 0;
            this.settings["low position limit"] = 0;
            this.settings["high position limit"] = 0;
        }

        abstract public double getPos();
        abstract public double setPos(double cmd);
        abstract public double moveRel(double dist);

        /// <summary>
        /// Set a motor property
        /// </summary>
        /// <param name="property">Property name</param>
        /// <param name="value">Property value</param>
        /// <returns>Previous value of property</returns>
        public double setProperty(string property, double value)
        {
            if (this.settings.ContainsKey(property))
            {
                double previous = this.settings[property];
                this.settings[property] = value;
                return previous;
            }
            throw new ArgumentException("Invalid property name");
        }

        /// <summary>
        /// Gets a motor property value
        /// </summary>
        /// <param name="property">Name of the property</param>
        /// <returns>The value of the specified property</returns>
        public double getProperty(string property)
        {
            return this.settings[property];
        }

        abstract public bool initialize();

        abstract public Dictionary<string, double> getAllProperties();
    }
}
