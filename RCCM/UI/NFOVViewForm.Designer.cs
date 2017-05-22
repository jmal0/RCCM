namespace RCCM.UI
{
    partial class NFOVViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NFOVViewForm));
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnEditSequence = new System.Windows.Forms.Button();
            this.listMeasurements = new System.Windows.Forms.ListBox();
            this.btnNewSequence = new System.Windows.Forms.Button();
            this.btnDeleteSequence = new System.Windows.Forms.Button();
            this.btnSaveCrack = new System.Windows.Forms.Button();
            this.btnCrosshairMeasure = new System.Windows.Forms.Button();
            this.btnDeletePoint = new System.Windows.Forms.Button();
            this.btnNfovProperties = new System.Windows.Forms.Button();
            this.btnGotoPoint = new System.Windows.Forms.Button();
            this.checkCrosshair = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listPoints = new System.Windows.Forms.ListView();
            this.columnCycle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panelNFOVControl = new System.Windows.Forms.Panel();
            this.btnNFOVSave = new System.Windows.Forms.Button();
            this.nfovImage = new System.Windows.Forms.PictureBox();
            this.btnNfovStart = new System.Windows.Forms.Button();
            this.btnNfovRecord = new System.Windows.Forms.Button();
            this.btnNfovStop = new System.Windows.Forms.Button();
            this.btnNfovSnap = new System.Windows.Forms.Button();
            this.panelMeasurementControl = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.editFocus = new System.Windows.Forms.NumericUpDown();
            this.groupBox9.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelNFOVControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nfovImage)).BeginInit();
            this.panelMeasurementControl.SuspendLayout();
            this.tableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editFocus)).BeginInit();
            this.SuspendLayout();
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
            this.groupBox9.Location = new System.Drawing.Point(4, 4);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.groupBox9.Size = new System.Drawing.Size(173, 301);
            this.groupBox9.TabIndex = 25;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Cracks";
            // 
            // btnEditSequence
            // 
            this.btnEditSequence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditSequence.Location = new System.Drawing.Point(8, 132);
            this.btnEditSequence.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEditSequence.Name = "btnEditSequence";
            this.btnEditSequence.Size = new System.Drawing.Size(157, 32);
            this.btnEditSequence.TabIndex = 19;
            this.btnEditSequence.Text = "Edit";
            this.toolTip.SetToolTip(this.btnEditSequence, "Modify highlighted crack");
            this.btnEditSequence.UseVisualStyleBackColor = true;
            this.btnEditSequence.Click += new System.EventHandler(this.btnEditSequence_Click);
            // 
            // listMeasurements
            // 
            this.listMeasurements.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.listMeasurements.FormattingEnabled = true;
            this.listMeasurements.ItemHeight = 16;
            this.listMeasurements.Location = new System.Drawing.Point(8, 24);
            this.listMeasurements.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listMeasurements.Name = "listMeasurements";
            this.listMeasurements.ScrollAlwaysVisible = true;
            this.listMeasurements.Size = new System.Drawing.Size(155, 100);
            this.listMeasurements.TabIndex = 8;
            this.toolTip.SetToolTip(this.listMeasurements, "Created crack measurements. Click to select a crack");
            this.listMeasurements.SelectedIndexChanged += new System.EventHandler(this.listMeasurements_SelectedIndexChanged);
            // 
            // btnNewSequence
            // 
            this.btnNewSequence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewSequence.Location = new System.Drawing.Point(8, 172);
            this.btnNewSequence.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNewSequence.Name = "btnNewSequence";
            this.btnNewSequence.Size = new System.Drawing.Size(157, 32);
            this.btnNewSequence.TabIndex = 10;
            this.btnNewSequence.Text = "New";
            this.toolTip.SetToolTip(this.btnNewSequence, "Create a new crack measurement ");
            this.btnNewSequence.UseVisualStyleBackColor = true;
            this.btnNewSequence.Click += new System.EventHandler(this.btnNewSequence_Click);
            // 
            // btnDeleteSequence
            // 
            this.btnDeleteSequence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteSequence.Location = new System.Drawing.Point(8, 211);
            this.btnDeleteSequence.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDeleteSequence.Name = "btnDeleteSequence";
            this.btnDeleteSequence.Size = new System.Drawing.Size(157, 32);
            this.btnDeleteSequence.TabIndex = 18;
            this.btnDeleteSequence.Text = "Delete";
            this.toolTip.SetToolTip(this.btnDeleteSequence, "Delete highlighted crack");
            this.btnDeleteSequence.UseVisualStyleBackColor = true;
            this.btnDeleteSequence.Click += new System.EventHandler(this.btnDeleteSequence_Click);
            // 
            // btnSaveCrack
            // 
            this.btnSaveCrack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveCrack.Location = new System.Drawing.Point(8, 250);
            this.btnSaveCrack.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSaveCrack.Name = "btnSaveCrack";
            this.btnSaveCrack.Size = new System.Drawing.Size(157, 32);
            this.btnSaveCrack.TabIndex = 28;
            this.btnSaveCrack.Text = "Save to file";
            this.toolTip.SetToolTip(this.btnSaveCrack, "Open save dialog to save measurement data");
            this.btnSaveCrack.UseVisualStyleBackColor = true;
            this.btnSaveCrack.Click += new System.EventHandler(this.btnSaveCrack_Click);
            // 
            // btnCrosshairMeasure
            // 
            this.btnCrosshairMeasure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrosshairMeasure.Location = new System.Drawing.Point(8, 50);
            this.btnCrosshairMeasure.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCrosshairMeasure.Name = "btnCrosshairMeasure";
            this.btnCrosshairMeasure.Size = new System.Drawing.Size(157, 32);
            this.btnCrosshairMeasure.TabIndex = 23;
            this.btnCrosshairMeasure.Text = "Measure at crosshair";
            this.toolTip.SetToolTip(this.btnCrosshairMeasure, "Add a new crack measurement at the center of the image");
            this.btnCrosshairMeasure.UseVisualStyleBackColor = true;
            this.btnCrosshairMeasure.Click += new System.EventHandler(this.btnCrosshairMeasure_Click);
            // 
            // btnDeletePoint
            // 
            this.btnDeletePoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeletePoint.Location = new System.Drawing.Point(8, 186);
            this.btnDeletePoint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDeletePoint.Name = "btnDeletePoint";
            this.btnDeletePoint.Size = new System.Drawing.Size(157, 32);
            this.btnDeletePoint.TabIndex = 27;
            this.btnDeletePoint.Text = "Delete point";
            this.toolTip.SetToolTip(this.btnDeletePoint, "Delete highlighted point from measurement");
            this.btnDeletePoint.UseVisualStyleBackColor = true;
            this.btnDeletePoint.Click += new System.EventHandler(this.btnDeletePoint_Click);
            // 
            // btnNfovProperties
            // 
            this.btnNfovProperties.Location = new System.Drawing.Point(229, 630);
            this.btnNfovProperties.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNfovProperties.Name = "btnNfovProperties";
            this.btnNfovProperties.Size = new System.Drawing.Size(100, 32);
            this.btnNfovProperties.TabIndex = 31;
            this.btnNfovProperties.Text = "Properties";
            this.toolTip.SetToolTip(this.btnNfovProperties, "Open camera property window");
            this.btnNfovProperties.UseVisualStyleBackColor = true;
            this.btnNfovProperties.Click += new System.EventHandler(this.btnNfovProperties_Click);
            // 
            // btnGotoPoint
            // 
            this.btnGotoPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGotoPoint.Location = new System.Drawing.Point(8, 147);
            this.btnGotoPoint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGotoPoint.Name = "btnGotoPoint";
            this.btnGotoPoint.Size = new System.Drawing.Size(157, 32);
            this.btnGotoPoint.TabIndex = 34;
            this.btnGotoPoint.Text = "Go to point";
            this.toolTip.SetToolTip(this.btnGotoPoint, "Move fine actuators to location when highlighted point was measured");
            this.btnGotoPoint.UseVisualStyleBackColor = true;
            this.btnGotoPoint.Click += new System.EventHandler(this.btnGotoPoint_Click);
            // 
            // checkCrosshair
            // 
            this.checkCrosshair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.checkCrosshair.AutoSize = true;
            this.checkCrosshair.Location = new System.Drawing.Point(8, 22);
            this.checkCrosshair.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkCrosshair.Name = "checkCrosshair";
            this.checkCrosshair.Size = new System.Drawing.Size(126, 21);
            this.checkCrosshair.TabIndex = 35;
            this.checkCrosshair.Text = "Show crosshair";
            this.toolTip.SetToolTip(this.checkCrosshair, "Check to draw crosshair on live image");
            this.checkCrosshair.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.checkCrosshair);
            this.groupBox1.Controls.Add(this.btnCrosshairMeasure);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(4, 313);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.groupBox1.Size = new System.Drawing.Size(173, 101);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Crosshair";
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.listPoints);
            this.groupBox2.Controls.Add(this.btnGotoPoint);
            this.groupBox2.Controls.Add(this.btnDeletePoint);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(4, 422);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.groupBox2.Size = new System.Drawing.Size(173, 237);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Points";
            // 
            // listPoints
            // 
            this.listPoints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listPoints.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnCycle,
            this.columnLength});
            this.listPoints.FullRowSelect = true;
            this.listPoints.HideSelection = false;
            this.listPoints.Location = new System.Drawing.Point(8, 23);
            this.listPoints.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listPoints.MultiSelect = false;
            this.listPoints.Name = "listPoints";
            this.listPoints.Size = new System.Drawing.Size(155, 118);
            this.listPoints.TabIndex = 35;
            this.toolTip.SetToolTip(this.listPoints, "Points in selected crack. Click to highlight a point from the list");
            this.listPoints.UseCompatibleStateImageBehavior = false;
            this.listPoints.View = System.Windows.Forms.View.Details;
            this.listPoints.SelectedIndexChanged += new System.EventHandler(this.listPoints_SelectedIndexChanged);
            // 
            // columnCycle
            // 
            this.columnCycle.Text = "Cycle";
            this.columnCycle.Width = 59;
            // 
            // columnLength
            // 
            this.columnLength.Text = "Length";
            this.columnLength.Width = 59;
            // 
            // panelNFOVControl
            // 
            this.panelNFOVControl.AutoSize = true;
            this.panelNFOVControl.Controls.Add(this.editFocus);
            this.panelNFOVControl.Controls.Add(this.label1);
            this.panelNFOVControl.Controls.Add(this.btnNFOVSave);
            this.panelNFOVControl.Controls.Add(this.nfovImage);
            this.panelNFOVControl.Controls.Add(this.btnNfovStart);
            this.panelNFOVControl.Controls.Add(this.btnNfovRecord);
            this.panelNFOVControl.Controls.Add(this.btnNfovStop);
            this.panelNFOVControl.Controls.Add(this.btnNfovSnap);
            this.panelNFOVControl.Controls.Add(this.btnNfovProperties);
            this.panelNFOVControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNFOVControl.Location = new System.Drawing.Point(4, 4);
            this.panelNFOVControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelNFOVControl.Name = "panelNFOVControl";
            this.panelNFOVControl.Size = new System.Drawing.Size(808, 660);
            this.panelNFOVControl.TabIndex = 39;
            // 
            // btnNFOVSave
            // 
            this.btnNFOVSave.BackColor = System.Drawing.Color.Transparent;
            this.btnNFOVSave.Image = global::RCCM.Properties.Resources.save;
            this.btnNFOVSave.Location = new System.Drawing.Point(144, 630);
            this.btnNFOVSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNFOVSave.Name = "btnNFOVSave";
            this.btnNFOVSave.Size = new System.Drawing.Size(35, 32);
            this.btnNFOVSave.TabIndex = 34;
            this.toolTip.SetToolTip(this.btnNFOVSave, "Record video");
            this.btnNFOVSave.UseVisualStyleBackColor = false;
            this.btnNFOVSave.Click += new System.EventHandler(this.btnNFOVSave_Click);
            // 
            // nfovImage
            // 
            this.nfovImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.nfovImage.Location = new System.Drawing.Point(0, 0);
            this.nfovImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nfovImage.Name = "nfovImage";
            this.nfovImage.Size = new System.Drawing.Size(808, 630);
            this.nfovImage.TabIndex = 26;
            this.nfovImage.TabStop = false;
            this.nfovImage.Paint += new System.Windows.Forms.PaintEventHandler(this.nfovImage_Paint);
            this.nfovImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.nfovImage_MouseDown);
            this.nfovImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.nfovImage_MouseMove);
            this.nfovImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.nfovImage_MouseUp);
            // 
            // btnNfovStart
            // 
            this.btnNfovStart.Image = global::RCCM.Properties.Resources.play;
            this.btnNfovStart.Location = new System.Drawing.Point(4, 630);
            this.btnNfovStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNfovStart.Name = "btnNfovStart";
            this.btnNfovStart.Size = new System.Drawing.Size(35, 32);
            this.btnNfovStart.TabIndex = 29;
            this.toolTip.SetToolTip(this.btnNfovStart, "Start live image display");
            this.btnNfovStart.UseVisualStyleBackColor = true;
            this.btnNfovStart.Click += new System.EventHandler(this.btnNfovStart_Click);
            // 
            // btnNfovRecord
            // 
            this.btnNfovRecord.BackColor = System.Drawing.Color.Transparent;
            this.btnNfovRecord.Image = global::RCCM.Properties.Resources.record;
            this.btnNfovRecord.Location = new System.Drawing.Point(187, 630);
            this.btnNfovRecord.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNfovRecord.Name = "btnNfovRecord";
            this.btnNfovRecord.Size = new System.Drawing.Size(35, 32);
            this.btnNfovRecord.TabIndex = 33;
            this.toolTip.SetToolTip(this.btnNfovRecord, "Record video");
            this.btnNfovRecord.UseVisualStyleBackColor = false;
            this.btnNfovRecord.Click += new System.EventHandler(this.btnNfovRecord_Click);
            // 
            // btnNfovStop
            // 
            this.btnNfovStop.Enabled = false;
            this.btnNfovStop.Image = global::RCCM.Properties.Resources.stop;
            this.btnNfovStop.Location = new System.Drawing.Point(47, 630);
            this.btnNfovStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNfovStop.Name = "btnNfovStop";
            this.btnNfovStop.Size = new System.Drawing.Size(35, 32);
            this.btnNfovStop.TabIndex = 30;
            this.toolTip.SetToolTip(this.btnNfovStop, "Stop live image display");
            this.btnNfovStop.UseVisualStyleBackColor = true;
            this.btnNfovStop.Click += new System.EventHandler(this.btnNfovStop_Click);
            // 
            // btnNfovSnap
            // 
            this.btnNfovSnap.Image = global::RCCM.Properties.Resources.snap;
            this.btnNfovSnap.Location = new System.Drawing.Point(89, 630);
            this.btnNfovSnap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNfovSnap.Name = "btnNfovSnap";
            this.btnNfovSnap.Size = new System.Drawing.Size(47, 32);
            this.btnNfovSnap.TabIndex = 32;
            this.toolTip.SetToolTip(this.btnNfovSnap, "Snap image and automatically save to images folder");
            this.btnNfovSnap.UseVisualStyleBackColor = true;
            this.btnNfovSnap.Click += new System.EventHandler(this.btnNfovSnap_Click);
            // 
            // panelMeasurementControl
            // 
            this.panelMeasurementControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMeasurementControl.AutoSize = true;
            this.panelMeasurementControl.ColumnCount = 1;
            this.panelMeasurementControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelMeasurementControl.Controls.Add(this.groupBox9, 0, 0);
            this.panelMeasurementControl.Controls.Add(this.groupBox1, 0, 1);
            this.panelMeasurementControl.Controls.Add(this.groupBox2, 0, 2);
            this.panelMeasurementControl.Location = new System.Drawing.Point(820, 4);
            this.panelMeasurementControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMeasurementControl.Name = "panelMeasurementControl";
            this.panelMeasurementControl.RowCount = 3;
            this.panelMeasurementControl.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelMeasurementControl.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelMeasurementControl.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelMeasurementControl.Size = new System.Drawing.Size(181, 660);
            this.panelMeasurementControl.TabIndex = 34;
            // 
            // tableLayout
            // 
            this.tableLayout.AutoSize = true;
            this.tableLayout.ColumnCount = 2;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 816F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayout.Controls.Add(this.panelNFOVControl, 0, 0);
            this.tableLayout.Controls.Add(this.panelMeasurementControl, 1, 0);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 1;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Size = new System.Drawing.Size(1005, 668);
            this.tableLayout.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(336, 638);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 35;
            this.label1.Text = "Focus Offset:";
            // 
            // editFocus
            // 
            this.editFocus.DecimalPlaces = 2;
            this.editFocus.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.editFocus.Location = new System.Drawing.Point(434, 635);
            this.editFocus.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.editFocus.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.editFocus.Name = "editFocus";
            this.editFocus.Size = new System.Drawing.Size(120, 22);
            this.editFocus.TabIndex = 36;
            this.editFocus.ValueChanged += new System.EventHandler(this.editFocus_ValueChanged);
            // 
            // NFOVViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 668);
            this.Controls.Add(this.tableLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(2725, 715);
            this.Name = "NFOVViewForm";
            this.Text = "NFOV";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NFOVViewForm_FormClosed);
            this.Load += new System.EventHandler(this.NFOVViewForm_Load);
            this.groupBox9.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panelNFOVControl.ResumeLayout(false);
            this.panelNFOVControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nfovImage)).EndInit();
            this.panelMeasurementControl.ResumeLayout(false);
            this.panelMeasurementControl.PerformLayout();
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editFocus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.ListBox listMeasurements;
        private System.Windows.Forms.Button btnNewSequence;
        private System.Windows.Forms.Button btnDeleteSequence;
        private System.Windows.Forms.Button btnCrosshairMeasure;
        private System.Windows.Forms.PictureBox nfovImage;
        private System.Windows.Forms.Button btnSaveCrack;
        private System.Windows.Forms.Button btnDeletePoint;
        private System.Windows.Forms.Button btnEditSequence;
        private System.Windows.Forms.Button btnNfovSnap;
        private System.Windows.Forms.Button btnNfovProperties;
        private System.Windows.Forms.Button btnNfovStop;
        private System.Windows.Forms.Button btnNfovStart;
        private System.Windows.Forms.Button btnNfovRecord;
        private System.Windows.Forms.Button btnGotoPoint;
        private System.Windows.Forms.CheckBox checkCrosshair;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Panel panelNFOVControl;
        private System.Windows.Forms.TableLayoutPanel panelMeasurementControl;
        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.ListView listPoints;
        private System.Windows.Forms.ColumnHeader columnCycle;
        private System.Windows.Forms.ColumnHeader columnLength;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnNFOVSave;
        private System.Windows.Forms.NumericUpDown editFocus;
        private System.Windows.Forms.Label label1;
    }
}