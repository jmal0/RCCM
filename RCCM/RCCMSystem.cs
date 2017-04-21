using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxTrioPCLib;

namespace RCCM
{
    public class RCCMSystem
    {
        public static string[] AXES = new string[8] { "coarse X", "coarse Y", "fine 1 X", "fine 1 Y", "fine 1 Z", "fine 2 X", "fine 2 Y", "fine 2 Z" };
        
        public NFOV NFOV1 { get; }
        public NFOV NFOV2 { get; }
        public WFOV WFOV1 { get; }
        public WFOV WFOV2 { get; }

        public NFOVLensController LensController { get; private set; }

        public Dictionary<string, Motor> motors { get; private set; }

        public ICycleCounter Counter { get; private set; }

        public double FineStageAngle
        {
            get { return (double)Program.Settings.json["rotation angle"]; }
            set
            {
                Program.Settings.json["rotation angle"] = value;
                this.setFineStageRotationMatrix();
            }
        }
        protected double[,] fineStageRotation;

        public double PanelAngle
        {
            get { return (double)Program.Settings.json["panel"]["rotation"]; }
            set
            {
                Program.Settings.json["panel"]["rotation"] = value;
                this.setPanelRotationMatrix();
            }
        }
        protected double[,] panelRotation;

        public double PanelOffsetX
        {
            get { return (double)Program.Settings.json["panel"]["x"]; }
            set { Program.Settings.json["panel"]["x"] = value; }
        }
        public double PanelOffsetY
        {
            get { return (double)Program.Settings.json["panel"]["y"]; }
            set { Program.Settings.json["panel"]["y"] = value; }
        }
        public double PanelWidth
        {
            get { return (double)Program.Settings.json["panel"]["width"]; }
            set { Program.Settings.json["panel"]["width"] = value; }
        }
        public double PanelHeight
        {
            get { return (double)Program.Settings.json["panel"]["height"]; }
            set { Program.Settings.json["panel"]["height"] = value; }
        }
        public double PanelRadius
        {
            get { return (double)Program.Settings.json["panel"]["radius"]; }
            set { Program.Settings.json["panel"]["radius"] = value; }
        }

        public double NFOV1X
        {
            get { return (double)Program.Settings.json["nfov 1"]["x offset"]; }
            set { Program.Settings.json["nfov 1"]["x offset"] = value; }
        }
        public double NFOV1Y
        {
            get { return (double)Program.Settings.json["nfov 1"]["y offset"]; }
            set { Program.Settings.json["nfov 1"]["y offset"] = value; }
        }
        public double NFOV2X
        {
            get { return (double)Program.Settings.json["nfov 2"]["x offset"]; }
            set { Program.Settings.json["nfov 2"]["x offset"] = value; }
        }
        public double NFOV2Y
        {
            get { return (double)Program.Settings.json["nfov 2"]["y offset"]; }
            set { Program.Settings.json["nfov 2"]["y offset"] = value; }
        }
        public double WFOV1X
        {
            get { return (double)Program.Settings.json["wfov 1"]["x offset"]; }
            set { Program.Settings.json["wfov 1"]["x offset"] = value; }
        }
        public double WFOV1Y
        {
            get { return (double)Program.Settings.json["wfov 1"]["y offset"]; }
            set { Program.Settings.json["wfov 1"]["y offset"] = value; }
        }
        public double WFOV2X
        {
            get { return (double)Program.Settings.json["wfov 2"]["x offset"]; }
            set { Program.Settings.json["wfov 2"]["x offset"] = value; }
        }
        public double WFOV2Y
        {
            get { return (double)Program.Settings.json["wfov 2"]["y offset"]; }
            set { Program.Settings.json["wfov 2"]["y offset"] = value; }
        }

        public TrioController triopc { get; private set; }

