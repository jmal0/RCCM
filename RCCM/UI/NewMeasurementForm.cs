using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RCCM.UI
{
    /// <summary>
    /// Form for defining settings for a new MeasurementSequence
    /// </summary>
    public partial class NewMeasurementForm : Form
    {
        protected string camera;
        public static int ColorInd = 0;
        public static Color[] Colors = { Color.Red, Color.Blue, Color.Green, Color.Purple, Color.Orange, Color.Yellow, Color.Black, Color.Brown, Color.DarkBlue, Color.LimeGreen, Color.PaleVioletRed };

        public NewMeasurementForm(string defaultName, string camera)
        {
            InitializeComponent();
            this.camera = camera;
            this.textName.Text = defaultName;
            this.colorPicker.BackColor = NewMeasurementForm.Colors[NewMeasurementForm.ColorInd];
            NewMeasurementForm.ColorInd = (NewMeasurementForm.ColorInd + 1) % NewMeasurementForm.Colors.Length;
        }

        public NewMeasurementForm(MeasurementSequence crack)
        {
            InitializeComponent();
            this.camera = crack.Camera;
            this.textName.Text = crack.Name;
            this.colorPicker.BackColor = crack.Color;
            this.editLineSize.Value = (decimal)crack.LineSize;
            this.editOrientation.Value = (decimal)crack.Orientation;
            switch (crack.Mode)
            {
                case MeasurementMode.Projection:
                    this.radioMeasureProjection.Checked = true;
                    break;
                case MeasurementMode.Tip:
                    this.radioMeasureTip.Checked = true;
                    break;
                case MeasurementMode.Total:
                    this.radioMeasureTotal.Checked = true;
                    break;
            }
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
        /// Get name defined on form 
        /// </summary>
        /// <returns>The given name for the MeasurementSequence</returns>
        public string GetName()
        {
            return this.textName.Text;
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
        /// Get line size defined on form 
        /// </summary>
        /// <returns>The given line size for the MeasurementSequence</returns>
        public float GetLineSize()
        {
            return (float)this.editLineSize.Value;
        }

        /// <summary>
        /// Get crack orientation defined on form
        /// </summary>
        /// <returns>Orientation angle of crack</returns>
        public double GetOrientation()
        {
            return (double)this.editOrientation.Value;
        }
        
        /// <summary>
        /// Get selected measurement mode from form
        /// </summary>
        /// <returns>The enum value for the selected measurement mode</returns>
        public MeasurementMode GetMode()
        {
            if (this.radioMeasureProjection.Checked)
            {
                return MeasurementMode.Projection;
            }
            if (this.radioMeasureTip.Checked)
            {
                return MeasurementMode.Tip;
            }
            return MeasurementMode.Total;
        }

        /// <summary>
        /// Get name of camera capturing this crack 
        /// </summary>
        /// <returns>The name of the camera capturing this MeasurementSequence</returns>
        public string GetCamera()
        {
            return this.camera;
        }
    }
}
