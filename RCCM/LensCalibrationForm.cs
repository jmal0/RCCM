using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCCM
{
    public partial class LensCalibrationForm : Form
    {
        protected NFOVLensController controller;
        protected RCCMStage stage;
        protected double[,] oldCalibration;
        protected BindingSource source;
        protected SortedList<double, CalibrationPoint> calibration;

        public LensCalibrationForm(NFOVLensController controller, RCCMStage stage)
        {
            this.controller = controller;
            this.stage = stage;

            this.oldCalibration = stage == RCCMStage.RCCM1 ? this.controller.NFOV1Calibration : this.controller.NFOV2Calibration;
            if (this.oldCalibration != null)
            {
                this.calibration = new SortedList<double, CalibrationPoint>();
                for (int i = 0; i < this.oldCalibration.GetLength(1); i++)
                {
                    this.calibration.Add(this.oldCalibration[i, 0], new CalibrationPoint(this.oldCalibration[i, 0], this.oldCalibration[i, 1]));
                }
            }

            InitializeComponent();
            this.calibrationTable.AutoGenerateColumns = true;
            this.source = new BindingSource();
            this.source.DataSource = this.calibration;
            this.calibrationTable.DataSource = this.source;
            this.calibrationTable.Refresh();
        }

        private void heightEdit_ValueChanged(object sender, EventArgs e)
        {
            // TODO: set z-stage position
        }

        private void focalPowerEdit_ValueChanged(object sender, EventArgs e)
        {
            bool result = this.controller.setFocalPower((double)this.focalPowerEdit.Value, this.stage);
            if (!result)
            {
                MessageBox.Show("Failed to send command");
            }
        }

        private void btnApplyCalibration_Click(object sender, EventArgs e)
        {
            // TODO: apply
            double reading = this.controller.getReading(this.stage);
            bool result = this.controller.setFocalPower((double) this.focalPowerEdit.Value, this.stage);
            if (!result)
            {
                MessageBox.Show("Failed to send command");
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            double[,] array = new double[this.calibration.Count, 2];
            for (int i = 0; i < this.calibration.Count; i++)
            {
                var point = this.calibration[i];
                array[i, 0] = point.Height;
                array[i, 1] = point.FocalPower;
            }
            this.controller.applyCalibration(array, this.stage);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.controller.applyCalibration(this.oldCalibration, this.stage);
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        protected class CalibrationPoint
        {
            public double Height { get; set; }
            public double FocalPower { get; set; }

            public CalibrationPoint(double h, double f)
            {
                this.Height = h;
                this.FocalPower = f;
            }
        }
    }
}
