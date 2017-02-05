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
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string filename;
            if (args.Length == 0)
            {
                filename = "settings.json";
            }
            else
            {
                filename = args[0];
            }

            Settings settings;
            RCCMSystem rccm;
            try
            {
                settings = new Settings(filename);
                rccm = new RCCMSystem(settings);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error encountered in settings file:\n\n" + ex.Message);
                return;
            }
            
            // Start GUI
            Application.Run(new RCCMMainForm(rccm, settings));
        }
    }
}
