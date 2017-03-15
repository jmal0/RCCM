using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    /// <summary>
    /// Class representing a measurement of a crack vertex and RCCM data on when it was taken
    /// </summary>
    public class Measurement
    {
        /// <summary>
        /// Fields for CSV header explaining the ordering of data in measurement file
        /// </summary>
        public static string[] CSV_HEADER = { "Timestamp", "Cycle", "Length", "Pressure", "Panel X", "Panel Y", "Coarse X", "Coarse Y", "Fine X", "Fine Y", "Fine Z", "Height", "Pixel X", "Pixel Y", "Global X", "Global Y"};
        /// <summary>
        /// Cycle number when measurement was taken
        /// </summary>
        public int Cycle { get; private set; }
        /// <summary>
        /// Global X coordinate where measurement was taken
        /// </summary>
        public double X { get; private set; }
        /// <summary>
        /// Global Y coordinate where measurement was taken
        /// </summary>
        public double Y { get; private set; }
        /// <summary>
        /// X coordinate in panel coordinate system
        /// </summary>
        public double PanelX { get; private set; }
        /// <summary>
        /// Y coordinate in panel coordinate system
        /// </summary>
        public double PanelY { get; private set; }
        /// <summary>
        /// Length of crack at this measurement
        /// </summary>
        public double CrackLength { get; set; }
        // Other important metadata about measurement
        public double Pressure { get; protected set; }
        public string Timestamp { get; protected set; }
        public double CoarseX { get; protected set; }
        public double CoarseY { get; protected set; }
        public double FineX { get; protected set; }
        public double FineY { get; protected set; }
        public double FineZ { get; protected set; }
        public double Height { get; protected set; }
        public double PixelX { get; protected set; }
        public double PixelY { get; protected set; }

        public Measurement(RCCMSystem rccm, RCCMStage fine, double pixelX, double pixelY)
        {
            // Get timestamp and save NFOV image
            NFOV nfov = fine == RCCMStage.RCCM1 ? rccm.NFOV1 : rccm.NFOV2;
            this.Timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt.fff}", DateTime.Now);
            nfov.Snap(this.Timestamp + ".bmp");
            this.Cycle = rccm.Counter.Cycle;
            this.Pressure = 0; // TODO

            this.CoarseX = rccm.motors["coarse X"].GetPos();
            this.CoarseY = rccm.motors["coarse Y"].GetPos();
            this.FineX = fine == RCCMStage.RCCM1 ? rccm.motors["fine 1 X"].GetPos() : rccm.motors["fine 2 X"].GetPos();
            this.FineY = fine == RCCMStage.RCCM1 ? rccm.motors["fine 1 Y"].GetPos() : rccm.motors["fine 2 Y"].GetPos();
            this.FineZ = fine == RCCMStage.RCCM1 ? rccm.motors["fine 1 Z"].GetActuatorPos() : rccm.motors["fine 2 Z"].GetActuatorPos();
            this.Height = fine == RCCMStage.RCCM1 ? rccm.LensController.Height1 : rccm.LensController.Height2;
            this.PixelX = pixelX;
            this.PixelY = pixelY;
            PointF globalPosition = rccm.GetNFOVLocation(fine, CoordinateSystem.Global);
            this.X = globalPosition.X + this.PixelX;
            this.Y = globalPosition.Y + this.PixelY;
            PointF panelPosition = rccm.GlobalVectorToPanelVector(this.X, this.Y);
            this.PanelX = globalPosition.X + this.PixelX;
            this.PanelY = globalPosition.Y + this.PixelY;
        }

        /// <summary>
        /// Create csv line containing all data pertaining to this measurement
        /// </summary>
        /// <returns>CSV string representing this Measurement</returns>
        public string ToCSVString()
        {
            return this.Timestamp   + "," +
                   this.Cycle       + "," +
                   this.CrackLength + "," +
                   this.Pressure    + "," +
                   this.PanelX      + "," +
                   this.PanelY      + "," +
                   this.CoarseX     + "," +
                   this.CoarseY     + "," +
                   this.FineX       + "," +
                   this.FineY       + "," +
                   this.FineZ       + "," +
                   this.Height      + "," +
                   this.PixelX      + "," +
                   this.PixelY      + "," +
                   this.X           + "," +
                   this.Y;
        }
    }
}
