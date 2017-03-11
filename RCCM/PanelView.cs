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
        protected readonly RCCMSystem rccm;

        protected Pen pen;
        protected Brush panelBrush;
        protected Brush coarseBrush;
        protected Brush fineBrush;
        // Rectangles holding travel distances and positions of stages
        protected RectangleF panel;
        protected RectangleF coarse;
        protected RectangleF fine1;
        protected RectangleF fine2;
        // Constant position offsets of rectangles
        protected SizeF panelOffset;
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

        public PanelView(RCCMSystem rccm)
        {
            this.rccm = rccm;
            // Create rectangle for displaying panel
            float xPanel = (float)Program.Settings.json["panel"]["x"];
            float yPanel = (float)Program.Settings.json["panel"]["y"];
            float wPanel = (float)Program.Settings.json["panel"]["width"];
            float hPanel = (float)Program.Settings.json["panel"]["height"];
            this.panel = new RectangleF(xPanel, yPanel, wPanel, hPanel);
            // Create rectangle for displaying coarse stages
            float wCoarse = (float)Program.Settings.json["coarse stage"]["x travel"];
            float hCoarse = (float)Program.Settings.json["coarse stage"]["y travel"];
            this.coarse = new RectangleF(0, 0, wCoarse, hCoarse);
            // Create rectangles for displaying fine stages
            float xFine1 = (float)Program.Settings.json["fine 1"]["x"];
            float yFine1 = (float)Program.Settings.json["fine 1"]["y"];
            float wFine1 = (float)Program.Settings.json["fine 1"]["x travel"];
            float hFine1 = (float)Program.Settings.json["fine 1"]["y travel"];
            float xFine2 = (float)Program.Settings.json["fine 2"]["x"];
            float yFine2 = (float)Program.Settings.json["fine 2"]["y"];
            float wFine2 = (float)Program.Settings.json["fine 2"]["x travel"];
            float hFine2 = (float)Program.Settings.json["fine 2"]["y travel"];
            this.fine1 = new RectangleF(xFine1, yFine1, wFine1, hFine1);
            this.fine2 = new RectangleF(xFine2, yFine2, wFine2, hFine2);
            PointF fine1Off = this.rccm.fineVectorToGlobalVector(xFine1, yFine1);
            PointF fine2Off = this.rccm.fineVectorToGlobalVector(xFine2, yFine2);
            this.fine1Offset = new SizeF(fine1Off.X, fine1Off.Y);
            this.fine2Offset = new SizeF(fine2Off.X, fine2Off.Y);
            // Create brushes/pens to draw with
            this.panelBrush = new SolidBrush(Color.FromArgb(255, Color.Gray));
            this.coarseBrush = new SolidBrush(Color.FromArgb(128, Color.LightGray));
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
        public void paint(Graphics g)
        {
            this.updatePositions();

            // Set axis limits
            g.Transform = this.transform;

            // Draw panel
            // Rotate drawing about pivot plate location (translate to location, rotate, translate back)
            this.rotateAt(g, (float)this.rccm.PanelAngle, this.panel.X, this.panel.Y);
            g.FillRectangle(this.panelBrush, this.panel);
            this.rotateAt(g, -(float)this.rccm.PanelAngle, this.panel.X, this.panel.Y);

            // Draw travel regions and crosshairs
            float size = 50;
            float x, y;
            // Coarse axis travel region
            g.FillRectangle(this.coarseBrush, this.coarse);
            // Fine 1 axis travel region
            float fine1x = this.coarseXPos + this.fine1Offset.Width;
            float fine1y = this.coarseYPos + this.fine1Offset.Height;
            this.rotateAt(g, (float)this.rccm.FineStageAngle, fine1x, fine1y);
            g.FillRectangle(this.fineBrush, this.fine1);
            // Fine 1 position crosshair
            x = fine1x + this.fine1XPos;
            y = fine1y + this.fine1YPos;
            g.DrawLine(this.pen, x, y - size, x, y + size); // Vert
            g.DrawLine(this.pen, x - size, y, x + size, y); // Horiz
            this.rotateAt(g, (float)-this.rccm.FineStageAngle, fine1x, fine1y);
            // Fine 2 axis travel region
            float fine2x = this.coarseXPos + this.fine2Offset.Width;
            float fine2y = this.coarseYPos + this.fine2Offset.Height;
            this.rotateAt(g, (float)this.rccm.FineStageAngle, fine2x, fine2y);
            g.FillRectangle(this.fineBrush, this.fine2);
            // Fine 2 position crosshair
            x = fine2x + this.fine2XPos;
            y = fine2y + this.fine2YPos;
            g.DrawLine(this.pen, x, y - size, x, y + size); // Vert
            g.DrawLine(this.pen, x - size, y, x + size, y); // Horiz
            this.rotateAt(g, (float)-this.rccm.FineStageAngle, x, y);
        }

        /// <summary>
        /// Rotate graphics transform about a certain point
        /// </summary>
        /// <param name="g">Graphics object</param>
        /// <param name="angle">Angle of rotation</param>
        /// <param name="x">X coordinate of rotation center (top left corner)</param>
        /// <param name="y">Y coordinate of rotation center (top left corner)</param>
        private void rotateAt(Graphics g, float angle, float x, float y)
        {
            g.TranslateTransform(x, y);
            g.RotateTransform(-angle);
            g.TranslateTransform(-x, -y);
        }

        /// <summary>
        /// Create the transform matrix to map pixel coordinates to the global coordinate system
        /// </summary>
        /// <param name="g">The graphics object representing the control on which to draw</param>
        public void setTransform(Graphics g)
        {
            RectangleF bounds = g.VisibleClipBounds;
            float rccmXSize = this.coarse.Width + this.fine1.Width + this.fine2.Width;
            float rccmYSize = this.coarse.Height + this.fine1.Height + this.fine2.Height;
            float scaleX = bounds.Width / rccmXSize;
            float scaleY = bounds.Height / rccmYSize;
            float scale = Math.Min(scaleX, scaleY);
            g.ScaleTransform(scale, scale);
            g.TranslateTransform(this.fine1.Width, this.fine1.Height);
            this.transform = g.Transform;
        }

        public void updatePositions()
        {
            // Get positions from rccm
            this.coarseXPos = (float)this.rccm.motors["coarse X"].getPos();
            this.coarseYPos = (float) this.rccm.motors["coarse Y"].getPos();
            this.fine1XPos = (float) this.rccm.motors["fine 1 X"].getPos();
            this.fine1YPos = (float) this.rccm.motors["fine 1 Y"].getPos();
            this.fine2XPos = (float) this.rccm.motors["fine 2 X"].getPos();
            this.fine2YPos = (float) this.rccm.motors["fine 2 Y"].getPos();
            // Move fine stage rectangles
            PointF coarsePos = new PointF(this.coarseXPos, this.coarseYPos);
            this.fine1.Location = PointF.Add(coarsePos, this.fine1Offset);
            this.fine2.Location = PointF.Add(coarsePos, this.fine2Offset);
            float xFine1 = (float)Program.Settings.json["fine 1"]["x"];
            float yFine1 = (float)Program.Settings.json["fine 1"]["y"];
            float xFine2 = (float)Program.Settings.json["fine 2"]["x"];
            float yFine2 = (float)Program.Settings.json["fine 2"]["y"];
            PointF fine1Off = this.rccm.fineVectorToGlobalVector(xFine1, yFine1);
            PointF fine2Off = this.rccm.fineVectorToGlobalVector(xFine2, yFine2);
            this.fine1Offset = new SizeF(fine1Off.X, fine1Off.Y);
            this.fine2Offset = new SizeF(fine2Off.X, fine2Off.Y);
            this.panel.X = (float)this.rccm.PanelOffsetX;
            this.panel.Y = (float)this.rccm.PanelOffsetY;
        }
    }
}
