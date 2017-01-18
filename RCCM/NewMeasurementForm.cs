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
    public partial class NewMeasurementSequenceForm : Form
    {
        public NewMeasurementSequenceForm(string defaultName)
        {
            InitializeComponent();
            this.textName.Text = defaultName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void colorPicker_Click(object sender, EventArgs e)
        {
            DialogResult result = this.colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.colorPicker.BackColor = colorDialog1.Color;
            }
        }

        public Color getColor()
        {
            return this.colorPicker.BackColor;
        }

        public string getName()
        {
            return this.textName.Text;
        }

        public RCCMStage getStage()
        {
            if (this.radioRccm1.Checked)
            {
                return RCCMStage.RCCM1;
            }
            return RCCMStage.RCCM2;
        }
    }
}
