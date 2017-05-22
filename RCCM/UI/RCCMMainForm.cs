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
using System.Runtime.InteropServices;

namespace RCCM.UI
{
    /// <summary>
    /// The main window of the program from which all hardware initialization and termination is directed
    /// </summary>
    public partial class RCCMMainForm : Form
    {
        /// <summary>
        /// The main object representing the RCCM containing references to all hardware
        /// </summary>
        protected RCCMSystem rccm;
        /// <summary>
        /// Flag for indicating if NFOV1 form is open
        /// </summary>
        protected bool nfov1Open;
        /// <summary>
        /// Flag for indicating if NFOV2 form is open
        /// </summary>
        protected bool nfov2Open;
        /// <summary>
        /// Flag for indicating if WFOV1 form is open
        /// </summary>
        protected bool wfov1Open;
        /// <summary>
        /// Flag for indicating if WFOV2 form is open
        /// </summary>
        protected bool wfov2Open;
        /// <summary>
        /// Timer for replotting certain panel graphic
        /// </summary>
        protected Timer panelRepaintTimer;
        /// <summary>
        /// List of measurement objects. Observable so controls can update when other forms update them
        /// </summary>
        protected ObservableCollection<MeasurementSequence> cracks;
        /// <summary>
        /// Object for managing test result plotting
        /// </summary>
        protected TestResults test;
        /// <summary>
        /// Object for managing panel graphic drawing
        /// </summary>
        protected PanelView view;
        /// <summary>
        /// Resource manager for loading ActiveX object for Trio controller
        /// </summary>
        protected ComponentResourceManager resources;
        /// <summary>
        /// ActiveX control for trio controller
        /// </summary>
        protected AxTrioPCLib.AxTrioPC triopc;
        /// <summary>
        /// Holds sleep setting of computer before starting program so it can be reverted on exit
        /// </summary>
        protected uint fPreviousExecutionState;
        /// <summary>
        /// Flag to indicate if user is jogging an actuator
        /// </summary>
        protected bool jogging;

        /// <summary>
        /// Create the main form and initialize all hardware
        /// </summary>
        /// <param name="plugins">List of plugins that were found on startup</param>
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

            InitializeComponent();

            this.rccm = new RCCMSystem(this.triopc);
            this.cracks = new ObservableCollection<MeasurementSequence>();
            this.test = new TestResults(this.rccm, this.cracks, this.chartCracks, this.chartCycles, this.textCycle, this.textPressure, this.listCrackSelection);
            this.view = new PanelView(this.rccm);

            // Apply certain settings to controls
            this.applyUISettings();
            
            // Create timer for redrawing panel graphics
            this.panelRepaintTimer = new Timer();
            this.panelRepaintTimer.Enabled = true;
            this.panelRepaintTimer.Interval = (int)Program.Settings.json["repaint period"];
            this.panelRepaintTimer.Tick += new EventHandler(refreshPanelView);

            // Add toolstrip item for each plugin
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

            this.jogging = false;

            // Set new state to prevent system sleep
            this.fPreviousExecutionState = NativeMethods.SetThreadExecutionState(
                NativeMethods.ES_CONTINUOUS | NativeMethods.ES_SYSTEM_REQUIRED);
            if (this.fPreviousExecutionState == 0)
            {
                Console.WriteLine("SetThreadExecutionState failed. Do something here...");
            }
            Show();
        }
        
        /// <summary>
        /// Load form and initialize things dependent on UI visibility
        /// </summary>
        private void RCCMMainForm_Load(object sender, EventArgs e)
        {
            this.view.SetTransform(this.panelView.CreateGraphics());
            this.panelRepaintTimer.Start();
            // Prevent radio buttons from accepting arrow key input
            foreach (Control c in this.groupBoxStageSelect.Controls)
            {
                c.PreviewKeyDown += this.radioButtonSuppress;
            }
            foreach (Control c in this.groupBoxMovementMode.Controls)
            {
                c.PreviewKeyDown += this.radioButtonSuppress;
            }
        }

