using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    public class RCCMSystem
    {
        private WFOV wfov1;
        private WFOV wfov2;

        private NFOV nfov1;
        private NFOV nfov2;

        private VirtualMotor coarseX;
        private VirtualMotor coarseY;
        private VirtualMotor fine1X;
        private VirtualMotor fine1Y;
        private VirtualMotor fine1Z;
        private VirtualMotor fine2X;
        private VirtualMotor fine2Y;
        private VirtualMotor fine2Z;

        public RCCMSystem()
        {
            this.coarseX = new VirtualMotor();
            this.coarseY = new VirtualMotor();
            this.fine1X = new VirtualMotor();
            this.fine1Y = new VirtualMotor();
            this.fine1Z = new VirtualMotor();
            this.fine2X = new VirtualMotor();
            this.fine2Y = new VirtualMotor();
            this.fine2Z = new VirtualMotor();
        }

        public double getCoarseX()
        {
            return this.coarseX.getPos();
        }

        public double getCoarseY()
        {
            return this.coarseY.getPos();
        }

        public double getFine1X()
        {
            return this.fine1X.getPos();
        }

        public double getFine1Y()
        {
            return this.fine1Y.getPos();
        }

        public double getFine1Z()
        {
            return this.fine1Z.getPos();
        }

        public double getFine2X()
        {
            return this.fine1X.getPos();
        }

        public double getFine2Y()
        {
            return this.fine2Y.getPos();
        }

        public double getFine2Z()
        {
            return this.fine2Z.getPos();
        }

        public double setCoarseX(double value)
        {
            double result = this.coarseX.setPos(value);
            Console.WriteLine("coarse X {0}", value);
            return result;
        }

        public double setCoarseY(double value)
        {
            double result = this.coarseY.setPos(value);
            Console.WriteLine("coarse Y {0}", value);
            return result;
        }

        public double setFine1X(double value)
        {
            double result = this.fine1X.setPos(value);
            Console.WriteLine("fine1 X {0}", value);
            return result;
        }

        public double setFine1Y(double value)
        {
            double result = this.fine1Y.setPos(value);
            Console.WriteLine("fine1 Y {0}", value);
            return result;
        }

        public double setFine1Z(double value)
        {
            double result = this.fine1Z.setPos(value);
            Console.WriteLine("fine1 Z {0}", value);
            return result;
        }

        public double setFine2X(double value)
        {
            double result = this.fine1X.setPos(value);
            Console.WriteLine("fine2 X {0}", value);
            return result;
        }

        public double setFine2Y(double value)
        {
            double result = this.fine2Y.setPos(value);
            Console.WriteLine("fine2 Y {0}", value);
            return result;
        }

        public double setFine2Z(double value)
        {
            double result = this.fine2Z.setPos(value);
            Console.WriteLine("fine2 Z {0}", value);
            return result;
        }
    }
}
