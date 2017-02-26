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
        /// Width of line
        /// </summary>
        public float LineSize { get; set; }
        /// <summary>
        /// Number of points in sequence
        /// </summary>
        public int CountPoints
        {
            get { return this.points.Count; }
        }
        
        public MeasurementSequence(Color uiColor, string uiName, float uiSize, RCCMStage uiParent)
        {
            this.points = new List<Measurement>();
            this.Color = Color.FromArgb(128, uiColor); // 50% transparent
            this.Name = uiName;
            this.LineSize = uiSize;
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
        public void plot(Graphics axes, float scale)
        {
            if (this.points.Count > 1)
            {
                Pen pen = new Pen(this.Color, this.LineSize / scale);
                Brush brush = new SolidBrush(this.Color);
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                RectangleF point = new RectangleF(0, 0, 5.0f * this.LineSize / scale, 5.0f * this.LineSize / scale);
                    
                // Draw each line segment
                PointF p0, p1;
                p0 = new PointF((float) this.points[0].X, (float) this.points[0].Y);
                point.X = (float) p0.X - point.Width / 2.0f;
                point.Y = (float) p0.Y - point.Height / 2.0f;
                axes.FillEllipse(brush, point);
                for (int i = 1; i < this.points.Count; i++)
                {
                    p1 = new PointF((float)this.points[i].X, (float)this.points[i].Y);
                    axes.DrawLine(pen, p0, p1);
                    // Draw point at line segment vertex
                    point.X = (float) p1.X - point.Width / 2.0f;
                    point.Y = (float) p1.Y - point.Height / 2.0f;
                    axes.FillEllipse(brush, point);
                    p0 = p1;
                }
            }
        }

        /// <summary>
        /// Create a filename with identifying information about sequence
        /// </summary>
        /// <returns>Filename formatted with current timestamp, crack name, and .csv extension</returns>
        public string getFileName()
        {
            string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt-fff}", DateTime.Now);
            return timestamp + "-" + this.Name +".csv";
        }

        /// <summary>
        /// Write measurement to file. Formatted as one line header followed with a line for each crack vertex
        /// </summary>
        /// <param name="filename"></param>
        public void writeToFile(string path)
        {
            using (StreamWriter file = new StreamWriter(path + "\\" + this.getFileName()))
            {
                // Write column headers
                for(int i = 0; i < Measurement.CSV_HEADER.Length; i++)
                {
                    file.Write(Measurement.CSV_HEADER[i] + ",");
                }
                file.WriteLine();
                // Write each measurement to row
                foreach (Measurement m in this.points)
                {
                    file.WriteLine(m.toCSVString());
                }
            }
        }

        /// <summary>
        /// Return name identifying this sequence
        /// </summary>
        /// <returns>Sequence name</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
