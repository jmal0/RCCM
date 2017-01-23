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
        public JObject json;

        public Settings(string filename)
        {
            this.json = JObject.Parse(File.ReadAllText(filename));
        }
    }
}
