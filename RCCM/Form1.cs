﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// WFOV Imports
using TIS.Imaging;
using TIS.Imaging.VCDHelpers;

// NFOV Imports
using FlyCapture2Managed;
using FlyCapture2Managed.Gui;

namespace RCCM
{
    public partial class Form1 : Form
    {
        protected RCCMSystem rccm;
        protected NFOV nfov1;
        protected bool recording = false;
        protected VCDSimpleProperty VCDProp;
        protected VCDPropertyItem focus = null;

        public Form1(RCCMSystem sys)
        {
            InitializeComponent();

            this.rccm = sys;

            this.nfov1 = new NFOV(this);

            CameraSelectionDialog camSlnDlg = new CameraSelectionDialog();
            bool retVal = camSlnDlg.ShowModal();
            if (retVal)
            {
                try
                {
                    bool success = nfov1.initialize(camSlnDlg.GetSelectedCameraGuids());
                    if (!success)
                    {
                        Close();
                        return;
                    }
                }
                catch (FC2Exception ex)
                {
                    Console.WriteLine("Failed to load form successfully: " + ex.Message);
                    Close();
                }

                btnNfovStart.Enabled = false;
                btnNfovStop.Enabled = true;
            }
            else
            {
                Close();
            }

            Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            wfovContainer.LoadDeviceStateFromFile(wfov1Config.Text, true);
            if (wfovContainer.DeviceValid)
            {
                wfovContainer.LivePrepare();

                this.VCDProp = VCDSimpleModule.GetSimplePropertyContainer(wfovContainer.VCDPropertyItems);
                this.focus = wfovContainer.VCDPropertyItems.FindItem(VCDIDs.VCDElement_OnePush);

                //  Setup the range of the zoom and focus sliders.
                sliderZoom.Minimum = VCDProp.RangeMin(VCDIDs.VCDID_Zoom);
                sliderZoom.Maximum = VCDProp.RangeMax(VCDIDs.VCDID_Zoom);
                sliderFocus.Minimum = VCDProp.RangeMin(VCDIDs.VCDID_Focus);
                sliderFocus.Maximum = VCDProp.RangeMax(VCDIDs.VCDID_Focus);

                //  Set the sliders to the current zoom and focus values.
                sliderZoom.Value = VCDProp.RangeValue[VCDIDs.VCDID_Zoom];
                textZoom.Text = VCDProp.RangeValue[VCDIDs.VCDID_Zoom].ToString();
                sliderFocus.Value = VCDProp.RangeValue[VCDIDs.VCDID_Focus];
                textFocus.Text = VCDProp.RangeValue[VCDIDs.VCDID_Focus].ToString();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnWfovStart_Click(object sender, EventArgs e)
        {
            WFOVStart();
        }
        
        private void WFOVStart()
        {
            // Device live, suspend it
            if (wfovContainer.LiveVideoRunning)
            {
                wfovContainer.LiveSuspend();
                btnWfovStart.Image = RCCM.Properties.Resources.play;
                btnSaveBitmap.Enabled = false;
                btnProperties.Enabled = false;
            }
            // Device suspended, start it
            else
            {
                try
                {
                    wfovContainer.LiveStart();
                    wfovContainer.LiveDisplayDefault = false;
                    wfovContainer.LiveDisplayHeight = wfovContainer.ImageHeight / 2;
                    wfovContainer.LiveDisplayWidth = wfovContainer.ImageWidth / 2;
                    wfovContainer.ScrollbarsEnabled = true;

                    // Update button states
                    if (wfovContainer.DeviceValid)
                    {
                        btnWfovStart.Image = RCCM.Properties.Resources.stop;
                        btnSaveBitmap.Enabled = true;
                        btnProperties.Enabled = true;
                    }
                    else
                    {
                        btnWfovStart.Image = RCCM.Properties.Resources.play;
                        btnSaveBitmap.Enabled = false;
                        btnProperties.Enabled = false;
                    }
                }
                catch (TIS.Imaging.ICException err)
                {
                    throw err;
                }
            }
        }

        private void btnSaveBitmap_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1;
            try
            {
                wfovContainer.MemorySnapImage();
            }
            catch (TIS.Imaging.ICException err)
            {
                throw err;
            }
            
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "bmp files (*.png)|*.png|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                wfovContainer.MemorySaveImage(saveFileDialog1.FileName);
            }
        }

        private void btnProperties_Click(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                wfovContainer.ShowPropertyDialog();
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                if (this.recording == false)
                {
                    wfovContainer.AviStartCapture("test.avi", wfovContainer.AviCompressors[0].ToString());
                    btnRecord.BackColor = Color.Gray;
                    this.recording = true;
                    btnWfovStart.Enabled = false;
                    btnSaveBitmap.Enabled = false;
                }
                else
                {
                    wfovContainer.AviStopCapture();
                    wfovContainer.LiveStart();
                    btnRecord.BackColor = Color.Transparent;
                    System.Windows.Forms.MessageBox.Show("Recording stopped");
                    this.recording = false;
                    btnWfovStart.Enabled = true;
                    btnSaveBitmap.Enabled = true;
                }
            }
        }

        private void btnFocus_Click(object sender, EventArgs e)
        {
            btnFocus.Enabled = false;
            VCDProp.OnePush(VCDIDs.VCDID_Focus);
            //sliderFocus.Value = VCDProp.RangeValue[VCDIDs.VCDID_Zoom];
            //textFocus.Text = sliderFocus.Value.ToString();
            btnFocus.Enabled = true;
        }

        private void sliderZoom_Scroll(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                VCDProp.RangeValue[VCDIDs.VCDID_Zoom] = sliderZoom.Value;
                textZoom.Text = sliderZoom.Value.ToString();
            }
        }

        private void sliderFocus_Scroll(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                VCDProp.RangeValue[VCDIDs.VCDID_Focus] = sliderFocus.Value;
                textFocus.Text = sliderFocus.Value.ToString();
            }
        }

        private void coarseXPos_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.setCoarseX((double) coarseXPos.Value);
        }

        private void coarseYPos_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.setCoarseY((double) coarseYPos.Value);
        }

        private void fine1XPos_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.setFine1X((double) fine1XPos.Value);
        }

        private void fine1YPos_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.setFine1Y((double) fine1YPos.Value);
        }

        private void fine1ZPos_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.setFine1Z((double) fine1ZPos.Value);
        }

        private void fine2XPos_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.setFine2X((double) fine2XPos.Value);
        }

        private void fine2YPos_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.setFine2Y((double) fine2YPos.Value);
        }

        private void fine2ZPos_ValueChanged(object sender, EventArgs e)
        {
            this.rccm.setFine2Z((double) fine2ZPos.Value);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            nfov1.disconnect();
        }

        private void btnNfovStart_Click(object sender, EventArgs e)
        {
            this.nfov1.start();

            btnNfovStart.Enabled = false;
            btnNfovStop.Enabled = true;
        }

        private void btnNfovStop_Click(object sender, EventArgs e)
        {
            this.nfov1.stop();

            btnNfovStart.Enabled = true;
            btnNfovStop.Enabled = false;
        }

        public void UpdateUI(Bitmap img)
        {
            nfovImage.Image = img;
            Invalidate();
        }
    }
}
