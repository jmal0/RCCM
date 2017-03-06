namespace RCCM.UI
{
    partial class WFOVViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WFOVViewForm));
            this.wfovContainer = new TIS.Imaging.ICImagingControl();
            this.panelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.panelWfovView = new System.Windows.Forms.Panel();
            this.btnWfovStop = new System.Windows.Forms.Button();
            this.btnFocus = new System.Windows.Forms.Button();
            this.btnWfovProperties = new System.Windows.Forms.Button();
            this.btnWfovRecord = new System.Windows.Forms.Button();
            this.btnWfovSnap = new System.Windows.Forms.Button();
            this.btnWfovStart = new System.Windows.Forms.Button();
            this.panelSliders = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.textFocus = new System.Windows.Forms.TextBox();
            this.textZoom = new System.Windows.Forms.TextBox();
            this.sliderFocus = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            this.sliderZoom = new System.Windows.Forms.TrackBar();
            this.panelMeasurement = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listPoints = new System.Windows.Forms.ListBox();
            this.btnGotoPoint = new System.Windows.Forms.Button();
            this.btnDeletePoint = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkCrosshair = new System.Windows.Forms.CheckBox();
            this.btnCrosshairMeasure = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnEditSequence = new System.Windows.Forms.Button();
            this.listMeasurements = new System.Windows.Forms.ListBox();
            this.btnNewSequence = new System.Windows.Forms.Button();
            this.btnDeleteSequence = new System.Windows.Forms.Button();
            this.btnSaveCrack = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.wfovContainer)).BeginInit();
            this.panelLayout.SuspendLayout();
            this.panelWfovView.SuspendLayout();
            this.panelSliders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderFocus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderZoom)).BeginInit();
            this.panelMeasurement.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // wfovContainer
            // 
            this.wfovContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wfovContainer.AutoSize = true;
            this.wfovContainer.BackColor = System.Drawing.Color.White;
            this.wfovContainer.DeviceListChangedExecutionMode = TIS.Imaging.EventExecutionMode.Invoke;
            this.wfovContainer.DeviceLostExecutionMode = TIS.Imaging.EventExecutionMode.AsyncInvoke;
            this.wfovContainer.ImageAvailableExecutionMode = TIS.Imaging.EventExecutionMode.MultiThreaded;
            this.wfovContainer.LiveDisplayPosition = new System.Drawing.Point(0, 0);
            this.wfovContainer.Location = new System.Drawing.Point(0, 0);
            this.wfovContainer.Margin = new System.Windows.Forms.Padding(0);
            this.wfovContainer.MaximumSize = new System.Drawing.Size(1280, 960);
            this.wfovContainer.MinimumSize = new System.Drawing.Size(640, 480);
            this.wfovContainer.Name = "wfovContainer";
            this.wfovContainer.Size = new System.Drawing.Size(644, 481);
            this.wfovContainer.TabIndex = 2;
            // 
            // panelLayout
            // 
            this.panelLayout.AutoSize = true;
            this.panelLayout.ColumnCount = 3;
            this.panelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.panelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.panelLayout.Controls.Add(this.panelWfovView, 0, 0);
            this.panelLayout.Controls.Add(this.panelSliders, 1, 0);
            this.panelLayout.Controls.Add(this.panelMeasurement, 2, 0);
            this.panelLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLayout.Location = new System.Drawing.Point(0, 0);
            this.panelLayout.Name = "panelLayout";
            this.panelLayout.RowCount = 1;
            this.panelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelLayout.Size = new System.Drawing.Size(888, 514);
            this.panelLayout.TabIndex = 3;
            // 
            // panelWfovView
            // 
            this.panelWfovView.AutoSize = true;
            this.panelWfovView.Controls.Add(this.btnWfovStop);
            this.panelWfovView.Controls.Add(this.btnFocus);
            this.panelWfovView.Controls.Add(this.btnWfovProperties);
            this.panelWfovView.Controls.Add(this.btnWfovRecord);
            this.panelWfovView.Controls.Add(this.btnWfovSnap);
            this.panelWfovView.Controls.Add(this.btnWfovStart);
            this.panelWfovView.Controls.Add(this.wfovContainer);
            this.panelWfovView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWfovView.Location = new System.Drawing.Point(3, 3);
            this.panelWfovView.MinimumSize = new System.Drawing.Size(640, 508);
            this.panelWfovView.Name = "panelWfovView";
            this.panelWfovView.Size = new System.Drawing.Size(644, 508);
            this.panelWfovView.TabIndex = 4;
            // 
            // btnWfovStop
            // 
            this.btnWfovStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWfovStop.Enabled = false;
            this.btnWfovStop.Image = global::RCCM.Properties.Resources.stop;
            this.btnWfovStop.Location = new System.Drawing.Point(32, 483);
            this.btnWfovStop.Name = "btnWfovStop";
            this.btnWfovStop.Size = new System.Drawing.Size(26, 26);
            this.btnWfovStop.TabIndex = 24;
            this.btnWfovStop.UseVisualStyleBackColor = true;
            this.btnWfovStop.Click += new System.EventHandler(this.btnWfovStop_Click);
            // 
            // btnFocus
            // 
            this.btnFocus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFocus.Enabled = false;
            this.btnFocus.Location = new System.Drawing.Point(218, 483);
            this.btnFocus.Name = "btnFocus";
            this.btnFocus.Size = new System.Drawing.Size(75, 26);
            this.btnFocus.TabIndex = 23;
            this.btnFocus.Text = "Autofocus";
            this.btnFocus.UseVisualStyleBackColor = true;
            this.btnFocus.Click += new System.EventHandler(this.btnFocus_Click);
            // 
            // btnWfovProperties
            // 
            this.btnWfovProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWfovProperties.Enabled = false;
            this.btnWfovProperties.Location = new System.Drawing.Point(137, 483);
            this.btnWfovProperties.Name = "btnWfovProperties";
            this.btnWfovProperties.Size = new System.Drawing.Size(75, 26);
            this.btnWfovProperties.TabIndex = 20;
            this.btnWfovProperties.Text = "Properties";
            this.btnWfovProperties.UseVisualStyleBackColor = true;
            this.btnWfovProperties.Click += new System.EventHandler(this.btnWfovProperties_Click);
            // 
            // btnWfovRecord
            // 
            this.btnWfovRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWfovRecord.BackColor = System.Drawing.Color.Transparent;
            this.btnWfovRecord.Enabled = false;
            this.btnWfovRecord.Image = global::RCCM.Properties.Resources.record;
            this.btnWfovRecord.Location = new System.Drawing.Point(105, 483);
            this.btnWfovRecord.Name = "btnWfovRecord";
            this.btnWfovRecord.Size = new System.Drawing.Size(26, 26);
            this.btnWfovRecord.TabIndex = 22;
            this.btnWfovRecord.UseVisualStyleBackColor = false;
            this.btnWfovRecord.Click += new System.EventHandler(this.btnWfovRecord_Click);
            // 
            // btnWfovSnap
            // 
            this.btnWfovSnap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWfovSnap.Enabled = false;
            this.btnWfovSnap.Image = global::RCCM.Properties.Resources.snap;
            this.btnWfovSnap.Location = new System.Drawing.Point(64, 483);
            this.btnWfovSnap.Name = "btnWfovSnap";
            this.btnWfovSnap.Size = new System.Drawing.Size(35, 26);
            this.btnWfovSnap.TabIndex = 21;
            this.btnWfovSnap.UseVisualStyleBackColor = true;
            this.btnWfovSnap.Click += new System.EventHandler(this.btnWfovSnap_Click);
            // 
            // btnWfovStart
            // 
            this.btnWfovStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWfovStart.BackgroundImage = global::RCCM.Properties.Resources.play;
            this.btnWfovStart.Enabled = false;
            this.btnWfovStart.Image = global::RCCM.Properties.Resources.play;
            this.btnWfovStart.Location = new System.Drawing.Point(0, 483);
            this.btnWfovStart.Name = "btnWfovStart";
            this.btnWfovStart.Size = new System.Drawing.Size(26, 26);
            this.btnWfovStart.TabIndex = 19;
            this.btnWfovStart.UseVisualStyleBackColor = true;
            this.btnWfovStart.Click += new System.EventHandler(this.btnWfovStart_Click);
            // 
            // panelSliders
            // 
            this.panelSliders.AutoSize = true;
            this.panelSliders.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelSliders.Controls.Add(this.label12);
            this.panelSliders.Controls.Add(this.textFocus);
            this.panelSliders.Controls.Add(this.textZoom);
            this.panelSliders.Controls.Add(this.sliderFocus);
            this.panelSliders.Controls.Add(this.label11);
            this.panelSliders.Controls.Add(this.sliderZoom);
            this.panelSliders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSliders.Location = new System.Drawing.Point(653, 3);
            this.panelSliders.Name = "panelSliders";
            this.panelSliders.Size = new System.Drawing.Size(99, 508);
            this.panelSliders.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(53, 0);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Focus";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textFocus
            // 
            this.textFocus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textFocus.Enabled = false;
            this.textFocus.Location = new System.Drawing.Point(51, 484);
            this.textFocus.Name = "textFocus";
            this.textFocus.Size = new System.Drawing.Size(45, 20);
            this.textFocus.TabIndex = 19;
            this.textFocus.Text = "0";
            this.textFocus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textZoom
            // 
            this.textZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textZoom.Enabled = false;
            this.textZoom.Location = new System.Drawing.Point(3, 484);
            this.textZoom.Name = "textZoom";
            this.textZoom.Size = new System.Drawing.Size(45, 20);
            this.textZoom.TabIndex = 16;
            this.textZoom.Text = "0";
            this.textZoom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sliderFocus
            // 
            this.sliderFocus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderFocus.Enabled = false;
            this.sliderFocus.Location = new System.Drawing.Point(51, 16);
            this.sliderFocus.Maximum = 100;
            this.sliderFocus.Name = "sliderFocus";
            this.sliderFocus.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sliderFocus.Size = new System.Drawing.Size(45, 464);
            this.sliderFocus.TabIndex = 18;
            this.sliderFocus.TickStyle = System.Windows.Forms.TickStyle.None;
            this.sliderFocus.Scroll += new System.EventHandler(this.sliderFocus_Scroll);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Zoom";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sliderZoom
            // 
            this.sliderZoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sliderZoom.Enabled = false;
            this.sliderZoom.Location = new System.Drawing.Point(3, 16);
            this.sliderZoom.Maximum = 100;
            this.sliderZoom.Name = "sliderZoom";
            this.sliderZoom.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sliderZoom.Size = new System.Drawing.Size(45, 464);
            this.sliderZoom.TabIndex = 15;
            this.sliderZoom.TickStyle = System.Windows.Forms.TickStyle.None;
            this.sliderZoom.Scroll += new System.EventHandler(this.sliderZoom_Scroll);
            // 
            // panelMeasurement
            // 
            this.panelMeasurement.Controls.Add(this.groupBox2);
            this.panelMeasurement.Controls.Add(this.groupBox1);
            this.panelMeasurement.Controls.Add(this.groupBox9);
            this.panelMeasurement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMeasurement.Location = new System.Drawing.Point(758, 3);
            this.panelMeasurement.Name = "panelMeasurement";
            this.panelMeasurement.Size = new System.Drawing.Size(127, 508);
            this.panelMeasurement.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.listPoints);
            this.groupBox2.Controls.Add(this.btnGotoPoint);
            this.groupBox2.Controls.Add(this.btnDeletePoint);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 329);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.groupBox2.Size = new System.Drawing.Size(127, 191);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Points";
            // 
            // listPoints
            // 
            this.listPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.listPoints.FormattingEnabled = true;
            this.listPoints.Location = new System.Drawing.Point(6, 16);
            this.listPoints.Name = "listPoints";
            this.listPoints.ScrollAlwaysVisible = true;
            this.listPoints.Size = new System.Drawing.Size(115, 95);
            this.listPoints.TabIndex = 24;
            this.listPoints.SelectedIndexChanged += new System.EventHandler(this.listPoints_SelectedIndexChanged);
            // 
            // btnGotoPoint
            // 
            this.btnGotoPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGotoPoint.Location = new System.Drawing.Point(6, 117);
            this.btnGotoPoint.Name = "btnGotoPoint";
            this.btnGotoPoint.Size = new System.Drawing.Size(115, 26);
            this.btnGotoPoint.TabIndex = 34;
            this.btnGotoPoint.Text = "Go to point";
            this.btnGotoPoint.UseVisualStyleBackColor = true;
            this.btnGotoPoint.Click += new System.EventHandler(this.btnGotoPoint_Click);
            // 
            // btnDeletePoint
            // 
            this.btnDeletePoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeletePoint.Location = new System.Drawing.Point(6, 149);
            this.btnDeletePoint.Name = "btnDeletePoint";
            this.btnDeletePoint.Size = new System.Drawing.Size(115, 26);
            this.btnDeletePoint.TabIndex = 27;
            this.btnDeletePoint.Text = "Delete point";
            this.btnDeletePoint.UseVisualStyleBackColor = true;
            this.btnDeletePoint.Click += new System.EventHandler(this.btnDeletePoint_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.checkCrosshair);
            this.groupBox1.Controls.Add(this.btnCrosshairMeasure);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 245);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.groupBox1.Size = new System.Drawing.Size(127, 84);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Crosshair";
            // 
            // checkCrosshair
            // 
            this.checkCrosshair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.checkCrosshair.AutoSize = true;
            this.checkCrosshair.Location = new System.Drawing.Point(6, 19);
            this.checkCrosshair.Name = "checkCrosshair";
            this.checkCrosshair.Size = new System.Drawing.Size(98, 17);
            this.checkCrosshair.TabIndex = 35;
            this.checkCrosshair.Text = "Show crosshair";
            this.checkCrosshair.UseVisualStyleBackColor = true;
            // 
            // btnCrosshairMeasure
            // 
            this.btnCrosshairMeasure.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCrosshairMeasure.Location = new System.Drawing.Point(6, 42);
            this.btnCrosshairMeasure.Name = "btnCrosshairMeasure";
            this.btnCrosshairMeasure.Size = new System.Drawing.Size(118, 26);
            this.btnCrosshairMeasure.TabIndex = 23;
            this.btnCrosshairMeasure.Text = "Measure at crosshair";
            this.btnCrosshairMeasure.UseVisualStyleBackColor = true;
            this.btnCrosshairMeasure.Click += new System.EventHandler(this.btnCrosshairMeasure_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.AutoSize = true;
            this.groupBox9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox9.Controls.Add(this.btnEditSequence);
            this.groupBox9.Controls.Add(this.listMeasurements);
            this.groupBox9.Controls.Add(this.btnNewSequence);
            this.groupBox9.Controls.Add(this.btnDeleteSequence);
            this.groupBox9.Controls.Add(this.btnSaveCrack);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox9.Location = new System.Drawing.Point(0, 0);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.groupBox9.Size = new System.Drawing.Size(127, 245);
            this.groupBox9.TabIndex = 37;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Cracks";
            // 
            // btnEditSequence
            // 
            this.btnEditSequence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditSequence.Location = new System.Drawing.Point(6, 107);
            this.btnEditSequence.Name = "btnEditSequence";
            this.btnEditSequence.Size = new System.Drawing.Size(115, 26);
            this.btnEditSequence.TabIndex = 19;
            this.btnEditSequence.Text = "Edit";
            this.btnEditSequence.UseVisualStyleBackColor = true;
            this.btnEditSequence.Click += new System.EventHandler(this.btnEditSequence_Click);
            // 
            // listMeasurements
            // 
            this.listMeasurements.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.listMeasurements.FormattingEnabled = true;
            this.listMeasurements.Location = new System.Drawing.Point(6, 19);
            this.listMeasurements.Name = "listMeasurements";
            this.listMeasurements.ScrollAlwaysVisible = true;
            this.listMeasurements.Size = new System.Drawing.Size(115, 82);
            this.listMeasurements.TabIndex = 8;
            this.listMeasurements.SelectedIndexChanged += new System.EventHandler(this.listMeasurements_SelectedIndexChanged);
            // 
            // btnNewSequence
            // 
            this.btnNewSequence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewSequence.Location = new System.Drawing.Point(6, 139);
            this.btnNewSequence.Name = "btnNewSequence";
            this.btnNewSequence.Size = new System.Drawing.Size(115, 26);
            this.btnNewSequence.TabIndex = 10;
            this.btnNewSequence.Text = "New";
            this.btnNewSequence.UseVisualStyleBackColor = true;
            this.btnNewSequence.Click += new System.EventHandler(this.btnNewSequence_Click);
            // 
            // btnDeleteSequence
            // 
            this.btnDeleteSequence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteSequence.Location = new System.Drawing.Point(6, 171);
            this.btnDeleteSequence.Name = "btnDeleteSequence";
            this.btnDeleteSequence.Size = new System.Drawing.Size(115, 26);
            this.btnDeleteSequence.TabIndex = 18;
            this.btnDeleteSequence.Text = "Delete";
            this.btnDeleteSequence.UseVisualStyleBackColor = true;
            this.btnDeleteSequence.Click += new System.EventHandler(this.btnDeleteSequence_Click);
            // 
            // btnSaveCrack
            // 
            this.btnSaveCrack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveCrack.Location = new System.Drawing.Point(6, 203);
            this.btnSaveCrack.Name = "btnSaveCrack";
            this.btnSaveCrack.Size = new System.Drawing.Size(115, 26);
            this.btnSaveCrack.TabIndex = 28;
            this.btnSaveCrack.Text = "Save to file";
            this.btnSaveCrack.UseVisualStyleBackColor = true;
            this.btnSaveCrack.Click += new System.EventHandler(this.btnSaveCrack_Click);
            // 
            // WFOVViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 514);
            this.Controls.Add(this.panelLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WFOVViewForm";
            this.Text = "WFOVViewForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WFOVViewForm_FormClosing);
            this.Load += new System.EventHandler(this.WFOVViewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wfovContainer)).EndInit();
            this.panelLayout.ResumeLayout(false);
            this.panelLayout.PerformLayout();
            this.panelWfovView.ResumeLayout(false);
            this.panelWfovView.PerformLayout();
            this.panelSliders.ResumeLayout(false);
            this.panelSliders.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderFocus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderZoom)).EndInit();
            this.panelMeasurement.ResumeLayout(false);
            this.panelMeasurement.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TIS.Imaging.ICImagingControl wfovContainer;
        private System.Windows.Forms.TableLayoutPanel panelLayout;
        private System.Windows.Forms.Panel panelWfovView;
        private System.Windows.Forms.Panel panelSliders;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textFocus;
        private System.Windows.Forms.TextBox textZoom;
        private System.Windows.Forms.TrackBar sliderFocus;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar sliderZoom;
        private System.Windows.Forms.Button btnWfovStop;
        private System.Windows.Forms.Button btnFocus;
        private System.Windows.Forms.Button btnWfovProperties;
        private System.Windows.Forms.Button btnWfovRecord;
        private System.Windows.Forms.Button btnWfovSnap;
        private System.Windows.Forms.Button btnWfovStart;
        private System.Windows.Forms.Panel panelMeasurement;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button btnEditSequence;
        private System.Windows.Forms.ListBox listMeasurements;
        private System.Windows.Forms.Button btnNewSequence;
        private System.Windows.Forms.Button btnDeleteSequence;
        private System.Windows.Forms.Button btnSaveCrack;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listPoints;
        private System.Windows.Forms.Button btnGotoPoint;
        private System.Windows.Forms.Button btnDeletePoint;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkCrosshair;
        private System.Windows.Forms.Button btnCrosshairMeasure;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}