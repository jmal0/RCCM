namespace RCCM.UI
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
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDlg = new System.Windows.Forms.ColorDialog();
            this.label24 = new System.Windows.Forms.Label();
            this.editCycleFreq = new System.Windows.Forms.NumericUpDown();
            this.radioRCCM1 = new System.Windows.Forms.RadioButton();
            this.radioRCCM2 = new System.Windows.Forms.RadioButton();
            this.groupBoxStageSelect = new System.Windows.Forms.GroupBox();
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
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.textPressure = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.textCycle = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editCycleFreq)).BeginInit();
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.pluginsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(764, 24);
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
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // nFOV1LensToolStripMenuItem
            // 
            this.nFOV1LensToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nFOV1ToolStripMenuItem,
            this.nFOV2ToolStripMenuItem});
            this.nFOV1LensToolStripMenuItem.Name = "nFOV1LensToolStripMenuItem";
            this.nFOV1LensToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.nFOV1LensToolStripMenuItem.Text = "Lens Calibration";
            // 
            // nFOV1ToolStripMenuItem
            // 
            this.nFOV1ToolStripMenuItem.Name = "nFOV1ToolStripMenuItem";
            this.nFOV1ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.nFOV1ToolStripMenuItem.Text = "NFOV 1";
            this.nFOV1ToolStripMenuItem.Click += new System.EventHandler(this.nFOV1ToolStripMenuItem_Click);
            // 
            // nFOV2ToolStripMenuItem
            // 
            this.nFOV2ToolStripMenuItem.Name = "nFOV2ToolStripMenuItem";
            this.nFOV2ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.nFOV2ToolStripMenuItem.Text = "NFOV 2";
            this.nFOV2ToolStripMenuItem.Click += new System.EventHandler(this.nFOV2ToolStripMenuItem_Click);
            // 
            // coordinateSystemToolStripMenuItem
            // 
            this.coordinateSystemToolStripMenuItem.Name = "coordinateSystemToolStripMenuItem";
            this.coordinateSystemToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.coordinateSystemToolStripMenuItem.Text = "Coordinate System";
            this.coordinateSystemToolStripMenuItem.Click += new System.EventHandler(this.coordinateSystemToolStripMenuItem_Click);
            // 
            // camerasToolStripMenuItem
            // 
            this.camerasToolStripMenuItem.Name = "camerasToolStripMenuItem";
            this.camerasToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.camerasToolStripMenuItem.Text = "Cameras";
            this.camerasToolStripMenuItem.Click += new System.EventHandler(this.camerasToolStripMenuItem_Click);
            // 
            // motorsToolStripMenuItem
            // 
            this.motorsToolStripMenuItem.Name = "motorsToolStripMenuItem";
            this.motorsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.motorsToolStripMenuItem.Text = "Motors";
            this.motorsToolStripMenuItem.Click += new System.EventHandler(this.motorsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(99, 24);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(86, 13);
            this.label24.TabIndex = 10;
            this.label24.Text = "Cycle Frequency";
            // 
            // editCycleFreq
            // 
            this.editCycleFreq.DecimalPlaces = 2;
            this.editCycleFreq.Location = new System.Drawing.Point(191, 22);
            this.editCycleFreq.Name = "editCycleFreq";
            this.editCycleFreq.Size = new System.Drawing.Size(121, 20);
            this.editCycleFreq.TabIndex = 9;
            this.editCycleFreq.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.editCycleFreq.Click += new System.EventHandler(this.editCycleFreq_Click);
            // 
            // radioRCCM1
            // 
            this.radioRCCM1.AutoSize = true;
            this.radioRCCM1.Location = new System.Drawing.Point(6, 42);
            this.radioRCCM1.Name = "radioRCCM1";
            this.radioRCCM1.Size = new System.Drawing.Size(62, 17);
            this.radioRCCM1.TabIndex = 11;
            this.radioRCCM1.Text = "RCCM1";
            this.radioRCCM1.UseVisualStyleBackColor = true;
            this.radioRCCM1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.radioButtonSuppress);
            // 
            // radioRCCM2
            // 
            this.radioRCCM2.AutoSize = true;
            this.radioRCCM2.Location = new System.Drawing.Point(6, 65);
            this.radioRCCM2.Name = "radioRCCM2";
            this.radioRCCM2.Size = new System.Drawing.Size(62, 17);
            this.radioRCCM2.TabIndex = 12;
            this.radioRCCM2.Text = "RCCM2";
            this.radioRCCM2.UseVisualStyleBackColor = true;
            this.radioRCCM2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.radioButtonSuppress);
            // 
            // groupBoxStageSelect
            // 
            this.groupBoxStageSelect.Controls.Add(this.radioCoarse);
            this.groupBoxStageSelect.Controls.Add(this.radioRCCM2);
            this.groupBoxStageSelect.Controls.Add(this.radioRCCM1);
            this.groupBoxStageSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxStageSelect.Location = new System.Drawing.Point(0, 0);
            this.groupBoxStageSelect.Name = "groupBoxStageSelect";
            this.groupBoxStageSelect.Size = new System.Drawing.Size(106, 89);
            this.groupBoxStageSelect.TabIndex = 19;
            this.groupBoxStageSelect.TabStop = false;
            this.groupBoxStageSelect.Text = "Stage Selection";
            this.toolTip.SetToolTip(this.groupBoxStageSelect, "Select which set of actuators is moved by pressing arrow keys");
            // 
            // radioCoarse
            // 
            this.radioCoarse.AutoSize = true;
            this.radioCoarse.Checked = true;
            this.radioCoarse.Location = new System.Drawing.Point(6, 19);
            this.radioCoarse.Name = "radioCoarse";
            this.radioCoarse.Size = new System.Drawing.Size(58, 17);
            this.radioCoarse.TabIndex = 13;
            this.radioCoarse.TabStop = true;
            this.radioCoarse.Text = "Coarse";
            this.radioCoarse.UseVisualStyleBackColor = true;
            this.radioCoarse.KeyDown += new System.Windows.Forms.KeyEventHandler(this.radioButtonSuppress);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.btnStartTest);
            this.groupBox11.Controls.Add(this.editCycleFreq);
            this.groupBox11.Controls.Add(this.label24);
            this.groupBox11.Controls.Add(this.btnPauseTest);
            this.groupBox11.Controls.Add(this.btnStopTest);
            this.groupBox11.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox11.Location = new System.Drawing.Point(0, 0);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(318, 49);
            this.groupBox11.TabIndex = 19;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Test Controls";
            // 
            // btnStartTest
            // 
            this.btnStartTest.Image = global::RCCM.Properties.Resources.play;
            this.btnStartTest.Location = new System.Drawing.Point(6, 17);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(26, 26);
            this.btnStartTest.TabIndex = 7;
            this.toolTip.SetToolTip(this.btnStartTest, "Start cycle counting");
            this.btnStartTest.UseVisualStyleBackColor = true;
            this.btnStartTest.Click += new System.EventHandler(this.btnStartTest_Click);
            // 
            // btnPauseTest
            // 
            this.btnPauseTest.Image = global::RCCM.Properties.Resources.pause;
            this.btnPauseTest.Location = new System.Drawing.Point(35, 17);
            this.btnPauseTest.Name = "btnPauseTest";
            this.btnPauseTest.Size = new System.Drawing.Size(26, 26);
            this.btnPauseTest.TabIndex = 6;
            this.toolTip.SetToolTip(this.btnPauseTest, "Pause cycle counting");
            this.btnPauseTest.UseVisualStyleBackColor = true;
            this.btnPauseTest.Click += new System.EventHandler(this.btnPauseTest_Click);
            // 
            // btnStopTest
            // 
            this.btnStopTest.Image = global::RCCM.Properties.Resources.stop;
            this.btnStopTest.Location = new System.Drawing.Point(67, 17);
            this.btnStopTest.Name = "btnStopTest";
            this.btnStopTest.Size = new System.Drawing.Size(26, 26);
            this.btnStopTest.TabIndex = 8;
            this.toolTip.SetToolTip(this.btnStopTest, "Stop cycle counting and save all current test data");
            this.btnStopTest.UseVisualStyleBackColor = true;
            this.btnStopTest.Click += new System.EventHandler(this.btnStopTest_Click);
            // 
            // tabPageResults
            // 
            this.tabPageResults.Controls.Add(this.tableLayoutResults);
            this.tabPageResults.Location = new System.Drawing.Point(4, 22);
            this.tabPageResults.Name = "tabPageResults";
            this.tabPageResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageResults.Size = new System.Drawing.Size(750, 284);
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
            this.tableLayoutResults.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutResults.Name = "tableLayoutResults";
            this.tableLayoutResults.RowCount = 1;
            this.tableLayoutResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutResults.Size = new System.Drawing.Size(744, 278);
            this.tableLayoutResults.TabIndex = 9;
            // 
            // chartCracks
            // 
            chartArea1.Name = "ChartArea1";
            this.chartCracks.ChartAreas.Add(chartArea1);
            this.chartCracks.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartCracks.Legends.Add(legend1);
            this.chartCracks.Location = new System.Drawing.Point(3, 3);
            this.chartCracks.Name = "chartCracks";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartCracks.Series.Add(series1);
            this.chartCracks.Size = new System.Drawing.Size(589, 272);
            this.chartCracks.TabIndex = 0;
            this.chartCracks.Text = "chart1";
            // 
            // listCrackSelection
            // 
            this.listCrackSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCrackSelection.FormattingEnabled = true;
            this.listCrackSelection.Location = new System.Drawing.Point(598, 3);
            this.listCrackSelection.Name = "listCrackSelection";
            this.listCrackSelection.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listCrackSelection.Size = new System.Drawing.Size(143, 272);
            this.listCrackSelection.TabIndex = 8;
            this.toolTip.SetToolTip(this.listCrackSelection, "Click to higlight and select which cracks are plotted");
            this.listCrackSelection.SelectedIndexChanged += new System.EventHandler(this.listCracksSelection_SelectedIndexChanged);
            // 
            // tabPageMotion
            // 
            this.tabPageMotion.Controls.Add(this.tableLayoutMotion);
            this.tabPageMotion.Location = new System.Drawing.Point(4, 22);
            this.tabPageMotion.Name = "tabPageMotion";
            this.tabPageMotion.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMotion.Size = new System.Drawing.Size(750, 284);
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
            this.tableLayoutMotion.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutMotion.Name = "tableLayoutMotion";
            this.tableLayoutMotion.RowCount = 1;
            this.tableLayoutMotion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMotion.Size = new System.Drawing.Size(744, 278);
            this.tableLayoutMotion.TabIndex = 22;
            // 
            // panelView
            // 
            this.panelView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelView.Location = new System.Drawing.Point(226, 3);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(403, 272);
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
            this.panelMotionButtons.Location = new System.Drawing.Point(635, 3);
            this.panelMotionButtons.Name = "panelMotionButtons";
            this.panelMotionButtons.Size = new System.Drawing.Size(106, 272);
            this.panelMotionButtons.TabIndex = 23;
            // 
            // btnSetHome
            // 
            this.btnSetHome.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSetHome.Location = new System.Drawing.Point(0, 182);
            this.btnSetHome.Name = "btnSetHome";
            this.btnSetHome.Size = new System.Drawing.Size(106, 23);
            this.btnSetHome.TabIndex = 22;
            this.btnSetHome.Text = "Set as Home";
            this.toolTip.SetToolTip(this.btnSetHome, "Save current actuator positions as home position");
            this.btnSetHome.UseVisualStyleBackColor = true;
            this.btnSetHome.Click += new System.EventHandler(this.btnSetHome_Click);
            // 
            // btnHome
            // 
            this.btnHome.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHome.Location = new System.Drawing.Point(0, 159);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(106, 23);
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
            this.groupBoxMovementMode.Location = new System.Drawing.Point(0, 89);
            this.groupBoxMovementMode.Name = "groupBoxMovementMode";
            this.groupBoxMovementMode.Size = new System.Drawing.Size(106, 70);
            this.groupBoxMovementMode.TabIndex = 4;
            this.groupBoxMovementMode.TabStop = false;
            this.groupBoxMovementMode.Text = "Mode";
            this.toolTip.SetToolTip(this.groupBoxMovementMode, "Select movement mode used by position controls");
            // 
            // radioMoveRel
            // 
            this.radioMoveRel.AutoSize = true;
            this.radioMoveRel.Location = new System.Drawing.Point(6, 42);
            this.radioMoveRel.Name = "radioMoveRel";
            this.radioMoveRel.Size = new System.Drawing.Size(64, 17);
            this.radioMoveRel.TabIndex = 13;
            this.radioMoveRel.Text = "Relative";
            this.toolTip.SetToolTip(this.radioMoveRel, "Move a specified distance from the current position");
            this.radioMoveRel.UseVisualStyleBackColor = true;
            this.radioMoveRel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.radioButtonSuppress);
            // 
            // radioMoveAbs
            // 
            this.radioMoveAbs.AutoSize = true;
            this.radioMoveAbs.Checked = true;
            this.radioMoveAbs.Location = new System.Drawing.Point(6, 19);
            this.radioMoveAbs.Name = "radioMoveAbs";
            this.radioMoveAbs.Size = new System.Drawing.Size(66, 17);
            this.radioMoveAbs.TabIndex = 12;
            this.radioMoveAbs.TabStop = true;
            this.radioMoveAbs.Text = "Absolute";
            this.toolTip.SetToolTip(this.radioMoveAbs, "Move to a position a specified distance from the end of travel");
            this.radioMoveAbs.UseVisualStyleBackColor = true;
            this.radioMoveAbs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.radioButtonSuppress);
            // 
            // panelMotionControls
            // 
            this.panelMotionControls.Controls.Add(this.groupBox3);
            this.panelMotionControls.Controls.Add(this.groupBox2);
            this.panelMotionControls.Controls.Add(this.groupBox1);
            this.panelMotionControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMotionControls.Location = new System.Drawing.Point(3, 3);
            this.panelMotionControls.Name = "panelMotionControls";
            this.panelMotionControls.Size = new System.Drawing.Size(217, 272);
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
            this.groupBox3.Location = new System.Drawing.Point(0, 174);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(217, 102);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Fine 2";
            // 
            // fine2ZIndicator
            // 
            this.fine2ZIndicator.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.fine2ZIndicator.Location = new System.Drawing.Point(123, 70);
            this.fine2ZIndicator.Name = "fine2ZIndicator";
            this.fine2ZIndicator.ReadOnly = true;
            this.fine2ZIndicator.Size = new System.Drawing.Size(80, 20);
            this.fine2ZIndicator.TabIndex = 20;
            this.toolTip.SetToolTip(this.fine2ZIndicator, "Current RCCM 2 distance sensor reading");
            // 
            // fine2YIndicator
            // 
            this.fine2YIndicator.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.fine2YIndicator.Location = new System.Drawing.Point(123, 44);
            this.fine2YIndicator.Name = "fine2YIndicator";
            this.fine2YIndicator.ReadOnly = true;
            this.fine2YIndicator.Size = new System.Drawing.Size(80, 20);
            this.fine2YIndicator.TabIndex = 19;
            this.toolTip.SetToolTip(this.fine2YIndicator, "Current fine 2 Y position from end of travel");
            // 
            // fine2XIndicator
            // 
            this.fine2XIndicator.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.fine2XIndicator.Location = new System.Drawing.Point(123, 18);
            this.fine2XIndicator.Name = "fine2XIndicator";
            this.fine2XIndicator.ReadOnly = true;
            this.fine2XIndicator.Size = new System.Drawing.Size(80, 20);
            this.fine2XIndicator.TabIndex = 18;
            this.toolTip.SetToolTip(this.fine2XIndicator, "Current fine 2 X position from end of travel");
            // 
            // fine2ZPos
            // 
            this.fine2ZPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.fine2ZPos.DecimalPlaces = 3;
            this.fine2ZPos.Location = new System.Drawing.Point(27, 70);
            this.fine2ZPos.Name = "fine2ZPos";
            this.fine2ZPos.Size = new System.Drawing.Size(90, 20);
            this.fine2ZPos.TabIndex = 5;
            this.toolTip.SetToolTip(this.fine2ZPos, "Press enter to send movement command to actuator");
            this.fine2ZPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fine2ZPos_KeyDown);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Z";
            // 
            // fine2YPos
            // 
            this.fine2YPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.fine2YPos.DecimalPlaces = 3;
            this.fine2YPos.Location = new System.Drawing.Point(27, 44);
            this.fine2YPos.Name = "fine2YPos";
            this.fine2YPos.Size = new System.Drawing.Size(90, 20);
            this.fine2YPos.TabIndex = 3;
            this.toolTip.SetToolTip(this.fine2YPos, "Press enter to send movement command to actuator");
            this.fine2YPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fine2YPos_KeyDown);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Y";
            // 
            // fine2XPos
            // 
            this.fine2XPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.fine2XPos.DecimalPlaces = 3;
            this.fine2XPos.Location = new System.Drawing.Point(27, 18);
            this.fine2XPos.Name = "fine2XPos";
            this.fine2XPos.Size = new System.Drawing.Size(90, 20);
            this.fine2XPos.TabIndex = 1;
            this.toolTip.SetToolTip(this.fine2XPos, "Press enter to send movement command to actuator");
            this.fine2XPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fine2XPos_KeyDown);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
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
            this.groupBox2.Location = new System.Drawing.Point(0, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(217, 102);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fine 1";
            // 
            // fine1ZIndicator
            // 
            this.fine1ZIndicator.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.fine1ZIndicator.Location = new System.Drawing.Point(123, 70);
            this.fine1ZIndicator.Name = "fine1ZIndicator";
            this.fine1ZIndicator.ReadOnly = true;
            this.fine1ZIndicator.Size = new System.Drawing.Size(80, 20);
            this.fine1ZIndicator.TabIndex = 17;
            this.toolTip.SetToolTip(this.fine1ZIndicator, "Current RCCM 1 distance sensor reading");
            // 
            // fine1YIndicator
            // 
            this.fine1YIndicator.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.fine1YIndicator.Location = new System.Drawing.Point(123, 44);
            this.fine1YIndicator.Name = "fine1YIndicator";
            this.fine1YIndicator.ReadOnly = true;
            this.fine1YIndicator.Size = new System.Drawing.Size(80, 20);
            this.fine1YIndicator.TabIndex = 16;
            this.toolTip.SetToolTip(this.fine1YIndicator, "Current fine 1 Y position from end of travel");
            // 
            // fine1XIndicator
            // 
            this.fine1XIndicator.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.fine1XIndicator.Location = new System.Drawing.Point(123, 18);
            this.fine1XIndicator.Name = "fine1XIndicator";
            this.fine1XIndicator.ReadOnly = true;
            this.fine1XIndicator.Size = new System.Drawing.Size(80, 20);
            this.fine1XIndicator.TabIndex = 15;
            this.toolTip.SetToolTip(this.fine1XIndicator, "Current fine 1 X position from end of travel");
            // 
            // fine1ZPos
            // 
            this.fine1ZPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.fine1ZPos.DecimalPlaces = 3;
            this.fine1ZPos.Location = new System.Drawing.Point(27, 70);
            this.fine1ZPos.Name = "fine1ZPos";
            this.fine1ZPos.Size = new System.Drawing.Size(90, 20);
            this.fine1ZPos.TabIndex = 5;
            this.toolTip.SetToolTip(this.fine1ZPos, "Press enter to send movement command to actuator");
            this.fine1ZPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fine1ZPos_KeyDown);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Z";
            // 
            // fine1YPos
            // 
            this.fine1YPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.fine1YPos.DecimalPlaces = 3;
            this.fine1YPos.Location = new System.Drawing.Point(27, 44);
            this.fine1YPos.Name = "fine1YPos";
            this.fine1YPos.Size = new System.Drawing.Size(90, 20);
            this.fine1YPos.TabIndex = 3;
            this.toolTip.SetToolTip(this.fine1YPos, "Press enter to send movement command to actuator");
            this.fine1YPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fine1YPos_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Y";
            // 
            // fine1XPos
            // 
            this.fine1XPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.fine1XPos.DecimalPlaces = 3;
            this.fine1XPos.Location = new System.Drawing.Point(27, 18);
            this.fine1XPos.Name = "fine1XPos";
            this.fine1XPos.Size = new System.Drawing.Size(90, 20);
            this.fine1XPos.TabIndex = 1;
            this.toolTip.SetToolTip(this.fine1XPos, "Press enter to send movement command to actuator");
            this.fine1XPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fine1XPos_KeyDown);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
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
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 72);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Coarse";
            // 
            // coarseYIndicator
            // 
            this.coarseYIndicator.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.coarseYIndicator.Location = new System.Drawing.Point(123, 44);
            this.coarseYIndicator.Name = "coarseYIndicator";
            this.coarseYIndicator.ReadOnly = true;
            this.coarseYIndicator.Size = new System.Drawing.Size(80, 20);
            this.coarseYIndicator.TabIndex = 14;
            this.toolTip.SetToolTip(this.coarseYIndicator, "Current coarse Y position from end of travel");
            // 
            // coarseXIndicator
            // 
            this.coarseXIndicator.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.coarseXIndicator.Location = new System.Drawing.Point(123, 18);
            this.coarseXIndicator.Name = "coarseXIndicator";
            this.coarseXIndicator.ReadOnly = true;
            this.coarseXIndicator.Size = new System.Drawing.Size(80, 20);
            this.coarseXIndicator.TabIndex = 13;
            this.toolTip.SetToolTip(this.coarseXIndicator, "Current coarse X position from end of travel");
            // 
            // coarseYPos
            // 
            this.coarseYPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.coarseYPos.DecimalPlaces = 3;
            this.coarseYPos.Location = new System.Drawing.Point(27, 44);
            this.coarseYPos.Name = "coarseYPos";
            this.coarseYPos.Size = new System.Drawing.Size(90, 20);
            this.coarseYPos.TabIndex = 3;
            this.toolTip.SetToolTip(this.coarseYPos, "Press enter to send movement command to actuator");
            this.coarseYPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.coarseYPos_KeyDown);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y";
            // 
            // coarseXPos
            // 
            this.coarseXPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.coarseXPos.DecimalPlaces = 3;
            this.coarseXPos.Location = new System.Drawing.Point(27, 18);
            this.coarseXPos.Name = "coarseXPos";
            this.coarseXPos.Size = new System.Drawing.Size(90, 20);
            this.coarseXPos.TabIndex = 1;
            this.toolTip.SetToolTip(this.coarseXPos, "Press enter to send movement command to actuator");
            this.coarseXPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.coarseXPos_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            // 
            // btnWFOV2Open
            // 
            this.btnWFOV2Open.Location = new System.Drawing.Point(679, 26);
            this.btnWFOV2Open.Name = "btnWFOV2Open";
            this.btnWFOV2Open.Size = new System.Drawing.Size(75, 23);
            this.btnWFOV2Open.TabIndex = 10;
            this.btnWFOV2Open.Text = "WFOV 2";
            this.btnWFOV2Open.UseVisualStyleBackColor = true;
            this.btnWFOV2Open.Click += new System.EventHandler(this.btnWFOV2Open_Click);
            // 
            // btnNFOV2Open
            // 
            this.btnNFOV2Open.Location = new System.Drawing.Point(679, 0);
            this.btnNFOV2Open.Name = "btnNFOV2Open";
            this.btnNFOV2Open.Size = new System.Drawing.Size(75, 23);
            this.btnNFOV2Open.TabIndex = 9;
            this.btnNFOV2Open.Text = "NFOV 2";
            this.btnNFOV2Open.UseVisualStyleBackColor = true;
            this.btnNFOV2Open.Click += new System.EventHandler(this.btnNFOV2Open_Click);
            // 
            // btnWFOV1Open
            // 
            this.btnWFOV1Open.Location = new System.Drawing.Point(602, 26);
            this.btnWFOV1Open.Name = "btnWFOV1Open";
            this.btnWFOV1Open.Size = new System.Drawing.Size(75, 23);
            this.btnWFOV1Open.TabIndex = 8;
            this.btnWFOV1Open.Text = "WFOV 1";
            this.btnWFOV1Open.UseVisualStyleBackColor = true;
            this.btnWFOV1Open.Click += new System.EventHandler(this.btnWFOV1Open_Click);
            // 
            // btnNFOV1Open
            // 
            this.btnNFOV1Open.Location = new System.Drawing.Point(602, 0);
            this.btnNFOV1Open.Name = "btnNFOV1Open";
            this.btnNFOV1Open.Size = new System.Drawing.Size(75, 23);
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
            this.tabControl1.Location = new System.Drawing.Point(3, 58);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(758, 310);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPagePressure
            // 
            this.tabPagePressure.Controls.Add(this.chartCycles);
            this.tabPagePressure.Location = new System.Drawing.Point(4, 22);
            this.tabPagePressure.Name = "tabPagePressure";
            this.tabPagePressure.Size = new System.Drawing.Size(750, 284);
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
            this.chartCycles.Name = "chartCycles";
            series2.ChartArea = "ChartArea1";
            series2.Name = "Series1";
            this.chartCycles.Series.Add(series2);
            this.chartCycles.Size = new System.Drawing.Size(750, 284);
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
            this.tableLayoutMain.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.RowCount = 2;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Size = new System.Drawing.Size(764, 371);
            this.tableLayoutMain.TabIndex = 21;
            // 
            // panelTestControls
            // 
            this.panelTestControls.Controls.Add(this.groupBox12);
            this.panelTestControls.Controls.Add(this.btnWFOV2Open);
            this.panelTestControls.Controls.Add(this.groupBox11);
            this.panelTestControls.Controls.Add(this.btnWFOV1Open);
            this.panelTestControls.Controls.Add(this.btnNFOV2Open);
            this.panelTestControls.Controls.Add(this.btnNFOV1Open);
            this.panelTestControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTestControls.Location = new System.Drawing.Point(3, 3);
            this.panelTestControls.Name = "panelTestControls";
            this.panelTestControls.Size = new System.Drawing.Size(758, 49);
            this.panelTestControls.TabIndex = 1;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.textPressure);
            this.groupBox12.Controls.Add(this.label26);
            this.groupBox12.Controls.Add(this.textCycle);
            this.groupBox12.Controls.Add(this.label25);
            this.groupBox12.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox12.Location = new System.Drawing.Point(318, 0);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(278, 49);
            this.groupBox12.TabIndex = 21;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Test Status";
            // 
            // textPressure
            // 
            this.textPressure.Location = new System.Drawing.Point(187, 22);
            this.textPressure.Name = "textPressure";
            this.textPressure.ReadOnly = true;
            this.textPressure.Size = new System.Drawing.Size(85, 20);
            this.textPressure.TabIndex = 14;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(133, 24);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(48, 13);
            this.label26.TabIndex = 13;
            this.label26.Text = "Pressure";
            // 
            // textCycle
            // 
            this.textCycle.Location = new System.Drawing.Point(45, 22);
            this.textCycle.Name = "textCycle";
            this.textCycle.ReadOnly = true;
            this.textCycle.Size = new System.Drawing.Size(82, 20);
            this.textCycle.TabIndex = 12;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 24);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(33, 13);
            this.label25.TabIndex = 11;
            this.label25.Text = "Cycle";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // pluginsToolStripMenuItem
            // 
            this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
            this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.pluginsToolStripMenuItem.Text = "Plugins";
            // 
            // RCCMMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(764, 395);
            this.Controls.Add(this.tableLayoutMain);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RCCMMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RCCM";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RCCMMainForm_FormClosed);
            this.Load += new System.EventHandler(this.RCCMMainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RCCMMainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RCCMMainForm_KeyUp);
            this.Resize += new System.EventHandler(this.RCCMMainForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editCycleFreq)).EndInit();
            this.groupBoxStageSelect.ResumeLayout(false);
            this.groupBoxStageSelect.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
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
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.NumericUpDown editCycleFreq;
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
    }
}

