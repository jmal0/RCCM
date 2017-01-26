using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCCM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Open dialog for user to select file containing RCCM settings
            OpenFileDialog settingsDlg = new OpenFileDialog();
            settingsDlg.Filter = "JSON files|*.json";
            settingsDlg.Title = "Select RCCM configuration file";

            Settings settings;
            RCCMSystem rccm;
            if (settingsDlg.ShowDialog() == DialogResult.OK)
            {
                settings = new Settings(settingsDlg.FileName);
                rccm = new RCCMSystem(settings);
            }
            else
            {
                // Default
                settings = new Settings("settings.json");
                rccm = new RCCMSystem(settings);
            }

            // Start GUI
            Application.Run(new RCCMMainForm(rccm, settings));
        }
    }
}
