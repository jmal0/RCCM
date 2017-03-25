using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RCCM
{
    public class Settings
    {
        public string file;
        public JObject json;

        public Settings(string filename)
        {
            this.file = filename;
            this.json = JObject.Parse(File.ReadAllText(filename));
        }

        /// <summary>
        /// Save settings to original file they were read from
        /// <returns>True if settings saved successfully, false otherwise</returns>
        /// </summary>
        public bool save()
        {
            try
            {
                File.WriteAllText(this.file, this.json.ToString());
                return true;
            }
            catch (Exception ex)
            {
                Logger.Out(ex.ToString());
                return false;
            }
            
        }

        /// <summary>
        /// Save settings to a specified file
        /// </summary>
        /// <param name="filename">Path of file to save to</param>
        /// <returns>True if settings saved successfully, false otherwise</returns>
        public bool save(string filename)
        {
            try
            {
                File.WriteAllText(filename, this.json.ToString());
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("wtfffff");
                Logger.Out(ex.ToString());
                return false;
            }
        }
    }
}
