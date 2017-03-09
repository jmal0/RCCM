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
using System.IO;

namespace RCCM.UI
{
    public partial class RCCMMainForm : Form
    {
        protected RCCMSystem rccm;

        protected NFOV nfov1;
        protected NFOV nfov2;
        protected WFOV wfov1;
        protected WFOV wfov2;

        protected bool nfov1Open, nfov2Open, wfov1Open, wfov2Open;

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

            this.rccm = new RCCMSystem(this.triopc);

            InitializeComponent();
            
            this.applyUISettings();
            
            this.nfov1 = this.rccm.NFOV1;
            this.nfov2 = this.rccm.NFOV2;
            this.wfov1 = this.rccm.WFOV1;
            this.wfov2 = this.rccm.WFOV2;

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
            this.nfov1.initialize();

            this.view.setTransform(this.panelView.CreateGraphics());
            
            this.panelRepaintTimer.Start();
        }
        
        private void RCCMMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.nfov1.disconnect();

            Logger.Save();
            Program.Settings.save();
        }

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
                this.rccm.motors[axis].setPos(value);
            }
            else if (this.radioMoveRel.Checked)
            {
                this.rccm.motors[axis].moveRel(value);
            }
        }
        
        private void btnMotorStatus_Click(object sender, EventArgs e)
        {
            var properties = this.rccm.motors["fine 1 X"].getAllProperties();
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

        public void applyUISettings()
        {
            // Cameras
            this.nfov1Scale.Text = (string)Program.Settings.json["nfov 1"]["microns / pixel"];
            this.nfov2Scale.Text = (string)Program.Settings.json["nfov 2"]["microns / pixel"];
            this.wfov1Scale.Text = (string)Program.Settings.json["wfov 1"]["microns / pixel"];
            this.wfov2Scale.Text = (string)Program.Settings.json["wfov 2"]["microns / pixel"];
            // Make NumericUpDown increment property equal to motor minimum step size
            this.coarseXPos.Increment = (decimal) Program.Settings.json["coarse X"]["step"];
            this.coarseYPos.Increment = (decimal) Program.Settings.json["coarse Y"]["step"];
            this.fine1XPos.Increment = (decimal) Program.Settings.json["fine 1 X"]["step"];
            this.fine1YPos.Increment = (decimal) Program.Settings.json["fine 1 Y"]["step"];
            this.fine1ZPos.Increment = (decimal) Program.Settings.json["fine 1 Z"]["step"];
            this.fine2XPos.Increment = (decimal) Program.Settings.json["fine 2 X"]["step"];
            this.fine2YPos.Increment = (decimal) Program.Settings.json["fine 2 Y"]["step"];
            this.fine2ZPos.Increment = (decimal) Program.Settings.json["fine 2 Z"]["step"];
            // Apply motor position limits to NumericUpDowns
            this.coarseXPos.Minimum = (decimal)Program.Settings.json["coarse X"]["low position limit"];
            this.coarseYPos.Minimum = (decimal)Program.Settings.json["coarse Y"]["low position limit"];
            this.fine1XPos.Minimum = (decimal)Program.Settings.json["fine 1 X"]["low position limit"];
            this.fine1YPos.Minimum = (decimal)Program.Settings.json["fine 1 Y"]["low position limit"];
            this.fine1ZPos.Minimum = (decimal)Program.Settings.json["fine 1 Z"]["low position limit"];
            this.fine2XPos.Minimum = (decimal)Program.Settings.json["fine 2 X"]["low position limit"];
            this.fine2YPos.Minimum = (decimal)Program.Settings.json["fine 2 Y"]["low position limit"];
            this.fine2ZPos.Minimum = (decimal)Program.Settings.json["fine 2 Z"]["low position limit"];
            this.coarseXPos.Maximum = (decimal)Program.Settings.json["coarse X"]["high position limit"];
            this.coarseYPos.Maximum = (decimal)Program.Settings.json["coarse Y"]["high position limit"];
            this.fine1XPos.Maximum = (decimal)Program.Settings.json["fine 1 X"]["high position limit"];
            this.fine1YPos.Maximum = (decimal)Program.Settings.json["fine 1 Y"]["high position limit"];
            this.fine1ZPos.Maximum = (decimal)Program.Settings.json["fine 1 Z"]["high position limit"];
            this.fine2XPos.Maximum = (decimal)Program.Settings.json["fine 2 X"]["high position limit"];
            this.fine2YPos.Maximum = (decimal)Program.Settings.json["fine 2 Y"]["high position limit"];
            this.fine2ZPos.Maximum = (decimal)Program.Settings.json["fine 2 Z"]["high position limit"];
            // Directories
            this.textImageDir.Text = (string)Program.Settings.json["image directory"];
            this.textVideoDir.Text = (string)Program.Settings.json["video directory"];
            this.textDataDir.Text = (string)Program.Settings.json["test data directory"];
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
            double xPos = this.rccm.motors[xAxis].getPos();
            double yPos = this.rccm.motors[yAxis].getPos();

            // Jog motors
            switch (keyData)
            {
                case Keys.W:
                    this.rccm.motors[xAxis].moveRel(0.1);
                    break;
                case Keys.A:
                    this.rccm.motors[xAxis].moveRel(-0.1);
                    break;
                case Keys.S:
                    this.rccm.motors[yAxis].moveRel(-0.1);
                    break;
                case Keys.D:
                    this.rccm.motors[yAxis].moveRel(0.1);
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
            this.coarseXIndicator.Text = this.rccm.motors["coarse X"].getPos().ToString();
            this.coarseYIndicator.Text = this.rccm.motors["coarse Y"].getPos().ToString();
            this.fine1XIndicator.Text = this.rccm.motors["fine 1 X"].getPos().ToString();
            this.fine1YIndicator.Text = this.rccm.motors["fine 1 Y"].getPos().ToString();
            this.fine1ZIndicator.Text = this.rccm.motors["fine 1 Z"].getPos().ToString();
            this.fine2XIndicator.Text = this.rccm.motors["fine 2 X"].getPos().ToString();
            this.fine2YIndicator.Text = this.rccm.motors["fine 2 Y"].getPos().ToString();
            this.fine2ZIndicator.Text = this.rccm.motors["fine 2 Z"].getPos().ToString();
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

        private void btnNFOV1Open_Click(object sender, EventArgs e)
        {
            if (!this.nfov1Open)
            {
                this.nfov1Open = true;
                NFOVViewForm form = new NFOVViewForm(this.rccm, this.nfov1, this.cracks);
                form.Show();
                form.FormClosed += delegate (object sender2, FormClosedEventArgs e2) { this.nfov1Open = false; };
            }
        }

        private void btnNFOV2Open_Click(object sender, EventArgs e)
        {
            if (!this.nfov2Open)
            {
                this.nfov2Open = true;
                NFOVViewForm form = new NFOVViewForm(this.rccm, this.nfov2, this.cracks);
                form.Show();
                form.FormClosed += delegate (object sender2, FormClosedEventArgs e2) { this.nfov2Open = false; };
            }
        }

        private void btnWFOV1Open_Click(object sender, EventArgs e)
        {
            if (!this.wfov1Open)
            {
                this.wfov1Open = true;
                WFOVViewForm form = new WFOVViewForm(this.rccm, this.wfov1, this.cracks);
                form.Show();
                form.FormClosed += delegate (object sender2, FormClosedEventArgs e2) { this.wfov1Open = false; };
            }
        }

        private void btnWFOV2Open_Click(object sender, EventArgs e)
        {
            if (!this.wfov2Open)
            {
                this.wfov2Open = true;
                WFOVViewForm form = new WFOVViewForm(this.rccm, this.wfov2, this.cracks);
                form.Show();
                form.FormClosed += delegate (object sender2, FormClosedEventArgs e2) { this.wfov2Open = false; };
            }
        }
        
        private void lockCamera()
        {

        }

        private void RCCMMainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                // Do some stuff
                this.view.setTransform(this.panelView.CreateGraphics());
            }
        }

        private void tabPageMotion_Paint(object sender, PaintEventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                // Do some stuff
                this.view.setTransform(this.panelView.CreateGraphics());
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {

        }

        private void btnSetHome_Click(object sender, EventArgs e)
        {

        }

        private void btnMotorProperties_Click(object sender, EventArgs e)
        {
            MotorSettingsForm form = new MotorSettingsForm(this.rccm);
            form.Show();
        }

        private void editRotation_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.FineStageAngle = (double) this.editRotation.Value;
        }

        private void textImageDir_Enter(object sender, EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog.ShowDialog();
            Console.WriteLine(this.folderBrowserDialog.SelectedPath);
            if (Directory.Exists(this.folderBrowserDialog.SelectedPath) || Directory.CreateDirectory(this.folderBrowserDialog.SelectedPath).Exists)
            {
                this.textImageDir.Text = this.folderBrowserDialog.SelectedPath;
                Program.Settings.json["image directory"] = this.folderBrowserDialog.SelectedPath;
            }
        }

        private void textVideoDir_Enter(object sender, EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog.ShowDialog();
            Console.WriteLine(this.folderBrowserDialog.SelectedPath);
            if (Directory.Exists(this.folderBrowserDialog.SelectedPath) || Directory.CreateDirectory(this.folderBrowserDialog.SelectedPath).Exists)
            {
                this.textVideoDir.Text = this.folderBrowserDialog.SelectedPath;
                Program.Settings.json["video directory"] = this.folderBrowserDialog.SelectedPath;
            }
        }

        private void textDataDir_Enter(object sender, EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog.ShowDialog();
            Console.WriteLine(this.folderBrowserDialog.SelectedPath);
            if (result == DialogResult.OK)
            {
                if (Directory.Exists(this.folderBrowserDialog.SelectedPath) || Directory.CreateDirectory(this.folderBrowserDialog.SelectedPath).Exists)
                {
                    this.textDataDir.Text = this.folderBrowserDialog.SelectedPath;
                    Program.Settings.json["test data directory"] = this.folderBrowserDialog.SelectedPath;
                }
            }
        }
    }
}
