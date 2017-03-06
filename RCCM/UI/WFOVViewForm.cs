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
        /// <summary>
        /// The RCCM system object. Used for calculating NFOV position
        /// </summary>
        protected readonly RCCMSystem rccm;
        /// <summary>
        /// List of cracks that are being measured
        /// </summary>
        protected readonly ObservableCollection<MeasurementSequence> cracks;
        /// <summary>
        /// WFOV camera that is displayed by this window
        /// </summary>
        protected readonly WFOV camera;
        /// <summary>
        /// Parent stage of this camera
        /// </summary>
        protected RCCMStage stage;
        /// <summary>
        /// Counter for giving unique number to cracks
        /// </summary>
        protected int measurementCounter;
        /// <summary>
        /// Currently selected crack index. The selected crack will be edited by other controls
        /// </summary>
        protected int ActiveIndex { get; set; }
        /// <summary>
        /// Currently selected point in ListPoints. This point is indicated in the image display
        /// </summary>
        protected int ActivePoint { get; set; }
        /// <summary>
        /// Indicates whether or not user is drawing a line with mouse
        /// </summary>
        protected bool Drawing { get; private set; }
        
        public WFOVViewForm(RCCMSystem rccm, WFOV camera, ObservableCollection<MeasurementSequence> cracks)
        {
            this.rccm = rccm;
            this.camera = camera;
            this.stage = this.camera == rccm.WFOV1 ? RCCMStage.RCCM1 : RCCMStage.RCCM2;
            this.cracks = cracks;
            this.Drawing = false;
            this.ActiveIndex = -1;
            this.ActivePoint = -1;
            this.measurementCounter = 0;
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
            this.wfovContainer.OverlayUpdate += new EventHandler<ICImagingControl.OverlayUpdateEventArgs>(wfovOverlayPaint);
        }

        private void WFOVViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.camera.Recording)
            {
                this.camera.stopRecord();
            }
            this.camera.stop();
        }

        /// <summary>
        /// Draw cracks overlay on WFOV display
        /// </summary>
        private void wfovOverlayPaint(object sender, ICImagingControl.OverlayUpdateEventArgs e)
        {
            Graphics g = e.overlay.GetGraphics();
            //this.g.Clear(Color.Transparent);
            g.ResetTransform(); // Reset transform to draw NFOV image
            // Draw crosshair
            if (this.checkCrosshair.Checked)
            {
                float size = g.ClipBounds.Height / 20.0f;
                float x = g.ClipBounds.Width / 2.0f;
                float y = g.ClipBounds.Height / 2.0f;
                Pen pen = new Pen(Color.FromArgb(128, Color.Black));
                g.DrawLine(pen, x - size, y, x + size, y);
                g.DrawLine(pen, x, y - size, x, y + size);
            }

            // Now transform to world coordinates

            // Move image center to origin and rotate
            RectangleF bounds = g.VisibleClipBounds;
            g.TranslateTransform(bounds.Width / 2, bounds.Height / 2);
            g.RotateTransform((float)rccm.FineStageAngle);
            g.TranslateTransform(-bounds.Width / 2, -bounds.Height / 2);
            // Scale coordinate system by pixel to mm scaling
            float scaleX = bounds.Width / (float)this.camera.Width;
            float scaleY = bounds.Height / (float)this.camera.Height;
            g.ScaleTransform(scaleX, scaleY);
            // Move to NFOV location (first move origin to image center)
            g.TranslateTransform((float)this.camera.Width / 2, (float)this.camera.Height / 2);
            PointF pos = this.rccm.getNFOVLocation(this.stage);
            g.TranslateTransform(-pos.X, -pos.Y);

            // Draw each crack on the image
            foreach (MeasurementSequence crack in cracks)
            {
                crack.Plot(g, scaleX);
            }
            // Highlight selected point
            if (this.pointIndexValid())
            {
                MeasurementSequence crack = this.cracks[this.ActiveIndex];
                Measurement m = crack.GetPoint(this.ActivePoint);
                RectangleF point = new RectangleF(0, 0, 10.0f * crack.LineSize / scaleX, 10.0f * crack.LineSize / scaleX);
                point.X = (float)m.X - point.Width / 2.0f;
                point.Y = (float)m.Y - point.Height / 2.0f;
                g.FillEllipse(new SolidBrush(crack.Color), point);
            }
        }

        private void btnWfovStart_Click(object sender, EventArgs e)
        {
            this.camera.start();

            // Update button states
            if (this.wfovContainer.DeviceValid)
            {
                this.enableWfovControls();
                this.btnWfovStop.Enabled = true;
            }
        }

        private void btnWfovStop_Click(object sender, EventArgs e)
        {
            this.camera.stop();

            // Update button states
            if (!this.wfovContainer.LiveVideoRunning)
            {
                this.disableWfovControls();
                this.btnWfovStart.Enabled = true;
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

        private void listMeasurements_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ActiveIndex = this.listMeasurements.SelectedIndex;
            this.ActivePoint = -1;
        }

        private void btnEditSequence_Click(object sender, EventArgs e)
        {
            if (this.crackIndexValid())
            {
                MeasurementSequence crack = this.cracks[this.ActiveIndex];
                NewMeasurementForm form = new NewMeasurementForm(crack);
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    crack.Name = form.GetName();
                    crack.Color = Color.FromArgb(128, form.GetColor());
                    crack.LineSize = form.GetLineSize();
                    crack.Orientation = form.GetOrientation();
                    crack.Parent = form.GetStage();
                    this.updateMeasurementControls();
                }
            }
        }

        private void btnNewSequence_Click(object sender, EventArgs e)
        {
            NewMeasurementForm dlg = new NewMeasurementForm("Crack " + this.measurementCounter);
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                MeasurementSequence newCrack = new MeasurementSequence(dlg);
                this.measurementCounter++;
                this.cracks.Add(newCrack);

                this.listMeasurements.Items.Add(newCrack.Name);
                this.listMeasurements.SelectedIndex = this.cracks.Count - 1;
            }
            dlg.Dispose();
        }

        private void btnDeleteSequence_Click(object sender, EventArgs e)
        {
            if (this.crackIndexValid())
            {
                this.cracks.RemoveAt(this.ActiveIndex);
                this.listMeasurements.Items.RemoveAt(this.ActiveIndex);
                this.listMeasurements.SelectedIndex = -1;
                updateMeasurementControls();
            }
        }

        private void btnSaveCrack_Click(object sender, EventArgs e)
        {
            if (this.crackIndexValid())
            {
                this.saveFileDialog.FileName = this.cracks[this.ActiveIndex].GetFileName();
                DialogResult result = this.saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.cracks[this.ActiveIndex].WriteToFile(this.saveFileDialog.FileName, false);
                }
            }
        }

        private void btnCrosshairMeasure_Click(object sender, EventArgs e)
        {
            if (this.crackIndexValid())
            {
                Measurement pt = new Measurement(this.rccm, RCCMStage.RCCM1, 0, 0);
                this.cracks[this.ActiveIndex].AddPoint(pt);
                Logger.Out(this.cracks[this.ActiveIndex].ToString());
                // Refresh list of points
                this.updateMeasurementControls();
            }
        }

        private void btnGotoPoint_Click(object sender, EventArgs e)
        {

        }

        private void btnDeletePoint_Click(object sender, EventArgs e)
        {
            if (this.pointIndexValid())
            {
                this.cracks[this.ActiveIndex].removePoint(this.ActivePoint);
                this.listPoints.SelectedIndex = -1;
                // Refresh list of points
                this.updateMeasurementControls();
            }
        }

        private void listPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ActivePoint = this.listPoints.SelectedIndex;
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

        private void updateMeasurementControls()
        {
            // Update list of cracks
            int ind = this.ActiveIndex;
            this.listMeasurements.Items.Clear();
            for (int i = 0; i < this.cracks.Count; i++)
            {
                this.listMeasurements.Items.Add(this.cracks[i].Name);
            }
            this.listMeasurements.SelectedIndex = ind;
            // Update list of points
            if (this.crackIndexValid())
            {
                this.listPoints.Items.Clear();
                for (int i = 0; i < this.cracks[this.ActiveIndex].CountPoints; i++)
                {
                    Measurement m = this.cracks[this.ActiveIndex].GetPoint(i);
                    this.listPoints.Items.Add(string.Format("{0:0.000} {1:0.000}", m.X, m.Y));
                }
            }
        }

        /// <summary>
        /// Ensure that selected crack index is a valid array index of cracks
        /// </summary>
        private bool crackIndexValid()
        {
            return this.ActiveIndex >= 0 && this.ActiveIndex < this.cracks.Count;
        }

        /// <summary>
        /// Ensure that selected point index is a valid array index of active crack points
        /// </summary>
        private bool pointIndexValid()
        {
            return this.crackIndexValid() && this.ActivePoint >= 0 && this.ActivePoint < this.cracks[this.ActiveIndex].CountPoints;
        }
    }
}
