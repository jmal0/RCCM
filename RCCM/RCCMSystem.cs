using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    public class RCCMSystem
    {
        private static string[] AXES = new string[8] { "coarse X", "coarse Y", "fine 1 X", "fine 1 Y", "fine 1 Z", "fine 2 X", "fine 2 Y", "fine 2 Z" };
        private static string[] MOTOR_PROPERTIES = new string[2] { "low position limit", "high position limit" };

        protected WFOV wfov1;
        protected WFOV wfov2;
        
        public NFOV NFOV1 { get; }
        public NFOV NFOV2 { get; }

        public RCCMStage ActiveStage { get; private set; }

        protected NFOVLensController nfovLensController;

        protected Dictionary<string, Motor> motors;

        protected CycleCounter counter;

        protected PointF rccm1Offset;
        protected PointF rccm2Offset;
        public double FineStageAngle { get; private set; }
        protected double[,] fineStageRotation;

        public RCCMSystem(Settings settings)
        {
            this.NFOV1 = new NFOV((uint) settings.json["nfov 1"]["camera serial"],
                                  (double) settings.json["nfov 1"]["microns / pixel"]);

            this.nfovLensController = new NFOVLensController((int) settings.json["nfov 1"]["controller serial"],
                                                             (int) settings.json["nfov 2"]["controller serial"]);

            // TODO: lol yeah
            this.ActiveStage = RCCMStage.RCCM1;

            // Initialize each motor and apply settings
            this.motors = new Dictionary<string, Motor>();
            foreach (string motorName in RCCMSystem.AXES)
            {
                switch ((string) settings.json[motorName]["type"])
                {
                    case "virtual":
                        this.motors.Add(motorName, new VirtualMotor());
                        break;
                    default:
                        throw new NotImplementedException("Unknown motor type setting encountered for " + motorName);
                }
            }

            // Create cycle counter with test frequency specified in settings
            // Convert frequency to period in milliseconds
            double freq = (int) settings.json["cycle frequency"];
            this.counter = new CycleCounter((int) Math.Round(1000.0 / freq));

            // Save position vectors from pivot plate center to NFOV cameras
            this.rccm1Offset = new PointF((float) settings.json["fine 1"]["x"],
                                          (float) settings.json["fine 1"]["y"]);
            this.rccm2Offset = new PointF((float) settings.json["fine 2"]["x"],
                                          (float) settings.json["fine 2"]["y"]);
            // Create rotation matrix representing rotation plate angle
            this.FineStageAngle = (double) settings.json["rotation angle"];
            this.fineStageRotation = new double[2,2];
            this.fineStageRotation[0, 0] = Math.Cos(this.FineStageAngle * Math.PI / 180.0);
            this.fineStageRotation[0, 1] = Math.Sin(this.FineStageAngle * Math.PI / 180.0);
            this.fineStageRotation[1, 0] = -Math.Sin(this.FineStageAngle * Math.PI / 180.0);
            this.fineStageRotation[1, 1] = Math.Cos(this.FineStageAngle * Math.PI / 180.0);
            // Apply settings
            applyMotorSettings(settings);
        }
        
        // UNUSED
        public Region getImageLimits()
        {
            float imgWidth = (float) this.NFOV1.Width;
            float imgHeight = (float) this.NFOV1.Height;

            float topLeftX = (float) (this.getPosition("coarse X") + this.getPosition("fine 1 X") - imgWidth / 2);
            float topLeftY = (float) (this.getPosition("coarse X") + this.getPosition("fine 1 X") - imgHeight / 2);
            return new Region(new RectangleF(topLeftX, topLeftY, imgWidth, imgHeight));
        }

        public PointF getNFOVLocation(RCCMStage stage)
        {
            PointF coarselocation = new PointF((float)this.getPosition("coarse X"),
                                               (float)this.getPosition("coarse Y"));
            double xOffset;
            double yOffset;
            if (stage == RCCMStage.RCCM1)
            {
                xOffset = this.rccm1Offset.X + this.getPosition("fine 1 X");
                yOffset = this.rccm1Offset.Y + this.getPosition("fine 1 Y");
            }
            else
            {
                xOffset = this.rccm2Offset.X + this.getPosition("fine 2 X");
                yOffset = this.rccm2Offset.Y + this.getPosition("fine 2 Y");
            }
            double xOffRotated = this.fineStageRotation[0, 0] * xOffset + this.fineStageRotation[0, 1] * yOffset;
            double yOffRotated = this.fineStageRotation[1, 0] * xOffset + this.fineStageRotation[1, 1] * yOffset;
            return PointF.Add(coarselocation, new SizeF((float)xOffRotated, (float)yOffRotated));
        }

        public PointF pixelToGlobalVector(double x, double y)
        {
            double globalX = this.fineStageRotation[0, 0] * x + this.fineStageRotation[0, 1] * y;
            double globalY = this.fineStageRotation[1, 0] * x + this.fineStageRotation[1, 1] * y;
            return new PointF((float) globalX, (float) globalY);
        }

        #region Motors

        public void applyMotorSettings(Settings settings)
        {
            foreach (string motorName in RCCMSystem.AXES)
            {
                foreach (string property in RCCMSystem.MOTOR_PROPERTIES)
                {
                    this.motors[motorName].setProperty(property, (double) settings.json[motorName][property]);
                }
            }
        }

        public double getPosition(string axis)
        {
            return this.motors[axis].getPos();
        }

        public double setPosition(string axis, double value)
        {
            double result = this.motors[axis].setPos(value);
            Console.WriteLine(axis + " " + value);
            return result;
        }

        #endregion

        #region Cycles
        
        public int getCycle()
        {
            return this.counter.Cycle;
        }

        public bool setCycleFrequency(double freq)
        {
            if (freq > 0)
            {
                this.counter.setPeriod((int) Math.Round(1000.0 / freq));
                return true;
            }
            return false;
        }

        public void startCounting()
        {
            this.counter.start();
        }

        public void stopCounting()
        {
            this.counter.stop();
        }

        #endregion

        public void readHeight1()
        {
            Console.WriteLine(this.nfovLensController.getHeight(RCCMStage.RCCM1));
        }
    }
}
