using System;
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
    /// Form for adjusting settings of individual actuators
    /// </summary>
    public partial class MotorSettingsForm : Form
    {
        protected readonly RCCMSystem rccm;

        /// <summary>
        /// Create form
        /// </summary>
        /// <param name="rccm">Reference to RCCM object</param>
        public MotorSettingsForm(RCCMSystem rccm)
        {
            this.rccm = rccm;
            InitializeComponent();
        }

        /// <summary>
        /// Adds dropdown options for motors and properties
        /// </summary>
        private void MotorSettingsForm_Load(object sender, EventArgs e)
        {
            foreach (string motor in RCCMSystem.AXES)
            {
                this.dropdownMotor.Items.Add(motor);
            }
            foreach (string property in Motor.MOTOR_SETTINGS)
            {
                this.dropdownProperty.Items.Add(property);
            }
            this.dropdownMotor.SelectedIndexChanged -= dropdownMotor_SelectedIndexChanged;
            this.dropdownMotor.SelectedIndex = 0;
            this.dropdownMotor.SelectedIndexChanged += dropdownMotor_SelectedIndexChanged;
            this.dropdownProperty.SelectedIndex = 0;

            string motorName = this.dropdownMotor.Items[this.dropdownMotor.SelectedIndex].ToString();
            this.checkBoxEnable.Checked = this.rccm.motors[motorName].GetProperty("enabled") == 0.0 ? false : true;
        }

        /// <summary>
        /// Saves settings to file
        /// </summary>
        private void MotorSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.rccm.SaveMotorSettings();
        }

        /// <summary>
        /// Load settings for user selected motor
        /// </summary>
        private void dropdownMotor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string motorName = this.dropdownMotor.Items[this.dropdownMotor.SelectedIndex].ToString();
            string propertyName = this.dropdownProperty.Items[this.dropdownProperty.SelectedIndex].ToString();
            if (motorName != null && propertyName != null)
            {
                this.editValue.Value = (decimal)this.rccm.motors[motorName].GetProperty(propertyName);
                this.checkBoxEnable.Checked = this.rccm.motors[motorName].GetProperty("enabled") == 0.0 ? false : true;
            }
        }

        /// <summary>
        /// Enable / disable motor
        /// </summary>
        private void checkBoxEnable_CheckedChanged(object sender, EventArgs e)
        {
            string motorName = this.dropdownMotor.Items[this.dropdownMotor.SelectedIndex].ToString();
            string propertyName = "enabled";
            if (motorName != null && propertyName != null)
            {
                this.rccm.motors[motorName].SetProperty(propertyName, this.checkBoxEnable.Checked ? 1 : 0);
            }
        }

        /// <summary>
        /// Loads property value when property selection changed
        /// </summary>
        private void dropdownProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string motorName = this.dropdownMotor.Items[this.dropdownMotor.SelectedIndex].ToString();
            string propertyName = this.dropdownProperty.Items[this.dropdownProperty.SelectedIndex].ToString();
            if (motorName != null && propertyName != null)
            {
                this.editValue.Value = (decimal)this.rccm.motors[motorName].GetProperty(propertyName);
            }
        }

        /// <summary>
        /// Applies setting when enter button is pressed
        /// </summary>
        private void editValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string motorName = this.dropdownMotor.Items[this.dropdownMotor.SelectedIndex].ToString();
                string propertyName = this.dropdownProperty.Items[this.dropdownProperty.SelectedIndex].ToString();
                double value = (double)this.editValue.Value;
                if (motorName != null && propertyName != null)
                {
                    this.rccm.motors[motorName].SetProperty(propertyName, value);
                }
            }
        }

        /// <summary>
        /// Displays all property values for selected motor
        /// </summary>
        private void btnCheckStatus_Click(object sender, EventArgs e)
        {
            string motorName = this.dropdownMotor.Items[this.dropdownMotor.SelectedIndex].ToString();
            if (motorName != null)
            {
                Dictionary<string, double> properties = this.rccm.motors[motorName].GetAllProperties();
                MessageBox.Show(string.Join("\n", properties));
            }
        }

        /// <summary>
        /// Clear following errors
        /// </summary>
        private void btnZero_Click(object sender, EventArgs e)
        {
            string motorName = this.dropdownMotor.Items[this.dropdownMotor.SelectedIndex].ToString();
            if (motorName != null)
            {
                this.rccm.motors[motorName].Zero();
            }
        }

        /// <summary>
        /// Define selected actuator's position as entered value
        /// </summary>
        private void editPosition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string motorName = this.dropdownMotor.Items[this.dropdownMotor.SelectedIndex].ToString();
                if (motorName != null)
                {
                    DialogResult result = MessageBox.Show("Reset actuator position?", "Confirm Action", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        this.rccm.motors[motorName].FixPosition((double)this.editPosition.Value);
                    }
                }
            }
        }
    }
}
