using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    abstract public class Motor
    {
        protected double commandPos = 0;
        protected bool homed = false;
        protected string status = "NOT READY";
        protected Dictionary<string, double> settings;

        public Motor()
        {
            this.settings = new Dictionary<string, double>();
            this.settings["velocity"] = 0;
            this.settings["acceleration"] = 0;
            this.settings["pos_limit_upper"] = 0;
            this.settings["pos_limit_lower"] = 0;
        }

        abstract public double getPos();
        abstract public double setPos(double cmd);

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

        public double getProperty(string property)
        {
            return this.settings[property];
        }

        public string initialize()
        {
            this.homed = true;
            string previous = this.status;
            this.status = "OK";
            return previous;
        }
    }
}
