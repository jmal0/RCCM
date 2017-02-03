using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    public class MeasurementSequence
    {
        // List of points of crack vertices and relevant metadata
        protected List<Measurement> points;
        // Enum value indicating which set of fine axes capturing these measurements
        protected RCCMStage parent;
        
        /// <summary>
        /// Color of line to display
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// Name of sequence for display
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Number of points in sequence
        /// </summary>
        public int CountPoints
        {
            get { return this.points.Count; }
        }
        
        public MeasurementSequence(Color uiColor, string uiName, RCCMStage uiParent)
        {
            this.points = new List<Measurement>();
            this.Color = uiColor;
            this.Name = uiName;
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
        /// Get Measurement corresponding to the specified index
        /// </summary>
        /// <param name="ind">Index of measurement to return</param>
        /// <returns>Measurement at this index if it is valid</returns>
        public Measurement getPoint(int ind)
        {
            if (ind >= 0 && ind < this.points.Count)
            {
                return this.points[ind];
            }
            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// Get last Measurement in sequence
        /// </summary>
        /// <returns>Measurement at last index</returns>
        public Measurement getLastPoint()
        {
            return this.points[this.points.Count - 1];
        }

        /// <summary>
        /// Deletes a vertex from this sequence
        /// </summary>
        /// <param name="index">Index of the vertex to be deleted</param>
        /// <returns>True or false if deletion was successful</returns>
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
                p0 = this.points[0].toPoint(location, scale, imgCenter);
                for (int i = 1; i < this.points.Count; i++)
                {
                    p1 = this.points[i].toPoint(location, scale, imgCenter);
                    axes.DrawLine(new Pen(this.Color), p0, p1);
                    p0 = p1;
                }
            }
        }

        public void writeToFile(string filename)
        {
            using (StreamWriter file = new StreamWriter(filename))
            {
                foreach (Measurement m in this.points)
                {
                    file.WriteLine(m.toCSVString());
                }
            }
        }

        /// <summary>
        /// Display name, color, and list of point in sequence
        /// </summary>
        /// <returns>String describing sequence</returns>
        public override string ToString()
        {
            string description = this.Name + "\n" + this.Color.ToString() + "\n";
            foreach (Measurement pt in this.points)
            {
                description += pt.X + "\t" + pt.Y + "\n";
            }
            return description;
        }
    }
}