        /// <summary>
        /// Close form and disconnect from all hardware
        /// </summary>
        private async void RCCMMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.rccm.NFOV1.Disconnect();
            this.rccm.NFOV2.Disconnect();
            foreach (string motorName in RCCMSystem.AXES)
            {
                this.rccm.motors[motorName].JogStop();
            }
            await this.rccm.Stop();

            Logger.Save();
            Program.Settings.save();

            // Allow computer to sleep again
            NativeMethods.SetThreadExecutionState(this.fPreviousExecutionState);
        }

        #region Motion

        /// <summary>
        /// Handler for sending motion commands for coarse X
        /// </summary>
        private void coarseXPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("coarse X", (double)coarseXPos.Value, this.coarseXPos);
            }
        }

        /// <summary>
        /// Handler for sending motion commands for coarse Y
        /// </summary>
        private void coarseYPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("coarse Y", (double)coarseYPos.Value, this.coarseYPos);
            }
        }

        /// <summary>
        /// Handler for sending motion commands for fine 1 x
        /// </summary>
        private void fine1XPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 1 X", (double)fine1XPos.Value, this.fine1XPos);
            }
        }

        /// <summary>
        /// Handler for sending motion commands for fine 1 y
        /// </summary>
        private void fine1YPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 1 Y", (double)fine1YPos.Value, this.fine1YPos);
            }
        }

        /// <summary>
        /// Handler for sending motion commands for fine 1 z
        /// </summary>
        private void fine1ZPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 1 Z", (double)fine1ZPos.Value, this.fine1ZPos);
            }
        }

        /// <summary>
        /// Handler for sending motion commands for fine 2 x
        /// </summary>
        private void fine2XPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 2 X", (double)fine2XPos.Value, this.fine2XPos);
            }
        }

        /// <summary>
        /// Handler for sending motion commands for fine 2 y
        /// </summary>
        private void fine2YPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 2 Y", (double)fine2YPos.Value, this.fine2YPos);
            }
        }

        /// <summary>
        /// Handler for sending motion commands for fine 2 z
        /// </summary>
        private void fine2ZPos_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.moveStage("fine 2 Z", (double)fine2ZPos.Value, this.fine2ZPos);
            }
        }


        /// <summary>
        /// Send motion command for specified actions according to UI settings
        /// </summary>
        /// <param name="axis">Name of axis as defined in RCCMSystem</param>
        /// <param name="value">Position command value</param>
        /// <param name="c">UI Control sending this command</param>
        private void moveStage(string axis, double value, NumericUpDown c)
        {
            // Move to specified coordinate
            if (this.radioMoveAbs.Checked)
            {
                double returnPos = this.rccm.motors[axis].SetPos(value);
                // Set control text to output of set pos in case it was out of range
                c.Value = (decimal)returnPos;
            }
            // Move specified distance from current coordinate
            else if (this.radioMoveRel.Checked)
            {
                this.rccm.motors[axis].MoveRel(value);
            }
        }

        /// <summary>
        /// Send actuators to home position
        /// </summary>
        private void btnHome_Click(object sender, EventArgs e)
        {
            foreach (string motorName in this.rccm.motors.Keys)
            {
                this.rccm.motors[motorName].GotoHome();
                this.rccm.motors[motorName].WaitForEndOfMove();
            }
        }

        /// <summary>
        /// Set current positions as home position of actuators
        /// </summary>
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

        /// <summary>
        /// Set increment of position controls to actuator step value
        /// </summary>
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

        /// <summary>
        /// Replot cracks when user selects/deselects a crack
        /// </summary>
        private void listCracksSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.test.PlotCracks();
        }

        /// <summary>
        /// Start cycle counter and change button states
        /// </summary>
        private void btnStartTest_Click(object sender, EventArgs e)
        {
            this.rccm.Counter.Start();
            this.btnStartTest.Enabled = false;
            this.btnPauseTest.Enabled = true;
            this.btnStopTest.Enabled = true;
        }

        /// <summary>
        /// Pause cycle counting and change button states
        /// </summary>
        private void btnPauseTest_Click(object sender, EventArgs e)
        {
            this.rccm.Counter.Stop();
            this.btnStartTest.Enabled = true;
            this.btnPauseTest.Enabled = false;
            this.btnStopTest.Enabled = false;
        }

        /// <summary>
        /// Reset cycle counting and change button states
        /// </summary>
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
            this.rccm.Counter.Cycle = 0;
        }

        #endregion

        /// <summary>
        /// Paint panel graphic
        /// </summary>
        private void panelView_Paint(object sender, PaintEventArgs e)
        {
            this.view.Paint(e.Graphics);
        }

        /// <summary>
        /// Refresh panel graphic and update position indicators
        /// </summary>
        private void refreshPanelView(object sender, EventArgs e)
        {
            this.panelView.Invalidate();
            try
            {
                this.coarseXIndicator.Text = this.rccm.motors["coarse X"].GetPos().ToString();
                this.coarseYIndicator.Text = this.rccm.motors["coarse Y"].GetPos().ToString();
                this.fine1XIndicator.Text = this.rccm.motors["fine 1 X"].GetPos().ToString();
                this.fine1YIndicator.Text = this.rccm.motors["fine 1 Y"].GetPos().ToString();
                this.fine1ZIndicator.Text = this.rccm.motors["fine 1 Z"].GetPos().ToString();
                this.fine2XIndicator.Text = this.rccm.motors["fine 2 X"].GetPos().ToString();
                this.fine2YIndicator.Text = this.rccm.motors["fine 2 Y"].GetPos().ToString();
                this.fine2ZIndicator.Text = this.rccm.motors["fine 2 Z"].GetPos().ToString();
            }
            catch (Exception ex)
            {
                Logger.Out(ex.Message);
            }
            
        }

        #region Menu Items

        /// <summary>
        /// Open lens calibration form for NFOV1
        /// </summary>
        private void nFOV1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LensCalibrationForm form = new LensCalibrationForm(this.rccm, RCCMStage.RCCM1);
            form.Show();
        }

        /// <summary>
        /// Open lens calibratio form for NFOV2
        /// </summary>
        private void nFOV2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LensCalibrationForm form = new LensCalibrationForm(this.rccm, RCCMStage.RCCM2);
            form.Show();
        }

        /// <summary>
        /// Open form for coordinate system settings
        /// </summary>
        private void coordinateSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CoordinateSystemSettingsForm form = new CoordinateSystemSettingsForm(this.rccm);
            form.Show();
        }

        /// <summary>
        /// Open form for camera settings
        /// </summary>
        private void camerasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CameraSettingsForm form = new CameraSettingsForm(this.rccm);
            form.Show();
        }

        /// <summary>
        /// Open RCCM info form
        /// </summary>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutRCCMForm form = new AboutRCCMForm();
            form.Show();
        }

        /// <summary>
        /// Exit program
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        /// <summary>
        /// Open a form for initializing the specified plugin
        /// </summary>
        /// <param name="plugin">Plugin to initialize</param>
        private void PluginToolStripClick(IRCCMPlugin plugin)
        {
            PluginInitializationForm form = new PluginInitializationForm(this.rccm, plugin);
            form.Show();
        }

        #endregion

        /// <summary>
        /// Open NFOV1 GUI if it isn't already open
        /// </summary>
        private void btnNFOV1Open_Click(object sender, EventArgs e)
        {
            if (!this.nfov1Open)
            {
                this.nfov1Open = true;
                NFOVViewForm form = new NFOVViewForm(this.rccm, this.rccm.NFOV1, this.cracks);
                form.Show();
                form.FormClosed += delegate (object sender2, FormClosedEventArgs e2) { this.nfov1Open = false; };
            }
        }

        /// <summary>
        /// Open NFOV2 GUI if it isn't already open
        /// </summary>
        private void btnNFOV2Open_Click(object sender, EventArgs e)
        {
            if (!this.nfov2Open)
            {
                this.nfov2Open = true;
                NFOVViewForm form = new NFOVViewForm(this.rccm, this.rccm.NFOV2, this.cracks);
                form.Show();
                form.FormClosed += delegate (object sender2, FormClosedEventArgs e2) { this.nfov2Open = false; };
            }
        }

        /// <summary>
        /// Open WFOV1 GUI if it isn't already open
        /// </summary>
        private void btnWFOV1Open_Click(object sender, EventArgs e)
        {
            if (!this.wfov1Open)
            {
                this.wfov1Open = true;
                WFOVViewForm form = new WFOVViewForm(this.rccm, this.rccm.WFOV1, this.cracks);
                form.Show();
                form.FormClosed += delegate (object sender2, FormClosedEventArgs e2) { this.wfov1Open = false; };
            }
        }

        /// <summary>
        /// Open WFOV2 GUI if it isn't already open
        /// </summary>
        private void btnWFOV2Open_Click(object sender, EventArgs e)
        {
            if (!this.wfov2Open)
            {
                this.wfov2Open = true;
                WFOVViewForm form = new WFOVViewForm(this.rccm, this.rccm.WFOV2, this.cracks);
                form.Show();
                form.FormClosed += delegate (object sender2, FormClosedEventArgs e2) { this.wfov2Open = false; };
            }
        }

        /// <summary>
        /// Immediately stops all moving axes
        /// </summary>
        private void btnEStop_Click(object sender, EventArgs e)
        {
            this.rccm.motors["fine 1 Z"].SetProperty("feedback", 0);
            this.rccm.motors["fine 2 Z"].SetProperty("feedback", 0);
            this.rccm.triopc.Stop();
        }

        /// <summary>
        /// When UI resizes, reset panel graphic
        /// </summary>
        private void RCCMMainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                // Do some stuff
                this.view.SetTransform(this.panelView.CreateGraphics());
            }
        }

        /// <summary>
        /// Reset panel graphic only if it isnt minimized
        /// </summary>
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
        private void radioButtonSuppress(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.IsInputKey = true;
            }
        }

        /// <summary>
        /// Open form for changing motor settings
        /// </summary>
        private void motorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MotorSettingsForm form = new MotorSettingsForm(this.rccm);
            form.Show();
        }

        /// <summary>
        /// Catch arrow key presses for jogging actuators
        /// </summary>
        private void RCCMMainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if keydown was pressed to prevent event from firing continuously
            if (this.jogging)
            {
                return;
            }
            // Do not call jogging code if edit control is focused
            foreach (Control c in new Control[] { this.coarseXPos, this.coarseYPos, this.fine1XPos, this.fine1YPos, this.fine1ZPos, this.fine2XPos, this.fine2YPos, this.fine2ZPos })
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
                    this.jogging = true;
                    e.SuppressKeyPress = true;
                    this.rccm.motors[yAxis].Jog(true);
                    break;
                case Keys.Left:
                    this.jogging = true;
                    e.SuppressKeyPress = true;
                    this.rccm.motors[xAxis].Jog(false);
                    break;
                case Keys.Down:
                    this.jogging = true;
                    e.SuppressKeyPress = true;
                    this.rccm.motors[yAxis].Jog(false);
                    break;
                case Keys.Right:
                    this.jogging = true;
                    e.SuppressKeyPress = true;
                    this.rccm.motors[xAxis].Jog(true);
                    break;
            }
        }

        /// <summary>
        /// Stop jogging when arrow keys are released
        /// </summary>
        private void RCCMMainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up || e.KeyData == Keys.Left || e.KeyData == Keys.Down || e.KeyData == Keys.Right)
            {

                this.jogging = false;
                foreach (string motorName in RCCMSystem.AXES)
                {
                    this.rccm.motors[motorName].JogStop();
                }
            }
        }

        /// <summary>
        /// Get the UI selected stage to jog
        /// </summary>
        /// <returns>Enum value of selected option</returns>
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

        /// <summary>
        /// Helper class fo preventing OS from putting computer in sleep mode
        /// </summary>
        internal static class NativeMethods
        {
            // Import SetThreadExecutionState Win32 API and necessary flags
            [DllImport("kernel32.dll")]
            public static extern uint SetThreadExecutionState(uint esFlags);
            public const uint ES_CONTINUOUS = 0x80000000;
            public const uint ES_SYSTEM_REQUIRED = 0x00000001;
        }
    }
}
