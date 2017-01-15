using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TIS.Imaging;
using TIS.Imaging.VCDHelpers;

namespace RCCM
{
    public partial class Form1 : Form
    {
        private bool recording = false;
        private VCDSimpleProperty VCDProp;
        private VCDPropertyItem focus = null;

        public Form1()
        {
            InitializeComponent();
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
    }
}
