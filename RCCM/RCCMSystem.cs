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

        public CycleCounter Counter { get; private set; }

        protected PointF rccm1Offset;
        protected PointF rccm2Offset;
        public double FineStageAngle
        {
            get { return this.fineStageAngle; }
            set
            {
                this.fineStageAngle = value;
                this.setFineStageRotationMatrix();
            }
        }
        protected double fineStageAngle;
        protected double[,] fineStageRotation;

        public double PanelAngle
        {
            get { return this.panelAngle; }
            set
            {
                this.panelAngle = value;
                this.setPanelRotationMatrix();
            }
        }
        protected double panelAngle;
        protected double[,] panelRotation;

        public double PanelOffsetX { get; set; }
        public double PanelOffsetY { get; set; }

        public TrioController triopc { get; private set; }

        public RCCMSystem(AxTrioPC axTrioPC)
        {
            this.initializeMotion(axTrioPC);

            this.NFOV1 = new NFOV((uint)Program.Settings.json["nfov 1"]["camera serial"],
                                  (double)Program.Settings.json["nfov 1"]["microns / pixel"]);
            this.NFOV2 = new NFOV((uint)Program.Settings.json["nfov 2"]["camera serial"],
                                  (double)Program.Settings.json["nfov 2"]["microns / pixel"]);

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
            
            // Create cycle counter with test frequency specified in settings
            // Convert frequency to period in milliseconds
            double freq = (int)Program.Settings.json["cycle frequency"];
            this.Counter = new CycleCounter((int) Math.Round(1000.0 / freq));

            // Save position vectors from pivot plate center to NFOV cameras
            this.rccm1Offset = new PointF((float)Program.Settings.json["fine 1"]["x"],
                                          (float)Program.Settings.json["fine 1"]["y"]);
            this.rccm2Offset = new PointF((float)Program.Settings.json["fine 2"]["x"],
                                          (float)Program.Settings.json["fine 2"]["y"]);
            // Create rotation matrix representing rotation plate angle
            this.fineStageRotation = new double[2, 2];
            this.FineStageAngle = (double) Program.Settings.json["rotation angle"];
            // Create rotation matrix representing panel rotation
            this.panelRotation = new double[2, 2];
            this.PanelAngle = (double)Program.Settings.json["panel"]["rotation"];
            // Offset vector for panel (from corner of coarse stage travel to corner of panel
            this.PanelOffsetX = (double)Program.Settings.json["panel"]["x"];
            this.PanelOffsetY = (double)Program.Settings.json["panel"]["y"];
        }

        protected void setFineStageRotationMatrix()
        {
            this.fineStageRotation[0, 0] = Math.Cos(this.fineStageAngle * Math.PI / 180.0);
            this.fineStageRotation[0, 1] = Math.Sin(this.fineStageAngle * Math.PI / 180.0);
            this.fineStageRotation[1, 0] = -Math.Sin(this.fineStageAngle * Math.PI / 180.0);
            this.fineStageRotation[1, 1] = Math.Cos(this.fineStageAngle * Math.PI / 180.0);
        }

        protected void setPanelRotationMatrix()
        {
            this.panelRotation[0, 0] = Math.Cos(this.panelAngle * Math.PI / 180.0);
            this.panelRotation[0, 1] = Math.Sin(this.panelAngle * Math.PI / 180.0);
            this.panelRotation[1, 0] = -Math.Sin(this.panelAngle * Math.PI / 180.0);
            this.panelRotation[1, 1] = Math.Cos(this.panelAngle * Math.PI / 180.0);
        }

        public PointF GetNFOVLocation(RCCMStage stage, CoordinateSystem sys)
        {
            PointF coarselocation = new PointF((float)this.motors["coarse X"].GetPos(),
                                               (float)this.motors["coarse Y"].GetPos());
            double xOffset;
            double yOffset;
            if (stage == RCCMStage.RCCM1)
            {
                xOffset = this.rccm1Offset.X + this.motors["fine 1 X"].GetPos();
                yOffset = this.rccm1Offset.Y + this.motors["fine 1 Y"].GetPos();
            }
            else
            {
                xOffset = this.rccm2Offset.X + this.motors["fine 2 X"].GetPos();
                yOffset = this.rccm2Offset.Y + this.motors["fine 2 Y"].GetPos();
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
                if (!this.triopc.Open)
                {
                    this.motors.Add(motorName, new VirtualMotor());
                    continue;
                }
                switch ((string)Program.Settings.json[motorName]["type"])
                {
                    case "virtual":
                        this.motors.Add(motorName, new VirtualMotor());
                        break;
                    case "stepper":
                        this.motors.Add(motorName, new TrioStepperMotor(this.triopc, (short)Program.Settings.json[motorName]["axis number"]));
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

        public void readHeight1()
        {
            Logger.Out(this.LensController.GetHeight(RCCMStage.RCCM1).ToString());
        }
    }
}
