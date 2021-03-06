﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using RCCM.UI;

namespace RCCM
{
    /// <summary>
    /// Main program
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Global settings variable containing all user defined settings
        /// </summary>
        public static Settings Settings { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Use mutex to prevent multiple instances of program
            object[] assemblyObjects = System.Reflection.Assembly.GetEntryAssembly().GetCustomAttributes(typeof(GuidAttribute), true);
            Guid appGuid = new Guid(((GuidAttribute) assemblyObjects[0]).Value);            
            using (Mutex mutex = new Mutex(false, "Global\\" + appGuid.ToString()))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("RCCM is already open.");
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Load settings
                string filename;
                if (args.Length == 0)
                {
                    filename = "settings.json";
                }
                else
                {
                    filename = args[0];
                }
                
                try
                {
                    Program.Settings = new Settings(filename);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error encountered in settings file:\n\n" + ex.Message);
                    return;
                }

                // Load plugins
                ICollection<IRCCMPlugin> plugins = RCCMPluginLoader.LoadPlugins((string) Program.Settings.json["plugin directory"]);

                // Start GUI
                Application.Run(new RCCMMainForm(plugins));
            }
        }
    }
}
