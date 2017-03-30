namespace RCCM.UI
{
    partial class CoordinateSystemSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoordinateSystemSettingsForm));
            this.editPanelX = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.editPanelY = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.editPanelRotation = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.editRotation = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.editPanelWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.editPanelHeight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.editPanelRadius = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.editNFOV1Y = new System.Windows.Forms.NumericUpDown();
            this.editNFOV1X = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.editNFOV2Y = new System.Windows.Forms.NumericUpDown();
            this.editNFOV2X = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.editWFOV2Y = new System.Windows.Forms.NumericUpDown();
            this.editWFOV2X = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.editWFOV1Y = new System.Windows.Forms.NumericUpDown();
            this.editWFOV1X = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.editPanelX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPanelY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPanelRotation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editRotation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPanelWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPanelHeight)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editPanelRadius)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editNFOV1Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editNFOV1X)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editNFOV2Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editNFOV2X)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editWFOV2Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editWFOV2X)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editWFOV1Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editWFOV1X)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // editPanelX
            // 
            this.editPanelX.DecimalPlaces = 1;
            this.editPanelX.Location = new System.Drawing.Point(70, 42);
            this.editPanelX.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.editPanelX.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.editPanelX.Name = "editPanelX";
            this.editPanelX.Size = new System.Drawing.Size(100, 20);
            this.editPanelX.TabIndex = 20;
            this.toolTip.SetToolTip(this.editPanelX, "X offset of top left corner of panel from corner of coarse actuator travel");
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 44);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(43, 13);
            this.label21.TabIndex = 19;
            this.label21.Text = "X offset";
            // 
            // editPanelY
            // 
            this.editPanelY.DecimalPlaces = 1;
            this.editPanelY.Location = new System.Drawing.Point(70, 68);
            this.editPanelY.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.editPanelY.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.editPanelY.Name = "editPanelY";
            this.editPanelY.Size = new System.Drawing.Size(100, 20);
            this.editPanelY.TabIndex = 18;
            this.editPanelY.Tag = "";
            this.toolTip.SetToolTip(this.editPanelY, "Y offset of top left corner of panel from corner of coarse actuator travel");
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 70);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(43, 13);
            this.label18.TabIndex = 17;
            this.label18.Text = "Y offset";
            // 
            // editPanelRotation
            // 
            this.editPanelRotation.DecimalPlaces = 2;
            this.editPanelRotation.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.editPanelRotation.Location = new System.Drawing.Point(70, 16);
            this.editPanelRotation.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.editPanelRotation.Name = "editPanelRotation";
            this.editPanelRotation.Size = new System.Drawing.Size(100, 20);
            this.editPanelRotation.TabIndex = 16;
            this.toolTip.SetToolTip(this.editPanelRotation, "Rotation of panel with respect to the coarse actuators");
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 18);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(47, 13);
            this.label19.TabIndex = 15;
            this.label19.Text = "Rotation";
            // 
            // editRotation
            // 
            this.editRotation.DecimalPlaces = 2;
            this.editRotation.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.editRotation.Location = new System.Drawing.Point(107, 175);
            this.editRotation.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.editRotation.Name = "editRotation";
            this.editRotation.Size = new System.Drawing.Size(64, 20);
            this.editRotation.TabIndex = 14;
            this.toolTip.SetToolTip(this.editRotation, "Orientation of pivot plate");
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 178);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(94, 13);
            this.label20.TabIndex = 13;
            this.label20.Text = "Fine stage rotation";
            // 
            // editPanelWidth
            // 
            this.editPanelWidth.DecimalPlaces = 1;
            this.editPanelWidth.Location = new System.Drawing.Point(71, 95);
            this.editPanelWidth.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.editPanelWidth.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.editPanelWidth.Name = "editPanelWidth";
            this.editPanelWidth.Size = new System.Drawing.Size(100, 20);
            this.editPanelWidth.TabIndex = 24;
            this.editPanelWidth.Tag = "";
            this.toolTip.SetToolTip(this.editPanelWidth, "Axial length of panel");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Width";
            // 
            // editPanelHeight
            // 
            this.editPanelHeight.DecimalPlaces = 1;
            this.editPanelHeight.Location = new System.Drawing.Point(71, 121);
            this.editPanelHeight.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.editPanelHeight.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.editPanelHeight.Name = "editPanelHeight";
            this.editPanelHeight.Size = new System.Drawing.Size(100, 20);
            this.editPanelHeight.TabIndex = 22;
            this.toolTip.SetToolTip(this.editPanelHeight, "Hoop length of panel");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Height";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.editPanelRadius);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.editPanelHeight);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.editPanelWidth);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.editPanelRotation);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.editPanelY);
            this.groupBox1.Controls.Add(this.editPanelX);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 171);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Panel";
            // 
            // editPanelRadius
            // 
            this.editPanelRadius.DecimalPlaces = 1;
            this.editPanelRadius.Location = new System.Drawing.Point(71, 147);
            this.editPanelRadius.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.editPanelRadius.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.editPanelRadius.Name = "editPanelRadius";
            this.editPanelRadius.Size = new System.Drawing.Size(100, 20);
            this.editPanelRadius.TabIndex = 26;
            this.toolTip.SetToolTip(this.editPanelRadius, "Radius of curvature of panel");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Radius";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.editNFOV1Y);
            this.groupBox2.Controls.Add(this.editNFOV1X);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(177, 97);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "NFOV 1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "X offset";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Y offset";
            // 
            // editNFOV1Y
            // 
            this.editNFOV1Y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.editNFOV1Y.DecimalPlaces = 1;
            this.editNFOV1Y.Location = new System.Drawing.Point(68, 46);
            this.editNFOV1Y.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.editNFOV1Y.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.editNFOV1Y.Name = "editNFOV1Y";
            this.editNFOV1Y.Size = new System.Drawing.Size(103, 20);
            this.editNFOV1Y.TabIndex = 18;
            this.toolTip.SetToolTip(this.editNFOV1Y, "Y offset of NFOV 1 camera in X direction from center of rotation plate");
            // 
            // editNFOV1X
            // 
            this.editNFOV1X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.editNFOV1X.DecimalPlaces = 1;
            this.editNFOV1X.Location = new System.Drawing.Point(68, 20);
            this.editNFOV1X.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.editNFOV1X.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.editNFOV1X.Name = "editNFOV1X";
            this.editNFOV1X.Size = new System.Drawing.Size(103, 20);
            this.editNFOV1X.TabIndex = 20;
            this.toolTip.SetToolTip(this.editNFOV1X, "X offset of NFOV 1 camera in X direction from center of rotation plate");
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.editNFOV2Y);
            this.groupBox3.Controls.Add(this.editNFOV2X);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 106);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(177, 98);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "NFOV 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "X offset";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Y offset";
            // 
            // editNFOV2Y
            // 
            this.editNFOV2Y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.editNFOV2Y.DecimalPlaces = 1;
            this.editNFOV2Y.Location = new System.Drawing.Point(68, 45);
            this.editNFOV2Y.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.editNFOV2Y.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.editNFOV2Y.Name = "editNFOV2Y";
            this.editNFOV2Y.Size = new System.Drawing.Size(103, 20);
            this.editNFOV2Y.TabIndex = 18;
            this.toolTip.SetToolTip(this.editNFOV2Y, "Y offset of NFOV 2 camera in X direction from center of rotation plate");
            // 
            // editNFOV2X
            // 
            this.editNFOV2X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.editNFOV2X.DecimalPlaces = 1;
            this.editNFOV2X.Location = new System.Drawing.Point(68, 19);
            this.editNFOV2X.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.editNFOV2X.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.editNFOV2X.Name = "editNFOV2X";
            this.editNFOV2X.Size = new System.Drawing.Size(103, 20);
            this.editNFOV2X.TabIndex = 20;
            this.toolTip.SetToolTip(this.editNFOV2X, "X offset of NFOV 2 camera in X direction from center of rotation plate");
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.editWFOV2Y);
            this.groupBox4.Controls.Add(this.editWFOV2X);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(186, 106);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(178, 98);
            this.groupBox4.TabIndex = 28;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "WFOV 2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "X offset";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Y offset";
            // 
            // editWFOV2Y
            // 
            this.editWFOV2Y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.editWFOV2Y.DecimalPlaces = 1;
            this.editWFOV2Y.Location = new System.Drawing.Point(69, 45);
            this.editWFOV2Y.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.editWFOV2Y.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.editWFOV2Y.Name = "editWFOV2Y";
            this.editWFOV2Y.Size = new System.Drawing.Size(103, 20);
            this.editWFOV2Y.TabIndex = 18;
            this.toolTip.SetToolTip(this.editWFOV2Y, "Y offset of WFOV 2 camera in X direction from center of rotation plate");
            // 
            // editWFOV2X
            // 
            this.editWFOV2X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.editWFOV2X.DecimalPlaces = 1;
            this.editWFOV2X.Location = new System.Drawing.Point(69, 19);
            this.editWFOV2X.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.editWFOV2X.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.editWFOV2X.Name = "editWFOV2X";
            this.editWFOV2X.Size = new System.Drawing.Size(103, 20);
            this.editWFOV2X.TabIndex = 20;
            this.toolTip.SetToolTip(this.editWFOV2X, "X offset of WFOV 2 camera in X direction from center of rotation plate");
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.editWFOV1Y);
            this.groupBox5.Controls.Add(this.editWFOV1X);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(186, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(178, 97);
            this.groupBox5.TabIndex = 28;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "WFOV 1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "X offset";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Y offset";
            // 
            // editWFOV1Y
            // 
            this.editWFOV1Y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.editWFOV1Y.DecimalPlaces = 1;
            this.editWFOV1Y.Location = new System.Drawing.Point(69, 46);
            this.editWFOV1Y.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.editWFOV1Y.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.editWFOV1Y.Name = "editWFOV1Y";
            this.editWFOV1Y.Size = new System.Drawing.Size(103, 20);
            this.editWFOV1Y.TabIndex = 18;
            this.toolTip.SetToolTip(this.editWFOV1Y, "Y offset of WFOV 1 camera in X direction from center of rotation plate");
            // 
            // editWFOV1X
            // 
            this.editWFOV1X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.editWFOV1X.DecimalPlaces = 1;
            this.editWFOV1X.Location = new System.Drawing.Point(69, 20);
            this.editWFOV1X.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.editWFOV1X.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.editWFOV1X.Name = "editWFOV1X";
            this.editWFOV1X.Size = new System.Drawing.Size(103, 20);
            this.editWFOV1X.TabIndex = 20;
            this.toolTip.SetToolTip(this.editWFOV1X, "X offset of WFOV 1 camera in X direction from center of rotation plate");
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(183, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(367, 207);
            this.tableLayoutPanel1.TabIndex = 29;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.67F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(550, 207);
            this.tableLayoutPanel2.TabIndex = 30;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.editRotation);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(177, 201);
            this.panel1.TabIndex = 21;
            // 
            // CoordinateSystemSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 207);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CoordinateSystemSettingsForm";
            this.Text = "Coordinate System";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CoordinateSystemSettingsForm_FormClosed);
            this.Load += new System.EventHandler(this.CoordinateSystemSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.editPanelX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPanelY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPanelRotation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editRotation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPanelWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPanelHeight)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editPanelRadius)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editNFOV1Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editNFOV1X)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editNFOV2Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editNFOV2X)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editWFOV2Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editWFOV2X)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editWFOV1Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editWFOV1X)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown editPanelX;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown editPanelY;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown editPanelRotation;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown editRotation;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown editPanelWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown editPanelHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown editPanelRadius;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown editNFOV1Y;
        private System.Windows.Forms.NumericUpDown editNFOV1X;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown editNFOV2Y;
        private System.Windows.Forms.NumericUpDown editNFOV2X;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown editWFOV2Y;
        private System.Windows.Forms.NumericUpDown editWFOV2X;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown editWFOV1Y;
        private System.Windows.Forms.NumericUpDown editWFOV1X;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip;
    }
}