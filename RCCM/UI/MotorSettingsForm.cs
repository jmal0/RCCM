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
    public partial class MotorSettingsForm : Form
    {
        protected readonly RCCMSystem rccm;
        public MotorSettingsForm(RCCMSystem rccm)
        {
            this.rccm = rccm;
            InitializeComponent();
        }

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

        private void MotorSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.rccm.SaveMotorSettings();
        }

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

        private void checkBoxEnable_CheckedChanged(object sender, EventArgs e)
        {
            string motorName = this.dropdownMotor.Items[this.dropdownMotor.SelectedIndex].ToString();
            string propertyName = "enabled";
            if (motorName != null && propertyName != null)
            {
                this.rccm.motors[motorName].SetProperty(propertyName, this.checkBoxEnable.Checked ? 1 : 0);
            }
        }

        private void dropdownProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string motorName = this.dropdownMotor.Items[this.dropdownMotor.SelectedIndex].ToString();
            string propertyName = this.dropdownProperty.Items[this.dropdownProperty.SelectedIndex].ToString();
            if (motorName != null && propertyName != null)
            {
                this.editValue.Value = (decimal)this.rccm.motors[motorName].GetProperty(propertyName);
            }
        }

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
    }
}
