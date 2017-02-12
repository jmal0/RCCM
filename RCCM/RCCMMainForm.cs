using System;
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
        protected Timer panelRepaintTimer;

        // List of measurement objects and counter for default naming convention
        protected List<MeasurementSequence> cracks;
        protected int measurementCounter = 0;

        // Flag to indicate if NFOV camera is recording video
        protected bool recording = false;
        protected bool wfov1Recording = false;

        protected RCCMStage activeStage;

        protected bool drawing;
        protected Point drawnLineStart;
        protected Point drawnLineEnd;
        static double NFOVIMAGE_SCALE = 0.25;

        protected TestResults test;
        
        protected PanelView view;
        protected NFOVView nfovView;

        public RCCMMainForm(RCCMSystem sys, Settings settings)
        {
            InitializeComponent();

            this.settings = settings;
            this.applyUISettings(this.settings);

            this.rccm = sys;

            this.nfov1 = this.rccm.NFOV1;
            this.wfov1 = new WFOV(this.wfovContainer, this.wfov1Config.Text);

            this.activeStage = RCCMStage.RCCM1;

            this.nfovRepaintTimer = new Timer();
            this.nfovRepaintTimer.Enabled = true;
            this.nfovRepaintTimer.Interval = (int) this.settings.json["repaint period"];
            this.nfovRepaintTimer.Tick += new EventHandler(refreshNfov);

            this.panelRepaintTimer = new Timer();
            this.nfovRepaintTimer.Enabled = true;
            this.nfovRepaintTimer.Interval = (int)this.settings.json["repaint period"];
            this.nfovRepaintTimer.Tick += new EventHandler(refreshPanelView);

            this.cracks = new List<MeasurementSequence>();

            this.drawnLineStart = new Point(0, 0);
            this.drawnLineEnd = new Point(0, 0);

            this.test = new TestResults(this.chartCracks, this.chartCycles);

            this.view = new PanelView(this.rccm, settings);
            this.nfovView = new NFOVView(this.rccm, this.cracks);

            Show();
        }
        
        private void RCCMMainForm_Load(object sender, EventArgs e)
        {
            if (this.wfov1.Available)
            {
                this.wfov1.initialize();
                btnWfovStart.Enabled = true;

                //  Setup the range of the zoom and focus sliders.
                sliderZoom.Minimum = this.wfov1.ZoomMin;
                sliderZoom.Maximum = this.wfov1.ZoomMax;
                sliderFocus.Minimum = this.wfov1.FocusMin;
                sliderFocus.Maximum = this.wfov1.FocusMax;

                //  Set the sliders to the current zoom and focus values.
                sliderZoom.Value = this.wfov1.Zoom;
                textZoom.Text = this.wfov1.Zoom.ToString();
                sliderFocus.Value = this.wfov1.Focus;
                textFocus.Text = this.wfov1.Focus.ToString();
            }

            this.nfov1.initialize();

            this.nfovRepaintTimer.Start();
            this.panelRepaintTimer.Start();

            this.view.setTransform(this.panelView.CreateGraphics());
        }
        
        private void RCCMMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.nfovRepaintTimer.Stop();
            this.nfov1.disconnect();

            if (this.wfov1Recording)
            {
                this.wfov1.stopRecord();
            }
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
            string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt-fff}", DateTime.Now);
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
                if (this.wfov1Recording == false)
                {
                    string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt-fff}", DateTime.Now);
                    this.wfov1.record(this.textVideoDir.Text + "\\" + timestamp + ".avi");
                    this.wfov1Recording = true;
                    btnWfovRecord.BackColor = Color.Gray;
                    btnWfovStart.Enabled = false;
                    btnWfovSnap.Enabled = false;
                }
                else
                {
                    this.wfov1.stopRecord();
                    this.wfov1Recording = false;
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
            int newFocus = this.wfov1.autoFocus();
            this.sliderFocus.Value = newFocus;
            this.textFocus.Text = newFocus.ToString();
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
            string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt-fff}", DateTime.Now);
            Console.WriteLine(textImageDir.Text + "\\" + timestamp + ".bmp");
            this.nfov1.snap(textImageDir.Text + "\\"+ timestamp + ".bmp");
        }

        private void btnNfovRecord_Click(object sender, EventArgs e)
        {
            if (this.nfov1.Recording)
            {
                // Stop recording
                this.nfov1.Recording = false;
                btnNfovRecord.BackColor = Color.Transparent;
                System.Windows.Forms.MessageBox.Show("Recording stopped");
                btnNfovStart.Enabled = true;
                btnNfovStop.Enabled = true;
                btnNfovSnap.Enabled = true;

            }
            else
            {
                // Start recording
                string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt-fff}", DateTime.Now);
                //this.nfov1.record(this.textVideoDir + "\\" + timestamp + ".avi");
                this.nfov1.Recording = true;
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
                this.nfovView.createDrawnLine(e.X, e.Y, this.nfovImage.Width, this.nfovImage.Height);
            }
        }

        private void nfovImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.nfovView.Drawing)
            {
                // Move end of line to mouse location
                this.nfovView.moveDrawnLineEnd(e.X, e.Y, this.nfovImage.Width, this.nfovImage.Height);
            }
        }

        private void nfovImage_MouseUp(object sender, MouseEventArgs e)
        {
            int index = this.listMeasurements.SelectedIndex;
            if (nfovView.Drawing)
            {
                nfovView.createSegment();
            }
            // Refresh list of points
            this.updateMeasurementControls(this.listMeasurements.SelectedIndex);
        }

        private void nfovImage_Paint(object sender, PaintEventArgs e)
        {
            this.nfovView.paint(e.Graphics);           
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

                this.listPoints.Items.Clear();
                for (int i = 0; i < this.cracks[measurementIndex].CountPoints; i++)
                {
                    Measurement m = this.cracks[measurementIndex].getPoint(i);
                    this.listPoints.Items.Add(string.Format("{0:0.0000, -10} {1:0.0000, -10}", m.X, m.Y));
                }
            }
        }

        private void listMeasurements_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.nfovView.ActiveIndex = this.listMeasurements.SelectedIndex;
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
            this.test.plotCracks(this.cracks);
        }
        
        private void btnCrosshairMeasure_Click(object sender, EventArgs e)
        {
            int index = this.listMeasurements.SelectedIndex;
            if (index >= 0)
            {
                Measurement pt = new Measurement(this.rccm, RCCMStage.RCCM1, 0, 0);
                this.cracks[index].addPoint(pt);
                Console.WriteLine(this.cracks[index]);
                // Refresh list of points
                this.updateMeasurementControls(index);
            }
        }

        private void btnDeletePoint_Click(object sender, EventArgs e)
        {
            int mIndex = this.listMeasurements.SelectedIndex;
            int ptIndex = this.listPoints.SelectedIndex;
            if (mIndex >= 0 && ptIndex >= 0)
            {
                this.cracks[mIndex].removePoint(ptIndex);
                // Refresh list of points
                this.updateMeasurementControls(mIndex);
                
                this.test.plotCracks(this.cracks);
            }
        }

        private void btnSaveCrack_Click(object sender, EventArgs e)
        {
            int index = this.listMeasurements.SelectedIndex;
            if (index >= 0)
            {
                this.cracks[index].writeToFile(this.textDataDir.Text);
            }
        }

        private void btnSaveAllCracks_Click(object sender, EventArgs e)
        {
            foreach (MeasurementSequence crack in this.cracks)
            {
                crack.writeToFile(this.textDataDir.Text);
            }
        }

        #endregion

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

        #region Fatigue Testing

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            this.rccm.startCounting();
            this.btnStartTest.Enabled = false;
            this.btnPauseTest.Enabled = true;
            this.btnStopTest.Enabled = true;
        }

        #endregion

        private void btnPauseTest_Click(object sender, EventArgs e)
        {
            this.rccm.stopCounting();
            this.btnStartTest.Enabled = true;
            this.btnPauseTest.Enabled = false;
            this.btnStopTest.Enabled = false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.setCycleFrequency((double) this.numericUpDown1.Value);
        }

        private void panelView_Paint(object sender, PaintEventArgs e)
        {
            this.view.paint(e.Graphics);
        }

        private void refreshPanelView(object sender, EventArgs e)
        {
            this.panelView.Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nFOV1LensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LensCalibrationForm form = new LensCalibrationForm(rccm.LensController, RCCMStage.RCCM1);
            form.Show();
        }
    }
}
