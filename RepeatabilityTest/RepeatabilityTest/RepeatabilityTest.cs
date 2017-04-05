using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RCCM;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using System.Windows.Forms;

namespace RepeatabilityTest
{
    public class RepeatabilityTest : IRCCMPluginActor
    {
        protected readonly ICamera camera;
        protected readonly Motor motor;
        protected string path;
        protected int repititions;
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
            this.repititions = Int32.Parse(parameters["Repititions"]);
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
                for (int i = 0; i < this.repititions; i++)
                {
                    lock (this.camera)
                    {
                        this.camera.Snap(this.path + "\\repeatability-" + i + ".bmp");
                    }
                    this.motor.MoveRel(this.distance);
                    this.motor.WaitForEndOfMove();
                    Thread.Sleep(200);
                    this.motor.MoveRel(-this.distance);
                    this.motor.WaitForEndOfMove();
                    Thread.Sleep(200);
                }
                lock (this.camera)
                {
                    this.camera.Snap(this.path + "\\repeatability-" + this.repititions + ".bmp");
                }                

                var imgProccessing = new MatlabDFTRegistration.DFTRegistration();
                MWArray[] argsOut = imgProccessing.get_offsets(2, this.path);
                MWNumericArray dxMW = new MWNumericArray(MWArrayComplexity.Real, MWNumericType.Double, argsOut[0].NumberOfElements);
                MWNumericArray dyMW = new MWNumericArray(MWArrayComplexity.Real, MWNumericType.Double, argsOut[1].NumberOfElements);
                double[,] dx = (double[,])dxMW.ToArray(MWArrayComponent.Real);
                double[,] dy = (double[,])dyMW.ToArray(MWArrayComponent.Real);

                double stdX = RepeatabilityTest.CalculateStdev(dx);
                double stdY = RepeatabilityTest.CalculateStdev(dy);
                MessageBox.Show(string.Format("Standard deviation x: {0}, y: {1}", stdX, stdY));
                using (StreamWriter file = new StreamWriter(this.path + "\\results.csv"))
                {
                    for (int r = 0; r < dx.GetLength(0); r++)
                    {
                        file.WriteLine(dx[r,0].ToString() + "," + dy[r,0].ToString());
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

        private static double CalculateStdev(double[,] nums)
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
            return Math.Sqrt(sumSq / (nums.GetLength(0) - 1));
        }
    }
}
