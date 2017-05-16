using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    /// <summary>
    /// Interface that plugins must implement to provide identifying information about plugin
    /// </summary>
    public interface IRCCMPlugin
    {
        /// <summary>
        /// User visible name of plugin
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Plugin inputs
        /// </summary>
        string[] Params { get; }
        /// <summary>
        /// Create an instance of the plugin
        /// </summary>
        /// <param name="rccm">Reference to the RCCM object</param>
        /// <param name="parameters">User inputs to the plugin</param>
        /// <returns>An instance of the runnable plugin interface</returns>
        IRCCMPluginActor Instance(RCCMSystem rccm, Dictionary<string, string> parameters);
    }
}
