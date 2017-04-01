using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    public interface IRCCMPlugin
    {
        string Name { get; }
        string[] Params { get; }

        IRCCMPluginActor Instance(RCCMSystem rccm, Dictionary<string, string> parameters);
    }
}
