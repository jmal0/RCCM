using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using MathWorks.MATLAB.NET.Arrays;
using RCCM;

namespace CameraCalibration
{
    public class CameraCalibration : IRCCMPluginActor
    {
        public bool Running { get; private set; }

        protected readonly RCCMSystem rccm;
        protected readonly ICamera camera;
        protected string path;
        protected readonly Motor xMotor;
        protected readonly Motor yMotor;
        protected Thread testThread;

        public CameraCalibration(RCCMSystem rccm, Dictionary<string, string> parameters)
        {
            this.rccm = rccm;
            this.Running = false;

            // Camera string must be one of 4 options
            switch (parameters["Camera"].ToLower())
            {
                case "wfov 1":
                    this.camera = this.rccm.WFOV1;
                    this.xMotor = this.rccm.motors["fine 1 X"];
                    this.yMotor = this.rccm.motors["fine 1 Y"];
                    break;
                case "wfov 2":
                    this.camera = this.rccm.WFOV2;
                    this.xMotor = this.rccm.motors["fine 2 X"];
                    this.yMotor = this.rccm.motors["fine 2 Y"];
                    break;
                case "nfov 1":
                    this.camera = this.rccm.NFOV1;
                    this.xMotor = this.rccm.motors["fine 1 X"];
                    this.yMotor = this.rccm.motors["fine 1 Y"];
                    break;
                case "nfov 2":
                    this.camera = this.rccm.NFOV2;
                    this.xMotor = this.rccm.motors["fine 2 X"];
                    this.yMotor = this.rccm.motors["fine 2 Y"];
                    break;
                default:
                    throw new ArgumentException("Camera must be nfov/wfov 1/2");
            }
            this.path = (string)Program.Settings.json[parameters["Camera"]]["test data directory"] + 
                        string.Format("\\scaling-{0:yyyy-MM-dd_hh-mm-ss-tt-fff}", DateTime.Now);
            Directory.CreateDirectory(this.path);
        }

