using System;
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
    /// <summary>
    /// Form for displaying info about program and RCCM team
    /// </summary>
    public partial class AboutRCCMForm : Form
    {
        public AboutRCCMForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Open link to github page with source code
        /// </summary>
        private void linkGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        /// <summary>
        /// Adds github link when form opens
        /// </summary>
        private void AboutRCCMForm_Load(object sender, EventArgs e)
        {
            this.linkGithub.Links.Add(0, 6, "https://github.com/jmal0/RCCM");
        }
    }
}
