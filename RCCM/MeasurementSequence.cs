﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCCM.UI;

namespace RCCM
{
    public class MeasurementSequence
    {
        // List of points of crack vertices and relevant metadata
        protected List<Measurement> points;
        /// <summary>
        /// Enum value indicating which set of fine axes capturing these measurements
        /// </summary>
        public RCCMStage Parent { get; set; }        
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
        /// Angular orientation (in degrees) of initial notch relative to panel coordinate system
        /// </summary>
        public double Orientation
        {
            get { return this.orientation; }
            set 
            {
                this.orientation = Orientation;
                this.RecomputeLength();
            }
        }
        private double orientation;
        /// <summary>
        /// Method to use for crack length calculation
        /// </summary>
        public MeasurementMode Mode
        {
            get { return this.mode; }
            set
            {
                this.mode = Mode;
                this.RecomputeLength();
            }
        }
        private MeasurementMode mode;
        /// <summary>
        /// Number of points in sequence
        /// </summary>
        public int CountPoints
        {
            get { return this.points.Count; }
        }
        
        public MeasurementSequence(Color uiColor, string uiName, float uiSize, float uiOrientation, MeasurementMode uiMode, RCCMStage uiParent)
        {
            this.points = new List<Measurement>();
            this.Name = uiName;
            this.Color = Color.FromArgb(128, uiColor); // 50% transparent
            this.LineSize = uiSize;
            this.Orientation = uiOrientation;
            this.Mode = uiMode;
            this.Parent = uiParent;
        }

        public MeasurementSequence(NewMeasurementForm parentForm)
        {
            this.points = new List<Measurement>();
            this.Name = parentForm.GetName();
            this.Color = Color.FromArgb(128, parentForm.GetColor()); // 50% transparent
            this.LineSize = parentForm.GetLineSize();
            this.Orientation = parentForm.GetOrientation();
            this.Mode = parentForm.GetMode();
            this.Parent = parentForm.GetStage();
        }

        /// <summary>
        /// Add a point to the list of vertices in this sequence
        /// </summary>
        /// <param name="pt">Crack vertex</param>
        public void AddPoint(Measurement pt)
        {
            this.points.Add(pt);
            double length = this.CalculateLength(this.CountPoints - 1);
            this.points[this.CountPoints - 1].CrackLength = length;
            this.WriteToFile((string)Program.Settings.json["test data directory"], true);
        }

        /// <summary>
        /// Get Measurement corresponding to the specified index
        /// </summary>
        /// <param name="ind">Index of measurement to return</param>
        /// <returns>Measurement at this index if it is valid</returns>
        public Measurement GetPoint(int ind)
        {
            if (ind >= 0 && ind < this.points.Count)
            {
                return this.points[ind];
            }
            return null;
        }

        /// <summary>
        /// Get last Measurement in sequence
        /// </summary>
        /// <returns>Measurement at last index</returns>
        public Measurement GetLastPoint()
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
                this.RecomputeLength();
                this.WriteToFile((string)Program.Settings.json["test data directory"], true);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Plot the line segments of this measurement sequence on a graphics container
        /// </summary>
        /// <param name="axes">Graphics object of the container that will display the plot</param>
        public void Plot(Graphics axes, float scale)
        {
            if (this.points.Count > 0)
            {
                // Drawing object
                Pen pen = new Pen(this.Color, this.LineSize / scale); // Used to draw lines
                Brush brush = new SolidBrush(this.Color); // Used to draw points
                RectangleF point = new RectangleF(0, 0, 5.0f * this.LineSize / scale, 5.0f * this.LineSize / scale); // Rectangle with dimensions for drawing a point
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center; // Center line on coordinates
                
                PointF p0, p1;
                // Draw first point
                p0 = new PointF((float) this.points[0].X, (float) this.points[0].Y);
                point.X = (float) p0.X - point.Width / 2.0f;
                point.Y = (float) p0.Y - point.Height / 2.0f;
                axes.FillEllipse(brush, point);
                // Draw each line segment
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
        public string GetFileName()
        {
            string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt-fff}", DateTime.Now);
            return timestamp + "-" + this.Name +".csv";
        }
        
        /// <summary>
        /// Write measurement to file. Formatted as one line header followed with a line for each crack vertex
        /// </summary>
        /// <param name="path">Filename and directory path where data will be saved</param>
        /// <param name="autoName">If true, filename will be automatically defined</param>
        public void WriteToFile(string path, bool autoName)
        {
            if (autoName)
            {
                path += "\\" + this.GetFileName();
            }
            using (StreamWriter file = new StreamWriter(path))
            {
                // Write column headers
                for (int i = 0; i < Measurement.CSV_HEADER.Length; i++)
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

        /// <summary>
        /// Calculate the length of the crack at the specified measurement
        /// </summary>
        /// <param name="ind">Index in measurement sequence of the desired measurement</param>
        /// <returns>Crack length when specified measurement was made</returns>
        public double CalculateLength(int ind)
        {
            if (ind == 0)
            {
                return 0.0;
            }
            double dx, dy;
            switch (this.Mode)
            {
                case MeasurementMode.Projection:
                    dx = this.points[ind].X - this.points[0].X;
                    dy = this.points[ind].Y - this.points[0].Y;
                    return dx * Math.Cos(Math.PI / 180.0 * this.Orientation) + dy * Math.Sin(Math.PI / 180.0 * this.Orientation);
                case MeasurementMode.Tip:
                    dx = this.points[ind].X - this.points[0].X;
                    dy = this.points[ind].Y - this.points[0].Y;
                    return Math.Sqrt(dx * dx + dy * dy);
                case MeasurementMode.Total:
                    dx = this.points[ind].X - this.points[ind - 1].X;
                    dy = this.points[ind].Y - this.points[ind - 1].Y;
                    return Math.Sqrt(dx * dx + dy * dy);
                default:
                    // Use tip to tip calculation by default
                    dx = this.points[ind].X - this.points[0].X;
                    dy = this.points[ind].Y - this.points[0].Y;
                    return Math.Sqrt(dx * dx + dy * dy);
            }
        }

        /// <summary>
        /// Recalculate length of crack at time of each measurement
        /// </summary>
        public void RecomputeLength()
        {
            for (int i = 0; i < this.CountPoints; i++)
            {
                double length = this.CalculateLength(i);
                this.points[i].CrackLength = length;
            }
        }
    }
}
