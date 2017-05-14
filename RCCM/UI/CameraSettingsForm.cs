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
    /// <summary>
    /// Form for adjusting some camera settings
    /// </summary>
    public partial class CameraSettingsForm : Form
    {
        protected readonly RCCMSystem rccm;

        /// <summary>
        /// Create camera settings form
        /// </summary>
        /// <param name="rccm">Reference to RCCM object</param>
        public CameraSettingsForm(RCCMSystem rccm)
        {
            this.rccm = rccm;
            InitializeComponent();
        }

        /// <summary>
        /// Sets scale for NFOV 1 camera if input is valid
        /// </summary>
        private void nfov1Scale_TextChanged(object sender, EventArgs e)
        {
            double newScale;
            if (Double.TryParse(this.nfov1Scale.Text, out newScale) &&
                newScale != (double)Program.Settings.json["nfov 1"]["units / pixel"] &&
                newScale != 0)
            {
                this.rccm.NFOV1.SetScale(this.rccm, newScale);
                Program.Settings.json["nfov 1"]["units / pixel"] = newScale;
            }
        }

        /// <summary>
        /// Sets scale for NFOV 2 camera if input is valid
        /// </summary>
        private void nfov2Scale_TextChanged(object sender, EventArgs e)
        {
            double newScale;
            if (Double.TryParse(this.nfov2Scale.Text, out newScale) &&
                newScale != (double)Program.Settings.json["nfov 2"]["units / pixel"] &&
                newScale != 0)
            {
                this.rccm.NFOV2.SetScale(this.rccm, newScale);
                Program.Settings.json["nfov 2"]["units / pixel"] = newScale;
            }
        }

        /// <summary>
        /// Sets scale for WFOV 1 camera if input is valid
        /// </summary>
        private void wfov1Scale_TextChanged(object sender, EventArgs e)
        {
            double newScale;
            if (Double.TryParse(this.wfov1Scale.Text, out newScale) &&
                newScale != (double)Program.Settings.json["wfov 1"]["units / pixel"] &&
                newScale != 0)
            {
                this.rccm.WFOV1.SetScale(this.rccm, newScale);
                Program.Settings.json["wfov 1"]["units / pixel"] = newScale;
            }
        }

        /// <summary>
        /// Sets scale for WFOV 2 camera if input is valid
        /// </summary>
        private void wfov2Scale_TextChanged(object sender, EventArgs e)
        {
            double newScale;
            if (Double.TryParse(this.wfov2Scale.Text, out newScale) &&
                newScale != (double)Program.Settings.json["wfov 2"]["units / pixel"] &&
                newScale != 0)
            {
                this.rccm.WFOV2.SetScale(this.rccm, newScale);
                Program.Settings.json["wfov 2"]["units / pixel"] = newScale;
            }
        }

        /// <summary>
        /// Loads camera settings to form
        /// </summary>
        private void CameraSettingsForm_Load(object sender, EventArgs e)
        {
            this.applySettings();
        }

        /// <summary>
        /// Opens a dialog box for selecting save directory for NFOV 1 images
        /// </summary>
        private void textNFOV1ImageDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("nfov 1", "image directory");
        }

        /// <summary>
        /// Opens a dialog box for selecting save directory for NFOV 2 images
        /// </summary>
        private void textNFOV2ImageDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("nfov 2", "image directory");
        }

        /// <summary>
        /// Opens a dialog box for selecting save directory for WFOV 1 images
        /// </summary>
        private void textWFOV1ImageDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("wfov 1", "image directory");
        }

        /// <summary>
        /// Opens a dialog box for selecting save directory for WFOV 2 images
        /// </summary>
        private void textWFOV2ImageDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("wfov 2", "image directory");
        }

        /// <summary>
        /// Opens a dialog box for selecting save directory for NFOV 1 videos
        /// </summary>
        private void textNFOV1VideoDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("nfov 1", "video directory");
        }

        /// <summary>
        /// Opens a dialog box for selecting save directory for NFOV 2 videos
        /// </summary>
        private void textNFOV2VideoDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("nfov 2", "video directory");
        }

        /// <summary>
        /// Opens a dialog box for selecting save directory for WFOV 1 videos
        /// </summary>
        private void textWFOV1VideoDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("wfov 1", "video directory");
        }

        /// <summary>
        /// Opens a dialog box for selecting save directory for WFOV 2 videos
        /// </summary>
        private void textWFOV2VideoDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("wfov 2", "video directory");
        }

        /// <summary>
        /// Opens a dialog box for selecting save directory for NFOV 1 data
        /// </summary>
        private void textNFOV1DataDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("nfov 1", "test data directory");
        }

        /// <summary>
        /// Opens a dialog box for selecting save directory for NFOV 2 data
        /// </summary>
        private void textNFOV2DataDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("nfov 2", "test data directory");
        }

        /// <summary>
        /// Opens a dialog box for selecting save directory for WFOV 1 data
        /// </summary>
        private void textWFOV1DataDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("wfov 1", "test data directory");
        }

        /// <summary>
        /// Opens a dialog box for selecting save directory for WFOV 2 data
        /// </summary>
        private void textWFOV2DataDir_Enter(object sender, EventArgs e)
        {
            this.showDirectoryDialog("wfov 2", "test data directory");
        }

        /// <summary>
        /// Opens a dialog box for selecting a directory
        /// </summary>
        /// <remarks>
        /// User selected directory is added to settings if it is valid. This function
        /// will create a directory if it does not exist
        /// </remarks>
        /// <returns>True if directory is valid</returns>
        private bool showDirectoryDialog(string camera, string directory)
        {
            DialogResult result = this.folderBrowserDialog.ShowDialog();
            Console.WriteLine(this.folderBrowserDialog.SelectedPath);
            if (this.folderBrowserDialog.SelectedPath != "" && (Directory.Exists(this.folderBrowserDialog.SelectedPath) || Directory.CreateDirectory(this.folderBrowserDialog.SelectedPath).Exists))
            {
                Program.Settings.json[camera][directory] = this.folderBrowserDialog.SelectedPath;
                this.applySettings();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Loads camera settings to form
        /// </summary>
        private void applySettings()
        {
            this.nfov1Scale.Text = (string)Program.Settings.json["nfov 1"]["units / pixel"];
            this.textNFOV1ImageDir.Text = (string)Program.Settings.json["nfov 1"]["image directory"];
            this.textNFOV1VideoDir.Text = (string)Program.Settings.json["nfov 1"]["video directory"];
            this.textNFOV1DataDir.Text = (string)Program.Settings.json["nfov 1"]["test data directory"];

            this.nfov2Scale.Text = (string)Program.Settings.json["nfov 2"]["units / pixel"];
            this.textNFOV2ImageDir.Text = (string)Program.Settings.json["nfov 2"]["image directory"];
            this.textNFOV2VideoDir.Text = (string)Program.Settings.json["nfov 2"]["video directory"];
            this.textNFOV2DataDir.Text = (string)Program.Settings.json["nfov 2"]["test data directory"];

            this.wfov1Scale.Text = (string)Program.Settings.json["wfov 1"]["units / pixel"];
            this.textWFOV1ImageDir.Text = (string)Program.Settings.json["wfov 1"]["image directory"];
            this.textWFOV1VideoDir.Text = (string)Program.Settings.json["wfov 1"]["video directory"];
            this.textWFOV1DataDir.Text = (string)Program.Settings.json["wfov 1"]["test data directory"];

            this.wfov2Scale.Text = (string)Program.Settings.json["wfov 2"]["units / pixel"];
            this.textWFOV2ImageDir.Text = (string)Program.Settings.json["wfov 2"]["image directory"];
            this.textWFOV2VideoDir.Text = (string)Program.Settings.json["wfov 2"]["video directory"];
            this.textWFOV2DataDir.Text = (string)Program.Settings.json["wfov 2"]["test data directory"];
        }

        /// <summary>
        /// Saves settings to file on exit
        /// </summary>
        private void CameraSettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Settings.save();
        }

        /// <summary>
        /// Change configuration file for WFOV 1 camera
        /// </summary>
        private void wfov1Config_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.json["wfov 1"]["configuration file"] = this.wfov1Config.Text;
        }

        /// <summary>
        /// Change configuration file for WFOV 2 camera
        /// </summary>
        private void wfov2Config_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.json["wfov 2"]["configuration file"] = this.wfov2Config.Text;
        }
    }
}
