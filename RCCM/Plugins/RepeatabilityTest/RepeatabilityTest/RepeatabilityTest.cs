using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using RCCM;
using MathWorks.MATLAB.NET.Arrays;

namespace RepeatabilityTest
{
    public class RepeatabilityTest : IRCCMPluginActor
    {
        protected readonly ICamera camera;
        protected readonly Motor motor;
        protected string path;
        protected int repetitions;
        protected double distance;
        public bool Running { get; protected set; }
        protected readonly RCCMSystem rccm;
        protected Thread testThread;

        public RepeatabilityTest(RCCMSystem rccm, Dictionary<string, string> parameters)
        {
            this.rccm = rccm;
            this.Running = false;

            // Create instance values from provided map from parameter name to user inputted value
            
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
            this.path = (string)Program.Settings.json[parameters["Camera"]]["image directory"] +
                        string.Format("\\Repeatability-{0:yyyy-MM-dd_hh-mm-ss-tt-fff}", DateTime.Now);
            Directory.CreateDirectory(this.path);
            // Actuator string must be one of 8 options
            try
            {
                this.motor = this.rccm.motors[parameters["Actuator"]];
            }
            catch (KeyNotFoundException e)
            {
                throw new ArgumentException("Actuator must be coarse X/Y or fine 1/2 X/Y");
            }
            // Extract numeric value from user input for repititions and move distance
            this.repetitions = Int32.Parse(parameters["Repetitions"]);
            this.distance = Double.Parse(parameters["Distance"]);
        }

        public void Run()
        {
            try
            {
                // Set running to true so main GUI knows that plugin has not finished
                this.Running = true;
                this.testThread = Thread.CurrentThread;

                // Repeatedly move and snap image
                for (int i = 0; i < this.repetitions; i++)
                {
                    this.camera.Snap(this.path + "\\repeatability-" + i + ".bmp");
                    // Move actuator out and back
                    this.motor.MoveRel(this.distance);
                    this.motor.WaitForEndOfMove();
                    Thread.Sleep(200);
                    this.motor.MoveRel(-this.distance);
                    this.motor.WaitForEndOfMove();
                    Thread.Sleep(200);
                }
                this.camera.Snap(this.path + "\\repeatability-" + this.repetitions + ".bmp");
                
                var imgProccessing = new MatlabDFTRegistration.DFTRegistration();
                MWArray[] argsOut = imgProccessing.get_offsets(2, this.path);
                double[,] dx = (double[,])argsOut[0].ToArray();
                double[,] dy = (double[,])argsOut[1].ToArray();

                double stdX = this.CalculateStdev(dx);
                double stdY = this.CalculateStdev(dy);
                MessageBox.Show(string.Format("Standard deviation x: {0:0.00}, y: {1:0.00}", stdX, stdY));
                using (StreamWriter file = new StreamWriter(this.path + "\\results.csv"))
                {
                    for (int r = 0; r < dx.GetLength(0); r++)
                    {
                        double dxi = this.camera.Scale * dx[r, 0];
                        double dyi = this.camera.Scale * dy[r, 0];
                        file.WriteLine(dxi.ToString() + "," + dyi.ToString());
                    }
                }
            }
            catch (ThreadInterruptedException e)
            {
                return;
            }            
        }

        // Stops the test by interrupting the test thread
        public void Stop()
        {
            this.Running = false;
            if (this.testThread != null)
            {
                this.testThread.Interrupt();
            }
        }

        private double CalculateStdev(double[,] nums)
        {
            double sum = 0;
            for (int i = 0; i < nums.GetLength(0); i++)
            {
                sum += nums[i,0];
            }
            double mean = sum / nums.GetLength(0);
            double sumSq = 0;
            for (int i = 0; i < nums.GetLength(0); i++)
            {
                sumSq += Math.Pow(nums[i, 0] - mean, 2);
            }
            return this.camera.Scale * Math.Sqrt(sumSq / (nums.GetLength(0) - 1));
        }
    }
}
