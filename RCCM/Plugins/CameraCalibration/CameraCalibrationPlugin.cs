using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCCM;

namespace CameraCalibration
{
    public class CameraCalibrationPlugin : IRCCMPlugin
    {
        public string Name
        {
            get { return "Camera Calibration"; }
        }

        public string[] Params
        {
            get { return new string[] { "Camera" }; }
        }

        public CameraCalibrationPlugin() { }

        public IRCCMPluginActor Instance(RCCMSystem rccm, Dictionary<string, string> parameters)
        {
            return new CameraCalibration(rccm, parameters);
        }
    }
}
