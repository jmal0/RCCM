using System;
using System.Collections.Generic;
using System.Drawing;
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

        protected NFOV nfov1;
        public NFOV NFOV1
        {
            get { return this.nfov1; }
        }
        protected NFOV nfov2;
        public NFOV NFOV2
        {
            get { return this.nfov2; }
        }

        protected NFOVLensController nfovLensController;

        protected Dictionary<string, Motor> motors;

        public RCCMSystem(Settings settings)
        {
            this.nfov1 = new NFOV((double) settings.json["nfov 1"]["microns / pixel"]);

            this.nfovLensController = new NFOVLensController((int) settings.json["nfov 1"]["serial number"], (int) settings.json["nfov 2"]["serial number"]);

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
            
            // Apply settings
            applyMotorSettings(settings);
        }
        
        // UNUSED
        public Region getImageLimits()
        {
            float imgWidth = (float) this.nfov1.Width;
            float imgHeight = (float) this.nfov1.Height;

            float topLeftX = (float) (this.getPosition("coarse X") + this.getPosition("fine 1 X") - imgWidth / 2);
            float topLeftY = (float) (this.getPosition("coarse X") + this.getPosition("fine 1 X") - imgHeight / 2);
            return new Region(new RectangleF(topLeftX, topLeftY, imgWidth, imgHeight));
        }

        public PointF getNFOV1Location()
        {
            float centerX = (float) (this.getPosition("coarse X") + this.getPosition("fine 1 X"));
            float centerY = (float) (this.getPosition("coarse Y") + this.getPosition("fine 1 Y"));
            return new PointF(centerX, centerY);
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

        public void readHeight1()
        {
            Console.WriteLine(this.nfovLensController.getHeight(RCCMStage.RCCM1));
        }
    }
}
