﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// WFOV Imports
using TIS.Imaging;

// NFOV Imports
using FlyCapture2Managed;
using FlyCapture2Managed.Gui;

namespace RCCM
{
    public partial class RCCMMainForm : Form
    {
        protected RCCMSystem rccm;
        protected Settings settings;

        protected NFOV nfov1;
        protected WFOV wfov1;

        protected Timer nfovRepaintTimer;

        // List of measurement objects and counter for default naming convention
        protected List<MeasurementSequence> cracks;
        protected int measurementCounter = 0;

        // Flag to indicate if NFOV camera is recording video
        protected bool recording = false;
        
        protected RCCMStage activeStage;

        protected bool drawing;
        protected Point drawnLineStart;
        protected Point drawnLineEnd;

        public RCCMMainForm(RCCMSystem sys, Settings set)
        {
            InitializeComponent();

            this.settings = set;
            this.applyUISettings(this.settings);

            this.rccm = sys;

            this.nfov1 = this.rccm.NFOV1;
            this.wfov1 = new WFOV(this.wfovContainer, this.wfov1Config.Text);
            
            this.nfovRepaintTimer = new Timer();

            this.activeStage = RCCMStage.RCCM1;

            this.nfovRepaintTimer.Enabled = true;
            this.nfovRepaintTimer.Interval = (int) this.settings.json["repaint period"];
            this.nfovRepaintTimer.Tick += new EventHandler(refreshNfov);

            this.cracks = new List<MeasurementSequence>();

            this.drawnLineStart = new Point(0, 0);
            this.drawnLineEnd = new Point(0, 0);

            Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (this.wfov1.isAvailable())
            {
                this.wfov1.initialize();
                btnWfovStart.Enabled = true;

                //  Setup the range of the zoom and focus sliders.
                sliderZoom.Minimum = this.wfov1.getPropertyMin(VCDIDs.VCDID_Zoom);
                sliderZoom.Maximum = this.wfov1.getPropertyMax(VCDIDs.VCDID_Zoom);
                sliderFocus.Minimum = this.wfov1.getPropertyMin(VCDIDs.VCDID_Focus);
                sliderFocus.Maximum = this.wfov1.getPropertyMax(VCDIDs.VCDID_Focus);

                //  Set the sliders to the current zoom and focus values.
                sliderZoom.Value = this.wfov1.getPropertyValue(VCDIDs.VCDID_Zoom);
                textZoom.Text = this.wfov1.getPropertyValue(VCDIDs.VCDID_Zoom).ToString();
                sliderFocus.Value = this.wfov1.getPropertyValue(VCDIDs.VCDID_Focus);
                textFocus.Text = this.wfov1.getPropertyValue(VCDIDs.VCDID_Focus).ToString();
            }


            CameraSelectionDialog camSlnDlg = new CameraSelectionDialog();
            bool retVal = camSlnDlg.ShowModal();
            if (retVal)
            {
                try
                {
                    bool success = this.nfov1.initialize(camSlnDlg.GetSelectedCameraGuids());
                    if (!success)
                    {
                        MessageBox.Show("No camera selected. NFOV will be unavailable.");
                    }
                    else
                    {
                        btnNfovStart.Enabled = false;
                        btnNfovStop.Enabled = true;
                    }
                }
                catch (FC2Exception ex)
                {
                    Console.WriteLine("Failed to load form successfully: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No camera selected. NFOV will be unavailable.");
                disableNfovControls();
            }

            this.nfovRepaintTimer.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.nfovRepaintTimer.Stop();
            this.nfov1.disconnect();
        }

        #region WFOV

        private void btnWfovStart_Click(object sender, EventArgs e)
        {
            this.wfov1.start();

            // Update button states
            if (wfovContainer.DeviceValid)
            {
                enableWfovControls();
                btnWfovStop.Enabled = true;
            }
        }

        private void btnWfovStop_Click(object sender, EventArgs e)
        {
            this.wfov1.stop();

            // Update button states
            if (!wfovContainer.LiveVideoRunning)
            {
                disableWfovControls();
                btnWfovStart.Enabled = true;
            }
        }
        
        private void btnWfovSnap_Click(object sender, EventArgs e)
        {
            string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt.fff}", DateTime.Now);
            this.wfov1.snapImage(textImageDir.Text + "\\" + timestamp + ".png");
        }

        private void btnProperties_Click(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                this.wfov1.editProperties();
            }
        }
        
