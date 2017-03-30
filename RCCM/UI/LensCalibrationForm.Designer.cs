namespace RCCM.UI
{
    partial class LensCalibrationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LensCalibrationForm));
            this.heightEdit = new System.Windows.Forms.NumericUpDown();
            this.focalPowerEdit = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnApplyCalibration = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.listCalibration = new System.Windows.Forms.ListView();
            this.columnHeight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnFocalPower = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.editFocusOffset = new System.Windows.Forms.NumericUpDown();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.heightEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.focalPowerEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editFocusOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // heightEdit
            // 
            this.heightEdit.Location = new System.Drawing.Point(84, 211);
            this.heightEdit.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.heightEdit.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.heightEdit.Name = "heightEdit";
            this.heightEdit.Size = new System.Drawing.Size(142, 20);
            this.heightEdit.TabIndex = 0;
            this.toolTip.SetToolTip(this.heightEdit, "Height at which this focal power is applied");
            this.heightEdit.ValueChanged += new System.EventHandler(this.heightEdit_ValueChanged);
            // 
            // focalPowerEdit
            // 
            this.focalPowerEdit.DecimalPlaces = 2;
            this.focalPowerEdit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.focalPowerEdit.Location = new System.Drawing.Point(84, 237);
            this.focalPowerEdit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.focalPowerEdit.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.focalPowerEdit.Name = "focalPowerEdit";
            this.focalPowerEdit.Size = new System.Drawing.Size(142, 20);
            this.focalPowerEdit.TabIndex = 1;
            this.toolTip.SetToolTip(this.focalPowerEdit, "Focal power to lens. Click and adjust with arrow keys or buttons");
            this.focalPowerEdit.ValueChanged += new System.EventHandler(this.focalPowerEdit_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 213);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Height";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Focal Power";
            // 
            // btnApplyCalibration
            // 
            this.btnApplyCalibration.Location = new System.Drawing.Point(12, 289);
            this.btnApplyCalibration.Name = "btnApplyCalibration";
            this.btnApplyCalibration.Size = new System.Drawing.Size(214, 23);
            this.btnApplyCalibration.TabIndex = 4;
            this.btnApplyCalibration.Text = "Apply";
            this.toolTip.SetToolTip(this.btnApplyCalibration, "Click to save this calibration point");
            this.btnApplyCalibration.UseVisualStyleBackColor = true;
            this.btnApplyCalibration.Click += new System.EventHandler(this.btnApplyCalibration_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 318);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.toolTip.SetToolTip(this.btnSave, "Save entire calibration and exit");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(122, 318);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.toolTip.SetToolTip(this.btnCancel, "Revert calibration and exit");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // listCalibration
            // 
            this.listCalibration.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeight,
            this.columnFocalPower});
            this.listCalibration.FullRowSelect = true;
            this.listCalibration.Location = new System.Drawing.Point(12, 12);
            this.listCalibration.MultiSelect = false;
            this.listCalibration.Name = "listCalibration";
            this.listCalibration.Size = new System.Drawing.Size(214, 193);
            this.listCalibration.TabIndex = 8;
            this.toolTip.SetToolTip(this.listCalibration, "List of calibration points. Click to highlight and press delete to remove points");
            this.listCalibration.UseCompatibleStateImageBehavior = false;
            this.listCalibration.View = System.Windows.Forms.View.Details;
            this.listCalibration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listCalibration_KeyDown);
            // 
            // columnHeight
            // 
            this.columnHeight.Text = "Height (mm)";
            this.columnHeight.Width = 105;
            // 
            // columnFocalPower
            // 
            this.columnFocalPower.Text = "Focal Power";
            this.columnFocalPower.Width = 105;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 265);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Focus Offset";
            // 
            // editFocusOffset
            // 
            this.editFocusOffset.DecimalPlaces = 2;
            this.editFocusOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.editFocusOffset.Location = new System.Drawing.Point(84, 263);
            this.editFocusOffset.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.editFocusOffset.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.editFocusOffset.Name = "editFocusOffset";
            this.editFocusOffset.Size = new System.Drawing.Size(142, 20);
            this.editFocusOffset.TabIndex = 9;
            this.toolTip.SetToolTip(this.editFocusOffset, "When focus is slightly off, use this to slightly adjust focus");
            this.editFocusOffset.ValueChanged += new System.EventHandler(this.editFocusOffset_ValueChanged);
            // 
            // LensCalibrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 349);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.editFocusOffset);
            this.Controls.Add(this.listCalibration);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnApplyCalibration);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.focalPowerEdit);
            this.Controls.Add(this.heightEdit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LensCalibrationForm";
            this.Text = "Lens Calibration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LensCalibrationForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.heightEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.focalPowerEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editFocusOffset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown heightEdit;
        private System.Windows.Forms.NumericUpDown focalPowerEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnApplyCalibration;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListView listCalibration;
        private System.Windows.Forms.ColumnHeader columnHeight;
        private System.Windows.Forms.ColumnHeader columnFocalPower;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown editFocusOffset;
        private System.Windows.Forms.ToolTip toolTip;
    }
}