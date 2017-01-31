using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    public class TestResults
    {
        public TestResults()
        {

        }

        public double[] calculateCrackLength(MeasurementSequence crack)
        {
            double[] lengths = new double[crack.CountPoints];
            double crackpreviousX = crack.getPoint(0).X;
            double crackpreviousY = crack.getPoint(0).Y;
            for (int i = 1; i<crack.CountPoints;i++)
            {
                Measurement point = crack.getPoint(i);
                lengths[i] = Math.Sqrt(Math.Pow((point.X - crackpreviousX),2) + Math.Pow((point.Y - crackpreviousY),2));
                crackpreviousX = point.X;
                crackpreviousY = point.Y;
            }
            return lengths; 
        }
    }
}
