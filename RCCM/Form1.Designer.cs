﻿namespace RCCM
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.wfov2Config = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.wfov1Config = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fine2ZPos = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.fine2YPos = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.fine2XPos = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fine1ZPos = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.fine1YPos = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.fine1XPos = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.coarseYPos = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.coarseXPos = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.wfovSelection = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.textFocus = new System.Windows.Forms.TextBox();
            this.sliderFocus = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            this.textZoom = new System.Windows.Forms.TextBox();
            this.btnFocus = new System.Windows.Forms.Button();
            this.sliderZoom = new System.Windows.Forms.TrackBar();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnSaveBitmap = new System.Windows.Forms.Button();
            this.btnProperties = new System.Windows.Forms.Button();
            this.btnWfovStart = new System.Windows.Forms.Button();
            this.wfovContainer = new TIS.Imaging.ICImagingControl();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.nfovImage = new System.Windows.Forms.PictureBox();
            this.btnNfovStart = new System.Windows.Forms.Button();
            this.btnNfovStop = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.wfovSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderFocus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wfovContainer)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nfovImage)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(769, 564);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(761, 538);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Setup";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.wfov2Config);
            this.groupBox5.Location = new System.Drawing.Point(6, 60);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 48);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "WFOV 2";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Configuration";
            // 
            // wfov2Config
            // 
            this.wfov2Config.Location = new System.Drawing.Point(82, 17);
            this.wfov2Config.Name = "wfov2Config";
            this.wfov2Config.Size = new System.Drawing.Size(100, 20);
            this.wfov2Config.TabIndex = 0;
            this.wfov2Config.Text = "config\\WFOV2.xml";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.wfov1Config);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 48);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "WFOV 1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Configuration";
            // 
            // wfov1Config
            // 
            this.wfov1Config.Location = new System.Drawing.Point(82, 17);
            this.wfov1Config.Name = "wfov1Config";
            this.wfov1Config.Size = new System.Drawing.Size(100, 20);
            this.wfov1Config.TabIndex = 0;
            this.wfov1Config.Text = "config\\WFOV1.xml";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(761, 538);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Motion";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.fine2ZPos);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.fine2YPos);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.fine2XPos);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(342, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(162, 102);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Fine 2";
            // 
            // fine2ZPos
            // 
            this.fine2ZPos.Location = new System.Drawing.Point(27, 70);
            this.fine2ZPos.Name = "fine2ZPos";
            this.fine2ZPos.Size = new System.Drawing.Size(120, 20);
            this.fine2ZPos.TabIndex = 5;
            this.fine2ZPos.ValueChanged += new System.EventHandler(this.fine2ZPos_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Z";
            // 
            // fine2YPos
            // 
            this.fine2YPos.Location = new System.Drawing.Point(27, 44);
            this.fine2YPos.Name = "fine2YPos";
            this.fine2YPos.Size = new System.Drawing.Size(120, 20);
            this.fine2YPos.TabIndex = 3;
            this.fine2YPos.ValueChanged += new System.EventHandler(this.fine2YPos_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Y";
            // 
            // fine2XPos
            // 
            this.fine2XPos.Location = new System.Drawing.Point(27, 18);
            this.fine2XPos.Name = "fine2XPos";
            this.fine2XPos.Size = new System.Drawing.Size(120, 20);
            this.fine2XPos.TabIndex = 1;
            this.fine2XPos.ValueChanged += new System.EventHandler(this.fine2XPos_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "X";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fine1ZPos);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.fine1YPos);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.fine1XPos);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(174, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(162, 102);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fine 1";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // fine1ZPos
            // 
            this.fine1ZPos.Location = new System.Drawing.Point(27, 70);
            this.fine1ZPos.Name = "fine1ZPos";
            this.fine1ZPos.Size = new System.Drawing.Size(120, 20);
            this.fine1ZPos.TabIndex = 5;
            this.fine1ZPos.ValueChanged += new System.EventHandler(this.fine1ZPos_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Z";
            // 
            // fine1YPos
            // 
            this.fine1YPos.Location = new System.Drawing.Point(27, 44);
            this.fine1YPos.Name = "fine1YPos";
            this.fine1YPos.Size = new System.Drawing.Size(120, 20);
            this.fine1YPos.TabIndex = 3;
            this.fine1YPos.ValueChanged += new System.EventHandler(this.fine1YPos_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Y";
            // 
            // fine1XPos
            // 
            this.fine1XPos.Location = new System.Drawing.Point(27, 18);
            this.fine1XPos.Name = "fine1XPos";
            this.fine1XPos.Size = new System.Drawing.Size(120, 20);
            this.fine1XPos.TabIndex = 1;
            this.fine1XPos.ValueChanged += new System.EventHandler(this.fine1XPos_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "X";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.coarseYPos);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.coarseXPos);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(162, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Coarse";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // coarseYPos
            // 
            this.coarseYPos.Location = new System.Drawing.Point(27, 44);
            this.coarseYPos.Name = "coarseYPos";
            this.coarseYPos.Size = new System.Drawing.Size(120, 20);
            this.coarseYPos.TabIndex = 3;
            this.coarseYPos.ValueChanged += new System.EventHandler(this.coarseYPos_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y";
            // 
            // coarseXPos
            // 
            this.coarseXPos.Location = new System.Drawing.Point(27, 18);
            this.coarseXPos.Name = "coarseXPos";
            this.coarseXPos.Size = new System.Drawing.Size(120, 20);
            this.coarseXPos.TabIndex = 1;
            this.coarseXPos.ValueChanged += new System.EventHandler(this.coarseXPos_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnNfovStop);
            this.tabPage4.Controls.Add(this.btnNfovStart);
            this.tabPage4.Controls.Add(this.nfovImage);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(761, 538);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "NFOV";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.wfovSelection);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.textFocus);
            this.tabPage3.Controls.Add(this.sliderFocus);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.textZoom);
            this.tabPage3.Controls.Add(this.btnFocus);
            this.tabPage3.Controls.Add(this.sliderZoom);
            this.tabPage3.Controls.Add(this.btnRecord);
            this.tabPage3.Controls.Add(this.btnSaveBitmap);
            this.tabPage3.Controls.Add(this.btnProperties);
            this.tabPage3.Controls.Add(this.btnWfovStart);
            this.tabPage3.Controls.Add(this.wfovContainer);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(761, 538);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "WFOV";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // wfovSelection
            // 
            this.wfovSelection.Controls.Add(this.radioButton1);
            this.wfovSelection.Controls.Add(this.radioButton2);
            this.wfovSelection.Location = new System.Drawing.Point(269, 487);
            this.wfovSelection.Name = "wfovSelection";
            this.wfovSelection.Size = new System.Drawing.Size(142, 45);
            this.wfovSelection.TabIndex = 17;
            this.wfovSelection.TabStop = false;
            this.wfovSelection.Text = "Camera Selection";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(63, 17);
            this.radioButton1.TabIndex = 15;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "WFOV1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(75, 19);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(63, 17);
            this.radioButton2.TabIndex = 16;
            this.radioButton2.Text = "WFOV2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(699, 3);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Focus";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textFocus
            // 
            this.textFocus.Enabled = false;
            this.textFocus.Location = new System.Drawing.Point(700, 487);
            this.textFocus.Name = "textFocus";
            this.textFocus.Size = new System.Drawing.Size(45, 20);
            this.textFocus.TabIndex = 13;
            this.textFocus.Text = "0";
            this.textFocus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sliderFocus
            // 
            this.sliderFocus.Location = new System.Drawing.Point(700, 19);
            this.sliderFocus.Maximum = 100;
            this.sliderFocus.Name = "sliderFocus";
            this.sliderFocus.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sliderFocus.Size = new System.Drawing.Size(45, 464);
            this.sliderFocus.TabIndex = 12;
            this.sliderFocus.TickStyle = System.Windows.Forms.TickStyle.None;
            this.sliderFocus.Scroll += new System.EventHandler(this.sliderFocus_Scroll);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(649, 3);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Zoom";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textZoom
            // 
            this.textZoom.Enabled = false;
            this.textZoom.Location = new System.Drawing.Point(649, 487);
            this.textZoom.Name = "textZoom";
            this.textZoom.Size = new System.Drawing.Size(45, 20);
            this.textZoom.TabIndex = 10;
            this.textZoom.Text = "0";
            this.textZoom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnFocus
            // 
            this.btnFocus.Location = new System.Drawing.Point(107, 488);
            this.btnFocus.Name = "btnFocus";
            this.btnFocus.Size = new System.Drawing.Size(75, 26);
            this.btnFocus.TabIndex = 9;
            this.btnFocus.Text = "Autofocus";
            this.btnFocus.UseVisualStyleBackColor = true;
            this.btnFocus.Click += new System.EventHandler(this.btnFocus_Click);
            // 
            // sliderZoom
            // 
            this.sliderZoom.Location = new System.Drawing.Point(649, 19);
            this.sliderZoom.Maximum = 100;
            this.sliderZoom.Name = "sliderZoom";
            this.sliderZoom.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sliderZoom.Size = new System.Drawing.Size(45, 464);
            this.sliderZoom.TabIndex = 8;
            this.sliderZoom.TickStyle = System.Windows.Forms.TickStyle.None;
            this.sliderZoom.Scroll += new System.EventHandler(this.sliderZoom_Scroll);
            // 
            // btnRecord
            // 
            this.btnRecord.BackColor = System.Drawing.Color.Transparent;
            this.btnRecord.Image = global::RCCM.Properties.Resources.record;
            this.btnRecord.Location = new System.Drawing.Point(75, 488);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(26, 26);
            this.btnRecord.TabIndex = 5;
            this.btnRecord.UseVisualStyleBackColor = false;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnSaveBitmap
            // 
            this.btnSaveBitmap.Enabled = false;
            this.btnSaveBitmap.Image = global::RCCM.Properties.Resources.snap;
            this.btnSaveBitmap.Location = new System.Drawing.Point(34, 488);
            this.btnSaveBitmap.Name = "btnSaveBitmap";
            this.btnSaveBitmap.Size = new System.Drawing.Size(35, 26);
            this.btnSaveBitmap.TabIndex = 4;
            this.btnSaveBitmap.UseVisualStyleBackColor = true;
            this.btnSaveBitmap.Click += new System.EventHandler(this.btnSaveBitmap_Click);
            // 
            // btnProperties
            // 
            this.btnProperties.Enabled = false;
            this.btnProperties.Location = new System.Drawing.Point(188, 488);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(75, 26);
            this.btnProperties.TabIndex = 3;
            this.btnProperties.Text = "Properties";
            this.btnProperties.UseVisualStyleBackColor = true;
            this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
            // 
            // btnWfovStart
            // 
            this.btnWfovStart.Image = global::RCCM.Properties.Resources.play;
            this.btnWfovStart.Location = new System.Drawing.Point(3, 488);
            this.btnWfovStart.Name = "btnWfovStart";
            this.btnWfovStart.Size = new System.Drawing.Size(26, 26);
            this.btnWfovStart.TabIndex = 2;
            this.btnWfovStart.UseVisualStyleBackColor = true;
            this.btnWfovStart.Click += new System.EventHandler(this.btnWfovStart_Click);
            // 
            // wfovContainer
            // 
            this.wfovContainer.BackColor = System.Drawing.Color.White;
            this.wfovContainer.DeviceListChangedExecutionMode = TIS.Imaging.EventExecutionMode.Invoke;
            this.wfovContainer.DeviceLostExecutionMode = TIS.Imaging.EventExecutionMode.AsyncInvoke;
            this.wfovContainer.ImageAvailableExecutionMode = TIS.Imaging.EventExecutionMode.MultiThreaded;
            this.wfovContainer.LiveDisplayPosition = new System.Drawing.Point(0, 0);
            this.wfovContainer.Location = new System.Drawing.Point(3, 3);
            this.wfovContainer.Name = "wfovContainer";
            this.wfovContainer.Size = new System.Drawing.Size(640, 480);
            this.wfovContainer.TabIndex = 1;
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
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(793, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // nfovImage
            // 
            this.nfovImage.Location = new System.Drawing.Point(6, 6);
            this.nfovImage.Name = "nfovImage";
            this.nfovImage.Size = new System.Drawing.Size(612, 512);
            this.nfovImage.TabIndex = 0;
            this.nfovImage.TabStop = false;
            // 
            // btnNfovStart
            // 
            this.btnNfovStart.Image = global::RCCM.Properties.Resources.play;
            this.btnNfovStart.Location = new System.Drawing.Point(624, 6);
            this.btnNfovStart.Name = "btnNfovStart";
            this.btnNfovStart.Size = new System.Drawing.Size(26, 26);
            this.btnNfovStart.TabIndex = 3;
            this.btnNfovStart.UseVisualStyleBackColor = true;
            this.btnNfovStart.Click += new System.EventHandler(this.btnNfovStart_Click);
            // 
            // btnNfovStop
            // 
            this.btnNfovStop.Enabled = false;
            this.btnNfovStop.Image = global::RCCM.Properties.Resources.stop;
            this.btnNfovStop.Location = new System.Drawing.Point(624, 38);
            this.btnNfovStop.Name = "btnNfovStop";
            this.btnNfovStop.Size = new System.Drawing.Size(26, 26);
            this.btnNfovStop.TabIndex = 4;
            this.btnNfovStop.UseVisualStyleBackColor = true;
            this.btnNfovStop.Click += new System.EventHandler(this.btnNfovStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 596);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "RCCM";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
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
            this.tabPage4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.wfovSelection.ResumeLayout(false);
            this.wfovSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderFocus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wfovContainer)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nfovImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown coarseYPos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown coarseXPos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown fine1ZPos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown fine1YPos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown fine1XPos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown fine2ZPos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown fine2YPos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown fine2XPos;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnProperties;
        private System.Windows.Forms.Button btnWfovStart;
        private TIS.Imaging.ICImagingControl wfovContainer;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button btnSaveBitmap;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox wfov2Config;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox wfov1Config;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textZoom;
        private System.Windows.Forms.Button btnFocus;
        private System.Windows.Forms.TrackBar sliderZoom;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textFocus;
        private System.Windows.Forms.TrackBar sliderFocus;
        private System.Windows.Forms.GroupBox wfovSelection;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button btnNfovStart;
        private System.Windows.Forms.PictureBox nfovImage;
        private System.Windows.Forms.Button btnNfovStop;
    }
}