        public RCCMSystem(AxTrioPC axTrioPC)
        {
            // Create rotation matrix representing rotation plate angle
            this.fineStageRotation = new double[2, 2];
            this.FineStageAngle = (double)Program.Settings.json["rotation angle"];
            // Create rotation matrix representing panel rotation
            this.panelRotation = new double[2, 2];
            this.PanelAngle = (double)Program.Settings.json["panel"]["rotation"];

            // Initialize NFOV cameras & apply settings
            this.NFOV1 = new NFOV((uint)Program.Settings.json["nfov 1"]["camera serial"],
                                  (double)Program.Settings.json["nfov 1"]["microns / pixel"],
                                  (uint)Program.Settings.json["nfov 1"]["height"],
                                  (uint)Program.Settings.json["nfov 1"]["width"]);
            this.NFOV2 = new NFOV((uint)Program.Settings.json["nfov 2"]["camera serial"],
                                  (double)Program.Settings.json["nfov 2"]["microns / pixel"],
                                  (uint)Program.Settings.json["nfov 2"]["height"],
                                  (uint)Program.Settings.json["nfov 2"]["width"]);

            // Read NFOV lens voltage to distance conversion into double arrays
            double[] conversion1 = { (double)Program.Settings.json["nfov 1"]["conversion"][0], (double)Program.Settings.json["nfov 1"]["conversion"][1] };
            double[] conversion2 = { (double)Program.Settings.json["nfov 2"]["conversion"][0], (double)Program.Settings.json["nfov 2"]["conversion"][1] };
            // Read NFOV lens calibrations into double arrays
            int i;
            int calibration1Rows = Program.Settings.json["nfov 1"]["calibration"].Count();
            Logger.Out(calibration1Rows.ToString());
            double[,] calibration1 = new double[calibration1Rows, 2];
            i = 0;
            foreach (JArray row in Program.Settings.json["nfov 1"]["calibration"].Children())
            {
                calibration1[i, 0] = (double) row[0];
                calibration1[i, 1] = (double) row[1];
                i++;
            }
            int calibration2Rows = Program.Settings.json["nfov 2"]["calibration"].Count();
            double[,] calibration2 = new double[calibration2Rows, 2];
            i = 0;
            foreach (JArray row in Program.Settings.json["nfov 2"]["calibration"].Children())
            {
                calibration2[i, 0] = (double)row[0];
                calibration2[i, 1] = (double)row[1];
                i++;
            }
            // Create lens controller
            this.LensController = new NFOVLensController((int) Program.Settings.json["nfov 1"]["controller serial"],
                                                         (int) Program.Settings.json["nfov 2"]["controller serial"],
                                                         conversion1,
                                                         conversion2,
                                                         calibration1,
                                                         calibration2);

            // Initialize WFOV cameras
            this.WFOV1 = new WFOV((string)Program.Settings.json["wfov 1"]["configuration file"],
                                  (double)Program.Settings.json["wfov 1"]["microns / pixel"]);
            this.WFOV2 = new WFOV((string)Program.Settings.json["wfov 2"]["configuration file"],
                                  (double)Program.Settings.json["wfov 2"]["microns / pixel"]);

            // Initialize motors
            this.initializeMotion(axTrioPC);

            // Create cycle counter with test frequency specified in settings
            // Convert frequency to period in milliseconds
            if ((string)Program.Settings.json["cycle counter"]["type"] == "virtual")
            {
                double freq = (int)Program.Settings.json["cycle frequency"];
                this.Counter = new CycleCounter((int)Math.Round(1000.0 / freq));
            }
            else
            {
                this.Counter = new DataqCycleCounter();
            }
        }

        protected void setFineStageRotationMatrix()
        {
            this.fineStageRotation[0, 0] = Math.Cos(this.FineStageAngle * Math.PI / 180.0);
            this.fineStageRotation[0, 1] = Math.Sin(this.FineStageAngle * Math.PI / 180.0);
            this.fineStageRotation[1, 0] = -Math.Sin(this.FineStageAngle * Math.PI / 180.0);
            this.fineStageRotation[1, 1] = Math.Cos(this.FineStageAngle * Math.PI / 180.0);
        }

