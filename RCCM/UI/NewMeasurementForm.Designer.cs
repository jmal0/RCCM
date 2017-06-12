namespace RCCM.UI
{
    partial class NewMeasurementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewMeasurementForm));
            this.textName = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorPicker = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.editLineSize = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioMeasureTotal = new System.Windows.Forms.RadioButton();
            this.radioMeasureProjection = new System.Windows.Forms.RadioButton();
            this.radioMeasureTip = new System.Windows.Forms.RadioButton();
            this.editOrientation = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.editLineSize)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editOrientation)).BeginInit();
            this.SuspendLayout();
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(80, 9);
            this.textName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(144, 26);
            this.textName.TabIndex = 0;
            this.toolTip.SetToolTip(this.textName, "Enter an identifying name for the crack measurement");
            // 
            // colorPicker
            // 
            this.colorPicker.AutoSize = true;
            this.colorPicker.BackColor = System.Drawing.Color.Red;
            this.colorPicker.Location = new System.Drawing.Point(108, 48);
            this.colorPicker.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.colorPicker.Name = "colorPicker";
            this.colorPicker.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.colorPicker.Size = new System.Drawing.Size(24, 24);
            this.colorPicker.TabIndex = 18;
            this.toolTip.SetToolTip(this.colorPicker, "Click to change the color of the lines that will be drawn on the live imaeg");
            this.colorPicker.Click += new System.EventHandler(this.colorPicker_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "Line Color";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(18, 311);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(98, 40);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "OK";
            this.toolTip.SetToolTip(this.btnOK, "Click to create crack");
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(129, 311);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 40);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.toolTip.SetToolTip(this.btnCancel, "Cancel crack measurement creation");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 78);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 23;
            this.label3.Text = "Line Size";
            // 
            // editLineSize
            // 
            this.editLineSize.DecimalPlaces = 1;
            this.editLineSize.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.editLineSize.Location = new System.Drawing.Point(112, 75);
            this.editLineSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.editLineSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.editLineSize.Name = "editLineSize";
            this.editLineSize.Size = new System.Drawing.Size(112, 26);
            this.editLineSize.TabIndex = 24;
            this.toolTip.SetToolTip(this.editLineSize, "Thickness of line on live image");
            this.editLineSize.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioMeasureTotal);
            this.groupBox2.Controls.Add(this.radioMeasureProjection);
            this.groupBox2.Controls.Add(this.radioMeasureTip);
            this.groupBox2.Location = new System.Drawing.Point(18, 155);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(207, 145);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Measurement mode";
            // 
            // radioMeasureTotal
            // 
            this.radioMeasureTotal.AutoSize = true;
            this.radioMeasureTotal.Location = new System.Drawing.Point(9, 100);
            this.radioMeasureTotal.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioMeasureTotal.Name = "radioMeasureTotal";
            this.radioMeasureTotal.Size = new System.Drawing.Size(110, 24);
            this.radioMeasureTotal.TabIndex = 3;
            this.radioMeasureTotal.Text = "Total length";
            this.toolTip.SetToolTip(this.radioMeasureTotal, "Sum lengths of each segment of the crack");
            this.radioMeasureTotal.UseVisualStyleBackColor = true;
            // 
            // radioMeasureProjection
            // 
            this.radioMeasureProjection.AutoSize = true;
            this.radioMeasureProjection.Location = new System.Drawing.Point(9, 29);
            this.radioMeasureProjection.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioMeasureProjection.Name = "radioMeasureProjection";
            this.radioMeasureProjection.Size = new System.Drawing.Size(97, 24);
            this.radioMeasureProjection.TabIndex = 1;
            this.radioMeasureProjection.Text = "Projection";
            this.toolTip.SetToolTip(this.radioMeasureProjection, "Measure length of crack in direction specified by orientation angle");
            this.radioMeasureProjection.UseVisualStyleBackColor = true;
            // 
            // radioMeasureTip
            // 
            this.radioMeasureTip.AutoSize = true;
            this.radioMeasureTip.Checked = true;
            this.radioMeasureTip.Location = new System.Drawing.Point(9, 65);
            this.radioMeasureTip.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioMeasureTip.Name = "radioMeasureTip";
            this.radioMeasureTip.Size = new System.Drawing.Size(87, 24);
            this.radioMeasureTip.TabIndex = 2;
            this.radioMeasureTip.TabStop = true;
            this.radioMeasureTip.Text = "Tip to tip";
            this.toolTip.SetToolTip(this.radioMeasureTip, "Measure straight line distance from crack beginning to end");
            this.radioMeasureTip.UseVisualStyleBackColor = true;
            // 
            // editOrientation
            // 
            this.editOrientation.DecimalPlaces = 1;
            this.editOrientation.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.editOrientation.Location = new System.Drawing.Point(112, 115);
            this.editOrientation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.editOrientation.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.editOrientation.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.editOrientation.Name = "editOrientation";
            this.editOrientation.Size = new System.Drawing.Size(112, 26);
            this.editOrientation.TabIndex = 28;
            this.toolTip.SetToolTip(this.editOrientation, "Angular orientation to use with \"projection\" mode");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 118);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 20);
            this.label4.TabIndex = 27;
            this.label4.Text = "Orientation";
            // 
            // NewMeasurementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 363);
            this.Controls.Add(this.editOrientation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.editLineSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.colorPicker);
            this.Controls.Add(this.textName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "NewMeasurementForm";
            this.Text = "New Measurement";
            ((System.ComponentModel.ISupportInitialize)(this.editLineSize)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editOrientation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label colorPicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown editLineSize;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioMeasureTotal;
        private System.Windows.Forms.RadioButton radioMeasureProjection;
        private System.Windows.Forms.RadioButton radioMeasureTip;
        private System.Windows.Forms.NumericUpDown editOrientation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip;
    }
}