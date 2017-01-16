using System;
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
        protected WFOV wfov1;
        protected bool recording = false;
        protected VCDSimpleProperty VCDProp;
        protected VCDPropertyItem focus = null;

        public Form1(RCCMSystem sys)
        {
            InitializeComponent();

            this.rccm = sys;

            this.nfov1 = new NFOV(this);
            this.wfov1 = new WFOV(this.wfovContainer, this.wfov1Config.Text);

            CameraSelectionDialog camSlnDlg = new CameraSelectionDialog();
            bool retVal = camSlnDlg.ShowModal();
            if (retVal)
            {
                try
                {
                    bool success = this.nfov1.initialize(camSlnDlg.GetSelectedCameraGuids());
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
            if (this.wfov1.isAvailable())
            {
                this.wfov1.initialize();
                btnWfovStart.Enabled = true;

                //  Setup the range of the zoom and focus sliders.
                sliderZoom.Minimum = this.wfov1.getPropertyMin(VCDIDs.VCDID_Zoom);
                sliderZoom.Maximum = this.wfov1.getPropertyMax(VCDIDs.VCDID_Zoom);
                sliderFocus.Minimum = this.wfov1.getPropertyMin(VCDIDs.VCDID_Focus);
                sliderFocus.Maximum = this.wfov1.getPropertyMax(VCDIDs.VCDID_Focus);

                //  Set the sliders to the current zoom and focus values.
                sliderZoom.Value = this.wfov1.getPropertyValue(VCDIDs.VCDID_Zoom);
                textZoom.Text = this.wfov1.getPropertyValue(VCDIDs.VCDID_Zoom).ToString();
                sliderFocus.Value = this.wfov1.getPropertyValue(VCDIDs.VCDID_Focus);
                textFocus.Text = this.wfov1.getPropertyValue(VCDIDs.VCDID_Focus).ToString();
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
            this.wfov1.start();

            // Update button states
            if (wfovContainer.DeviceValid)
            {
                enableWfovControls();
                btnWfovStop.Enabled = true;
            }
        }

        private void btnWfovStop_Click(object sender, EventArgs e)
        {
            this.wfov1.stop();

            // Update button states
            if (!wfovContainer.LiveVideoRunning)
            {
                disableWfovControls();
                btnWfovStart.Enabled = true;
            }
        }
        
        private void btnWfovSnap_Click(object sender, EventArgs e)
        {
            this.wfov1.snapImage(textImageDir.Text + "\test.png");
        }

        private void btnProperties_Click(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                this.wfov1.editProperties();
            }
        }
        
        private void btnWfovRecord_Click(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                if (this.recording == false)
                {
                    wfovContainer.AviStartCapture("test.avi", wfovContainer.AviCompressors[0].ToString());
                    btnWfovRecord.BackColor = Color.Gray;
                    this.recording = true;
                    btnWfovStart.Enabled = false;
                    btnWfovSnap.Enabled = false;
                }
                else
                {
                    wfovContainer.AviStopCapture();
                    wfovContainer.LiveStart();
                    btnWfovRecord.BackColor = Color.Transparent;
                    System.Windows.Forms.MessageBox.Show("Recording stopped");
                    this.recording = false;
                    btnWfovStart.Enabled = true;
                    btnWfovSnap.Enabled = true;
                }
            }
        }

        private void btnFocus_Click(object sender, EventArgs e)
        {
            btnFocus.Enabled = false;
            this.wfov1.autoFocus();
            //sliderFocus.Value = VCDProp.RangeValue[VCDIDs.VCDID_Zoom];
            //textFocus.Text = sliderFocus.Value.ToString();
            btnFocus.Enabled = true;
        }

        private void sliderZoom_Scroll(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                this.wfov1.setZoom(sliderZoom.Value);
                textZoom.Text = sliderZoom.Value.ToString();
            }
        }

        private void sliderFocus_Scroll(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                this.wfov1.setFocus(sliderFocus.Value);
                textFocus.Text = sliderFocus.Value.ToString();
            }
        }

        private void enableWfovControls()
        {
            btnWfovStart.Enabled = true;
            btnWfovStop.Enabled = true;
            btnWfovSnap.Enabled = true;
            btnWfovRecord.Enabled = true;
            btnWfovProperties.Enabled = true;
            btnFocus.Enabled = true;
            sliderFocus.Enabled = true;
            sliderZoom.Enabled = true;
        }

        private void disableWfovControls()
        {
            btnWfovStart.Enabled = false;
            btnWfovStop.Enabled = false;
            btnWfovSnap.Enabled = false;
            btnWfovRecord.Enabled = false;
            btnWfovProperties.Enabled = false;
            btnFocus.Enabled = false;
            sliderFocus.Enabled = false;
            sliderZoom.Enabled = false;
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
            this.nfov1.disconnect();
        }

        private void btnNfovStart_Click(object sender, EventArgs e)
        {
            this.nfov1.start();

            btnNfovStart.Enabled = false;
            btnNfovStop.Enabled = true;
            btnNfovSnap.Enabled = true;
            btnNfovRecord.Enabled = true;
        }

        private void btnNfovStop_Click(object sender, EventArgs e)
        {
            this.nfov1.stop();

            btnNfovStart.Enabled = true;
            btnNfovStop.Enabled = false;
            btnNfovSnap.Enabled = false;
            btnNfovRecord.Enabled = false;
        }

        public void UpdateUI(Bitmap img)
        {
            nfovImage.Image = img;
            Invalidate();
        }

        private void btnNfovProperties_Click(object sender, EventArgs e)
        {
            this.nfov1.showPropertiesDlg();
        }

        private void btnNfovSnap_Click(object sender, EventArgs e)
        {
            this.nfov1.snap("test.bmp");
        }

        private void btnNfovRecord_Click(object sender, EventArgs e)
        {
            if (this.nfov1.isRecording())
            {
                // Stop recording
                this.nfov1.setRecord(false);
                btnNfovRecord.BackColor = Color.Transparent;
                System.Windows.Forms.MessageBox.Show("Recording stopped");
                btnNfovStart.Enabled = true;
                btnNfovStop.Enabled = true;
                btnNfovSnap.Enabled = true;

            }
            else
            {
                // Start recording
                this.nfov1.record();
                btnNfovRecord.BackColor = Color.Gray;
                btnNfovStart.Enabled = false;
                btnNfovStop.Enabled = false;
                btnNfovSnap.Enabled = false;
            }
        }

        private void nfovImage_Click(object sender, EventArgs e)
        {
            Console.Write(e.ToString());
        }
    }
}
