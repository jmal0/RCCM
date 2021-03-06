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
    /// <summary>
    /// Form for starting plugin and entering inputs
    /// </summary>
    public partial class PluginInitializationForm : Form
    {
        protected readonly RCCMSystem rccm;
        protected readonly IRCCMPlugin plugin;
        protected IRCCMPluginActor actor;
        protected TableLayoutPanel tableLayoutPanelParams;
        protected Dictionary<string, TextBox> parameterControls;

        /// <summary>
        /// Open form for given plugin
        /// </summary>
        /// <param name="rccm">Reference to RCCM object</param>
        /// <param name="plugin">Plugin to be started from this form</param>
        public PluginInitializationForm(RCCMSystem rccm, IRCCMPlugin plugin)
        {
            this.rccm = rccm;
            this.plugin = plugin;
            
            InitializeComponent();

            // Add rows for each input and give them a fixed size
            this.tableLayoutPanelParams = new TableLayoutPanel();
            this.tableLayoutPanelParams.Dock = DockStyle.Fill;
            this.tableLayoutPanelParams.ColumnCount = 2;
            this.tableLayoutPanelParams.RowCount = plugin.Params.Length;
            this.tableLayoutPanelGrid.Controls.Add(this.tableLayoutPanelParams, 0, 0);
            this.parameterControls = new Dictionary<string, TextBox>();
            for (int i = 0; i < plugin.Params.Length; i++)
            {
                this.tableLayoutPanelParams.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                // Add label
                Label paramLabel = new Label();
                paramLabel.Text = plugin.Params[i];
                this.tableLayoutPanelParams.Controls.Add(paramLabel, 0, i);
                // Add control
                TextBox paramTextBox = new TextBox();
                paramTextBox.Dock = DockStyle.Fill;
                this.tableLayoutPanelParams.Controls.Add(paramTextBox, 1, i);
                // Add textbox to dictionary of parameter controls
                parameterControls[plugin.Params[i]] = paramTextBox;
            }
            this.Text = plugin.Name;
            this.buttonStop.Enabled = false;
            this.Height = 80 + 32 * this.plugin.Params.Length;
            this.tableLayoutPanelGrid.RowStyles[0].Height = 32 * this.plugin.Params.Length;
        }

        /// <summary>
        /// Start plugin with user entered inputs
        /// </summary>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            // Get parameter values
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            foreach (string param in this.plugin.Params)
            {
                parameters[param] = this.parameterControls[param].Text;
            }

            // Initialize plugin
            try
            {
                this.actor = plugin.Instance(rccm, parameters);
            }
            catch (Exception ex)
            {
                Logger.Out(ex.Message);
                MessageBox.Show("Error occured while initializing plugin " + this.plugin.Name + "\nDetails:\n" + ex.Message);
                return;
            }

            // Run plugin actor in background thread
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += delegate (object doWorkSender, DoWorkEventArgs doWorkArgs)
            {
                try
                {
                    actor.Run();
                }
                catch (Exception ex)
                {
                    Logger.Out(ex.Message);
                    MessageBox.Show("Error occured in plugin " + this.plugin.Name + "\nDetails:\n" + ex.Message);
                }
            };
            // On completion, exit window
            bw.RunWorkerCompleted += delegate (object doneSender, RunWorkerCompletedEventArgs doneArgs) 
            {
                this.Close();
            };
            // Run plugin
            bw.RunWorkerAsync();
            // Set button states so that user can stop plugin
            this.buttonStart.Enabled = false;
            this.buttonStop.Enabled = true;
        }

        /// <summary>
        /// Stop plugin
        /// </summary>
        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.actor.Stop();
        }
    }
}
