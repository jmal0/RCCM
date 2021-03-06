﻿namespace RCCM.UI
{
    partial class RCCMMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RCCMMainForm));
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nFOV1LensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nFOV1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nFOV2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coordinateSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.camerasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.motorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDlg = new System.Windows.Forms.ColorDialog();
            this.radioRCCM1 = new System.Windows.Forms.RadioButton();
            this.radioRCCM2 = new System.Windows.Forms.RadioButton();
            this.groupBoxStageSelect = new System.Windows.Forms.GroupBox();
            this.radioNoStage = new System.Windows.Forms.RadioButton();
            this.radioCoarse = new System.Windows.Forms.RadioButton();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.btnStartTest = new System.Windows.Forms.Button();
            this.btnPauseTest = new System.Windows.Forms.Button();
            this.btnStopTest = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabPageResults = new System.Windows.Forms.TabPage();
            this.tableLayoutResults = new System.Windows.Forms.TableLayoutPanel();
            this.chartCracks = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.listCrackSelection = new System.Windows.Forms.ListBox();
            this.tabPageMotion = new System.Windows.Forms.TabPage();
            this.tableLayoutMotion = new System.Windows.Forms.TableLayoutPanel();
            this.panelView = new System.Windows.Forms.PictureBox();
            this.panelMotionButtons = new System.Windows.Forms.Panel();
            this.btnSetHome = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.groupBoxMovementMode = new System.Windows.Forms.GroupBox();
            this.radioMoveRel = new System.Windows.Forms.RadioButton();
            this.radioMoveAbs = new System.Windows.Forms.RadioButton();
            this.panelMotionControls = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fine2ZIndicator = new System.Windows.Forms.TextBox();
            this.fine2YIndicator = new System.Windows.Forms.TextBox();
            this.fine2XIndicator = new System.Windows.Forms.TextBox();
            this.fine2ZPos = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.fine2YPos = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.fine2XPos = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fine1ZIndicator = new System.Windows.Forms.TextBox();
            this.fine1YIndicator = new System.Windows.Forms.TextBox();
            this.fine1XIndicator = new System.Windows.Forms.TextBox();
            this.fine1ZPos = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.fine1YPos = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.fine1XPos = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.coarseYIndicator = new System.Windows.Forms.TextBox();
            this.coarseXIndicator = new System.Windows.Forms.TextBox();
            this.coarseYPos = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.coarseXPos = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnWFOV2Open = new System.Windows.Forms.Button();
            this.btnNFOV2Open = new System.Windows.Forms.Button();
            this.btnWFOV1Open = new System.Windows.Forms.Button();
            this.btnNFOV1Open = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPagePressure = new System.Windows.Forms.TabPage();
            this.chartCycles = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelTestControls = new System.Windows.Forms.Panel();
            this.btnDisable = new System.Windows.Forms.CheckBox();
            this.btnJogLeft = new System.Windows.Forms.CheckBox();
            this.btnJogRight = new System.Windows.Forms.CheckBox();
            this.btnJogDown = new System.Windows.Forms.CheckBox();
            this.btnJogUp = new System.Windows.Forms.CheckBox();
            this.btnEStop = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.textPressure = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.textCycle = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.btnJogPlusZ = new System.Windows.Forms.CheckBox();
            this.btnJogMinusZ = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.groupBoxStageSelect.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.tabPageResults.SuspendLayout();
            this.tableLayoutResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartCracks)).BeginInit();
            this.tabPageMotion.SuspendLayout();
            this.tableLayoutMotion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelView)).BeginInit();
            this.panelMotionButtons.SuspendLayout();
            this.groupBoxMovementMode.SuspendLayout();
            this.panelMotionControls.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fine2ZPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fine2YPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fine2XPos)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fine1ZPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fine1YPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fine1XPos)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coarseYPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coarseXPos)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPagePressure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartCycles)).BeginInit();
            this.tableLayoutMain.SuspendLayout();
            this.panelTestControls.SuspendLayout();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 32);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(121, 32);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.pluginsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1146, 38);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nFOV1LensToolStripMenuItem,
            this.coordinateSystemToolStripMenuItem,
            this.camerasToolStripMenuItem,
            this.motorsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(95, 32);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // nFOV1LensToolStripMenuItem
            // 
            this.nFOV1LensToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nFOV1ToolStripMenuItem,
            this.nFOV2ToolStripMenuItem});
            this.nFOV1LensToolStripMenuItem.Name = "nFOV1LensToolStripMenuItem";
            this.nFOV1LensToolStripMenuItem.Size = new System.Drawing.Size(255, 32);
            this.nFOV1LensToolStripMenuItem.Text = "Lens Calibration";
            // 
            // nFOV1ToolStripMenuItem
            // 
            this.nFOV1ToolStripMenuItem.Name = "nFOV1ToolStripMenuItem";
            this.nFOV1ToolStripMenuItem.Size = new System.Drawing.Size(158, 32);
            this.nFOV1ToolStripMenuItem.Text = "NFOV 1";
            this.nFOV1ToolStripMenuItem.Click += new System.EventHandler(this.nFOV1ToolStripMenuItem_Click);
            // 
            // nFOV2ToolStripMenuItem
            // 
            this.nFOV2ToolStripMenuItem.Name = "nFOV2ToolStripMenuItem";
            this.nFOV2ToolStripMenuItem.Size = new System.Drawing.Size(158, 32);
            this.nFOV2ToolStripMenuItem.Text = "NFOV 2";
            this.nFOV2ToolStripMenuItem.Click += new System.EventHandler(this.nFOV2ToolStripMenuItem_Click);
            // 
            // coordinateSystemToolStripMenuItem
            // 
            this.coordinateSystemToolStripMenuItem.Name = "coordinateSystemToolStripMenuItem";
            this.coordinateSystemToolStripMenuItem.Size = new System.Drawing.Size(255, 32);
            this.coordinateSystemToolStripMenuItem.Text = "Coordinate System";
            this.coordinateSystemToolStripMenuItem.Click += new System.EventHandler(this.coordinateSystemToolStripMenuItem_Click);
            // 
            // camerasToolStripMenuItem
            // 
            this.camerasToolStripMenuItem.Name = "camerasToolStripMenuItem";
            this.camerasToolStripMenuItem.Size = new System.Drawing.Size(255, 32);
            this.camerasToolStripMenuItem.Text = "Cameras";
            this.camerasToolStripMenuItem.Click += new System.EventHandler(this.camerasToolStripMenuItem_Click);
            // 
            // motorsToolStripMenuItem
            // 
            this.motorsToolStripMenuItem.Name = "motorsToolStripMenuItem";
            this.motorsToolStripMenuItem.Size = new System.Drawing.Size(255, 32);
            this.motorsToolStripMenuItem.Text = "Motors";
            this.motorsToolStripMenuItem.Click += new System.EventHandler(this.motorsToolStripMenuItem_Click);
            // 
            // pluginsToolStripMenuItem
            // 
            this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
            this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(87, 32);
            this.pluginsToolStripMenuItem.Text = "Plugins";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 32);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(145, 32);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // radioRCCM1
            // 
            this.radioRCCM1.AutoSize = true;
            this.radioRCCM1.Location = new System.Drawing.Point(9, 65);
            this.radioRCCM1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioRCCM1.Name = "radioRCCM1";
            this.radioRCCM1.Size = new System.Drawing.Size(104, 29);
            this.radioRCCM1.TabIndex = 11;
            this.radioRCCM1.Text = "RCCM1";
            this.radioRCCM1.UseVisualStyleBackColor = true;
            // 
            // radioRCCM2
            // 
            this.radioRCCM2.AutoSize = true;
            this.radioRCCM2.Location = new System.Drawing.Point(9, 100);
            this.radioRCCM2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioRCCM2.Name = "radioRCCM2";
            this.radioRCCM2.Size = new System.Drawing.Size(104, 29);
            this.radioRCCM2.TabIndex = 12;
            this.radioRCCM2.Text = "RCCM2";
            this.radioRCCM2.UseVisualStyleBackColor = true;
            // 
            // groupBoxStageSelect
            // 
            this.groupBoxStageSelect.Controls.Add(this.radioNoStage);
            this.groupBoxStageSelect.Controls.Add(this.radioCoarse);
            this.groupBoxStageSelect.Controls.Add(this.radioRCCM2);
            this.groupBoxStageSelect.Controls.Add(this.radioRCCM1);
            this.groupBoxStageSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxStageSelect.Location = new System.Drawing.Point(0, 0);
            this.groupBoxStageSelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxStageSelect.Name = "groupBoxStageSelect";
            this.groupBoxStageSelect.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxStageSelect.Size = new System.Drawing.Size(161, 173);
            this.groupBoxStageSelect.TabIndex = 19;
            this.groupBoxStageSelect.TabStop = false;
            this.groupBoxStageSelect.Text = "Stage Selection";
            this.toolTip.SetToolTip(this.groupBoxStageSelect, "Select which set of actuators is moved by pressing arrow keys");
            // 
            // radioNoStage
            // 
            this.radioNoStage.AutoSize = true;
            this.radioNoStage.Checked = true;
            this.radioNoStage.Location = new System.Drawing.Point(9, 31);
            this.radioNoStage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioNoStage.Name = "radioNoStage";
            this.radioNoStage.Size = new System.Drawing.Size(80, 29);
            this.radioNoStage.TabIndex = 14;
            this.radioNoStage.TabStop = true;
            this.radioNoStage.Text = "None";
            this.radioNoStage.UseVisualStyleBackColor = true;
            // 
            // radioCoarse
            // 
            this.radioCoarse.AutoSize = true;
            this.radioCoarse.Location = new System.Drawing.Point(9, 134);
            this.radioCoarse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioCoarse.Name = "radioCoarse";
            this.radioCoarse.Size = new System.Drawing.Size(97, 29);
            this.radioCoarse.TabIndex = 13;
            this.radioCoarse.Text = "Coarse";
            this.radioCoarse.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.btnStartTest);
            this.groupBox11.Controls.Add(this.btnPauseTest);
            this.groupBox11.Controls.Add(this.btnStopTest);
            this.groupBox11.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox11.Location = new System.Drawing.Point(0, 0);
            this.groupBox11.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox11.Size = new System.Drawing.Size(151, 75);
            this.groupBox11.TabIndex = 19;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Test Controls";
            // 
            // btnStartTest
            // 
            this.btnStartTest.Image = global::RCCM.Properties.Resources.play;
            this.btnStartTest.Location = new System.Drawing.Point(9, 26);
            this.btnStartTest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(39, 40);
            this.btnStartTest.TabIndex = 7;
            this.toolTip.SetToolTip(this.btnStartTest, "Start cycle counting");
            this.btnStartTest.UseVisualStyleBackColor = true;
            this.btnStartTest.Click += new System.EventHandler(this.btnStartTest_Click);
            // 
            // btnPauseTest
            // 
            this.btnPauseTest.Image = global::RCCM.Properties.Resources.pause;
            this.btnPauseTest.Location = new System.Drawing.Point(52, 26);
            this.btnPauseTest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPauseTest.Name = "btnPauseTest";
            this.btnPauseTest.Size = new System.Drawing.Size(39, 40);
            this.btnPauseTest.TabIndex = 6;
            this.toolTip.SetToolTip(this.btnPauseTest, "Pause cycle counting");
            this.btnPauseTest.UseVisualStyleBackColor = true;
            this.btnPauseTest.Click += new System.EventHandler(this.btnPauseTest_Click);
            // 
            // btnStopTest
            // 
            this.btnStopTest.Image = global::RCCM.Properties.Resources.stop;
            this.btnStopTest.Location = new System.Drawing.Point(100, 26);
            this.btnStopTest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStopTest.Name = "btnStopTest";
            this.btnStopTest.Size = new System.Drawing.Size(39, 40);
            this.btnStopTest.TabIndex = 8;
            this.toolTip.SetToolTip(this.btnStopTest, "Stop cycle counting and save all current test data");
            this.btnStopTest.UseVisualStyleBackColor = true;
            this.btnStopTest.Click += new System.EventHandler(this.btnStopTest_Click);
            // 
            // tabPageResults
            // 
            this.tabPageResults.Controls.Add(this.tableLayoutResults);
            this.tabPageResults.Location = new System.Drawing.Point(4, 34);
            this.tabPageResults.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageResults.Name = "tabPageResults";
            this.tabPageResults.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageResults.Size = new System.Drawing.Size(1130, 444);
            this.tabPageResults.TabIndex = 4;
            this.tabPageResults.Text = "Test Results";
            this.tabPageResults.UseVisualStyleBackColor = true;
            // 
            // tableLayoutResults
            // 
            this.tableLayoutResults.ColumnCount = 2;
            this.tableLayoutResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutResults.Controls.Add(this.chartCracks, 0, 0);
            this.tableLayoutResults.Controls.Add(this.listCrackSelection, 1, 0);
            this.tableLayoutResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutResults.Location = new System.Drawing.Point(4, 5);
            this.tableLayoutResults.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutResults.Name = "tableLayoutResults";
            this.tableLayoutResults.RowCount = 1;
            this.tableLayoutResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutResults.Size = new System.Drawing.Size(1122, 434);
            this.tableLayoutResults.TabIndex = 9;
            // 
            // chartCracks
            // 
            this.chartCracks.CausesValidation = false;
            chartArea1.Name = "ChartArea1";
            this.chartCracks.ChartAreas.Add(chartArea1);
            this.chartCracks.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartCracks.Legends.Add(legend1);
            this.chartCracks.Location = new System.Drawing.Point(4, 5);
            this.chartCracks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chartCracks.Name = "chartCracks";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartCracks.Series.Add(series1);
            this.chartCracks.Size = new System.Drawing.Size(889, 424);
            this.chartCracks.TabIndex = 0;
            this.chartCracks.Text = "chart1";
            // 
            // listCrackSelection
            // 
            this.listCrackSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCrackSelection.FormattingEnabled = true;
            this.listCrackSelection.ItemHeight = 25;
            this.listCrackSelection.Location = new System.Drawing.Point(901, 5);
            this.listCrackSelection.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listCrackSelection.Name = "listCrackSelection";
            this.listCrackSelection.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listCrackSelection.Size = new System.Drawing.Size(217, 424);
            this.listCrackSelection.TabIndex = 8;
            this.toolTip.SetToolTip(this.listCrackSelection, "Click to higlight and select which cracks are plotted");
            this.listCrackSelection.SelectedIndexChanged += new System.EventHandler(this.listCracksSelection_SelectedIndexChanged);
            // 
            // tabPageMotion
            // 
            this.tabPageMotion.Controls.Add(this.tableLayoutMotion);
            this.tabPageMotion.Location = new System.Drawing.Point(4, 34);
            this.tabPageMotion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageMotion.Name = "tabPageMotion";
            this.tabPageMotion.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageMotion.Size = new System.Drawing.Size(1130, 437);
            this.tabPageMotion.TabIndex = 1;
            this.tabPageMotion.Text = "Motion";
            this.tabPageMotion.UseVisualStyleBackColor = true;
            this.tabPageMotion.Paint += new System.Windows.Forms.PaintEventHandler(this.tabPageMotion_Paint);
            // 
            // tableLayoutMotion
            // 
            this.tableLayoutMotion.ColumnCount = 3;
            this.tableLayoutMotion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutMotion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutMotion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutMotion.Controls.Add(this.panelView, 1, 0);
            this.tableLayoutMotion.Controls.Add(this.panelMotionButtons, 2, 0);
            this.tableLayoutMotion.Controls.Add(this.panelMotionControls, 0, 0);
            this.tableLayoutMotion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMotion.Location = new System.Drawing.Point(4, 5);
            this.tableLayoutMotion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutMotion.Name = "tableLayoutMotion";
            this.tableLayoutMotion.RowCount = 1;
            this.tableLayoutMotion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMotion.Size = new System.Drawing.Size(1122, 427);
            this.tableLayoutMotion.TabIndex = 22;
            // 
            // panelView
            // 
            this.panelView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelView.Location = new System.Drawing.Point(340, 5);
            this.panelView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(609, 417);
            this.panelView.TabIndex = 23;
            this.panelView.TabStop = false;
            this.panelView.Paint += new System.Windows.Forms.PaintEventHandler(this.panelView_Paint);
            // 
            // panelMotionButtons
            // 
            this.panelMotionButtons.Controls.Add(this.btnSetHome);
            this.panelMotionButtons.Controls.Add(this.btnHome);
            this.panelMotionButtons.Controls.Add(this.groupBoxMovementMode);
            this.panelMotionButtons.Controls.Add(this.groupBoxStageSelect);
            this.panelMotionButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMotionButtons.Location = new System.Drawing.Point(957, 5);
            this.panelMotionButtons.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelMotionButtons.Name = "panelMotionButtons";
            this.panelMotionButtons.Size = new System.Drawing.Size(161, 417);
            this.panelMotionButtons.TabIndex = 23;
            // 
            // btnSetHome
            // 
            this.btnSetHome.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSetHome.Location = new System.Drawing.Point(0, 316);
            this.btnSetHome.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSetHome.Name = "btnSetHome";
            this.btnSetHome.Size = new System.Drawing.Size(161, 35);
            this.btnSetHome.TabIndex = 22;
            this.btnSetHome.Text = "Set as Home";
            this.toolTip.SetToolTip(this.btnSetHome, "Save current actuator positions as home position");
            this.btnSetHome.UseVisualStyleBackColor = true;
            this.btnSetHome.Click += new System.EventHandler(this.btnSetHome_Click);
            // 
            // btnHome
            // 
            this.btnHome.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHome.Location = new System.Drawing.Point(0, 281);
            this.btnHome.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(161, 35);
            this.btnHome.TabIndex = 21;
            this.btnHome.Text = "Go to Home";
            this.toolTip.SetToolTip(this.btnHome, "Move all actuators to their saved \"home\" locations");
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // groupBoxMovementMode
            // 
            this.groupBoxMovementMode.Controls.Add(this.radioMoveRel);
            this.groupBoxMovementMode.Controls.Add(this.radioMoveAbs);
            this.groupBoxMovementMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxMovementMode.Location = new System.Drawing.Point(0, 173);
            this.groupBoxMovementMode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxMovementMode.Name = "groupBoxMovementMode";
            this.groupBoxMovementMode.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxMovementMode.Size = new System.Drawing.Size(161, 108);
            this.groupBoxMovementMode.TabIndex = 4;
            this.groupBoxMovementMode.TabStop = false;
            this.groupBoxMovementMode.Text = "Mode";
            this.toolTip.SetToolTip(this.groupBoxMovementMode, "Select movement mode used by position controls");
            // 
            // radioMoveRel
            // 
            this.radioMoveRel.AutoSize = true;
            this.radioMoveRel.Location = new System.Drawing.Point(9, 65);
            this.radioMoveRel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioMoveRel.Name = "radioMoveRel";
            this.radioMoveRel.Size = new System.Drawing.Size(102, 29);
            this.radioMoveRel.TabIndex = 13;
            this.radioMoveRel.Text = "Relative";
            this.toolTip.SetToolTip(this.radioMoveRel, "Move a specified distance from the current position");
            this.radioMoveRel.UseVisualStyleBackColor = true;
            // 
            // radioMoveAbs
            // 
            this.radioMoveAbs.AutoSize = true;
            this.radioMoveAbs.Checked = true;
            this.radioMoveAbs.Location = new System.Drawing.Point(9, 29);
            this.radioMoveAbs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioMoveAbs.Name = "radioMoveAbs";
            this.radioMoveAbs.Size = new System.Drawing.Size(110, 29);
            this.radioMoveAbs.TabIndex = 12;
            this.radioMoveAbs.TabStop = true;
            this.radioMoveAbs.Text = "Absolute";
            this.toolTip.SetToolTip(this.radioMoveAbs, "Move to a position a specified distance from the end of travel");
            this.radioMoveAbs.UseVisualStyleBackColor = true;
            // 
            // panelMotionControls
            // 
            this.panelMotionControls.Controls.Add(this.groupBox3);
            this.panelMotionControls.Controls.Add(this.groupBox2);
            this.panelMotionControls.Controls.Add(this.groupBox1);
            this.panelMotionControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMotionControls.Location = new System.Drawing.Point(4, 5);
            this.panelMotionControls.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelMotionControls.Name = "panelMotionControls";
            this.panelMotionControls.Size = new System.Drawing.Size(328, 417);
            this.panelMotionControls.TabIndex = 24;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.fine2ZIndicator);
            this.groupBox3.Controls.Add(this.fine2YIndicator);
            this.groupBox3.Controls.Add(this.fine2XIndicator);
            this.groupBox3.Controls.Add(this.fine2ZPos);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.fine2YPos);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.fine2XPos);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 268);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(328, 157);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Fine 2";
            // 
            // fine2ZIndicator
            // 
            this.fine2ZIndicator.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.fine2ZIndicator.Location = new System.Drawing.Point(186, 108);
            this.fine2ZIndicator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fine2ZIndicator.Name = "fine2ZIndicator";
            this.fine2ZIndicator.ReadOnly = true;
            this.fine2ZIndicator.Size = new System.Drawing.Size(118, 30);
            this.fine2ZIndicator.TabIndex = 20;
            this.toolTip.SetToolTip(this.fine2ZIndicator, "Current RCCM 2 distance sensor reading");
            // 
            // fine2YIndicator
            // 
            this.fine2YIndicator.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.fine2YIndicator.Location = new System.Drawing.Point(186, 68);
            this.fine2YIndicator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fine2YIndicator.Name = "fine2YIndicator";
            this.fine2YIndicator.ReadOnly = true;
            this.fine2YIndicator.Size = new System.Drawing.Size(118, 30);
            this.fine2YIndicator.TabIndex = 19;
            this.toolTip.SetToolTip(this.fine2YIndicator, "Current fine 2 Y position from end of travel");
            // 
            // fine2XIndicator
            // 
            this.fine2XIndicator.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.fine2XIndicator.Location = new System.Drawing.Point(186, 28);
            this.fine2XIndicator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fine2XIndicator.Name = "fine2XIndicator";
            this.fine2XIndicator.ReadOnly = true;
            this.fine2XIndicator.Size = new System.Drawing.Size(118, 30);
            this.fine2XIndicator.TabIndex = 18;
            this.toolTip.SetToolTip(this.fine2XIndicator, "Current fine 2 X position from end of travel");
            // 
            // fine2ZPos
            // 
            this.fine2ZPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.fine2ZPos.DecimalPlaces = 3;
            this.fine2ZPos.Location = new System.Drawing.Point(40, 108);
            this.fine2ZPos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fine2ZPos.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.fine2ZPos.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.fine2ZPos.Name = "fine2ZPos";
            this.fine2ZPos.Size = new System.Drawing.Size(137, 30);
            this.fine2ZPos.TabIndex = 5;
            this.toolTip.SetToolTip(this.fine2ZPos, "Press enter to send movement command to actuator");
            this.fine2ZPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fine2ZPos_KeyDown);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 111);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 25);
            this.label6.TabIndex = 4;
            this.label6.Text = "Z";
            // 
            // fine2YPos
            // 
            this.fine2YPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.fine2YPos.DecimalPlaces = 3;
            this.fine2YPos.Location = new System.Drawing.Point(40, 68);
            this.fine2YPos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fine2YPos.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.fine2YPos.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.fine2YPos.Name = "fine2YPos";
            this.fine2YPos.Size = new System.Drawing.Size(137, 30);
            this.fine2YPos.TabIndex = 3;
            this.toolTip.SetToolTip(this.fine2YPos, "Press enter to send movement command to actuator");
            this.fine2YPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fine2YPos_KeyDown);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 71);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 25);
            this.label7.TabIndex = 2;
            this.label7.Text = "Y";
            // 
            // fine2XPos
            // 
            this.fine2XPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.fine2XPos.DecimalPlaces = 3;
            this.fine2XPos.Location = new System.Drawing.Point(40, 28);
            this.fine2XPos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fine2XPos.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.fine2XPos.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.fine2XPos.Name = "fine2XPos";
            this.fine2XPos.Size = new System.Drawing.Size(137, 30);
            this.fine2XPos.TabIndex = 1;
            this.toolTip.SetToolTip(this.fine2XPos, "Press enter to send movement command to actuator");
            this.fine2XPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fine2XPos_KeyDown);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 31);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 25);
            this.label8.TabIndex = 0;
            this.label8.Text = "X";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fine1ZIndicator);
            this.groupBox2.Controls.Add(this.fine1YIndicator);
            this.groupBox2.Controls.Add(this.fine1XIndicator);
            this.groupBox2.Controls.Add(this.fine1ZPos);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.fine1YPos);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.fine1XPos);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 111);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(328, 157);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fine 1";
            // 
            // fine1ZIndicator
            // 
            this.fine1ZIndicator.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.fine1ZIndicator.Location = new System.Drawing.Point(186, 108);
            this.fine1ZIndicator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fine1ZIndicator.Name = "fine1ZIndicator";
            this.fine1ZIndicator.ReadOnly = true;
            this.fine1ZIndicator.Size = new System.Drawing.Size(118, 30);
            this.fine1ZIndicator.TabIndex = 17;
            this.toolTip.SetToolTip(this.fine1ZIndicator, "Current RCCM 1 distance sensor reading");
            // 
            // fine1YIndicator
            // 
            this.fine1YIndicator.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.fine1YIndicator.Location = new System.Drawing.Point(186, 68);
            this.fine1YIndicator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fine1YIndicator.Name = "fine1YIndicator";
            this.fine1YIndicator.ReadOnly = true;
            this.fine1YIndicator.Size = new System.Drawing.Size(118, 30);
            this.fine1YIndicator.TabIndex = 16;
            this.toolTip.SetToolTip(this.fine1YIndicator, "Current fine 1 Y position from end of travel");
            // 
            // fine1XIndicator
            // 
            this.fine1XIndicator.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.fine1XIndicator.Location = new System.Drawing.Point(186, 28);
            this.fine1XIndicator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fine1XIndicator.Name = "fine1XIndicator";
            this.fine1XIndicator.ReadOnly = true;
            this.fine1XIndicator.Size = new System.Drawing.Size(118, 30);
            this.fine1XIndicator.TabIndex = 15;
            this.toolTip.SetToolTip(this.fine1XIndicator, "Current fine 1 X position from end of travel");
            // 
            // fine1ZPos
            // 
            this.fine1ZPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.fine1ZPos.DecimalPlaces = 3;
            this.fine1ZPos.Location = new System.Drawing.Point(40, 108);
            this.fine1ZPos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fine1ZPos.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.fine1ZPos.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.fine1ZPos.Name = "fine1ZPos";
            this.fine1ZPos.Size = new System.Drawing.Size(137, 30);
            this.fine1ZPos.TabIndex = 5;
            this.toolTip.SetToolTip(this.fine1ZPos, "Press enter to send movement command to actuator");
            this.fine1ZPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fine1ZPos_KeyDown);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 111);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Z";
            // 
            // fine1YPos
            // 
            this.fine1YPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.fine1YPos.DecimalPlaces = 3;
            this.fine1YPos.Location = new System.Drawing.Point(40, 68);
            this.fine1YPos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fine1YPos.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.fine1YPos.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.fine1YPos.Name = "fine1YPos";
            this.fine1YPos.Size = new System.Drawing.Size(137, 30);
            this.fine1YPos.TabIndex = 3;
            this.toolTip.SetToolTip(this.fine1YPos, "Press enter to send movement command to actuator");
            this.fine1YPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fine1YPos_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 71);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Y";
            // 
            // fine1XPos
            // 
            this.fine1XPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.fine1XPos.DecimalPlaces = 3;
            this.fine1XPos.Location = new System.Drawing.Point(40, 28);
            this.fine1XPos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fine1XPos.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.fine1XPos.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.fine1XPos.Name = "fine1XPos";
            this.fine1XPos.Size = new System.Drawing.Size(137, 30);
            this.fine1XPos.TabIndex = 1;
            this.toolTip.SetToolTip(this.fine1XPos, "Press enter to send movement command to actuator");
            this.fine1XPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fine1XPos_KeyDown);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 31);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "X";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.coarseYIndicator);
            this.groupBox1.Controls.Add(this.coarseXIndicator);
            this.groupBox1.Controls.Add(this.coarseYPos);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.coarseXPos);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(328, 111);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Coarse";
            // 
            // coarseYIndicator
            // 
            this.coarseYIndicator.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.coarseYIndicator.Location = new System.Drawing.Point(186, 68);
            this.coarseYIndicator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.coarseYIndicator.Name = "coarseYIndicator";
            this.coarseYIndicator.ReadOnly = true;
            this.coarseYIndicator.Size = new System.Drawing.Size(118, 30);
            this.coarseYIndicator.TabIndex = 14;
            this.toolTip.SetToolTip(this.coarseYIndicator, "Current coarse Y position from end of travel");
            // 
            // coarseXIndicator
            // 
            this.coarseXIndicator.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.coarseXIndicator.Location = new System.Drawing.Point(186, 28);
            this.coarseXIndicator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.coarseXIndicator.Name = "coarseXIndicator";
            this.coarseXIndicator.ReadOnly = true;
            this.coarseXIndicator.Size = new System.Drawing.Size(118, 30);
            this.coarseXIndicator.TabIndex = 13;
            this.toolTip.SetToolTip(this.coarseXIndicator, "Current coarse X position from end of travel");
            // 
            // coarseYPos
            // 
            this.coarseYPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.coarseYPos.DecimalPlaces = 3;
            this.coarseYPos.Location = new System.Drawing.Point(40, 68);
            this.coarseYPos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.coarseYPos.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.coarseYPos.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.coarseYPos.Name = "coarseYPos";
            this.coarseYPos.Size = new System.Drawing.Size(137, 30);
            this.coarseYPos.TabIndex = 3;
            this.toolTip.SetToolTip(this.coarseYPos, "Press enter to send movement command to actuator");
            this.coarseYPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.coarseYPos_KeyDown);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 71);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y";
            // 
            // coarseXPos
            // 
            this.coarseXPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.coarseXPos.DecimalPlaces = 3;
            this.coarseXPos.Location = new System.Drawing.Point(40, 28);
            this.coarseXPos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.coarseXPos.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.coarseXPos.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.coarseXPos.Name = "coarseXPos";
            this.coarseXPos.Size = new System.Drawing.Size(137, 30);
            this.coarseXPos.TabIndex = 1;
            this.toolTip.SetToolTip(this.coarseXPos, "Press enter to send movement command to actuator");
            this.coarseXPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.coarseXPos_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            // 
            // btnWFOV2Open
            // 
            this.btnWFOV2Open.Location = new System.Drawing.Point(1018, 40);
            this.btnWFOV2Open.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnWFOV2Open.Name = "btnWFOV2Open";
            this.btnWFOV2Open.Size = new System.Drawing.Size(112, 35);
            this.btnWFOV2Open.TabIndex = 10;
            this.btnWFOV2Open.Text = "WFOV 2";
            this.btnWFOV2Open.UseVisualStyleBackColor = true;
            this.btnWFOV2Open.Click += new System.EventHandler(this.btnWFOV2Open_Click);
            // 
            // btnNFOV2Open
            // 
            this.btnNFOV2Open.Location = new System.Drawing.Point(1018, 0);
            this.btnNFOV2Open.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNFOV2Open.Name = "btnNFOV2Open";
            this.btnNFOV2Open.Size = new System.Drawing.Size(112, 35);
            this.btnNFOV2Open.TabIndex = 9;
            this.btnNFOV2Open.Text = "NFOV 2";
            this.btnNFOV2Open.UseVisualStyleBackColor = true;
            this.btnNFOV2Open.Click += new System.EventHandler(this.btnNFOV2Open_Click);
            // 
            // btnWFOV1Open
            // 
            this.btnWFOV1Open.Location = new System.Drawing.Point(903, 40);
            this.btnWFOV1Open.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnWFOV1Open.Name = "btnWFOV1Open";
            this.btnWFOV1Open.Size = new System.Drawing.Size(112, 35);
            this.btnWFOV1Open.TabIndex = 8;
            this.btnWFOV1Open.Text = "WFOV 1";
            this.btnWFOV1Open.UseVisualStyleBackColor = true;
            this.btnWFOV1Open.Click += new System.EventHandler(this.btnWFOV1Open_Click);
            // 
            // btnNFOV1Open
            // 
            this.btnNFOV1Open.Location = new System.Drawing.Point(903, 0);
            this.btnNFOV1Open.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNFOV1Open.Name = "btnNFOV1Open";
            this.btnNFOV1Open.Size = new System.Drawing.Size(112, 35);
            this.btnNFOV1Open.TabIndex = 7;
            this.btnNFOV1Open.Text = "NFOV 1";
            this.btnNFOV1Open.UseVisualStyleBackColor = true;
            this.btnNFOV1Open.Click += new System.EventHandler(this.btnNFOV1Open_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageMotion);
            this.tabControl1.Controls.Add(this.tabPageResults);
            this.tabControl1.Controls.Add(this.tabPagePressure);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(4, 90);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1138, 475);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPagePressure
            // 
            this.tabPagePressure.Controls.Add(this.chartCycles);
            this.tabPagePressure.Location = new System.Drawing.Point(4, 34);
            this.tabPagePressure.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPagePressure.Name = "tabPagePressure";
            this.tabPagePressure.Size = new System.Drawing.Size(1130, 444);
            this.tabPagePressure.TabIndex = 5;
            this.tabPagePressure.Text = "Pressure";
            this.tabPagePressure.UseVisualStyleBackColor = true;
            // 
            // chartCycles
            // 
            chartArea2.Name = "ChartArea1";
            this.chartCycles.ChartAreas.Add(chartArea2);
            this.chartCycles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartCycles.Location = new System.Drawing.Point(0, 0);
            this.chartCycles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chartCycles.Name = "chartCycles";
            series2.ChartArea = "ChartArea1";
            series2.Name = "Series1";
            this.chartCycles.Series.Add(series2);
            this.chartCycles.Size = new System.Drawing.Size(1130, 444);
            this.chartCycles.TabIndex = 8;
            this.chartCycles.Text = "chart2";
            this.toolTip.SetToolTip(this.chartCycles, "Past pressure readings");
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.ColumnCount = 1;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutMain.Controls.Add(this.panelTestControls, 0, 0);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(0, 38);
            this.tableLayoutMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.RowCount = 2;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Size = new System.Drawing.Size(1146, 570);
            this.tableLayoutMain.TabIndex = 21;
            // 
            // panelTestControls
            // 
            this.panelTestControls.Controls.Add(this.btnJogMinusZ);
            this.panelTestControls.Controls.Add(this.btnJogPlusZ);
            this.panelTestControls.Controls.Add(this.btnDisable);
            this.panelTestControls.Controls.Add(this.btnJogLeft);
            this.panelTestControls.Controls.Add(this.btnJogRight);
            this.panelTestControls.Controls.Add(this.btnJogDown);
            this.panelTestControls.Controls.Add(this.btnJogUp);
            this.panelTestControls.Controls.Add(this.btnEStop);
            this.panelTestControls.Controls.Add(this.groupBox12);
            this.panelTestControls.Controls.Add(this.btnWFOV2Open);
            this.panelTestControls.Controls.Add(this.groupBox11);
            this.panelTestControls.Controls.Add(this.btnWFOV1Open);
            this.panelTestControls.Controls.Add(this.btnNFOV2Open);
            this.panelTestControls.Controls.Add(this.btnNFOV1Open);
            this.panelTestControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTestControls.Location = new System.Drawing.Point(4, 5);
            this.panelTestControls.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelTestControls.Name = "panelTestControls";
            this.panelTestControls.Size = new System.Drawing.Size(1138, 75);
            this.panelTestControls.TabIndex = 1;
            // 
            // btnDisable
            // 
            this.btnDisable.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnDisable.AutoSize = true;
            this.btnDisable.Location = new System.Drawing.Point(650, 20);
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.Size = new System.Drawing.Size(106, 35);
            this.btnDisable.TabIndex = 27;
            this.btnDisable.Text = "DISABLE";
            this.btnDisable.UseVisualStyleBackColor = true;
            this.btnDisable.CheckedChanged += new System.EventHandler(this.btnDisable_CheckedChanged);
            // 
            // btnJogLeft
            // 
            this.btnJogLeft.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnJogLeft.AutoSize = true;
            this.btnJogLeft.BackgroundImage = global::RCCM.Properties.Resources.arrow_left;
            this.btnJogLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnJogLeft.FlatAppearance.BorderSize = 0;
            this.btnJogLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJogLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJogLeft.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnJogLeft.Location = new System.Drawing.Point(807, 25);
            this.btnJogLeft.Margin = new System.Windows.Forms.Padding(0);
            this.btnJogLeft.Name = "btnJogLeft";
            this.btnJogLeft.Padding = new System.Windows.Forms.Padding(1);
            this.btnJogLeft.Size = new System.Drawing.Size(34, 27);
            this.btnJogLeft.TabIndex = 26;
            this.btnJogLeft.Text = "-X";
            this.btnJogLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnJogLeft.UseVisualStyleBackColor = true;
            this.btnJogLeft.CheckedChanged += new System.EventHandler(this.btnJogLeft_CheckedChanged);
            // 
            // btnJogRight
            // 
            this.btnJogRight.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnJogRight.AutoSize = true;
            this.btnJogRight.BackgroundImage = global::RCCM.Properties.Resources.arrow_right;
            this.btnJogRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnJogRight.FlatAppearance.BorderSize = 0;
            this.btnJogRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJogRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJogRight.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnJogRight.Location = new System.Drawing.Point(861, 25);
            this.btnJogRight.Margin = new System.Windows.Forms.Padding(0);
            this.btnJogRight.Name = "btnJogRight";
            this.btnJogRight.Padding = new System.Windows.Forms.Padding(1);
            this.btnJogRight.Size = new System.Drawing.Size(37, 27);
            this.btnJogRight.TabIndex = 25;
            this.btnJogRight.Text = "+X";
            this.btnJogRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnJogRight.UseVisualStyleBackColor = true;
            this.btnJogRight.CheckedChanged += new System.EventHandler(this.btnJogRight_CheckedChanged);
            // 
            // btnJogDown
            // 
            this.btnJogDown.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnJogDown.AutoSize = true;
            this.btnJogDown.BackgroundImage = global::RCCM.Properties.Resources.arrow_down;
            this.btnJogDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnJogDown.FlatAppearance.BorderSize = 0;
            this.btnJogDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJogDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJogDown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnJogDown.Location = new System.Drawing.Point(832, 46);
            this.btnJogDown.Margin = new System.Windows.Forms.Padding(0);
            this.btnJogDown.Name = "btnJogDown";
            this.btnJogDown.Padding = new System.Windows.Forms.Padding(1);
            this.btnJogDown.Size = new System.Drawing.Size(37, 27);
            this.btnJogDown.TabIndex = 24;
            this.btnJogDown.Text = "+Y";
            this.btnJogDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnJogDown.UseVisualStyleBackColor = true;
            this.btnJogDown.CheckedChanged += new System.EventHandler(this.btnJogDown_CheckedChanged);
            // 
            // btnJogUp
            // 
            this.btnJogUp.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnJogUp.AutoSize = true;
            this.btnJogUp.BackgroundImage = global::RCCM.Properties.Resources.arrow_up;
            this.btnJogUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnJogUp.FlatAppearance.BorderSize = 0;
            this.btnJogUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJogUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJogUp.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnJogUp.Location = new System.Drawing.Point(833, 4);
            this.btnJogUp.Margin = new System.Windows.Forms.Padding(0);
            this.btnJogUp.Name = "btnJogUp";
            this.btnJogUp.Padding = new System.Windows.Forms.Padding(1);
            this.btnJogUp.Size = new System.Drawing.Size(34, 27);
            this.btnJogUp.TabIndex = 23;
            this.btnJogUp.Text = "-Y";
            this.btnJogUp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnJogUp.UseVisualStyleBackColor = true;
            this.btnJogUp.CheckedChanged += new System.EventHandler(this.btnJogUp_CheckedChanged);
            // 
            // btnEStop
            // 
            this.btnEStop.FlatAppearance.BorderSize = 0;
            this.btnEStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEStop.ForeColor = System.Drawing.Color.Transparent;
            this.btnEStop.Image = global::RCCM.Properties.Resources.estop;
            this.btnEStop.Location = new System.Drawing.Point(572, 0);
            this.btnEStop.Margin = new System.Windows.Forms.Padding(0);
            this.btnEStop.Name = "btnEStop";
            this.btnEStop.Size = new System.Drawing.Size(75, 75);
            this.btnEStop.TabIndex = 22;
            this.btnEStop.Text = "STOP";
            this.btnEStop.UseVisualStyleBackColor = true;
            this.btnEStop.Click += new System.EventHandler(this.btnEStop_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.textPressure);
            this.groupBox12.Controls.Add(this.label26);
            this.groupBox12.Controls.Add(this.textCycle);
            this.groupBox12.Controls.Add(this.label25);
            this.groupBox12.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox12.Location = new System.Drawing.Point(151, 0);
            this.groupBox12.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox12.Size = new System.Drawing.Size(417, 75);
            this.groupBox12.TabIndex = 21;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Test Status";
            // 
            // textPressure
            // 
            this.textPressure.Location = new System.Drawing.Point(280, 34);
            this.textPressure.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textPressure.Name = "textPressure";
            this.textPressure.ReadOnly = true;
            this.textPressure.Size = new System.Drawing.Size(126, 30);
            this.textPressure.TabIndex = 14;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(200, 37);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(90, 25);
            this.label26.TabIndex = 13;
            this.label26.Text = "Pressure";
            // 
            // textCycle
            // 
            this.textCycle.Location = new System.Drawing.Point(68, 34);
            this.textCycle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textCycle.Name = "textCycle";
            this.textCycle.ReadOnly = true;
            this.textCycle.Size = new System.Drawing.Size(121, 30);
            this.textCycle.TabIndex = 12;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(9, 37);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(62, 25);
            this.label25.TabIndex = 11;
            this.label25.Text = "Cycle";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnJogPlusZ
            // 
            this.btnJogPlusZ.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnJogPlusZ.AutoSize = true;
            this.btnJogPlusZ.BackgroundImage = global::RCCM.Properties.Resources.arrow_up;
            this.btnJogPlusZ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnJogPlusZ.FlatAppearance.BorderSize = 0;
            this.btnJogPlusZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJogPlusZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJogPlusZ.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnJogPlusZ.Location = new System.Drawing.Point(760, 11);
            this.btnJogPlusZ.Margin = new System.Windows.Forms.Padding(0);
            this.btnJogPlusZ.Name = "btnJogPlusZ";
            this.btnJogPlusZ.Padding = new System.Windows.Forms.Padding(1);
            this.btnJogPlusZ.Size = new System.Drawing.Size(37, 27);
            this.btnJogPlusZ.TabIndex = 28;
            this.btnJogPlusZ.Text = "+Z";
            this.btnJogPlusZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnJogPlusZ.UseVisualStyleBackColor = true;
            this.btnJogPlusZ.CheckedChanged += new System.EventHandler(this.btnJogPlusZ_CheckedChanged);
            // 
            // btnJogMinusZ
            // 
            this.btnJogMinusZ.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnJogMinusZ.AutoSize = true;
            this.btnJogMinusZ.BackgroundImage = global::RCCM.Properties.Resources.arrow_down;
            this.btnJogMinusZ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnJogMinusZ.FlatAppearance.BorderSize = 0;
            this.btnJogMinusZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJogMinusZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJogMinusZ.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnJogMinusZ.Location = new System.Drawing.Point(761, 40);
            this.btnJogMinusZ.Margin = new System.Windows.Forms.Padding(0);
            this.btnJogMinusZ.Name = "btnJogMinusZ";
            this.btnJogMinusZ.Padding = new System.Windows.Forms.Padding(1);
            this.btnJogMinusZ.Size = new System.Drawing.Size(34, 27);
            this.btnJogMinusZ.TabIndex = 29;
            this.btnJogMinusZ.Text = "-Z";
            this.btnJogMinusZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnJogMinusZ.UseVisualStyleBackColor = true;
            this.btnJogMinusZ.CheckedChanged += new System.EventHandler(this.btnJogMinusZ_CheckedChanged);
            // 
            // RCCMMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1146, 608);
            this.Controls.Add(this.tableLayoutMain);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "RCCMMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RCCM";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RCCMMainForm_FormClosed);
            this.Load += new System.EventHandler(this.RCCMMainForm_Load);
            this.Resize += new System.EventHandler(this.RCCMMainForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxStageSelect.ResumeLayout(false);
            this.groupBoxStageSelect.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.tabPageResults.ResumeLayout(false);
            this.tableLayoutResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartCracks)).EndInit();
            this.tabPageMotion.ResumeLayout(false);
            this.tableLayoutMotion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelView)).EndInit();
            this.panelMotionButtons.ResumeLayout(false);
            this.groupBoxMovementMode.ResumeLayout(false);
            this.groupBoxMovementMode.PerformLayout();
            this.panelMotionControls.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fine2ZPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fine2YPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fine2XPos)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fine1ZPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fine1YPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fine1XPos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coarseYPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coarseXPos)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPagePressure.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartCycles)).EndInit();
            this.tableLayoutMain.ResumeLayout(false);
            this.panelTestControls.ResumeLayout(false);
            this.panelTestControls.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ColorDialog colorDlg;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nFOV1LensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nFOV1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nFOV2ToolStripMenuItem;
        private System.Windows.Forms.Button btnStopTest;
        private System.Windows.Forms.Button btnStartTest;
        private System.Windows.Forms.Button btnPauseTest;
        private System.Windows.Forms.RadioButton radioRCCM1;
        private System.Windows.Forms.RadioButton radioRCCM2;
        private System.Windows.Forms.GroupBox groupBoxStageSelect;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageResults;
        private System.Windows.Forms.ListBox listCrackSelection;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCracks;
        private System.Windows.Forms.TabPage tabPageMotion;
        private System.Windows.Forms.GroupBox groupBoxMovementMode;
        private System.Windows.Forms.RadioButton radioMoveRel;
        private System.Windows.Forms.RadioButton radioMoveAbs;
        private System.Windows.Forms.Button btnNFOV1Open;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.BindingSource bindingSource2;
        private System.Windows.Forms.Button btnWFOV2Open;
        private System.Windows.Forms.Button btnNFOV2Open;
        private System.Windows.Forms.Button btnWFOV1Open;
        private System.Windows.Forms.RadioButton radioCoarse;
        private System.Windows.Forms.TableLayoutPanel tableLayoutMotion;
        private System.Windows.Forms.TabPage tabPagePressure;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCycles;
        private System.Windows.Forms.PictureBox panelView;
        private System.Windows.Forms.Panel panelMotionButtons;
        private System.Windows.Forms.Panel panelMotionControls;
        private System.Windows.Forms.Button btnSetHome;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.TableLayoutPanel tableLayoutResults;
        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.Panel panelTestControls;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.TextBox textPressure;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox textCycle;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox coarseYIndicator;
        private System.Windows.Forms.TextBox coarseXIndicator;
        private System.Windows.Forms.NumericUpDown coarseYPos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown coarseXPos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox fine2ZIndicator;
        private System.Windows.Forms.TextBox fine2YIndicator;
        private System.Windows.Forms.TextBox fine2XIndicator;
        private System.Windows.Forms.NumericUpDown fine2ZPos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown fine2YPos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown fine2XPos;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox fine1ZIndicator;
        private System.Windows.Forms.TextBox fine1YIndicator;
        private System.Windows.Forms.TextBox fine1XIndicator;
        private System.Windows.Forms.NumericUpDown fine1ZPos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown fine1YPos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown fine1XPos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.ToolStripMenuItem coordinateSystemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem camerasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem motorsToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem pluginsToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioNoStage;
        private System.Windows.Forms.Button btnEStop;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox btnJogLeft;
        private System.Windows.Forms.CheckBox btnJogRight;
        private System.Windows.Forms.CheckBox btnJogDown;
        private System.Windows.Forms.CheckBox btnJogUp;
        private System.Windows.Forms.CheckBox btnDisable;
        private System.Windows.Forms.CheckBox btnJogMinusZ;
        private System.Windows.Forms.CheckBox btnJogPlusZ;
    }
}

