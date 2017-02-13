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
        protected SortedList<double, CalibrationPoint> calibration;

        public LensCalibrationForm(NFOVLensController controller, RCCMStage stage)
        {
            InitializeComponent();

            this.controller = controller;
            this.stage = stage;

            this.oldCalibration = stage == RCCMStage.RCCM1 ? this.controller.NFOV1Calibration : this.controller.NFOV2Calibration;
            this.calibration = new SortedList<double, CalibrationPoint>();
            if (this.oldCalibration != null)
            {
                for (int i = 0; i < this.oldCalibration.GetLength(0); i++)
                {
                    this.calibration.Add(this.oldCalibration[i, 0], 
                                         new CalibrationPoint(this.oldCalibration[i, 0], this.oldCalibration[i, 1]));
                }
            }

            // Set text box values
            this.heightEdit.Value = (decimal) this.controller.getHeight(this.stage);
            this.focalPowerEdit.Value = (decimal) this.controller.getFocalPower(this.stage);
            this.updateListView();
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
            else
            {
                this.calibration.Add(reading, new CalibrationPoint(reading, (double) this.focalPowerEdit.Value));
                this.applyCalibration();
                this.updateListView();
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool result = this.applyCalibration();
            if (!result)
            {
                MessageBox.Show("Failed to apply old calibration.");
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool result = this.controller.applyCalibration(this.oldCalibration, this.stage);
            if (!result)
            {
                MessageBox.Show("Failed to apply old calibration.");
            }
            this.Close();
        }
        
        private bool applyCalibration()
        {
            double[,] array = new double[this.calibration.Count, 2];
            int i = 0;
            foreach (double key in this.calibration.Keys)
            {
                array[i, 0] = this.calibration[key].InputPower;
                array[i, 1] = this.calibration[key].FocalPower;
                i++;
            }
            return this.controller.applyCalibration(array, this.stage);
        }

        private void updateListView()
        {
            this.listCalibration.Items.Clear();
            foreach (double key in this.calibration.Keys)
            {
                this.listCalibration.Items.Add(new ListViewItem(new string[] {
                    string.Format("{0:0.000}", NFOVLensController.ToHeight(this.calibration[key].InputPower)),
                    string.Format("{0:0.000}", this.calibration[key].FocalPower),
                }));
            }

            this.listCalibration.Columns[0].Width = 105;
            this.listCalibration.Columns[1].Width = 105;
        }

        protected class CalibrationPoint
        {
            public double InputPower { get; set; }
            public double FocalPower { get; set; }

            public CalibrationPoint(double i, double f)
            {
                this.InputPower = i;
                this.FocalPower = f;
            }
        }
    }
}
