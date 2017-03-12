﻿using System;
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
        public static string[] CSV_HEADER = { "Timestamp", "Cycle", "Length", "Pressure", "Panel X", "Panel Y", "Coarse X", "Coarse Y", "Fine X", "Fine Y", "Pixel X", "Pixel Y", "Global X", "Global Y"};
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
        protected double pressure;
        protected string timestamp;
        protected double coarseX;
        protected double coarseY;
        protected double fineX;
        protected double fineY;
        protected double pixelX;
        protected double pixelY;

        public Measurement(RCCMSystem rccm, RCCMStage fine, double pixelX, double pixelY)
        {
            // Get timestamp and save NFOV image
            NFOV nfov = fine == RCCMStage.RCCM1 ? rccm.NFOV1 : rccm.NFOV2;
            this.timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt.fff}", DateTime.Now);
            nfov.Snap(this.timestamp + ".bmp");
            this.Cycle = rccm.Counter.Cycle;
            this.pressure = 0; // TODO

            this.coarseX = rccm.motors["coarse X"].GetPos();
            this.coarseY = rccm.motors["coarse Y"].GetPos();
            this.fineX = fine == RCCMStage.RCCM1 ? rccm.motors["fine 1 X"].GetPos() : rccm.motors["fine 2 X"].GetPos();
            this.fineY = fine == RCCMStage.RCCM1 ? rccm.motors["fine 1 Y"].GetPos() : rccm.motors["fine 2 Y"].GetPos();
            this.pixelX = pixelX;
            this.pixelY = pixelY;
            PointF globalPosition = rccm.GetNFOVLocation(fine, CoordinateSystem.Global);
            this.X = globalPosition.X + this.pixelX;
            this.Y = globalPosition.Y + this.pixelY;
            PointF panelPosition = rccm.GlobalVectorToPanelVector(this.X, this.Y);
            this.PanelX = globalPosition.X + this.pixelX;
            this.PanelY = globalPosition.Y + this.pixelY;
        }

        /// <summary>
        /// Create csv line containing all data pertaining to this measurement
        /// </summary>
        /// <returns>CSV string representing this Measurement</returns>
        public string ToCSVString()
        {
            return this.timestamp   + "," +
                   this.Cycle       + "," +
                   this.CrackLength + "," +
                   this.pressure    + "," +
                   this.PanelX      + "," +
                   this.PanelY      + "," +
                   this.coarseX     + "," +
                   this.coarseY     + "," +
                   this.fineX       + "," +
                   this.fineY       + "," +
                   this.pixelX      + "," +
                   this.pixelY      + "," +
                   this.X           + "," +
                   this.Y;
        }
    }
}
