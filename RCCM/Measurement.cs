using System;
using System.Collections.Generic;
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
        protected double globalX;
        protected double globalY;
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
            this.globalX = this.coarseX + this.fineX + this.pixelX; // TODO: robustify
            this.globalY = this.coarseY + this.fineY + this.pixelY; // TODO: robustify

            this.cycle = 0; // TODO
            this.pressure = 0; // TODO
        }

        public string toCSVString()
        {
            return ",\n";
        }

        public double getX()
        {
            return this.globalX;
        }

        public double getY()
        {
            return this.globalY;
        }
    }
}
