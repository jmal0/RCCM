using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    public interface ICycleCounter
    {
        /// <summary>
        /// A boolean indicating whether or not the counter is active
        /// </summary>
        bool Active { get; }
        /// <summary>
        /// Current cycle number
        /// </summary>
        int Cycle { get; set; }

        /// <summary>
        /// Starts cycle counting
        /// </summary>
        void Start();
        /// <summary>
        /// Starts cycle counting at the specified cycle number
        /// </summary>
        void Start(int cycle);
        /// <summary>
        /// Stops cycle counting
        /// </summary>
        void Stop();
        /// <summary>
        /// Get most recent pressure reading
        /// </summary>
        /// <returns>Most recent pressure reading</returns>
        double GetPressure();
        /// <summary>
        /// Get time elapsed in test
        /// </summary>
        /// <returns>milliseconds since test start</returns>
        int GetElapsed();
        /// <summary>
        /// Perform any action neccessary for stopping cycle counter
        /// </summary>
        /// <returns>Task completion</returns>
        Task Terminate();
    }
}
