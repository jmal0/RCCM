using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    /// <summary>
    /// Interface that allows plugins to define a function that is called to run them
    /// </summary>
    public interface IRCCMPluginActor
    {
        /// <summary>
        /// A flag indicating if the plugin is running
        /// </summary>
        bool Running { get; }
        
        /// <summary>
        /// Run the function with the inputs already passed to the plugin
        /// </summary>
        void Run();
        /// <summary>
        /// This function should cause the plugin to stop as soon as possible
        /// </summary>
        void Stop();
    }
}
