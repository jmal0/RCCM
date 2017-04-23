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
        /// <summary>
        /// Conversion from pixels to microns
        /// </summary>
        double Scale { get; }
        /// <summary>
        /// Height in microns of image
        /// </summary>
        double Height { get; }
        /// <summary>
        /// Width in microns of image
        /// </summary>
        double Width { get; }
        /// <summary>
        /// A flag to indicate if camera is recording video
        /// </summary>
        bool Recording { get; }
        /// <summary>
        /// Begin live image transmission from camera
        /// </summary>
        void Start();
        /// <summary>
        /// Stop live image transmission from camera
        /// </summary>
        void Stop();
        /// <summary>
        /// Save live image from camera to bitmap file
        /// </summary>
        /// <param name="filename">Path to .bmp file where image will save</param>
        void Snap(string filename);
        /// <summary>
        /// Begin recording video from camera
        /// </summary>
        /// <param name="filename">Path to .avi file where video will save</param>
        void Record(string filename);
        /// <summary>
        /// Stop recording video from camera
        /// </summary>
        void StopRecord();
        /// <summary>
        /// Returns true if current state of RCCM matches calibration state for FOV
        /// </summary>
        /// <param name="rccm">Handle to RCCM object for getting z position</param>
        bool CheckFOV(RCCMSystem rccm);
        /// <summary>
        /// Set value for pixel to micron conversion and save relevant info for 
        /// checking that fov is correct
        /// </summary>
        /// <param name="rccm">Handle to RCCM object for getting z position</param>
        /// <param name="scale">New value of pixels to microns conversion</param>
        void SetScale(RCCMSystem rccm, double scale);
    }
}
