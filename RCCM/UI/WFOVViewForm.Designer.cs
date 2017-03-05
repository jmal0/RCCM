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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFocus = new System.Windows.Forms.Button();
            this.btnWfovProperties = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.textFocus = new System.Windows.Forms.TextBox();
            this.textZoom = new System.Windows.Forms.TextBox();
            this.sliderFocus = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            this.sliderZoom = new System.Windows.Forms.TrackBar();
            this.btnWfovStop = new System.Windows.Forms.Button();
            this.btnWfovRecord = new System.Windows.Forms.Button();
            this.btnWfovSnap = new System.Windows.Forms.Button();
            this.btnWfovStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.wfovContainer)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderFocus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // wfovContainer
            // 
            this.wfovContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
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
            this.wfovContainer.Size = new System.Drawing.Size(644, 480);
            this.wfovContainer.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 646F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(757, 513);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.btnWfovStop);
            this.panel1.Controls.Add(this.btnFocus);
            this.panel1.Controls.Add(this.btnWfovProperties);
            this.panel1.Controls.Add(this.btnWfovRecord);
            this.panel1.Controls.Add(this.btnWfovSnap);
            this.panel1.Controls.Add(this.btnWfovStart);
            this.panel1.Controls.Add(this.wfovContainer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 507);
            this.panel1.TabIndex = 4;
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
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.textFocus);
            this.panel2.Controls.Add(this.textZoom);
            this.panel2.Controls.Add(this.sliderFocus);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.sliderZoom);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(649, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(147, 507);
            this.panel2.TabIndex = 5;
            // 
            // label12
            // 
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
            this.textFocus.Enabled = false;
            this.textFocus.Location = new System.Drawing.Point(54, 484);
            this.textFocus.Name = "textFocus";
            this.textFocus.Size = new System.Drawing.Size(45, 20);
            this.textFocus.TabIndex = 19;
            this.textFocus.Text = "0";
            this.textFocus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textZoom
            // 
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
            this.sliderFocus.Enabled = false;
            this.sliderFocus.Location = new System.Drawing.Point(54, 16);
            this.sliderFocus.Maximum = 100;
            this.sliderFocus.Name = "sliderFocus";
            this.sliderFocus.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sliderFocus.Size = new System.Drawing.Size(45, 464);
            this.sliderFocus.TabIndex = 18;
            this.sliderFocus.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // label11
            // 
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
            this.sliderZoom.Enabled = false;
            this.sliderZoom.Location = new System.Drawing.Point(3, 16);
            this.sliderZoom.Maximum = 100;
            this.sliderZoom.Name = "sliderZoom";
            this.sliderZoom.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sliderZoom.Size = new System.Drawing.Size(45, 464);
            this.sliderZoom.TabIndex = 15;
            this.sliderZoom.TickStyle = System.Windows.Forms.TickStyle.None;
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
            // 
            // WFOVViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 513);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WFOVViewForm";
            this.Text = "WFOVViewForm";
            ((System.ComponentModel.ISupportInitialize)(this.wfovContainer)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderFocus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderZoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TIS.Imaging.ICImagingControl wfovContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
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
    }
}