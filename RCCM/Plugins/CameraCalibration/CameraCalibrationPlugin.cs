using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCCM;

namespace CameraCalibration
{
    /// <summary>
    /// Defines parameters for automatic calibration plugin
    /// </summary>
    public class CameraCalibrationPlugin : IRCCMPlugin
    {
        /// <summary>
        /// Publicly visible plugin name
        /// </summary>
        public string Name
        {
            get { return "Camera Calibration"; }
        }

        /// <summary>
        /// Test has one parameter - name of camera being calibrated
        /// </summary>
        public string[] Params
        {
            get { return new string[] { "Camera" }; }
        }

        public CameraCalibrationPlugin() { }

        /// <summary>
        /// Create plugin with given test parameters
        /// </summary>
        /// <param name="rccm"></param>
        /// <param name="parameters">User entered test parameters</param>
        /// <returns></returns>
        public IRCCMPluginActor Instance(RCCMSystem rccm, Dictionary<string, string> parameters)
        {
            return new CameraCalibration(rccm, parameters);
        }
    }
}
