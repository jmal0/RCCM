using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    /// <summary>
    /// Interface defining the basic functionalities of the NFOV and WFOV cameras
    /// to allow both cameras to be referenced by the exact same code
    /// </summary>
    public interface ICamera
    {
        double Scale { get; set; }
        double Height { get; }
        double Width { get; }
        bool Recording { get; }

        void Start();
        void Stop();
        void Snap(string filename);
        void Record(string filename);
    }
}
