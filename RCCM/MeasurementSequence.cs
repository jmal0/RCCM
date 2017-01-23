using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    public class MeasurementSequence
    {
        protected Color color;
        protected string name;
        protected List<Measurement> points;
        protected RCCMStage parent;

        public MeasurementSequence(Color uiColor, string uiName, RCCMStage uiParent)
        {
            this.points = new List<Measurement>();
            this.color = uiColor;
            this.name = uiName;
            this.parent = uiParent;
        }

        public void addPoint(Measurement pt)
        {
            this.points.Add(pt);
        }

        public bool removePoint(int index)
        {
            if (index >= 0 && index < this.points.Count)
            {
                this.points.RemoveAt(index);
                return true;
            }
            return false;
        }

        public void plot(Graphics axes, PointF location, double scale, Point imgCenter)
        {
            Console.WriteLine(axes.ClipBounds.Size);
            if (this.points.Count > 1)
            {
                PointF p0, p1;
                
                p0 = measurementToPoint(this.points[0], location, scale, imgCenter);
                for (int i = 1; i < this.points.Count; i++)
                {
                    Console.WriteLine(p0.X + " " + p0.Y);
                    p1 = measurementToPoint(this.points[i], location, scale, imgCenter);
                    axes.DrawLine(new Pen(this.color), p0, p1);
                    p0 = p1;                   
                }
            }            
        }

        private PointF measurementToPoint(Measurement pt, PointF location, double scale, Point imgCenter)
        {
            double x = (location.X - pt.getX()) * scale + imgCenter.X;
            double y = (location.Y - pt.getY()) * scale + imgCenter.Y;
            return new PointF((float) x, (float) y);
        }

        public void writeToFile(string filename)
        {
            // TODO
        }

        public string getName()
        {
            return this.name;
        }

        public Color getColor()
        {
            return this.color;
        }

        public void setColor(Color c)
        {
            this.color = c;
        }

        public void setName(string n)
        {
            this.name = n;
        }

        public override string ToString()
        {
            string description = this.name + "\n";
            foreach (Measurement pt in this.points)
            {
                description += pt.getX() + "\t" + pt.getY() + "\n";
            }
            return description;
        }
    }
}
