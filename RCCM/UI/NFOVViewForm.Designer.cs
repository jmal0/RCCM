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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NFOVViewForm));
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnEditSequence = new System.Windows.Forms.Button();
            this.listMeasurements = new System.Windows.Forms.ListBox();
            this.btnNewSequence = new System.Windows.Forms.Button();
            this.btnDeleteSequence = new System.Windows.Forms.Button();
            this.btnSaveCrack = new System.Windows.Forms.Button();
            this.listPoints = new System.Windows.Forms.ListBox();
            this.btnCrosshairMeasure = new System.Windows.Forms.Button();
            this.nfovImage = new System.Windows.Forms.PictureBox();
            this.btnDeletePoint = new System.Windows.Forms.Button();
            this.btnNfovSnap = new System.Windows.Forms.Button();
            this.btnNfovProperties = new System.Windows.Forms.Button();
            this.btnNfovStop = new System.Windows.Forms.Button();
            this.btnNfovStart = new System.Windows.Forms.Button();
            this.btnNfovRecord = new System.Windows.Forms.Button();
            this.btnGotoPoint = new System.Windows.Forms.Button();
            this.checkCrosshair = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panelNFOVControl = new System.Windows.Forms.Panel();
            this.panelMeasurementControl = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nfovImage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelNFOVControl.SuspendLayout();
            this.panelMeasurementControl.SuspendLayout();
            this.tableLayout.SuspendLayout();
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
            this.groupBox9.Location = new System.Drawing.Point(3, 3);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.groupBox9.Size = new System.Drawing.Size(130, 245);
            this.groupBox9.TabIndex = 25;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Cracks";
            // 
            // btnEditSequence
            // 
            this.btnEditSequence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditSequence.Location = new System.Drawing.Point(6, 107);
            this.btnEditSequence.Name = "btnEditSequence";
            this.btnEditSequence.Size = new System.Drawing.Size(118, 26);
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
            this.listMeasurements.Size = new System.Drawing.Size(118, 82);
            this.listMeasurements.TabIndex = 8;
            this.listMeasurements.SelectedIndexChanged += new System.EventHandler(this.listMeasurements_SelectedIndexChanged);
            // 
            // btnNewSequence
            // 
            this.btnNewSequence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewSequence.Location = new System.Drawing.Point(6, 139);
            this.btnNewSequence.Name = "btnNewSequence";
            this.btnNewSequence.Size = new System.Drawing.Size(118, 26);
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
            this.btnDeleteSequence.Size = new System.Drawing.Size(118, 26);
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
            this.btnSaveCrack.Size = new System.Drawing.Size(118, 26);
            this.btnSaveCrack.TabIndex = 28;
            this.btnSaveCrack.Text = "Save to file";
            this.btnSaveCrack.UseVisualStyleBackColor = true;
            this.btnSaveCrack.Click += new System.EventHandler(this.btnSaveCrack_Click);
            // 
            // listPoints
            // 
            this.listPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.listPoints.FormattingEnabled = true;
            this.listPoints.Location = new System.Drawing.Point(6, 19);
            this.listPoints.Name = "listPoints";
            this.listPoints.ScrollAlwaysVisible = true;
            this.listPoints.Size = new System.Drawing.Size(118, 95);
            this.listPoints.TabIndex = 24;
            this.listPoints.SelectedIndexChanged += new System.EventHandler(this.listPoints_SelectedIndexChanged);
            // 
            // btnCrosshairMeasure
            // 
            this.btnCrosshairMeasure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrosshairMeasure.Location = new System.Drawing.Point(6, 42);
            this.btnCrosshairMeasure.Name = "btnCrosshairMeasure";
            this.btnCrosshairMeasure.Size = new System.Drawing.Size(118, 26);
            this.btnCrosshairMeasure.TabIndex = 23;
            this.btnCrosshairMeasure.Text = "Measure at crosshair";
            this.btnCrosshairMeasure.UseVisualStyleBackColor = true;
            this.btnCrosshairMeasure.Click += new System.EventHandler(this.btnCrosshairMeasure_Click);
            // 
            // nfovImage
            // 
            this.nfovImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.nfovImage.Location = new System.Drawing.Point(0, 0);
            this.nfovImage.Name = "nfovImage";
            this.nfovImage.Size = new System.Drawing.Size(606, 512);
            this.nfovImage.TabIndex = 26;
            this.nfovImage.TabStop = false;
            this.nfovImage.Paint += new System.Windows.Forms.PaintEventHandler(this.nfovImage_Paint);
            this.nfovImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.nfovImage_MouseDown);
            this.nfovImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.nfovImage_MouseMove);
            this.nfovImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.nfovImage_MouseUp);
            // 
            // btnDeletePoint
            // 
            this.btnDeletePoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeletePoint.Location = new System.Drawing.Point(6, 152);
            this.btnDeletePoint.Name = "btnDeletePoint";
            this.btnDeletePoint.Size = new System.Drawing.Size(118, 26);
            this.btnDeletePoint.TabIndex = 27;
            this.btnDeletePoint.Text = "Delete point";
            this.btnDeletePoint.UseVisualStyleBackColor = true;
            this.btnDeletePoint.Click += new System.EventHandler(this.btnDeletePoint_Click);
            // 
            // btnNfovSnap
            // 
            this.btnNfovSnap.Image = global::RCCM.Properties.Resources.snap;
            this.btnNfovSnap.Location = new System.Drawing.Point(67, 512);
            this.btnNfovSnap.Name = "btnNfovSnap";
            this.btnNfovSnap.Size = new System.Drawing.Size(35, 26);
            this.btnNfovSnap.TabIndex = 32;
            this.btnNfovSnap.UseVisualStyleBackColor = true;
            this.btnNfovSnap.Click += new System.EventHandler(this.btnNfovSnap_Click);
            // 
            // btnNfovProperties
            // 
            this.btnNfovProperties.Location = new System.Drawing.Point(140, 512);
            this.btnNfovProperties.Name = "btnNfovProperties";
            this.btnNfovProperties.Size = new System.Drawing.Size(75, 26);
            this.btnNfovProperties.TabIndex = 31;
            this.btnNfovProperties.Text = "Properties";
            this.btnNfovProperties.UseVisualStyleBackColor = true;
            this.btnNfovProperties.Click += new System.EventHandler(this.btnNfovProperties_Click);
            // 
            // btnNfovStop
            // 
            this.btnNfovStop.Enabled = false;
            this.btnNfovStop.Image = global::RCCM.Properties.Resources.stop;
            this.btnNfovStop.Location = new System.Drawing.Point(35, 512);
            this.btnNfovStop.Name = "btnNfovStop";
            this.btnNfovStop.Size = new System.Drawing.Size(26, 26);
            this.btnNfovStop.TabIndex = 30;
            this.btnNfovStop.UseVisualStyleBackColor = true;
            this.btnNfovStop.Click += new System.EventHandler(this.btnNfovStop_Click);
            // 
            // btnNfovStart
            // 
            this.btnNfovStart.Image = global::RCCM.Properties.Resources.play;
            this.btnNfovStart.Location = new System.Drawing.Point(3, 512);
            this.btnNfovStart.Name = "btnNfovStart";
            this.btnNfovStart.Size = new System.Drawing.Size(26, 26);
            this.btnNfovStart.TabIndex = 29;
            this.btnNfovStart.UseVisualStyleBackColor = true;
            this.btnNfovStart.Click += new System.EventHandler(this.btnNfovStart_Click);
            // 
            // btnNfovRecord
            // 
            this.btnNfovRecord.BackColor = System.Drawing.Color.Transparent;
            this.btnNfovRecord.Image = global::RCCM.Properties.Resources.record;
            this.btnNfovRecord.Location = new System.Drawing.Point(108, 512);
            this.btnNfovRecord.Name = "btnNfovRecord";
            this.btnNfovRecord.Size = new System.Drawing.Size(26, 26);
            this.btnNfovRecord.TabIndex = 33;
            this.btnNfovRecord.UseVisualStyleBackColor = false;
            this.btnNfovRecord.Click += new System.EventHandler(this.btnNfovRecord_Click);
            // 
            // btnGotoPoint
            // 
            this.btnGotoPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGotoPoint.Location = new System.Drawing.Point(6, 120);
            this.btnGotoPoint.Name = "btnGotoPoint";
            this.btnGotoPoint.Size = new System.Drawing.Size(118, 26);
            this.btnGotoPoint.TabIndex = 34;
            this.btnGotoPoint.Text = "Go to point";
            this.btnGotoPoint.UseVisualStyleBackColor = true;
            this.btnGotoPoint.Click += new System.EventHandler(this.btnGotoPoint_Click);
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
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.checkCrosshair);
            this.groupBox1.Controls.Add(this.btnCrosshairMeasure);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 254);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.groupBox1.Size = new System.Drawing.Size(130, 84);
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
            this.groupBox2.Location = new System.Drawing.Point(3, 344);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.groupBox2.Size = new System.Drawing.Size(130, 194);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Points";
            // 
            // panelNFOVControl
            // 
            this.panelNFOVControl.AutoSize = true;
            this.panelNFOVControl.Controls.Add(this.nfovImage);
            this.panelNFOVControl.Controls.Add(this.btnNfovStart);
            this.panelNFOVControl.Controls.Add(this.btnNfovRecord);
            this.panelNFOVControl.Controls.Add(this.btnNfovStop);
            this.panelNFOVControl.Controls.Add(this.btnNfovSnap);
            this.panelNFOVControl.Controls.Add(this.btnNfovProperties);
            this.panelNFOVControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNFOVControl.Location = new System.Drawing.Point(3, 3);
            this.panelNFOVControl.Name = "panelNFOVControl";
            this.panelNFOVControl.Size = new System.Drawing.Size(606, 539);
            this.panelNFOVControl.TabIndex = 39;
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
            this.panelMeasurementControl.Location = new System.Drawing.Point(615, 3);
            this.panelMeasurementControl.Name = "panelMeasurementControl";
            this.panelMeasurementControl.RowCount = 3;
            this.panelMeasurementControl.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelMeasurementControl.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelMeasurementControl.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelMeasurementControl.Size = new System.Drawing.Size(136, 539);
            this.panelMeasurementControl.TabIndex = 34;
            // 
            // tableLayout
            // 
            this.tableLayout.AutoSize = true;
            this.tableLayout.ColumnCount = 2;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 612F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayout.Controls.Add(this.panelNFOVControl, 0, 0);
            this.tableLayout.Controls.Add(this.panelMeasurementControl, 1, 0);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 1;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Size = new System.Drawing.Size(754, 545);
            this.tableLayout.TabIndex = 34;
            // 
            // NFOVViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 545);
            this.Controls.Add(this.tableLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(2048, 590);
            this.Name = "NFOVViewForm";
            this.Text = "NFOV";
            this.Load += new System.EventHandler(this.NFOVViewForm_Load);
            this.groupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nfovImage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panelNFOVControl.ResumeLayout(false);
            this.panelMeasurementControl.ResumeLayout(false);
            this.panelMeasurementControl.PerformLayout();
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.ListBox listMeasurements;
        private System.Windows.Forms.Button btnNewSequence;
        private System.Windows.Forms.Button btnDeleteSequence;
        private System.Windows.Forms.ListBox listPoints;
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
    }
}