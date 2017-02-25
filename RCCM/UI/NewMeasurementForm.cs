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
    /// Form for defining settings for a new MeasurementSequence
    /// </summary>
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

        /// <summary>
        /// Get color selected with color picker dialog on form 
        /// </summary>
        /// <returns>The selected color</returns>
        public Color GetColor()
        {
            return this.colorPicker.BackColor;
        }

        /// <summary>
        /// Get name defined on form 
        /// </summary>
        /// <returns>The given name for the MeasurementSequence</returns>
        public string GetName()
        {
            return this.textName.Text;
        }
        
        /// <summary>
        /// Get line size defined on form 
        /// </summary>
        /// <returns>The given line size for the MeasurementSequence</returns>
        public float GetLineSize()
        {
            return (float) this.editLineSize.Value;
        }

        /// <summary>
        /// Get select parent stage from form 
        /// </summary>
        /// <returns>The enum value for the parent stage of the MeasurementSequence</returns>
        public RCCMStage GetStage()
        {
            if (this.radioRccm1.Checked)
            {
                return RCCMStage.RCCM1;
            }
            return RCCMStage.RCCM2;
        }
    }
}