        protected void setPanelRotationMatrix()
        {
            this.panelRotation[0, 0] = Math.Cos(this.PanelAngle * Math.PI / 180.0);
            this.panelRotation[0, 1] = Math.Sin(this.PanelAngle * Math.PI / 180.0);
            this.panelRotation[1, 0] = -Math.Sin(this.PanelAngle * Math.PI / 180.0);
            this.panelRotation[1, 1] = Math.Cos(this.PanelAngle * Math.PI / 180.0);
        }

        /// <summary>
        /// Calculate expected distance to panel from distance sensor at a given point
        /// </summary>
        /// <param name="panelX">X coordinate on panel</param>
        /// <param name="panelY">Y coordinate on panel</param>
        /// <returns>Distance to panel - may be negative</returns>
        public double GetPanelDistance(double panelX, double panelY)
        {
            if (panelX < 0 || panelX > this.PanelWidth || panelY < 0 || panelX > this.PanelHeight)
            {
                return 0;
            }
            double yCenter = this.PanelHeight / 2 - panelY; // Y coordinate from center of panel
            double zCorner = Math.Sqrt(this.PanelRadius * this.PanelRadius - this.PanelHeight * this.PanelHeight / 4);
            double zPoint = Math.Sqrt(this.PanelRadius * this.PanelRadius - yCenter * yCenter);
            return zPoint - zCorner;
        }

        public PointF GetNFOVLocation(RCCMStage stage, CoordinateSystem sys)
        {
            PointF coarselocation = new PointF((float)this.motors["coarse X"].GetPos(),
                                               (float)this.motors["coarse Y"].GetPos());
            double xOffset;
            double yOffset;
            if (stage == RCCMStage.RCCM1)
            {
                xOffset = this.NFOV1X + this.motors["fine 1 X"].GetPos();
                yOffset = this.NFOV1Y + this.motors["fine 1 Y"].GetPos();
            }
            else
            {
                xOffset = this.NFOV2X + this.motors["fine 2 X"].GetPos();
                yOffset = this.NFOV2Y + this.motors["fine 2 Y"].GetPos();
            }
            double xOffRotated = this.fineStageRotation[0, 0] * xOffset + this.fineStageRotation[0, 1] * yOffset;
            double yOffRotated = this.fineStageRotation[1, 0] * xOffset + this.fineStageRotation[1, 1] * yOffset;
            PointF globalPt =  PointF.Add(coarselocation, new SizeF((float)xOffRotated, (float)yOffRotated));
            switch (sys)
            {
                case CoordinateSystem.Local:
                    return this.GlobalVectorToPanelVector(this.motors["coarse X"].GetPos() + xOffRotated,
                                                          this.motors["coarse Y"].GetPos() + yOffRotated);
                default:
                    return globalPt;
            }
        }

        public PointF GetWFOVLocation(RCCMStage stage, CoordinateSystem sys)
        {
            PointF coarselocation = new PointF((float)this.motors["coarse X"].GetPos(),
                                               (float)this.motors["coarse Y"].GetPos());
            double xOffset;
            double yOffset;
            if (stage == RCCMStage.RCCM1)
            {
                xOffset = this.WFOV1X + this.motors["fine 1 X"].GetPos();
                yOffset = this.WFOV1Y + this.motors["fine 1 Y"].GetPos();
            }
            else
            {
                xOffset = this.WFOV2X + this.motors["fine 2 X"].GetPos();
                yOffset = this.WFOV2Y + this.motors["fine 2 Y"].GetPos();
            }
            double xOffRotated = this.fineStageRotation[0, 0] * xOffset + this.fineStageRotation[0, 1] * yOffset;
            double yOffRotated = this.fineStageRotation[1, 0] * xOffset + this.fineStageRotation[1, 1] * yOffset;
            PointF globalPt = PointF.Add(coarselocation, new SizeF((float)xOffRotated, (float)yOffRotated));
            switch (sys)
            {
                case CoordinateSystem.Local:
                    return this.GlobalVectorToPanelVector(this.motors["coarse X"].GetPos() + xOffRotated,
                                                          this.motors["coarse Y"].GetPos() + yOffRotated);
                default:
                    return globalPt;
            }
        }

