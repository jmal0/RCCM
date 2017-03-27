namespace RCCM.UI
{
    partial class MotorSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MotorSettingsForm));
            this.dropdownProperty = new System.Windows.Forms.ComboBox();
            this.editValue = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxEnable = new System.Windows.Forms.CheckBox();
            this.dropdownMotor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCheckStatus = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.editValue)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dropdownProperty
            // 
            this.dropdownProperty.Dock = System.Windows.Forms.DockStyle.Top;
            this.dropdownProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropdownProperty.FormattingEnabled = true;
            this.dropdownProperty.Location = new System.Drawing.Point(98, 63);
            this.dropdownProperty.Name = "dropdownProperty";
            this.dropdownProperty.Size = new System.Drawing.Size(138, 21);
            this.dropdownProperty.TabIndex = 1;
            this.dropdownProperty.SelectedIndexChanged += new System.EventHandler(this.dropdownProperty_SelectedIndexChanged);
            // 
            // editValue
            // 
            this.editValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.editValue.Location = new System.Drawing.Point(98, 93);
            this.editValue.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.editValue.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.editValue.Name = "editValue";
            this.editValue.Size = new System.Drawing.Size(138, 20);
            this.editValue.TabIndex = 2;
            this.editValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.editValue_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Property";
            // 
            // checkBoxEnable
            // 
            this.checkBoxEnable.AutoSize = true;
            this.checkBoxEnable.Location = new System.Drawing.Point(98, 33);
            this.checkBoxEnable.Name = "checkBoxEnable";
            this.checkBoxEnable.Size = new System.Drawing.Size(15, 14);
            this.checkBoxEnable.TabIndex = 5;
            this.checkBoxEnable.UseVisualStyleBackColor = true;
            this.checkBoxEnable.CheckedChanged += new System.EventHandler(this.checkBoxEnable_CheckedChanged);
            // 
            // dropdownMotor
            // 
            this.dropdownMotor.Dock = System.Windows.Forms.DockStyle.Top;
            this.dropdownMotor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropdownMotor.FormattingEnabled = true;
            this.dropdownMotor.Location = new System.Drawing.Point(98, 3);
            this.dropdownMotor.Name = "dropdownMotor";
            this.dropdownMotor.Size = new System.Drawing.Size(138, 21);
            this.dropdownMotor.TabIndex = 6;
            this.dropdownMotor.SelectedIndexChanged += new System.EventHandler(this.dropdownMotor_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Motor";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.dropdownMotor, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.editValue, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.dropdownProperty, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxEnable, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnCheckStatus, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(239, 150);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Enabled";
            // 
            // btnCheckStatus
            // 
            this.btnCheckStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCheckStatus.Location = new System.Drawing.Point(98, 123);
            this.btnCheckStatus.Name = "btnCheckStatus";
            this.btnCheckStatus.Size = new System.Drawing.Size(138, 24);
            this.btnCheckStatus.TabIndex = 9;
            this.btnCheckStatus.Text = "Status";
            this.btnCheckStatus.UseVisualStyleBackColor = true;
            this.btnCheckStatus.Click += new System.EventHandler(this.btnCheckStatus_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Info";
            // 
            // MotorSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(239, 150);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MotorSettingsForm";
            this.Text = "Motor Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MotorSettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.MotorSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.editValue)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox dropdownProperty;
        private System.Windows.Forms.NumericUpDown editValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxEnable;
        private System.Windows.Forms.ComboBox dropdownMotor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCheckStatus;
        private System.Windows.Forms.Label label5;
    }
}