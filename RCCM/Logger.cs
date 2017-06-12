using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCCM
{
    /// <summary>
    /// Helper object for logging debug information with relevant metadata
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Log file where debug info will be saved
        /// </summary>
        public static StreamWriter Logfile = new StreamWriter("log/" + string.Format("{0:yyyy-MM-dd_hh-mm-ss}", DateTime.Now) + ".txt", false);

        /// <summary>
        /// Write a line to the logfile
        /// </summary>
        /// <param name="str">Line to write</param>
        /// <param name="path">Path of file containing calling function (automatic)</param>
        /// <param name="lineNumber">Line number of calling function (automatic)</param>
        /// <param name="caller">Calling function (automatic)</param>
        public static void Out(string str, [CallerFilePath] string path = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = "")
        {
            Console.WriteLine(str);

            string metadata = string.Format("{0:yyyy-MM-dd_hh-mm-ss-fff} {1}:{2}:{3}", DateTime.Now, path, lineNumber, caller);
            Logfile.WriteLine(metadata + " - " + str);
            Logfile.Flush();
        }

        /// <summary>
        /// Load positions from file
        /// </summary>
        public static void LoadPositions(RCCMSystem rccm)
        {
            try
            {
                string[] pairs = File.ReadAllText("log/positions.csv").Split('\n');

                foreach (string pair in pairs)
                {
                    string[] values = pair.Split(',');
                    if (values.Length == 2)
                    {
                        string motor = values[0];
                        double position = Double.Parse(values[1]);
                        rccm.motors[motor].FixPosition(position);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to load positions file");
            }
        }

        /// <summary>
        /// Write positions to file
        /// </summary>
        public static void SavePositions(RCCMSystem rccm)
        {
            using (StreamWriter positionsFile = new StreamWriter("log/positions.csv"))
            {
                positionsFile.Write(String.Format("coarse X,{0}\n", rccm.motors["coarse X"].GetActuatorPos()));
                positionsFile.Write(String.Format("coarse Y,{0}\n", rccm.motors["coarse Y"].GetActuatorPos()));
                positionsFile.Write(String.Format("fine 1 X,{0}\n", rccm.motors["fine 1 X"].GetActuatorPos()));
                positionsFile.Write(String.Format("fine 1 Y,{0}\n", rccm.motors["fine 1 Y"].GetActuatorPos()));
                positionsFile.Write(String.Format("fine 1 Z,{0}\n", rccm.motors["fine 1 Z"].GetActuatorPos()));
                positionsFile.Write(String.Format("fine 2 X,{0}\n", rccm.motors["fine 2 X"].GetActuatorPos()));
                positionsFile.Write(String.Format("fine 2 Y,{0}\n", rccm.motors["fine 2 Y"].GetActuatorPos()));
                positionsFile.Write(String.Format("fine 2 Z,{0}\n", rccm.motors["fine 2 Z"].GetActuatorPos()));
            }
        }

        /// <summary>
        /// Save file to disk
        /// </summary>
        public static void Save()
        {
            Logfile.Close();
        }
    }
}
