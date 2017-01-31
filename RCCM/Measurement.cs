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
        protected double coarseX;
        protected double coarseY;
        protected double fineX;
        protected double fineY;
        protected double pixelX;
        protected double pixelY;
        public double X { get; private set; }
        public double Y { get; private set; }
        protected int cycle;
        protected double pressure;

        public Measurement(RCCMSystem rccm, RCCMStage fine, double pixelX, double pixelY)
        {
            this.coarseX = rccm.getPosition("coarse X");
            this.coarseY = rccm.getPosition("coarse Y");
            this.fineX = fine == RCCMStage.RCCM1 ? rccm.getPosition("fine 1 X") : rccm.getPosition("fine 2 X");
            this.fineY = fine == RCCMStage.RCCM1 ? rccm.getPosition("fine 1 Y") : rccm.getPosition("fine 2 Y");
            this.pixelX = pixelX;
            this.pixelY = pixelY;
            this.X = this.coarseX + this.fineX + this.pixelX; // TODO: robustify
            this.Y = this.coarseY + this.fineY + this.pixelY; // TODO: robustify

            this.cycle = rccm.getCycle();
            this.pressure = 0; // TODO
        }

        public string toCSVString()
        {
            return ",\n";
        }

        // Helper function for converting a Measurement to its pixel location on the image
        /// <summary>
        /// Calculate the pixel coordinates within an image display where this Measurement would appear
        /// </summary>
        /// <param name="location">Global coordinate where image is being taken</param>
        /// <param name="scale">Conversion from global coordinate to pixels</param>
        /// <param name="imgCenter">Pixel location in image coordinates of center</param>
        /// <returns></returns>
        public Point toPoint(PointF location, double scale, Point imgCenter)
        {
            int x = (int)((this.X - location.X) * scale) + imgCenter.X;
            // y axis points from top to bottom, so flip sign
            int y = (int)(-(this.Y - location.Y) * scale) + imgCenter.Y;
            return new Point(x, y);
        }
    }
}
