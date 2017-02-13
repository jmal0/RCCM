using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    public class Logger
    {
        public static StreamWriter Logfile = new StreamWriter("log/output.txt", false);

        public static void Out(string str, [CallerFilePath] string path = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = "")
        {
            Console.WriteLine(str);

            string metadata = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt-fff} {1}:{2}:{3}", DateTime.Now, path, lineNumber, caller);
            Logfile.WriteLine(metadata + " - " + str);
        }

        public static void Save()
        {
            Logfile.Close();
        }
    }
}
