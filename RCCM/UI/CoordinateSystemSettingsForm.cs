﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCCM.UI
{
    public partial class CoordinateSystemSettingsForm : Form
    {
        protected readonly RCCMSystem rccm;

        public CoordinateSystemSettingsForm(RCCMSystem rccm)
        {
            this.rccm = rccm;
            InitializeComponent();
        }

        private void CoordinateSystemSettingsForm_Load(object sender, EventArgs e)
        {
            this.editRotation.Value = (decimal)this.rccm.FineStageAngle;

            this.editPanelRotation.Value = (decimal)this.rccm.PanelAngle;
            this.editPanelX.Value = (decimal)this.rccm.PanelOffsetX;
            this.editPanelY.Value = (decimal)this.rccm.PanelOffsetY;
            this.editPanelRadius.Value = (decimal)this.rccm.PanelRadius;
            this.editPanelHeight.Value = (decimal)this.rccm.PanelHeight;
            this.editPanelWidth.Value = (decimal)this.rccm.PanelWidth;

            this.editNFOV1X.Value = (decimal)this.rccm.NFOV1X;
            this.editNFOV1Y.Value = (decimal)this.rccm.NFOV1Y;
            this.editNFOV2X.Value = (decimal)this.rccm.NFOV2X;
            this.editNFOV2Y.Value = (decimal)this.rccm.NFOV2Y;
            this.editWFOV1X.Value = (decimal)this.rccm.WFOV1X;
            this.editWFOV1Y.Value = (decimal)this.rccm.WFOV1Y;
            this.editWFOV2X.Value = (decimal)this.rccm.WFOV2X;
            this.editWFOV2Y.Value = (decimal)this.rccm.WFOV2Y;
            
            this.addValueChangedHandlers(this);
        }

        private void CoordinateSystemSettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.applySettings();
        }

        private void applySettings()
        {
            this.rccm.FineStageAngle = (double)this.editRotation.Value;

            this.rccm.PanelAngle = (double)this.editPanelRotation.Value;
            this.rccm.PanelOffsetX = (double)this.editPanelX.Value;
            this.rccm.PanelOffsetY = (double)this.editPanelY.Value;
            this.rccm.PanelRadius = (double)this.editPanelRadius.Value;
            this.rccm.PanelHeight = (double)this.editPanelHeight.Value;
            this.rccm.PanelWidth = (double)this.editPanelWidth.Value;

            this.rccm.NFOV1X = (double)this.editNFOV1X.Value;
            this.rccm.NFOV1Y = (double)this.editNFOV1Y.Value;
            this.rccm.NFOV2X = (double)this.editNFOV2X.Value;
            this.rccm.NFOV2Y = (double)this.editNFOV2Y.Value;
            this.rccm.WFOV1X = (double)this.editWFOV1X.Value;
            this.rccm.WFOV1Y = (double)this.editWFOV1Y.Value;
            this.rccm.WFOV2X = (double)this.editWFOV2X.Value;
            this.rccm.WFOV2Y = (double)this.editWFOV2Y.Value;
        }
        
        private void addValueChangedHandlers(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c.GetType() == typeof(NumericUpDown))
                {
                    ((NumericUpDown)c).ValueChanged += new EventHandler(this.controlValueChanged);
                }
                else
                {
                    addValueChangedHandlers(c);
                }
            }
        }

        private void controlValueChanged(object sender, EventArgs e)
        {
            this.applySettings();
            Console.WriteLine("updating");
        }
    }
}