        public void Run()
        {
            try
            {
                // Create image processing Matlab object
                var imgProccessing = new MatlabDFTRegistration.DFTRegistration();

                // Set running to true so main GUI knows that plugin has not finished
                this.Running = true;
                this.testThread = Thread.CurrentThread;
                
                int moves = 10;
                double moveDist = (this.camera.Width / 2) / moves;
                double[] Positions = new double[moves + 1];
                // Snap reference image (stage centered)
                this.camera.Snap(this.path + "\\scaling-0.bmp");
                // Move stage to left end of motion
                this.xMotor.MoveRel(-moves / 2 * moveDist);
                this.xMotor.WaitForEndOfMove();
                // Repeatedly move right and snap image
                for (int i = 0; i < moves; i++)
                {
                    this.camera.Snap(path + "\\scaling-x-" + i + ".bmp");
                    Positions[i] = moveDist * (-moves / 2 + i);
                    // Move actuator out and back
                    this.xMotor.MoveRel(moveDist);
                    this.xMotor.WaitForEndOfMove();
                    Thread.Sleep(200);
                }
                Positions[moves] = moveDist * moves / 2;
                this.camera.Snap(path + "\\scaling-x-" + moves + ".bmp");

                // Move stage to top end of motion
                this.xMotor.MoveRel(-moves / 2 * moveDist);
                this.xMotor.WaitForEndOfMove();
                this.yMotor.MoveRel(-moves / 2 * moveDist);
                this.yMotor.WaitForEndOfMove();
                // Repeatedly move down and snap image
                for (int i = 0; i < moves; i++)
                {
                    this.camera.Snap(path + "\\scaling-y-" + i + ".bmp");
                    // Move actuator out and back
                    this.yMotor.MoveRel(moveDist);
                    this.yMotor.WaitForEndOfMove();
                    Thread.Sleep(200);
                }
                this.camera.Snap(path + "\\scaling-y-" + moves + ".bmp");

                // Call image processing routine to get pixel offsets of each image
                MWArray[] argsOut = imgProccessing.get_offsets(2, this.path);
                double[,] dx = (double[,])argsOut[0].ToArray();
                double[,] dy = (double[,])argsOut[1].ToArray();

                double[,] PixelXX = new double[moves + 1, 1];
                double[,] PixelXY = new double[moves + 1, 1];
                double[,] PixelYY = new double[moves + 1, 1];
                double[,] PixelYX = new double[moves + 1, 1];

                Array.Copy(dx, 1, PixelXX, 0, moves + 1);
                Array.Copy(dy, 1, PixelXX, 0, moves + 1);
                Array.Copy(dy, 2 + moves, PixelXX, 0, moves + 1);
                Array.Copy(dx, 2 + moves, PixelXX, 0, moves + 1);

                double scaleXX, scaleXY, scaleYY, scaleYX;
                double offsetXX, offsetXY, offsetYY, offsetYX;
                double rsqXX = CameraCalibration.FindLinearLeastSquaresFit(Positions, PixelXX, out scaleXX, out offsetXX);
                double rsqXY = CameraCalibration.FindLinearLeastSquaresFit(Positions, PixelXY, out scaleXY, out offsetXY);
                double rsqYY = CameraCalibration.FindLinearLeastSquaresFit(Positions, PixelYY, out scaleYY, out offsetYY);
                double rsqYX = CameraCalibration.FindLinearLeastSquaresFit(Positions, PixelYX, out scaleYX, out offsetYX);
                
                // Write results to file
                using (StreamWriter file = new StreamWriter(this.path + "\\results.csv"))
                {
                    file.WriteLine("\"X Position\",\"Y Position\",\"Pixel X\",\"Pixel Y\"");
                    for (int r = 0; r < moves + 1; r++)
                    {
                        double dxi = dx[r + 1, 0];
                        double dyi = dy[r + 1, 0];
                        file.WriteLine(Positions[r] + ",0," + dxi.ToString() + "," + dyi.ToString());
                    }
                    for (int r = 0; r < moves + 1; r++)
                    {
                        double dxi = dx[r + moves + 2, 0];
                        double dyi = dy[r + moves + 2, 0];
                        file.WriteLine("0," + Positions[r] + "," + dxi.ToString() + "," + dyi.ToString());
                    }
                }

                // Display results and ask user to accept calibration
                double scale = (scaleXX + scaleYY) / 2;
                string resultMsg = string.Format("Scale X:{0:0.00}\tCrosstalk X:{1:0.00}\n" +
                                                 "X Scale R^2:{2:0.0000}\tCrosstalk R^2:{3:0.0000}\n" +
                                                 "Scale Y:{4:0.00}, Crosstalk Y:{5:0.00}\n" +
                                                 "Y Scale R^2:{6:0.0000}\tCrosstalk R^2:{7:0.0000}",
                                                 scaleXX, scaleXY, rsqXX, rsqXY, 
                                                 scaleYY, scaleYX, rsqYY, rsqYX);
                DialogResult result = MessageBox.Show(resultMsg, "Results", MessageBoxButtons.YesNo);
                if (result == DialogResult.OK)
                {
                    this.camera.SetScale(this.rccm, scale);
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

        // Return the error squared.
        public static double SumSquaresResidual(double[] x, double[,] y, double m, double b)
        {
            double total = 0;
            for (int i = 0; i < x.Length; i++)
            {
                double dy = y[i, 0] - (m * x[i] + b);
                total += dy * dy;
            }
            return total;
        }

        // Calculate total error from mean
        public static double SumSquaresTotal(double[,] y)
        {
            double total = 0;
            for (int i = 0; i < y.Length; i++)
            {
                total += y[i, 0];
            }
            double mean = total / y.Length;
            total = 0;
            for (int i = 0; i < y.Length; i++)
            {
                total += Math.Pow(y[i, 0] - mean, 2);
            }
            return total;
        }

        // Find the least squares linear fit.
        // Return the total error.
        public static double FindLinearLeastSquaresFit(double[] x, double[,] y, out double m, out double b)
        {
            // Perform the calculation.
            // Find the values S1, Sx, Sy, Sxx, and Sxy.
            double S1 = x.Length;
            double Sx = 0;
            double Sy = 0;
            double Sxx = 0;
            double Sxy = 0;
            for (int i = 0; i < S1; i++)
            {
                Sx += x[i];
                Sy += y[i, 0];
                Sxx += x[i] * x[i];
                Sxy += x[i] * y[i, 0];
            }

            // Solve for m and b.
            m = (Sxy * S1 - Sx * Sy) / (Sxx * S1 - Sx * Sx);
            b = (Sxy * Sx - Sy * Sxx) / (Sx * Sx - S1 * Sxx);

            // Return R^2
            return 1 - SumSquaresResidual(x, y, m, b) / SumSquaresTotal(y);
        }
    }
}
