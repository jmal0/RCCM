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
    public partial class AboutRCCMForm : Form
    {
        public AboutRCCMForm()
        {
            InitializeComponent();
        }

        private void linkGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void AboutRCCMForm_Load(object sender, EventArgs e)
        {
            this.linkGithub.Links.Add(0, 6, "https://github.com/jmal0/RCCM");
        }
    }
}
