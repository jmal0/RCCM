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

        void Start();
        void Start(int cycle);
        void Stop();
        double GetPressure();
        int GetElapsed();
        void Terminate();
    }
}
