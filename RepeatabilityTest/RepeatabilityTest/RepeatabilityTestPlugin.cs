using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCCM;

namespace RepeatabilityTest
{
    public class RepeatabilityTestPlugin : IRCCMPlugin
    {
        public string Name
        {
            get { return "Repeatability Test"; }
        }
        public string[] Params
        {
            get { return new string[] { "Camera", "Actuator", "Repititions", "Distance" }; }
        }

        public RepeatabilityTestPlugin() { }

        public IRCCMPluginActor Instance(RCCMSystem rccm, Dictionary<string, string> parameters)
        {
            return new RepeatabilityTest(rccm, parameters);
        }
    }
}
