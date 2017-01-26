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
        // Color of line to display
        protected Color color;
        // Name of sequence for display
        protected string name;
        // List of points of crack vertices and relevant metadata
        protected List<Measurement> points;
        // Enum value indicating which set of fine axes capturing these measurements
        protected RCCMStage parent;

        public MeasurementSequence(Color uiColor, string uiName, RCCMStage uiParent)
        {
            this.points = new List<Measurement>();
            this.color = uiColor;
            this.name = uiName;
            this.parent = uiParent;
        }

        /// <summary>
        /// Add a point to the list of vertices in this sequence
        /// </summary>
        /// <param name="pt">Crack vertex</param>
        public void addPoint(Measurement pt)
        {
            this.points.Add(pt);
        }

        /// <summary>
        /// Deletes a vertex from this sequence
        /// </summary>
        /// <param name="index">Index of the vertex to be deleted</param>
        /// <returns></returns>
        public bool removePoint(int index)
        {
            if (index >= 0 && index < this.points.Count)
            {
                this.points.RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Plot the line segments of this measurement sequence on a graphics container
        /// </summary>
        /// <param name="axes">Graphics object of the container that will display the plot</param>
        /// <param name="location">Global coordinates at which measurement was made</param>
        /// <param name="scale">Conversion from distance units to pixels in the image display</param>
        /// <param name="imgCenter">Pixel location of the center of the image</param>
        public void plot(Graphics axes, PointF location, double scale, Point imgCenter)
        {
            if (this.points.Count > 1)
            {
                PointF p0, p1;
                
                // Draw each line segment
                p0 = measurementToPoint(this.points[0], location, scale, imgCenter);
                for (int i = 1; i < this.points.Count; i++)
                {
                    p1 = MeasurementSequence.measurementToPoint(this.points[i], location, scale, imgCenter);
                    axes.DrawLine(new Pen(this.color), p0, p1);
                    p0 = p1;
                }
            }            
        }
        
        // Helper function for converting a Measurement to its pixel location on the image
        private static PointF measurementToPoint(Measurement pt, PointF location, double scale, Point imgCenter)
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
