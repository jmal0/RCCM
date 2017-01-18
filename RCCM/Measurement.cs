using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    public class Measurement
    {
        protected double coarseX;
        protected double coarseY;
        protected double fineX;
        protected double fineY;
        protected double pixelX;
        protected double pixelY;

        public Measurement(RCCMSystem rccm, RCCMStage fine, double pixelX, double pixelY)
        {
            this.coarseX = rccm.getCoarseX();
            this.coarseY = rccm.getCoarseY();
            this.fineX = fine == RCCMStage.RCCM1 ? rccm.getFine1X() : rccm.getFine2X();
            this.fineY = fine == RCCMStage.RCCM1 ? rccm.getFine1Y() : rccm.getFine2Y();
            this.pixelX = pixelX;
            this.pixelY = pixelY;
        }

        public string toCSVString()
        {
            return ",\n";
        }
    }
}
