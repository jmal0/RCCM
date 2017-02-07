using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    /// <summary>
    /// Object used to draw panel graphic to show location of stages
    /// </summary>
    public class PanelView
    {

        protected Pen pen;
        protected Brush coarseBrush;
        protected Brush fineBrush;
        // Rectangles holding travel distances and positions of stages
        protected RectangleF panel;
        protected RectangleF fine1;
        protected RectangleF fine2;
        // Constant position offsets of rectangles
        protected SizeF fine1Offset;
        protected SizeF fine2Offset;
        // Graphics transform mapping pixels to global coordinate system
        protected Matrix transform;
        // Positions of stages
        protected float coarseXPos;
        protected float coarseYPos;
        protected float fine1XPos;
        protected float fine1YPos;
        protected float fine2XPos;
        protected float fine2YPos;

        public PanelView(Settings settings)
        {
            // Create rectangle for displaying panel
            float w = (float) settings.json["panel"]["width"];
            float h = (float) settings.json["panel"]["height"];
            this.panel = new RectangleF(0, 0, w, h);
            // Create rectangles for displaying
            float xFine1 = (float)settings.json["fine 1"]["x"];
            float yFine1 = (float)settings.json["fine 1"]["y"];
            float wFine1 = (float)settings.json["fine 1"]["x travel"];
            float hFine1 = (float)settings.json["fine 1"]["y travel"];
            float xFine2 = (float)settings.json["fine 2"]["x"];
            float yFine2 = (float)settings.json["fine 2"]["y"];
            float wFine2 = (float)settings.json["fine 2"]["x travel"];
            float hFine2 = (float)settings.json["fine 2"]["y travel"];
            this.fine1Offset = new SizeF(xFine1, yFine1);
            this.fine2Offset = new SizeF(xFine2, yFine2);
            this.fine1 = new RectangleF(xFine1, yFine1, wFine1, hFine1);
            this.fine2 = new RectangleF(xFine2, yFine2, wFine2, hFine2);
            // Create brushes/pens to draw with
            this.coarseBrush = new SolidBrush(Color.FromArgb(128, Color.Gray));
            this.fineBrush = new SolidBrush(Color.FromArgb(128, Color.Green));
            this.pen = new Pen(Color.Black);
            // Initialize matrix for holding coordinate system transform
            this.transform = new Matrix();
        }

        /// <summary>
        /// Paint panel, stages, and position crosshair graphics
        /// </summary>
        /// <param name="g">The graphics object representing the control on which to draw</param>
        /// <param name="rccm">Handle to RCCMSystem object. Will be used to get positions</param>
        public void paint(Graphics g, RCCMSystem rccm)
        {
            this.updatePositions(rccm);

            // Set axis limits
            g.Transform = this.transform;

            // Draw travel regions
            g.FillRectangle(this.coarseBrush, this.panel);
            g.FillRectangle(this.fineBrush, this.fine1);
            g.FillRectangle(this.fineBrush, this.fine2);
            // Draw position crosshairs
            g.DrawLine(this.pen, this.panel.Left + this.coarseXPos, this.panel.Top, this.panel.Left + this.coarseXPos, this.panel.Bottom); // Vert
            g.DrawLine(this.pen, this.panel.Left, this.panel.Top + this.coarseYPos, this.panel.Right, this.panel.Top + this.coarseYPos); // Horiz
            g.DrawLine(this.pen, this.fine1.Left + this.fine1XPos, this.fine1.Top, this.fine1.Left + this.fine1XPos, this.fine1.Bottom); // Vert
            g.DrawLine(this.pen, this.fine1.Left, this.fine1.Top + this.fine1YPos, this.fine1.Right, this.fine1.Top + this.fine1YPos); // Horiz
            g.DrawLine(this.pen, this.fine2.Left + this.fine2XPos, this.fine2.Top, this.fine2.Left + this.fine2XPos, this.fine2.Bottom); // Vert
            g.DrawLine(this.pen, this.fine2.Left, this.fine2.Top + this.fine2YPos, this.fine2.Right, this.fine2.Top + this.fine2YPos); // Horiz
        }

        /// <summary>
        /// Create the transform matrix to map pixel coordinates to the global coordinate system
        /// </summary>
        /// <param name="g">The graphics object representing the control on which to draw</param>
        public void setTransform(Graphics g)
        {
            RectangleF bounds = g.VisibleClipBounds;
            float rccmXSize = this.panel.Width + this.fine1.Width + this.fine2.Width;
            float rccmYSize = this.panel.Height + this.fine1.Height + this.fine2.Height;
            float scaleX = bounds.Width / rccmXSize;
            float scaleY = bounds.Height / rccmYSize;
            float scale = Math.Min(scaleX, scaleY);
            g.ScaleTransform(scale, scale);
            g.TranslateTransform(this.fine1.Width, this.fine1.Height);
            this.transform = g.Transform;
        }

        public void updatePositions(RCCMSystem rccm)
        {
            // Get positions from rccm
            this.coarseXPos = (float) rccm.getPosition("coarse X");
            this.coarseYPos = (float) rccm.getPosition("coarse Y");
            this.fine1XPos = (float)rccm.getPosition("fine 1 X");
            this.fine1YPos = (float)rccm.getPosition("fine 1 Y");
            this.fine2XPos = (float)rccm.getPosition("fine 2 X");
            this.fine2YPos = (float)rccm.getPosition("fine 2 Y");
            // Move fine stage rectangles
            PointF coarsePos = new PointF(this.coarseXPos, this.coarseYPos);
            this.fine1.Location = PointF.Add(coarsePos, this.fine1Offset);
            this.fine2.Location = PointF.Add(coarsePos, this.fine2Offset);
        }
    }
}
