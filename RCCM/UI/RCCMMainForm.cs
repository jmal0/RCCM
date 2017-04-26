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

        protected TestResults test;
        
        protected PanelView view;

        protected ComponentResourceManager resources;
        protected AxTrioPCLib.AxTrioPC triopc;

        public RCCMMainForm(ICollection<IRCCMPlugin> plugins)
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
            
            this.panelRepaintTimer = new Timer();
            this.panelRepaintTimer.Enabled = true;
            this.panelRepaintTimer.Interval = (int)Program.Settings.json["repaint period"];
            this.panelRepaintTimer.Tick += new EventHandler(refreshPanelView);

            this.cracks = new ObservableCollection<MeasurementSequence>();

            this.test = new TestResults(this.rccm, this.cracks, this.chartCracks, this.chartCycles, this.textCycle, this.textPressure, this.listCrackSelection);

            this.view = new PanelView(this.rccm);

            if (plugins != null)
            {
                foreach (IRCCMPlugin plugin in plugins)
                {
                    ToolStripMenuItem pluginItem = new ToolStripMenuItem(plugin.Name);
                    this.pluginsToolStripMenuItem.DropDownItems.Add(pluginItem);
                    pluginItem.Click += delegate (object sender, EventArgs e)
                    {
                        this.PluginToolStripClick(plugin);
                    };
                }
            }
            Show();
        }

        private void PluginToolStripClick(IRCCMPlugin plugin)
        {
            PluginInitializationForm form = new PluginInitializationForm(this.rccm, plugin);
            form.Show();
        }
        
        private void RCCMMainForm_Load(object sender, EventArgs e)
        {
            this.view.SetTransform(this.panelView.CreateGraphics());
            this.panelRepaintTimer.Start();
        }
        
        private async void RCCMMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.nfov1.Disconnect();
            this.nfov2.Disconnect();
            foreach (string motorName in RCCMSystem.AXES)
            {
                this.rccm.motors[motorName].JogStop();
            }
            await this.rccm.Stop();

            Logger.Save();
            Program.Settings.save();
        }

        #region Motion

        private void coarseXPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("coarse X", (double)coarseXPos.Value, this.coarseXPos);
            }
        }

        private void coarseYPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("coarse Y", (double)coarseYPos.Value, this.coarseYPos);
            }
        }

        private void fine1XPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 1 X", (double)fine1XPos.Value, this.fine1XPos);
            }
        }

        private void fine1YPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 1 Y", (double)fine1YPos.Value, this.fine1YPos);
            }
        }

        private void fine1ZPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 1 Z", (double)fine1ZPos.Value, this.fine1ZPos);
            }
        }

        private void fine2XPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 2 X", (double)fine2XPos.Value, this.fine2XPos);
            }
        }

        private void fine2YPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 2 Y", (double)fine2YPos.Value, this.fine2YPos);
            }
        }

        private void fine2ZPos_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 2 Z", (double)fine2ZPos.Value, this.fine2ZPos);
            }
        }

        private void moveStage(string axis, double value, NumericUpDown c)
        {
            if (this.radioMoveAbs.Checked)
            {
                double returnPos = this.rccm.motors[axis].SetPos(value);
                c.Value = (decimal)returnPos;
            }
            else if (this.radioMoveRel.Checked)
            {
                this.rccm.motors[axis].MoveRel(value);
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            foreach (string motorName in this.rccm.motors.Keys)
            {
                this.rccm.motors[motorName].GotoHome();
                this.rccm.motors[motorName].WaitForEndOfMove();
            }
        }

        private void btnSetHome_Click(object sender, EventArgs e)
        {
            foreach (string motorName in this.rccm.motors.Keys)
            {
                this.rccm.motors[motorName].SetProperty("home", this.rccm.motors[motorName].GetPos());
            }
            this.rccm.SaveMotorSettings();
            Program.Settings.save();
        }


        #endregion

        #region Settings

        public void applyUISettings()
        {
            // Make NumericUpDown increment property equal to motor minimum step size
            this.coarseXPos.Increment = (decimal) Program.Settings.json["coarse X"]["step"];
            this.coarseYPos.Increment = (decimal) Program.Settings.json["coarse Y"]["step"];
            this.fine1XPos.Increment = (decimal) Program.Settings.json["fine 1 X"]["step"];
            this.fine1YPos.Increment = (decimal) Program.Settings.json["fine 1 Y"]["step"];
            this.fine1ZPos.Increment = (decimal) Program.Settings.json["fine 1 Z"]["step"];
            this.fine2XPos.Increment = (decimal) Program.Settings.json["fine 2 X"]["step"];
            this.fine2YPos.Increment = (decimal) Program.Settings.json["fine 2 Y"]["step"];
            this.fine2ZPos.Increment = (decimal) Program.Settings.json["fine 2 Z"]["step"];
        }

        #endregion

        #region Fatigue Testing

        private void listCracksSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.test.PlotCracks();
        }

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            this.rccm.Counter.Start();
            this.btnStartTest.Enabled = false;
            this.btnPauseTest.Enabled = true;
            this.btnStopTest.Enabled = true;
        }

        private void btnPauseTest_Click(object sender, EventArgs e)
        {
            this.rccm.Counter.Stop();
            this.btnStartTest.Enabled = true;
            this.btnPauseTest.Enabled = false;
            this.btnStopTest.Enabled = false;
        }

        private void btnStopTest_Click(object sender, EventArgs e)
        {
            this.rccm.Counter.Stop();
            this.btnStartTest.Enabled = true;
            this.btnPauseTest.Enabled = false;
            this.btnStopTest.Enabled = false;
            foreach (MeasurementSequence crack in this.cracks)
            {
                crack.WriteToFile((string)Program.Settings.json[crack.Camera]["test data directory"], true);
            }
        }

        private void editCycleFreq_Click(object sender, EventArgs e)
        {
            //this.rccm.Counter.Period = (int)(2000.0 * Math.PI * (double)this.editCycleFreq.Value);
        }

        #endregion

        private void panelView_Paint(object sender, PaintEventArgs e)
        {
            this.view.Paint(e.Graphics);
        }

        private void refreshPanelView(object sender, EventArgs e)
        {
            this.panelView.Invalidate();
            this.coarseXIndicator.Text = this.rccm.motors["coarse X"].GetPos().ToString();
            this.coarseYIndicator.Text = this.rccm.motors["coarse Y"].GetPos().ToString();
            this.fine1XIndicator.Text = this.rccm.motors["fine 1 X"].GetPos().ToString();
            this.fine1YIndicator.Text = this.rccm.motors["fine 1 Y"].GetPos().ToString();
            this.fine1ZIndicator.Text = this.rccm.motors["fine 1 Z"].GetPos().ToString();
            this.fine2XIndicator.Text = this.rccm.motors["fine 2 X"].GetPos().ToString();
            this.fine2YIndicator.Text = this.rccm.motors["fine 2 Y"].GetPos().ToString();
            this.fine2ZIndicator.Text = this.rccm.motors["fine 2 Z"].GetPos().ToString();
        }

        #region Menu Items

        private void nFOV1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LensCalibrationForm form = new LensCalibrationForm(this.rccm, RCCMStage.RCCM1);
            form.Show();
        }

        private void nFOV2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LensCalibrationForm form = new LensCalibrationForm(this.rccm, RCCMStage.RCCM2);
            form.Show();
        }

        private void coordinateSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CoordinateSystemSettingsForm form = new CoordinateSystemSettingsForm(this.rccm);
            form.Show();
        }
        
        private void camerasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CameraSettingsForm form = new CameraSettingsForm(this.rccm);
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

        private void RCCMMainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                // Do some stuff
                this.view.SetTransform(this.panelView.CreateGraphics());
            }
        }

        private void tabPageMotion_Paint(object sender, PaintEventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                // Do some stuff
                this.view.SetTransform(this.panelView.CreateGraphics());
            }
        }

        /// <summary>
        /// Prevents arrow keys from changing radio button selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonSuppress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
            }                
        }

        private void motorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MotorSettingsForm form = new MotorSettingsForm(this.rccm);
            form.Show();
            form.FormClosed += delegate (object sender2, FormClosedEventArgs e2) {
                this.applyUISettings();
            };
        }

        private void RCCMMainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Do not call jogging code if edit control is focused
            foreach (Control c in new Control[] { this.coarseXPos, this.coarseYPos, this.fine1XPos, this.fine1YPos, this.fine1ZPos, this.fine2XPos, this.fine2YPos, this.fine2ZPos, this.editCycleFreq })
            {
                if (c.Focused)
                {
                    return;
                }
            }
            // Get active axis
            string xAxis, yAxis;
            switch (this.getActiveStage())
            {
                case RCCMStage.RCCM1:
                    xAxis = "fine 1 X";
                    yAxis = "fine 1 Y";
                    break;
                case RCCMStage.RCCM2:
                    xAxis = "fine 2 X";
                    yAxis = "fine 2 Y";
                    break;
                case RCCMStage.Coarse:
                    xAxis = "coarse X";
                    yAxis = "coarse Y";
                    break;
                default:
                    return;
            }

            // Get current stage position
            double xPos = this.rccm.motors[xAxis].GetPos();
            double yPos = this.rccm.motors[yAxis].GetPos();

            // Jog motors
            switch (e.KeyCode)
            {
                case Keys.Up:
                    e.SuppressKeyPress = true;
                    this.rccm.motors[yAxis].Jog(true);
                    break;
                case Keys.Left:
                    e.SuppressKeyPress = true;
                    this.rccm.motors[xAxis].Jog(false);
                    break;
                case Keys.Down:
                    e.SuppressKeyPress = true;
                    this.rccm.motors[yAxis].Jog(false);
                    break;
                case Keys.Right:
                    e.SuppressKeyPress = true;
                    this.rccm.motors[xAxis].Jog(true);
                    break;
            }
        }

        private void RCCMMainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up || e.KeyData == Keys.Left || e.KeyData == Keys.Down || e.KeyData == Keys.Right)
            {
                foreach (string motorName in RCCMSystem.AXES)
                {
                    this.rccm.motors[motorName].JogStop();
                }
            }
        }

        private RCCMStage getActiveStage()
        {
            if (this.radioRCCM1.Checked)
            {
                return RCCMStage.RCCM1;
            }
            if (this.radioRCCM2.Checked)
            {
                return RCCMStage.RCCM2;
            }
            if (this.radioCoarse.Checked)
            {
                return RCCMStage.Coarse;
            }
            return RCCMStage.None;
        }
    }
}