        public PointF FineVectorToGlobalVector(double x, double y)
        {
            double globalX = this.fineStageRotation[0, 0] * x + this.fineStageRotation[0, 1] * y;
            double globalY = this.fineStageRotation[1, 0] * x + this.fineStageRotation[1, 1] * y;
            return new PointF((float) globalX, (float) globalY);
        }

        public PointF GlobalVectorToPanelVector(double x, double y)
        {
            double panelX = this.panelRotation[0, 0] * x + this.panelRotation[0, 1] * y;
            double panelY = this.panelRotation[1, 0] * x + this.panelRotation[1, 1] * y;
            return new PointF((float)(panelX - this.PanelOffsetX), (float)(panelY - this.PanelOffsetY));
        }

        #region Motors

        private void initializeMotion(AxTrioPC axTrioPC)
        {
            // Create handler for Trio controller communication
            this.triopc = new TrioController(axTrioPC);

            // Initialize each motor and apply settings
            this.motors = new Dictionary<string, Motor>();
            foreach (string motorName in RCCMSystem.AXES)
            {
                // If controller is not open, all motors must be virtual
                if (!this.triopc.Open && ((string)Program.Settings.json[motorName]["type"]).Contains("stepper"))
                {
                    this.motors.Add(motorName, new VirtualMotor());
                    continue;
                }
                switch ((string)Program.Settings.json[motorName]["type"])
                {
                    case "virtual":
                        this.motors.Add(motorName, new VirtualMotor());
                        break;
                    case "virtual z 1":
                        this.motors.Add(motorName, new VirtualZMotor(delegate () { return this.LensController.Height1; }));
                        break;
                    case "virtual z 2":
                        this.motors.Add(motorName, new VirtualZMotor(delegate () { return this.LensController.Height2; }));
                        break;
                    case "stepper":
                        this.motors.Add(motorName, new TrioStepperMotor(this.triopc, (short)Program.Settings.json[motorName]["axis number"]));
                        break;
                    case "stepper z 1":
                        TrioStepperZMotor zMotor1 = new TrioStepperZMotor(this.triopc, (short)Program.Settings.json[motorName]["axis number"],
                                                                          this, RCCMStage.RCCM1);
                        this.motors.Add(motorName, zMotor1);
                        this.LensController.Motor1 = zMotor1;
                        break;
                    case "stepper z 2":
                        TrioStepperZMotor zMotor2 = new TrioStepperZMotor(this.triopc, (short)Program.Settings.json[motorName]["axis number"],
                                                                          this, RCCMStage.RCCM1);
                        this.motors.Add(motorName, zMotor2);
                        this.LensController.Motor2 = zMotor2;
                        break;
                    default:
                        throw new NotImplementedException("Unknown motor type setting encountered for " + motorName);
                }
            }
            // Apply settings
            ApplyMotorSettings();
        }

        public void ApplyMotorSettings()
        {
            foreach (string motorName in RCCMSystem.AXES)
            {
                foreach (string property in Motor.MOTOR_SETTINGS)
                {
                    this.motors[motorName].SetProperty(property, (double) Program.Settings.json[motorName][property]);
                }
            }
        }

        public void SaveMotorSettings()
        {
            foreach (string motorName in RCCMSystem.AXES)
            {
                foreach (string property in Motor.MOTOR_SETTINGS)
                {
                    Program.Settings.json[motorName][property] = this.motors[motorName].GetProperty(property);
                }
            }
        }

        #endregion

        public void Stop()
        {
            this.LensController.Stop();
            this.Counter.Terminate();
            foreach (string motorName in RCCMSystem.AXES)
            {
                this.motors[motorName].Terminate();
            }
        }
    }
}
