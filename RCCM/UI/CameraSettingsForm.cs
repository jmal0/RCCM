using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCCM.UI
{
    public partial class CameraSettingsForm : Form
    {
        protected readonly RCCMSystem rccm;

        public CameraSettingsForm(RCCMSystem rccm)
        {
            this.rccm = rccm;
            InitializeComponent();
        }

        private void nfov1Scale_TextChanged(object sender, EventArgs e)
        {
            double newScale;
            if (Double.TryParse(this.nfov1Scale.Text, out newScale))
            {
                this.rccm.NFOV1.Scale = newScale;
                Program.Settings.json["nfov 1"]["microns / pixel"] = newScale;
            }
        }

        private void nfov2Scale_TextChanged(object sender, EventArgs e)
        {
            double newScale;
            if (Double.TryParse(this.nfov2Scale.Text, out newScale))
            {
                this.rccm.NFOV2.Scale = newScale;
                Program.Settings.json["nfov 2"]["microns / pixel"] = newScale;
            }
        }

        private void wfov1Scale_TextChanged(object sender, EventArgs e)
        {
            double newScale;
            if (Double.TryParse(this.wfov1Scale.Text, out newScale))
            {
                this.rccm.WFOV1.Scale = newScale;
                Program.Settings.json["wfov 1"]["microns / pixel"] = newScale;
            }
        }

        private void wfov2Scale_TextChanged(object sender, EventArgs e)
        {
            double newScale;
            if (Double.TryParse(this.wfov2Scale.Text, out newScale))
            {
                this.rccm.WFOV2.Scale = newScale;
                Program.Settings.json["wfov 2"]["microns / pixel"] = newScale;
            }
        }

        private void CameraSettingsForm_Load(object sender, EventArgs e)
        {
            this.nfov1Scale.Text = (string)Program.Settings.json["nfov 1"]["microns / pixel"];
            this.textNFOV1ImageDir.Text = (string)Program.Settings.json["nfov 1"]["image directory"];
            this.textNFOV1VideoDir.Text = (string)Program.Settings.json["nfov 1"]["video directory"];
            this.textNFOV1DataDir.Text = (string)Program.Settings.json["nfov 1"]["test data directory"];

            this.nfov2Scale.Text = (string)Program.Settings.json["nfov 2"]["microns / pixel"];
            this.textNFOV2ImageDir.Text = (string)Program.Settings.json["nfov 2"]["image directory"];
            this.textNFOV2VideoDir.Text = (string)Program.Settings.json["nfov 2"]["video directory"];
            this.textNFOV2DataDir.Text = (string)Program.Settings.json["nfov 2"]["test data directory"];

            this.wfov1Scale.Text = (string)Program.Settings.json["wfov 1"]["microns / pixel"];
            this.textWFOV1ImageDir.Text = (string)Program.Settings.json["wfov 1"]["image directory"];
            this.textWFOV1VideoDir.Text = (string)Program.Settings.json["wfov 1"]["video directory"];
            this.textWFOV1DataDir.Text = (string)Program.Settings.json["wfov 1"]["test data directory"];

            this.wfov2Scale.Text = (string)Program.Settings.json["wfov 2"]["microns / pixel"];
            this.textWFOV2ImageDir.Text = (string)Program.Settings.json["wfov 2"]["image directory"];
            this.textWFOV2VideoDir.Text = (string)Program.Settings.json["wfov 2"]["video directory"];
            this.textWFOV2DataDir.Text = (string)Program.Settings.json["wfov 2"]["test data directory"];
        }
        
        private void textNFOV1ImageDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("nfov 1", "image directory");
        }

        private void textNFOV2ImageDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("nfov 2", "image directory");
        }

        private void textWFOV1ImageDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("wfov 1", "image directory");
        }

        private void textWFOV2ImageDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("wfov 2", "image directory");
        }

        private void textNFOV1VideoDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("nfov 1", "video directory");
        }

        private void textNFOV2VideoDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("nfov 2", "video directory");
        }

        private void textWFOV1VideoDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("wfov 1", "video directory");
        }

        private void textWFOV2VideoDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("wfov 2", "video directory");
        }

        private void textNFOV1DataDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("nfov 1", "test data directory");
        }

        private void textNFOV2DataDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("nfov 2", "test data directory");
        }

        private void textWFOV1DataDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("wfov 1", "test data directory");
        }

        private void textWFOV2DataDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("wfov 2", "test data directory");
        }

        private void showDirectoryDialog(string camera, string directory)
        {
            DialogResult result = this.folderBrowserDialog.ShowDialog();
            Console.WriteLine(this.folderBrowserDialog.SelectedPath);
            if (Directory.Exists(this.folderBrowserDialog.SelectedPath) || Directory.CreateDirectory(this.folderBrowserDialog.SelectedPath).Exists)
            {
                this.textNFOV1ImageDir.Text = this.folderBrowserDialog.SelectedPath;
                Program.Settings.json[camera][directory] = this.folderBrowserDialog.SelectedPath;
            }
        }

        private void CameraSettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Settings.save();
        }
    }
}