        private void btnWfovRecord_Click(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                if (this.recording == false)
                {
                    string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt.fff}", DateTime.Now);
                    this.wfov1.record(this.textVideoDir + "\\" + timestamp + ".avi");
                    btnWfovRecord.BackColor = Color.Gray;
                    btnWfovStart.Enabled = false;
                    btnWfovSnap.Enabled = false;
                }
                else
                {
                    this.wfov1.stopRecord();
                    MessageBox.Show("Recording stopped");

                    btnWfovRecord.BackColor = Color.Transparent;                    
                    this.recording = false;
                    btnWfovStart.Enabled = true;
                    btnWfovSnap.Enabled = true;
                }
            }
        }

        private void btnFocus_Click(object sender, EventArgs e)
        {
            btnFocus.Enabled = false;
            this.wfov1.autoFocus();
            //sliderFocus.Value = VCDProp.RangeValue[VCDIDs.VCDID_Zoom];
            //textFocus.Text = sliderFocus.Value.ToString();
            btnFocus.Enabled = true;
        }

        private void sliderZoom_Scroll(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                this.wfov1.Zoom = sliderZoom.Value;
                textZoom.Text = sliderZoom.Value.ToString();
            }
        }

        private void sliderFocus_Scroll(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                this.wfov1.Focus = sliderFocus.Value;
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

        #endregion

        #region Motion

        private void coarseXPos_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.setPosition("coarse X", (double) coarseXPos.Value);
        }

        private void coarseYPos_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.setPosition("coarse Y", (double) coarseYPos.Value);
        }

        private void fine1XPos_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.setPosition("fine 1 X", (double) fine1XPos.Value);
        }

        private void fine1YPos_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.setPosition("fine 1 X", (double) fine1YPos.Value);
        }

        private void fine1ZPos_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.setPosition("fine 1 X", (double) fine1ZPos.Value);
        }

        private void fine2XPos_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.setPosition("fine 2 X", (double) fine2XPos.Value);
        }

        private void fine2YPos_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.setPosition("fine 2 Y", (double) fine2YPos.Value);
        }

        private void fine2ZPos_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.setPosition("fine 2 Z", (double) fine2ZPos.Value);
        }

        #endregion

        #region NFOV

        private void btnNfovStart_Click(object sender, EventArgs e)
        {
            this.nfov1.start();
            this.nfovRepaintTimer.Start();

            btnNfovStart.Enabled = false;
            btnNfovStop.Enabled = true;
            btnNfovSnap.Enabled = true;
            btnNfovRecord.Enabled = true;
        }

        private void btnNfovStop_Click(object sender, EventArgs e)
        {
            this.nfov1.stop();
            this.nfovRepaintTimer.Stop();

            btnNfovStart.Enabled = true;
            btnNfovStop.Enabled = false;
            btnNfovSnap.Enabled = false;
            btnNfovRecord.Enabled = false;
        }

        private void refreshNfov(object sender, EventArgs e)
        {
            this.nfovImage.Invalidate();
        }

        private void btnNfovProperties_Click(object sender, EventArgs e)
        {
            this.nfov1.showPropertiesDlg();
        }

        private void btnNfovSnap_Click(object sender, EventArgs e)
        {
            string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt.fff}", DateTime.Now);
            Console.WriteLine(textImageDir.Text + "\\" + timestamp + ".bmp");
            this.nfov1.snap(textImageDir.Text + "\\"+ timestamp + ".bmp");
        }

        private void btnNfovRecord_Click(object sender, EventArgs e)
        {
            if (this.nfov1.isRecording())
            {
                // Stop recording
                this.nfov1.setRecord(false);
                btnNfovRecord.BackColor = Color.Transparent;
                System.Windows.Forms.MessageBox.Show("Recording stopped");
                btnNfovStart.Enabled = true;
                btnNfovStop.Enabled = true;
                btnNfovSnap.Enabled = true;

            }
            else
            {
                // Start recording
                string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt.fff}", DateTime.Now);
                this.nfov1.record(this.textVideoDir + "\\" + timestamp + ".avi");
                btnNfovRecord.BackColor = Color.Gray;
                btnNfovStart.Enabled = false;
                btnNfovStop.Enabled = false;
                btnNfovSnap.Enabled = false;
            }
        }

        private void enableNfovControls()
        {
            btnNfovStart.Enabled = true;
            btnNfovStop.Enabled = true;
            btnNfovSnap.Enabled = true;
            btnNfovRecord.Enabled = true;
            btnNfovProperties.Enabled = true;
        }

        private void disableNfovControls()
        {
            btnNfovStart.Enabled = false;
            btnNfovStop.Enabled = false;
            btnNfovSnap.Enabled = false;
            btnNfovRecord.Enabled = false;
            btnNfovProperties.Enabled = false;
        }

        #endregion

        #region Measurement

        private void nfovImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                // Check that a crack is selected
                int index = this.listMeasurements.SelectedIndex;
                if (index >= 0)
                {
                    this.drawing = true;

                    // Move user-drawn line endpoint to mouse location
                    this.drawnLineEnd.X = e.X;
                    this.drawnLineEnd.Y = e.Y;

                    // If crack has at least one point, connect a new point to it
                    if (this.cracks[index].CountPoints > 0)
                    {
                        // Get location info from NFOV
                        PointF location = this.rccm.getNFOV1Location();
                        float scale = (float)this.nfov1.Scale;
                        Point imgCenter = new Point(this.nfovImage.Width / 2, this.nfovImage.Height / 2);

                        Measurement lastPt = this.cracks[index].getLastPoint();
                        Point pt = lastPt.toPoint(location, scale, imgCenter);
                        this.drawnLineStart.X = pt.X;
                        this.drawnLineStart.Y = pt.Y;
                    }
                    // If no point in crack, move first point to mouse location
                    else
                    {
                        this.drawnLineStart.X = e.X;
                        this.drawnLineStart.Y = e.Y;                        
                    }
                }
            }                
        }

        private void nfovImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.drawing)
            {
                // Create point at mouse location
                this.drawnLineEnd.X = e.X;
                this.drawnLineEnd.Y = e.Y;
            }
        }

        private void nfovImage_MouseUp(object sender, MouseEventArgs e)
        {
            int index = this.listMeasurements.SelectedIndex;
            if (this.drawing && index >= 0)
            {
                // Get location info from NFOV
                PointF location = this.rccm.getNFOV1Location();
                float scale = (float)this.nfov1.Scale;
                Point imgCenter = new Point(this.nfovImage.Width / 2, this.nfovImage.Height / 2);

                // Add measurements for start and end if user is drawing both 
                if (this.cracks[index].CountPoints == 0)
                {
                    double p0x = (this.drawnLineStart.X - imgCenter.X) / this.nfov1.Scale;
                    double p0y = -(this.drawnLineStart.Y - imgCenter.Y) / this.nfov1.Scale;
                    Measurement p0 = new Measurement(this.rccm, RCCMStage.RCCM1, p0x, p0y);
                    this.cracks[index].addPoint(p0);
                }

                double p1x = (this.drawnLineEnd.X - imgCenter.X) / this.nfov1.Scale;
                double p1y = -(this.drawnLineEnd.Y - imgCenter.Y) / this.nfov1.Scale;
                Measurement p1 = new Measurement(this.rccm, RCCMStage.RCCM1, p1x, p1y);
                this.cracks[index].addPoint(p1);
            }
            this.drawing = false;
        }

        private void nfovImage_Paint(object sender, PaintEventArgs e)
        {
            Bitmap img = this.nfov1.getLiveImage();
            if (img != null)
            {
                e.Graphics.DrawImage(img, 0, 0);
            }

            // Get distance unit limits for crack overlay
            PointF location = this.rccm.getNFOV1Location();
            double scale = this.nfov1.Scale;
            Point imgCenter = new Point(this.nfovImage.Width / 2, this.nfovImage.Height / 2);
            
            // Draw each crack on the image
            foreach (MeasurementSequence crack in this.cracks)
            {
                crack.plot(e.Graphics, location, scale, imgCenter);
            }

            // Draw segment that user is creating with mouse
            int index = this.listMeasurements.SelectedIndex;
            if (index >= 0 && this.drawing)
            {
                Color c = this.cracks[index].Color;
                e.Graphics.DrawLine(new Pen(c), this.drawnLineStart, this.drawnLineEnd);
            }            
        }

        private void colorPicker_Click(object sender, EventArgs e)
        {
            DialogResult result = this.colorDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Set measurement color
                this.colorPicker.BackColor = colorDlg.Color;

                // Set measurement color
                int index = this.listMeasurements.SelectedIndex;
                if (index >= 0)
                {
                    this.cracks[index].Color = colorDlg.Color;
                }
            }
        }

        private void btnNewSequence_Click(object sender, EventArgs e)
        {
            NewMeasurementSequenceForm dlg = new NewMeasurementSequenceForm("Crack " + this.measurementCounter);
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                MeasurementSequence newCrack = new MeasurementSequence(dlg.getColor(), dlg.getName(), dlg.getStage());
                this.measurementCounter++;
                this.cracks.Add(newCrack);
                
                this.listMeasurements.Items.Add(newCrack.Name);
                this.listMeasurements.SelectedIndex = this.cracks.Count - 1;
            }
            dlg.Dispose();
        }

        private void updateMeasurementControls(int measurementIndex)
        {
            if (measurementIndex >= 0 && measurementIndex < this.listMeasurements.Items.Count)
            {
                this.colorPicker.BackColor = this.cracks[measurementIndex].Color;
                this.textLineName.Text = this.cracks[measurementIndex].Name;
            }
        }

        private void listMeasurements_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateMeasurementControls(this.listMeasurements.SelectedIndex);
        }

        private void btnDeleteSequence_Click(object sender, EventArgs e)
        {
            int deleteIndex = this.listMeasurements.SelectedIndex;
            if (deleteIndex >= 0 && deleteIndex < this.listMeasurements.Items.Count)
            {
                this.cracks.RemoveAt(deleteIndex);
                this.listMeasurements.Items.RemoveAt(deleteIndex);
                updateMeasurementControls(-1);
            }
        }

        #endregion

        private void btnCrosshairMeasure_Click(object sender, EventArgs e)
        {
            int index = this.listMeasurements.SelectedIndex;
            if (index >= 0)
            {
                Measurement pt = new Measurement(this.rccm, RCCMStage.RCCM1, 0, 0);
                this.cracks[index].addPoint(pt);
                Console.WriteLine(this.cracks[index]);
            }
        }

        private void nfov1Scale_TextChanged(object sender, EventArgs e)
        {
            this.nfov1.Scale = Double.Parse(nfov1Scale.Text);
        }

        #region Settings

        public void applyUISettings(Settings settings)
        {
            // Make NumericUpDown increment property equal to motor minimum step size
            this.coarseXPos.Increment = (decimal) settings.json["coarse X"]["step"];
            this.coarseYPos.Increment = (decimal) settings.json["coarse Y"]["step"];
            this.fine1XPos.Increment = (decimal) settings.json["fine 1 X"]["step"];
            this.fine1YPos.Increment = (decimal) settings.json["fine 1 Y"]["step"];
            this.fine1ZPos.Increment = (decimal) settings.json["fine 1 Z"]["step"];
            this.fine2XPos.Increment = (decimal) settings.json["fine 2 X"]["step"];
            this.fine2YPos.Increment = (decimal) settings.json["fine 2 Y"]["step"];
            this.fine2ZPos.Increment = (decimal) settings.json["fine 2 Z"]["step"];
            // Apply motor position limits to NumericUpDowns
            this.coarseXPos.Minimum = (decimal)settings.json["coarse X"]["low position limit"];
            this.coarseYPos.Minimum = (decimal)settings.json["coarse Y"]["low position limit"];
            this.fine1XPos.Minimum = (decimal)settings.json["fine 1 X"]["low position limit"];
            this.fine1YPos.Minimum = (decimal)settings.json["fine 1 Y"]["low position limit"];
            this.fine1ZPos.Minimum = (decimal)settings.json["fine 1 Z"]["low position limit"];
            this.fine2XPos.Minimum = (decimal)settings.json["fine 2 X"]["low position limit"];
            this.fine2YPos.Minimum = (decimal)settings.json["fine 2 Y"]["low position limit"];
            this.fine2ZPos.Minimum = (decimal)settings.json["fine 2 Z"]["low position limit"];
            this.coarseXPos.Maximum = (decimal)settings.json["coarse X"]["high position limit"];
            this.coarseYPos.Maximum = (decimal)settings.json["coarse Y"]["high position limit"];
            this.fine1XPos.Maximum = (decimal)settings.json["fine 1 X"]["high position limit"];
            this.fine1YPos.Maximum = (decimal)settings.json["fine 1 Y"]["high position limit"];
            this.fine1ZPos.Maximum = (decimal)settings.json["fine 1 Z"]["high position limit"];
            this.fine2XPos.Maximum = (decimal)settings.json["fine 2 X"]["high position limit"];
            this.fine2YPos.Maximum = (decimal)settings.json["fine 2 Y"]["high position limit"];
            this.fine2ZPos.Maximum = (decimal)settings.json["fine 2 Z"]["high position limit"];
        }
        #endregion

        private void RCCMMainForm_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        /// <summary>
        /// Handler for keyboard presses in form. Calls motion commands for WASD keys
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Only works when NFOV live display has mouse focus (so text entry is not interrupted)
            if (!this.tabControl1.Focused)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            // Get active axis
            string xAxis = this.activeStage == RCCMStage.RCCM1 ? "fine 1 X" : "fine 2 X";
            string yAxis = this.activeStage == RCCMStage.RCCM1 ? "fine 1 Y" : "fine 2 Y";

            // Get current stage position
            double xPos = this.rccm.getPosition(xAxis);
            double yPos = this.rccm.getPosition(yAxis);

            // Jog motors
            switch (keyData)
            {
                case Keys.W:
                    this.rccm.setPosition(yAxis, yPos + 0.1);
                    break;
                case Keys.A:
                    this.rccm.setPosition(xAxis, xPos - 0.1);
                    break;
                case Keys.S:
                    this.rccm.setPosition(yAxis, yPos - 0.1);
                    break;
                case Keys.D:
                    this.rccm.setPosition(xAxis, xPos + 0.1);
                    break;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.rccm.readHeight1();
        }
    }
}
