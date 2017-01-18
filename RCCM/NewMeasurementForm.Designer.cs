namespace RCCM
{
    partial class NewMeasurementSequenceForm
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
            this.textName = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.radioRccm1 = new System.Windows.Forms.RadioButton();
            this.radioRccm2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.colorPicker = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(53, 6);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(100, 20);
            this.textName.TabIndex = 0;
            // 
            // radioRccm1
            // 
            this.radioRccm1.AutoSize = true;
            this.radioRccm1.Checked = true;
            this.radioRccm1.Location = new System.Drawing.Point(6, 19);
            this.radioRccm1.Name = "radioRccm1";
            this.radioRccm1.Size = new System.Drawing.Size(65, 17);
            this.radioRccm1.TabIndex = 1;
            this.radioRccm1.TabStop = true;
            this.radioRccm1.Text = "RCCM 1";
            this.radioRccm1.UseVisualStyleBackColor = true;
            // 
            // radioRccm2
            // 
            this.radioRccm2.AutoSize = true;
            this.radioRccm2.Location = new System.Drawing.Point(6, 42);
            this.radioRccm2.Name = "radioRccm2";
            this.radioRccm2.Size = new System.Drawing.Size(65, 17);
            this.radioRccm2.TabIndex = 2;
            this.radioRccm2.Text = "RCCM 2";
            this.radioRccm2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioRccm1);
            this.groupBox1.Controls.Add(this.radioRccm2);
            this.groupBox1.Location = new System.Drawing.Point(15, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 71);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parent Stage";
            // 
            // colorPicker
            // 
            this.colorPicker.AutoSize = true;
            this.colorPicker.BackColor = System.Drawing.Color.Red;
            this.colorPicker.Location = new System.Drawing.Point(72, 31);
            this.colorPicker.Name = "colorPicker";
            this.colorPicker.Padding = new System.Windows.Forms.Padding(8, 1, 8, 1);
            this.colorPicker.Size = new System.Drawing.Size(16, 15);
            this.colorPicker.TabIndex = 18;
            this.colorPicker.Click += new System.EventHandler(this.colorPicker_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Line Color";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(15, 127);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(65, 26);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(89, 127);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 26);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // NewMeasurementSequenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(169, 161);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.colorPicker);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textName);
            this.Name = "NewMeasurementSequenceForm";
            this.Text = "New";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.RadioButton radioRccm1;
        private System.Windows.Forms.RadioButton radioRccm2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label colorPicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}