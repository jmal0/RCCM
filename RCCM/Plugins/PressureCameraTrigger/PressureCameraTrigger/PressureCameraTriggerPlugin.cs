using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using RCCM;

namespace PressureCameraTrigger
{
    public class PressureCameraTriggerPlugin : IRCCMPlugin
    {
        /// <summary>
        /// Time period of a single loop iteration
        /// </summary>
        public static double PERIOD = 50;        
        /// <summary>
        /// Publicly visible plugin name
        /// </summary>
        public string Name
        {
            get { return "Pressure Camera Trigger"; }
        }
        /// <summary>
        /// User entered test inputs
        /// </summary>
        public string[] Params
        {
            get { return new string[] { "Camera", "Path", "Pressure", "Ascending" }; }
        }

        public PressureCameraTriggerPlugin() { }

        /// <summary>
        /// Create plugin with given test parameters
        /// </summary>
        /// <param name="rccm"></param>
        /// <param name="parameters">User entered test parameters</param>
        /// <returns></returns>
        public IRCCMPluginActor Instance(RCCMSystem rccm, Dictionary<string, string> parameters)
        {
             return new PressureCameraTrigger(rccm, parameters);
        }
    }
}
