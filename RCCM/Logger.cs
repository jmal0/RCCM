using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
        /// Save file to disk
        /// </summary>
        public static void Save()
        {
            Logfile.Close();
        }
    }
}
