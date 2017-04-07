using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCCM;
using System.Diagnostics;
using System.Threading;

namespace PressureCameraTrigger
{
    public class PressureCameraTrigger : IRCCMPluginActor
    {
        protected readonly ICamera camera;
        protected string path;
        protected double triggerPressure;
        protected bool ascending;
        public bool Running { get; protected set; }
        protected readonly RCCMSystem rccm;

        public PressureCameraTrigger(RCCMSystem rccm, Dictionary<string, string> parameters)
        {
            this.rccm = rccm;
            this.Running = false;

            // Create instance values from provided map from parameter name to user inputted value
            this.path = parameters["Path"];
            // Camera string must be one of 4 options
            switch (parameters["Camera"].ToLower())
            {
                case "wfov 1":
                    this.camera = rccm.WFOV1;
                    break;
                case "wfov 2":
                    this.camera = rccm.WFOV2;
                    break;
                case "nfov 1":
                    this.camera = rccm.NFOV1;
                    break;
                case "nfov 2":
                    this.camera = rccm.NFOV2;
                    break;
                default:
                    throw new ArgumentException("Camera must be nfov/wfov 1/2");
            }
            // Extract numeric value from user input for pressure that triggers image capture
            this.triggerPressure = Double.Parse(parameters["Pressure"]);
            // Extract true or false from user input
            this.ascending = Boolean.Parse(parameters["Ascending"]);
        }

        public void Run()
        {
            // Set running to true so main GUI knows that plugin has not finished
            this.Running = true;

            // Initialize 
            Stopwatch stopwatch = new Stopwatch();
            double lastPressure = rccm.Counter.GetPressure();
            double pressure;

            // Continuously check pressure until user says to stop
            while (this.Running)
            {
                stopwatch.Start(); // Track time elapsed during loop iteration 

                pressure = rccm.Counter.GetPressure();
                // Snap image if trigger pressure passed
                if (ascending)
                {
                    // To determine if we are on the ascending portion of the cycle,
                    // check that most recent pressure reading was greater than 
                    // previous reading
                    if (this.triggerPressure > pressure && pressure > lastPressure)
                    {
                        // Save to user specified path as cycle-XXXXX.bmp
                        camera.Snap(this.path + "\\cycle-" + rccm.Counter.Cycle + ".bmp");
                    }
                }
                else
                {
                    // To determine if we are on the ascending portion of the cycle,
                    // check that most recent pressure reading was greater than 
                    // previous reading
                    if (triggerPressure < pressure && pressure < lastPressure)
                    {
                        // Save to user specified path as cycle-XXXXX.bmp
                        camera.Snap(path + "\\cycle-" + rccm.Counter.Cycle + ".bmp");
                    }
                }

                // Check elapsed time for this loop iteration and sleep rest of period
                // Sleep prevents this plugin from choking CPU
                stopwatch.Stop();
                Thread.Sleep((int)Math.Max(0, PressureCameraTriggerPlugin.PERIOD - stopwatch.ElapsedMilliseconds));
            }
        }

        // This method is mostly relevant for plugins that loop. The loop condition 
        // should include a check to see if Running is still true. (see Run function)
        public void Stop()
        {
            this.Running = false;
        }
    }
}
