using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    public interface IRCCMPluginActor
    {
        bool Running { get; }
        void Run();
        void Stop();
    }
}
