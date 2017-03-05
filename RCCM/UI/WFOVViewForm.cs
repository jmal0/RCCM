using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TIS.Imaging;

namespace RCCM.UI
{
    public partial class WFOVViewForm : Form
    {
        protected readonly RCCMSystem rccm;
        protected readonly ObservableCollection<MeasurementSequence> cracks;
        protected RCCMStage stage;
        protected WFOV camera;

        public WFOVViewForm(RCCMSystem rccm, WFOV camera, ObservableCollection<MeasurementSequence> cracks)
        {
            this.rccm = rccm;
            this.camera = camera;
            this.cracks = cracks;
            InitializeComponent();
        }

        private void WFOVViewForm_Load(object sender, EventArgs e)
        {
            bool success = this.camera.initialize(this.wfovContainer);
            if (success)
            {
                btnWfovStart.Enabled = true;

                //  Setup the range of the zoom and focus sliders.
                sliderZoom.Minimum = this.camera.ZoomMin;
                sliderZoom.Maximum = this.camera.ZoomMax;
                sliderFocus.Minimum = this.camera.FocusMin;
                sliderFocus.Maximum = this.camera.FocusMax;

                //  Set the sliders to the current zoom and focus values.
                sliderZoom.Value = this.camera.Zoom;
                textZoom.Text = this.camera.Zoom.ToString();
                sliderFocus.Value = this.camera.Focus;
                textFocus.Text = this.camera.Focus.ToString();
            }
        }

        private void WFOVViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.camera.Recording)
            {
                this.camera.stopRecord();
            }
            this.camera.stop();
        }

        private void btnWfovStart_Click(object sender, EventArgs e)
        {
            this.camera.start();

            // Update button states
            if (wfovContainer.DeviceValid)
            {
                enableWfovControls();
                btnWfovStop.Enabled = true;
            }
        }

        private void btnWfovStop_Click(object sender, EventArgs e)
        {
            this.camera.stop();

            // Update button states
            if (!wfovContainer.LiveVideoRunning)
            {
                disableWfovControls();
                btnWfovStart.Enabled = true;
            }
        }

        private void btnWfovSnap_Click(object sender, EventArgs e)
        {
            string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt-fff}", DateTime.Now);
            string dir = (string)Program.Settings.json["image directory"];
            this.camera.snapImage(dir + "\\" + timestamp + ".png");
        }

        private void btnWfovProperties_Click(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                this.camera.editProperties();
            }
        }

        private void btnWfovRecord_Click(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                if (this.camera.Recording == false)
                {
                    string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt-fff}", DateTime.Now);
                    string dir = (string)Program.Settings.json["video directory"];
                    this.camera.record(dir + @"\" + timestamp + ".avi");
                    btnWfovRecord.BackColor = Color.Gray;
                    btnWfovStart.Enabled = false;
                    btnWfovSnap.Enabled = false;
                }
                else
                {
                    this.camera.stopRecord();
                    MessageBox.Show("Recording stopped");
                    btnWfovRecord.BackColor = Color.Transparent;
                    btnWfovStart.Enabled = true;
                    btnWfovSnap.Enabled = true;
                }
            }
        }

        private void btnFocus_Click(object sender, EventArgs e)
        {
            btnFocus.Enabled = false;
            int newFocus = this.camera.autoFocus();
            this.sliderFocus.Value = newFocus;
            this.textFocus.Text = newFocus.ToString();
            btnFocus.Enabled = true;
        }

        private void sliderZoom_Scroll(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                this.camera.Zoom = sliderZoom.Value;
                textZoom.Text = sliderZoom.Value.ToString();
            }
        }

        private void sliderFocus_Scroll(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                this.camera.Focus = sliderFocus.Value;
                textFocus.Text = sliderFocus.Value.ToString();
            }
        }

        private void enableWfovControls()
        {
            btnWfovStart.Enabled = true;
            btnWfovStop.Enabled = true;
            btnWfovSnap.Enabled = true;
            btnWfovRecord.Enabled = true;
            btnWfovProperties.Enabled = true;
            btnFocus.Enabled = true;
            sliderFocus.Enabled = true;
            sliderZoom.Enabled = true;
        }

        private void disableWfovControls()
        {
            btnWfovStart.Enabled = false;
            btnWfovStop.Enabled = false;
            btnWfovSnap.Enabled = false;
            btnWfovRecord.Enabled = false;
            btnWfovProperties.Enabled = false;
            btnFocus.Enabled = false;
            sliderFocus.Enabled = false;
            sliderZoom.Enabled = false;
        }
    }
}
