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
using System.Collections.ObjectModel;

namespace RCCM.UI
{
    public partial class RCCMMainForm : Form
    {
        protected RCCMSystem rccm;

        protected NFOV nfov1;
        protected NFOV nfov2;
        protected WFOV wfov1;
        
        protected Timer panelRepaintTimer;

        // List of measurement objects and counter for default naming convention
        protected ObservableCollection<MeasurementSequence> cracks;
        protected int measurementCounter = 0;

        // Flag to indicate if NFOV camera is recording video
        protected bool recording = false;
        protected bool wfov1Recording = false;

        protected RCCMStage activeStage;

        protected TestResults test;
        
        protected PanelView view;

        protected ComponentResourceManager resources;
        protected AxTrioPCLib.AxTrioPC triopc;

        public RCCMMainForm()
        {
            // Need to load the ActiveX state or something, I'm not actually sure
            this.resources = new ComponentResourceManager(typeof(RCCMMainForm));
            // Create Trio controller ActiveX control and initialize
            this.triopc = new AxTrioPCLib.AxTrioPC();
            ((ISupportInitialize)(this.triopc)).BeginInit();
            this.Controls.Add(this.triopc);
            this.triopc.Name = "AxTrioPC1";
            this.triopc.Visible = false;
            this.triopc.OcxState = ((AxHost.State)(resources.GetObject("AxTrioPC1.OcxState")));
            ((ISupportInitialize)(this.triopc)).EndInit();

            this.rccm = new RCCMSystem();

            InitializeComponent();
            
            this.applyUISettings(Program.Settings);
            
            // Create controller for handling motion commands
            this.rccm.initializeMotion(this.triopc);
            this.triopc.Refresh();
            
            this.nfov1 = this.rccm.NFOV1;
            this.nfov2 = this.rccm.NFOV2;
            this.wfov1 = new WFOV(this.wfovContainer, this.wfov1Config.Text);

            this.activeStage = RCCMStage.RCCM1;
            
            this.panelRepaintTimer = new Timer();
            this.panelRepaintTimer.Enabled = true;
            this.panelRepaintTimer.Interval = (int)Program.Settings.json["repaint period"];
            this.panelRepaintTimer.Tick += new EventHandler(refreshPanelView);

            this.cracks = new ObservableCollection<MeasurementSequence>();

            this.test = new TestResults(this.rccm, this.cracks, this.chartCracks, this.chartCycles, this.textCycle, this.textPressure, this.listCrackSelection);

            this.view = new PanelView(this.rccm);
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

            this.view.setTransform(this.panelView.CreateGraphics());
            
            this.panelRepaintTimer.Start();
        }
        
        private void RCCMMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.nfov1.disconnect();

            if (this.wfov1Recording)
            {
                this.wfov1.stopRecord();
            }

            Logger.Save();
            Program.Settings.save();
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

