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
            this.label5 = new System.Windows.Forms.Label();
            this.btnCheckStatus = new System.Windows.Forms.Button();
            this.btnZero = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.editPosition = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.editValue)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editPosition)).BeginInit();
            this.SuspendLayout();
            // 
            // dropdownProperty
            // 
            this.dropdownProperty.Dock = System.Windows.Forms.DockStyle.Top;
            this.dropdownProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropdownProperty.FormattingEnabled = true;
            this.dropdownProperty.Location = new System.Drawing.Point(147, 97);
            this.dropdownProperty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dropdownProperty.Name = "dropdownProperty";
            this.dropdownProperty.Size = new System.Drawing.Size(207, 33);
            this.dropdownProperty.TabIndex = 1;
            this.dropdownProperty.SelectedIndexChanged += new System.EventHandler(this.dropdownProperty_SelectedIndexChanged);
            // 
            // editValue
            // 
            this.editValue.DecimalPlaces = 2;
            this.editValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.editValue.Location = new System.Drawing.Point(147, 143);
            this.editValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.editValue.Size = new System.Drawing.Size(207, 30);
            this.editValue.TabIndex = 2;
            this.editValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.editValue_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 138);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 92);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Property";
            // 
            // checkBoxEnable
            // 
            this.checkBoxEnable.AutoSize = true;
            this.checkBoxEnable.Location = new System.Drawing.Point(147, 51);
            this.checkBoxEnable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxEnable.Name = "checkBoxEnable";
            this.checkBoxEnable.Size = new System.Drawing.Size(18, 17);
            this.checkBoxEnable.TabIndex = 5;
            this.checkBoxEnable.UseVisualStyleBackColor = true;
            this.checkBoxEnable.CheckedChanged += new System.EventHandler(this.checkBoxEnable_CheckedChanged);
            // 
            // dropdownMotor
            // 
            this.dropdownMotor.Dock = System.Windows.Forms.DockStyle.Top;
            this.dropdownMotor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropdownMotor.FormattingEnabled = true;
            this.dropdownMotor.Location = new System.Drawing.Point(147, 5);
            this.dropdownMotor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dropdownMotor.Name = "dropdownMotor";
            this.dropdownMotor.Size = new System.Drawing.Size(207, 33);
            this.dropdownMotor.TabIndex = 6;
            this.dropdownMotor.SelectedIndexChanged += new System.EventHandler(this.dropdownMotor_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 25);
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
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnCheckStatus, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnZero, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.editPosition, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(358, 357);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 46);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Enabled";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 276);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 25);
            this.label5.TabIndex = 10;
            this.label5.Text = "Info";
            // 
            // btnCheckStatus
            // 
            this.btnCheckStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCheckStatus.Location = new System.Drawing.Point(147, 281);
            this.btnCheckStatus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCheckStatus.Name = "btnCheckStatus";
            this.btnCheckStatus.Size = new System.Drawing.Size(207, 71);
            this.btnCheckStatus.TabIndex = 9;
            this.btnCheckStatus.Text = "Status";
            this.btnCheckStatus.UseVisualStyleBackColor = true;
            this.btnCheckStatus.Click += new System.EventHandler(this.btnCheckStatus_Click);
            // 
            // btnZero
            // 
            this.btnZero.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnZero.Location = new System.Drawing.Point(146, 187);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(209, 40);
            this.btnZero.TabIndex = 11;
            this.btnZero.Text = "Clear Error";
            this.btnZero.UseVisualStyleBackColor = true;
            this.btnZero.Click += new System.EventHandler(this.btnZero_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 25);
            this.label6.TabIndex = 12;
            this.label6.Text = "Errors";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 230);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 46);
            this.label7.TabIndex = 13;
            this.label7.Text = "Correct Position";
            // 
            // editPosition
            // 
            this.editPosition.DecimalPlaces = 2;
            this.editPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editPosition.Location = new System.Drawing.Point(146, 233);
            this.editPosition.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.editPosition.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.editPosition.Name = "editPosition";
            this.editPosition.Size = new System.Drawing.Size(209, 30);
            this.editPosition.TabIndex = 14;
            this.editPosition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.editPosition_KeyDown);
            // 
            // MotorSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(358, 357);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MotorSettingsForm";
            this.Text = "Motor Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MotorSettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.MotorSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.editValue)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editPosition)).EndInit();
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
        private System.Windows.Forms.Button btnZero;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown editPosition;
    }
}