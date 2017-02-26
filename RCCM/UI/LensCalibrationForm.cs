﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCCM.UI
{
    /// <summary>
    /// GUI for adjusting NFOV lens focus calibration
    /// </summary>
    public partial class LensCalibrationForm : Form
    {
        /// <summary>
        /// Lens controller
        /// </summary>
        protected NFOVLensController controller;
        /// <summary>
        /// Parent stage of NFOV camera to be adjusted
        /// </summary>
        protected RCCMStage stage;
        /// <summary>
        /// Settings object that will store saved user changes
        /// </summary>
        protected Settings settings;
        /// <summary>
        /// Calibration data before creation of this form. Reverts calibration if cancel is clicked
        /// </summary>
        protected double[,] oldCalibration;
        /// <summary>
        /// Data of the active calibration that the user is entering
        /// </summary>
        protected SortedList<double, CalibrationPoint> calibration;

        /// <summary>
        /// Create a calibration UI form for the specified stage. Saves changes to specified settings object
        /// </summary>
        /// <param name="controller">NFOV lens controller</param>
        /// <param name="stage">Parent stage of NFOV camera to be adjusted</param>
        /// <param name="settings">Settings object that will store user changes</param>
        public LensCalibrationForm(NFOVLensController controller, RCCMStage stage, Settings settings)
        {
            InitializeComponent();

            this.controller = controller;
            this.stage = stage;
            this.settings = settings;

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

            this.controller.pauseFocusing();
        }

        private void heightEdit_ValueChanged(object sender, EventArgs e)
        {
            // TODO: set z-stage position
        }

        /// <summary>
        /// Adjust lens focal power to user entered value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void focalPowerEdit_ValueChanged(object sender, EventArgs e)
        {
            bool result = this.controller.setFocalPower((double)this.focalPowerEdit.Value, this.stage);
            if (!result)
            {
                MessageBox.Show("Failed to send command");
            }
        }

        /// <summary>
        /// Creates point in user applied calibration without saving it to the controller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApplyCalibration_Click(object sender, EventArgs e)
        {
            // Get distance sensor reading and adjust focus
            double reading = this.controller.getReading(this.stage);
            bool result = this.controller.setFocalPower((double) this.focalPowerEdit.Value, this.stage);
            if (!result)
            {
                MessageBox.Show("Failed to send command");
            }
            else
            {
                // Add to list holding user calibration points
                this.calibration.Add(reading, new CalibrationPoint(reading, (double) this.focalPowerEdit.Value));
                this.applyCalibration();
                // Update UI with new points
                this.updateListView();
            }
        }
        
        /// <summary>
        /// Applies new calibration and saves it to settings file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool result = this.applyCalibration();
            if (!result)
            {
                MessageBox.Show("Failed to apply old calibration.");
            }
            this.controller.saveToJSON(this.settings);
            // Set dialog to result so FormClosing handler does not reapply old calibration
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Reverts calibration when cancel button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        /// <summary>
        /// Applies user created calibration to NFOV Lenscontroller (i.e. the one used in analog mode)
        /// </summary>
        /// <returns>True if calibration was applied successfully</returns>
        private bool applyCalibration()
        {
            // Create 2D array for calibration. 1st column is input voltage, 2nd is output voltage
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

        /// <summary>
        /// Update UI ListView with new points
        /// </summary>
        private void updateListView()
        {
            // Create list "item" for each calibration point
            this.listCalibration.Items.Clear();
            foreach (double key in this.calibration.Keys)
            {
                // Item is represented in list as 2 element string array
                this.listCalibration.Items.Add(new ListViewItem(new string[] {
                    string.Format("{0:0.000}", NFOVLensController.ToHeight(this.calibration[key].InputPower)),
                    string.Format("{0:0.000}", this.calibration[key].FocalPower),
                }));
            }

            this.listCalibration.Columns[0].Width = this.listCalibration.Width / 2;
            this.listCalibration.Columns[1].Width = this.listCalibration.Width / 2;
        }

        /// <summary>
        /// When form is closed, resume the manual focus adjustment timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LensCalibrationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.controller.resumeFocusing();
            if(this.DialogResult != DialogResult.OK)
            {
                Logger.Out("Applying old calibration");
                bool result = this.controller.applyCalibration(this.oldCalibration, this.stage);
                if (!result)
                {
                    MessageBox.Show("Failed to send command");
                }
            }
        }
        
        /// <summary>
        /// Value class for storing the input output pairs of a calibration point
        /// </summary>
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