        private void coarseXPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("coarse X", (double)coarseXPos.Value);
            }
        }

        private void coarseYPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("coarse Y", (double)coarseYPos.Value);
            }
        }

        private void fine1XPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 1 X", (double)fine1XPos.Value);
            }
        }

        private void fine1YPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 1 Y", (double)fine1YPos.Value);
            }
        }

        private void fine1ZPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 1 Z", (double)fine1ZPos.Value);
            }
        }

        private void fine2XPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 2 X", (double)fine2XPos.Value);
            }
        }

        private void fine2YPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 2 Y", (double)fine2YPos.Value);
            }
        }

        private void fine2ZPos_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 2 Z", (double)fine2ZPos.Value);
            }
        }

        private void moveStage(string axis, double value)
        {
            if (this.radioMoveAbs.Checked)
            {
                this.rccm.setPosition(axis, value);
            }
            else if (this.radioMoveRel.Checked)
            {
                this.rccm.moveRelative(axis, value);
            }
        }
        
        private void btnMotorStatus_Click(object sender, EventArgs e)
        {
            var properties = this.rccm.getAxisStatus("fine 1 X");
            MessageBox.Show(string.Join("\n", properties));
        }

        #endregion
        
        private void nfov1Scale_TextChanged(object sender, EventArgs e)
        {
            double scale;
            if (this.nfov1 != null && Double.TryParse(this.nfov1Scale.Text, out scale))
            {
                this.nfov1.Scale = scale;
            }
        }

        private void nfov2Scale_TextChanged(object sender, EventArgs e)
        {
            double scale;
            if (this.nfov2 != null && Double.TryParse(this.nfov1Scale.Text, out scale))
            {
                this.nfov2.Scale = scale;
            }
        }

        #region Settings

        public void applyUISettings(Settings settings)
        {
            this.nfov1Scale.Text = (string)settings.json["nfov 1"]["microns / pixel"];
            this.nfov2Scale.Text = (string)settings.json["nfov 2"]["microns / pixel"];
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
                    this.rccm.moveRelative(yAxis, 0.1);
                    break;
                case Keys.A:
                    this.rccm.moveRelative(xAxis, -0.1);
                    break;
                case Keys.S:
                    this.rccm.moveRelative(yAxis, -0.1);
                    break;
                case Keys.D:
                    this.rccm.moveRelative(xAxis, 0.1);
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

        private void listCracksSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.test.plotCracks();
        }

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            this.rccm.Counter.start();
            this.btnStartTest.Enabled = false;
            this.btnPauseTest.Enabled = true;
            this.btnStopTest.Enabled = true;
        }

        private void btnPauseTest_Click(object sender, EventArgs e)
        {
            this.rccm.Counter.stop();
            this.btnStartTest.Enabled = true;
            this.btnPauseTest.Enabled = false;
            this.btnStopTest.Enabled = false;
        }

        private void btnStopTest_Click(object sender, EventArgs e)
        {
            this.rccm.Counter.stop();
            this.btnStartTest.Enabled = true;
            this.btnPauseTest.Enabled = false;
            this.btnStopTest.Enabled = false;
            foreach (MeasurementSequence crack in this.cracks)
            {
                crack.WriteToFile(this.textDataDir.Text, true);
            }
        }

        private void editCycleFreq_Click(object sender, EventArgs e)
        {
            this.rccm.Counter.Period = (int)(2000.0 * Math.PI * (double)this.editCycleFreq.Value);
        }

        #endregion

        private void panelView_Paint(object sender, PaintEventArgs e)
        {
            this.view.paint(e.Graphics);
        }

        private void refreshPanelView(object sender, EventArgs e)
        {
            this.panelView.Invalidate();
            this.coarseXIndicator.Text = this.rccm.getPosition("coarse X").ToString();
            this.coarseYIndicator.Text = this.rccm.getPosition("coarse Y").ToString();
            this.fine1XIndicator.Text = this.rccm.getPosition("fine 1 X").ToString();
            this.fine1YIndicator.Text = this.rccm.getPosition("fine 1 Y").ToString();
            this.fine1ZIndicator.Text = this.rccm.getPosition("fine 1 Z").ToString();
            this.fine2XIndicator.Text = this.rccm.getPosition("fine 2 X").ToString();
            this.fine2YIndicator.Text = this.rccm.getPosition("fine 2 Y").ToString();
            this.fine2ZIndicator.Text = this.rccm.getPosition("fine 2 Z").ToString();
        }

        #region Menu Items

        private void nFOV1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LensCalibrationForm form = new LensCalibrationForm(rccm.LensController, RCCMStage.RCCM1, Program.Settings);
            form.Show();
        }

        private void nFOV2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LensCalibrationForm form = new LensCalibrationForm(rccm.LensController, RCCMStage.RCCM2, Program.Settings);
            form.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutRCCMForm form = new AboutRCCMForm();
            form.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            NFOVViewForm form = new NFOVViewForm(this.rccm, Program.Settings, this.rccm.NFOV1, this.cracks);
            form.Show();
        }
    }
}
