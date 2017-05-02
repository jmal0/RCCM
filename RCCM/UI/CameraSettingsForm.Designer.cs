namespace RCCM.UI
{
    partial class CameraSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraSettingsForm));
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textNFOV1DataDir = new System.Windows.Forms.TextBox();
            this.nfov1Scale = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textNFOV1VideoDir = new System.Windows.Forms.TextBox();
            this.textNFOV1ImageDir = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textWFOV2DataDir = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textWFOV2VideoDir = new System.Windows.Forms.TextBox();
            this.textWFOV2ImageDir = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.wfov2Scale = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.wfov2Config = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textWFOV1DataDir = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textWFOV1VideoDir = new System.Windows.Forms.TextBox();
            this.textWFOV1ImageDir = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.wfov1Scale = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.wfov1Config = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textNFOV2DataDir = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textNFOV2VideoDir = new System.Windows.Forms.TextBox();
            this.textNFOV2ImageDir = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.nfov2Scale = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label17);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Controls.Add(this.textNFOV1DataDir);
            this.groupBox7.Controls.Add(this.nfov1Scale);
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Controls.Add(this.textNFOV1VideoDir);
            this.groupBox7.Controls.Add(this.textNFOV1ImageDir);
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(4, 5);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox7.Size = new System.Drawing.Size(329, 234);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "NFOV 1";
            this.toolTip.SetToolTip(this.groupBox7, "Select folder where measurement data taken from WFOV 1 will save");
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 154);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(108, 20);
            this.label17.TabIndex = 5;
            this.label17.Text = "Data directory";
            this.toolTip.SetToolTip(this.label17, "Select folder where measurement data taken from NFOV 1 will save");
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 31);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(91, 20);
            this.label15.TabIndex = 0;
            this.label15.Text = "units/pixel";
            this.toolTip.SetToolTip(this.label15, "Image resolution calibration. Equal to image width in units / 2448");
            // 
            // textNFOV1DataDir
            // 
            this.textNFOV1DataDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textNFOV1DataDir.Location = new System.Drawing.Point(170, 149);
            this.textNFOV1DataDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textNFOV1DataDir.Name = "textNFOV1DataDir";
            this.textNFOV1DataDir.Size = new System.Drawing.Size(148, 26);
            this.textNFOV1DataDir.TabIndex = 5;
            this.textNFOV1DataDir.Text = "data";
            this.toolTip.SetToolTip(this.textNFOV1DataDir, "Select folder where measurement data taken from NFOV 1 will save");
            this.textNFOV1DataDir.Enter += new System.EventHandler(this.textNFOV1DataDir_Enter);
            // 
            // nfov1Scale
            // 
            this.nfov1Scale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nfov1Scale.Location = new System.Drawing.Point(170, 29);
            this.nfov1Scale.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nfov1Scale.Name = "nfov1Scale";
            this.nfov1Scale.Size = new System.Drawing.Size(148, 26);
            this.nfov1Scale.TabIndex = 0;
            this.nfov1Scale.Text = "9.08";
            this.toolTip.SetToolTip(this.nfov1Scale, "Image resolution calibration. Equal to image width in units / 2448");
            this.nfov1Scale.TextChanged += new System.EventHandler(this.nfov1Scale_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 114);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(114, 20);
            this.label14.TabIndex = 4;
            this.label14.Text = "Video directory";
            this.toolTip.SetToolTip(this.label14, "Select folder where videos captured from NFOV 1 camera will save");
            // 
            // textNFOV1VideoDir
            // 
            this.textNFOV1VideoDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textNFOV1VideoDir.Location = new System.Drawing.Point(170, 109);
            this.textNFOV1VideoDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textNFOV1VideoDir.Name = "textNFOV1VideoDir";
            this.textNFOV1VideoDir.Size = new System.Drawing.Size(148, 26);
            this.textNFOV1VideoDir.TabIndex = 1;
            this.textNFOV1VideoDir.Text = "C:\\Users\\John\\Videos\\RCCM";
            this.toolTip.SetToolTip(this.textNFOV1VideoDir, "Select folder where videos captured from NFOV 1 camera will save");
            this.textNFOV1VideoDir.Enter += new System.EventHandler(this.textNFOV1VideoDir_Enter);
            // 
            // textNFOV1ImageDir
            // 
            this.textNFOV1ImageDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textNFOV1ImageDir.Location = new System.Drawing.Point(170, 69);
            this.textNFOV1ImageDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textNFOV1ImageDir.Name = "textNFOV1ImageDir";
            this.textNFOV1ImageDir.Size = new System.Drawing.Size(148, 26);
            this.textNFOV1ImageDir.TabIndex = 0;
            this.textNFOV1ImageDir.Text = "C:\\Users\\John\\Pictures\\RCCM";
            this.toolTip.SetToolTip(this.textNFOV1ImageDir, "Select folder where images captured from NFOV 1 camera will save");
            this.textNFOV1ImageDir.Enter += new System.EventHandler(this.textNFOV1ImageDir_Enter);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 74);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(118, 20);
            this.label13.TabIndex = 0;
            this.label13.Text = "Image directory";
            this.toolTip.SetToolTip(this.label13, "Select folder where images captured from NFOV 1 camera will save");
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.textWFOV2DataDir);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.textWFOV2VideoDir);
            this.groupBox5.Controls.Add(this.textWFOV2ImageDir);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.wfov2Scale);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.wfov2Config);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(341, 249);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Size = new System.Drawing.Size(329, 234);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "WFOV 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 151);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Data directory";
            this.toolTip.SetToolTip(this.label4, "Select folder where measurement data ideo taken from WFOV 2 will save");
            // 
            // textWFOV2DataDir
            // 
            this.textWFOV2DataDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textWFOV2DataDir.Location = new System.Drawing.Point(163, 146);
            this.textWFOV2DataDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textWFOV2DataDir.Name = "textWFOV2DataDir";
            this.textWFOV2DataDir.Size = new System.Drawing.Size(148, 26);
            this.textWFOV2DataDir.TabIndex = 11;
            this.textWFOV2DataDir.Text = "data";
            this.toolTip.SetToolTip(this.textWFOV2DataDir, "Select folder where measurement data ideo taken from WFOV 2 will save");
            this.textWFOV2DataDir.Enter += new System.EventHandler(this.textWFOV2DataDir_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 111);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Video directory";
            this.toolTip.SetToolTip(this.label5, "Select folder where videos captured from WFOV 2 camera will save");
            // 
            // textWFOV2VideoDir
            // 
            this.textWFOV2VideoDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textWFOV2VideoDir.Location = new System.Drawing.Point(163, 106);
            this.textWFOV2VideoDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textWFOV2VideoDir.Name = "textWFOV2VideoDir";
            this.textWFOV2VideoDir.Size = new System.Drawing.Size(148, 26);
            this.textWFOV2VideoDir.TabIndex = 8;
            this.textWFOV2VideoDir.Text = "C:\\Users\\John\\Videos\\RCCM";
            this.toolTip.SetToolTip(this.textWFOV2VideoDir, "Select folder where videos captured from WFOV 2 camera will save");
            this.textWFOV2VideoDir.Enter += new System.EventHandler(this.textWFOV2VideoDir_Enter);
            // 
            // textWFOV2ImageDir
            // 
            this.textWFOV2ImageDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textWFOV2ImageDir.Location = new System.Drawing.Point(163, 66);
            this.textWFOV2ImageDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textWFOV2ImageDir.Name = "textWFOV2ImageDir";
            this.textWFOV2ImageDir.Size = new System.Drawing.Size(148, 26);
            this.textWFOV2ImageDir.TabIndex = 6;
            this.textWFOV2ImageDir.Text = "C:\\Users\\John\\Pictures\\RCCM";
            this.toolTip.SetToolTip(this.textWFOV2ImageDir, "Select folder where images captured from WFOV 2 camera will save");
            this.textWFOV2ImageDir.Enter += new System.EventHandler(this.textWFOV2ImageDir_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 71);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Image directory";
            this.toolTip.SetToolTip(this.label6, "Select folder where images captured from WFOV 2 camera will save");
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 31);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 20);
            this.label12.TabIndex = 3;
            this.label12.Text = "units/pixel";
            this.toolTip.SetToolTip(this.label12, "Image resolution calibration. Equal to image width in units / 1280");
            // 
            // wfov2Scale
            // 
            this.wfov2Scale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wfov2Scale.Location = new System.Drawing.Point(163, 26);
            this.wfov2Scale.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.wfov2Scale.Name = "wfov2Scale";
            this.wfov2Scale.Size = new System.Drawing.Size(148, 26);
            this.wfov2Scale.TabIndex = 4;
            this.wfov2Scale.Text = "9.08";
            this.toolTip.SetToolTip(this.wfov2Scale, "Image resolution calibration. Equal to image width in units / 1280");
            this.wfov2Scale.TextChanged += new System.EventHandler(this.wfov2Scale_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 189);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "Configuration";
            // 
            // wfov2Config
            // 
            this.wfov2Config.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wfov2Config.Location = new System.Drawing.Point(163, 185);
            this.wfov2Config.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.wfov2Config.Name = "wfov2Config";
            this.wfov2Config.Size = new System.Drawing.Size(148, 26);
            this.wfov2Config.TabIndex = 0;
            this.wfov2Config.Text = "config\\WFOV2.xml";
            this.wfov2Config.TextChanged += new System.EventHandler(this.wfov2Config_TextChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.textWFOV1DataDir);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.textWFOV1VideoDir);
            this.groupBox4.Controls.Add(this.textWFOV1ImageDir);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.wfov1Scale);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.wfov1Config);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(341, 5);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(329, 234);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "WFOV 1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 154);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Data directory";
            this.toolTip.SetToolTip(this.label1, "Select folder where measurement data taken from WFOV 1 will save");
            // 
            // textWFOV1DataDir
            // 
            this.textWFOV1DataDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textWFOV1DataDir.Location = new System.Drawing.Point(167, 149);
            this.textWFOV1DataDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textWFOV1DataDir.Name = "textWFOV1DataDir";
            this.textWFOV1DataDir.Size = new System.Drawing.Size(148, 26);
            this.textWFOV1DataDir.TabIndex = 11;
            this.textWFOV1DataDir.Text = "data";
            this.toolTip.SetToolTip(this.textWFOV1DataDir, "Select folder where measurement data taken from WFOV 1 will save");
            this.textWFOV1DataDir.Enter += new System.EventHandler(this.textWFOV1DataDir_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 114);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Video directory";
            this.toolTip.SetToolTip(this.label2, "Select folder where videos captured from WFOV 1 camera will save");
            // 
            // textWFOV1VideoDir
            // 
            this.textWFOV1VideoDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textWFOV1VideoDir.Location = new System.Drawing.Point(167, 109);
            this.textWFOV1VideoDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textWFOV1VideoDir.Name = "textWFOV1VideoDir";
            this.textWFOV1VideoDir.Size = new System.Drawing.Size(148, 26);
            this.textWFOV1VideoDir.TabIndex = 8;
            this.textWFOV1VideoDir.Text = "C:\\Users\\John\\Videos\\RCCM";
            this.toolTip.SetToolTip(this.textWFOV1VideoDir, "Select folder where videos captured from WFOV 1 camera will save");
            this.textWFOV1VideoDir.Enter += new System.EventHandler(this.textWFOV1VideoDir_Enter);
            // 
            // textWFOV1ImageDir
            // 
            this.textWFOV1ImageDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textWFOV1ImageDir.Location = new System.Drawing.Point(167, 69);
            this.textWFOV1ImageDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textWFOV1ImageDir.Name = "textWFOV1ImageDir";
            this.textWFOV1ImageDir.Size = new System.Drawing.Size(148, 26);
            this.textWFOV1ImageDir.TabIndex = 6;
            this.textWFOV1ImageDir.Text = "C:\\Users\\John\\Pictures\\RCCM";
            this.toolTip.SetToolTip(this.textWFOV1ImageDir, "Select folder where images captured from WFOV 1 camera will save");
            this.textWFOV1ImageDir.Enter += new System.EventHandler(this.textWFOV1ImageDir_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 74);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Image directory";
            this.toolTip.SetToolTip(this.label3, "Select folder where images captured from WFOV 1 camera will save");
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 31);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 20);
            this.label11.TabIndex = 1;
            this.label11.Text = "units/pixel";
            this.toolTip.SetToolTip(this.label11, "Image resolution calibration. Equal to image width in units / 1280");
            // 
            // wfov1Scale
            // 
            this.wfov1Scale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wfov1Scale.Location = new System.Drawing.Point(167, 29);
            this.wfov1Scale.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.wfov1Scale.Name = "wfov1Scale";
            this.wfov1Scale.Size = new System.Drawing.Size(148, 26);
            this.wfov1Scale.TabIndex = 2;
            this.wfov1Scale.Text = "9.08";
            this.toolTip.SetToolTip(this.wfov1Scale, "Image resolution calibration. Equal to image width in units / 1280");
            this.wfov1Scale.TextChanged += new System.EventHandler(this.wfov1Scale_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 194);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "Configuration";
            // 
            // wfov1Config
            // 
            this.wfov1Config.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wfov1Config.Location = new System.Drawing.Point(167, 189);
            this.wfov1Config.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.wfov1Config.Name = "wfov1Config";
            this.wfov1Config.Size = new System.Drawing.Size(148, 26);
            this.wfov1Config.TabIndex = 0;
            this.wfov1Config.Text = "config\\WFOV1.xml";
            this.wfov1Config.TextChanged += new System.EventHandler(this.wfov1Config_TextChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label7);
            this.groupBox8.Controls.Add(this.textNFOV2DataDir);
            this.groupBox8.Controls.Add(this.label8);
            this.groupBox8.Controls.Add(this.textNFOV2VideoDir);
            this.groupBox8.Controls.Add(this.textNFOV2ImageDir);
            this.groupBox8.Controls.Add(this.label18);
            this.groupBox8.Controls.Add(this.label16);
            this.groupBox8.Controls.Add(this.nfov2Scale);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox8.Location = new System.Drawing.Point(4, 249);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox8.Size = new System.Drawing.Size(329, 234);
            this.groupBox8.TabIndex = 9;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "NFOV 2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 151);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "Data directory";
            this.toolTip.SetToolTip(this.label7, "Select folder where measurement data taken from NFOV 2 will save");
            // 
            // textNFOV2DataDir
            // 
            this.textNFOV2DataDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textNFOV2DataDir.Location = new System.Drawing.Point(170, 146);
            this.textNFOV2DataDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textNFOV2DataDir.Name = "textNFOV2DataDir";
            this.textNFOV2DataDir.Size = new System.Drawing.Size(148, 26);
            this.textNFOV2DataDir.TabIndex = 11;
            this.textNFOV2DataDir.Text = "data";
            this.toolTip.SetToolTip(this.textNFOV2DataDir, "Select folder where measurement data taken from NFOV 2 will save");
            this.textNFOV2DataDir.Enter += new System.EventHandler(this.textNFOV2DataDir_Enter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 111);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 20);
            this.label8.TabIndex = 9;
            this.label8.Text = "Video directory";
            this.toolTip.SetToolTip(this.label8, "Select folder where videos captured from NFOV 2 camera will save");
            // 
            // textNFOV2VideoDir
            // 
            this.textNFOV2VideoDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textNFOV2VideoDir.Location = new System.Drawing.Point(170, 106);
            this.textNFOV2VideoDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textNFOV2VideoDir.Name = "textNFOV2VideoDir";
            this.textNFOV2VideoDir.Size = new System.Drawing.Size(148, 26);
            this.textNFOV2VideoDir.TabIndex = 8;
            this.textNFOV2VideoDir.Text = "C:\\Users\\John\\Videos\\RCCM";
            this.toolTip.SetToolTip(this.textNFOV2VideoDir, "Select folder where videos captured from NFOV 2 camera will save");
            this.textNFOV2VideoDir.Enter += new System.EventHandler(this.textNFOV2VideoDir_Enter);
            // 
            // textNFOV2ImageDir
            // 
            this.textNFOV2ImageDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textNFOV2ImageDir.Location = new System.Drawing.Point(170, 66);
            this.textNFOV2ImageDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textNFOV2ImageDir.Name = "textNFOV2ImageDir";
            this.textNFOV2ImageDir.Size = new System.Drawing.Size(148, 26);
            this.textNFOV2ImageDir.TabIndex = 6;
            this.textNFOV2ImageDir.Text = "C:\\Users\\John\\Pictures\\RCCM";
            this.toolTip.SetToolTip(this.textNFOV2ImageDir, "Select folder where images captured from NFOV 2 camera will save");
            this.textNFOV2ImageDir.Enter += new System.EventHandler(this.textNFOV2ImageDir_Enter);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 71);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(118, 20);
            this.label18.TabIndex = 7;
            this.label18.Text = "Image directory";
            this.toolTip.SetToolTip(this.label18, "Select folder where images captured from NFOV 2 camera will save");
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 31);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(91, 20);
            this.label16.TabIndex = 0;
            this.label16.Text = "units/pixel";
            this.toolTip.SetToolTip(this.label16, "Image resolution calibration. Equal to image width in units / 2448");
            // 
            // nfov2Scale
            // 
            this.nfov2Scale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nfov2Scale.Location = new System.Drawing.Point(170, 26);
            this.nfov2Scale.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nfov2Scale.Name = "nfov2Scale";
            this.nfov2Scale.Size = new System.Drawing.Size(148, 26);
            this.nfov2Scale.TabIndex = 0;
            this.nfov2Scale.Text = "9.08";
            this.toolTip.SetToolTip(this.nfov2Scale, "Image resolution calibration. Equal to image width in units / 2448");
            this.nfov2Scale.TextChanged += new System.EventHandler(this.nfov2Scale_TextChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox7, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox8, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(674, 488);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // CameraSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 488);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CameraSettingsForm";
            this.Text = "Camera Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CameraSettingsForm_FormClosed);
            this.Load += new System.EventHandler(this.CameraSettingsForm_Load);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox nfov1Scale;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox wfov2Scale;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox wfov2Config;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox wfov1Scale;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox wfov1Config;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textNFOV1DataDir;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textNFOV1VideoDir;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textNFOV1ImageDir;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox nfov2Scale;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textWFOV2DataDir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textWFOV2VideoDir;
        private System.Windows.Forms.TextBox textWFOV2ImageDir;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textWFOV1DataDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textWFOV1VideoDir;
        private System.Windows.Forms.TextBox textWFOV1ImageDir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textNFOV2DataDir;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textNFOV2VideoDir;
        private System.Windows.Forms.TextBox textNFOV2ImageDir;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ToolTip toolTip;
    }
}