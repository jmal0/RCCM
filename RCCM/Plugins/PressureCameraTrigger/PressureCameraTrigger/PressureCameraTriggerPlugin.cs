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
        public static double PERIOD = 50; // Time period of a single loop iteration
        
        public string Name
        {
            get { return "Pressure Camera Trigger"; }
        }
        public string[] Params
        {
            get { return new string[] { "Camera", "Path", "Pressure", "Ascending" }; }
        }

        public PressureCameraTriggerPlugin() { }

        public IRCCMPluginActor Instance(RCCMSystem rccm, Dictionary<string, string> parameters)
        {
             return new PressureCameraTrigger(rccm, parameters);
        }
    }
}
