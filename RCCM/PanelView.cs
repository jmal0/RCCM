﻿using System;
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
        /// <summary>
        /// Reference to RCCMSystem object
        /// </summary>
        protected readonly RCCMSystem rccm;
        /// <summary>
        /// Pen for drawing lines
        /// </summary>
        protected Pen pen;
        /// <summary>
        /// Image loaded from file containing panel graphics
        /// </summary
        protected Image panelImage;
        /// <summary>
        /// Brush defining style for drawing coarse stage rectangle
        /// </summary>
        protected Brush coarseBrush;
        /// <summary>
        /// Brush defining style for drawing fine stage rectangles
        /// </summary>
        protected Brush fineBrush;
        /// <summary>
        /// Brush defining style for drawing fonta
        /// </summary>
        protected Brush fontBrush;
        /// <summary>
        /// Rectangle defining dimensions of panel
        /// </summary>
        protected RectangleF panel;
        /// <summary>
        /// Rectangle defining dimensions of coarse stage travel region
        /// </summary>
        protected RectangleF coarse;
        /// <summary>
        /// Rectangle defining dimensions of fine 1 stage travel region
        /// </summary>
        protected RectangleF fine1;
        /// <summary>
        /// Rectangle defining dimensions of fine 1 stage travel region
        /// </summary>
        protected RectangleF fine2;
        /// <summary>
        /// Position offset to fine 1 rectangle top left corner
        /// </summary>
        protected SizeF fine1Offset;
        /// <summary>
        /// Position offset to fine 2 rectangle top left corner
        /// </summary>
        protected SizeF fine2Offset;
        /// <summary>
        /// Graphics transform mapping pixels to global coordinate system
        /// </summary>
        protected Matrix transform;
        /// <summary>
        /// Coarse X actuator position
        /// </summary>
        protected float coarseXPos;
        /// <summary>
        /// Coarse Y actuator position
        /// </summary>
        protected float coarseYPos;
        /// <summary>
        /// Fine 1 X actuator position
        /// </summary>
        protected float fine1XPos;
        /// <summary>
        /// Fine 1 Y actuator position
        /// </summary>
        protected float fine1YPos;
        /// <summary>
        /// Fine 2 X actuator position
        /// </summary>
        protected float fine2XPos;
        /// <summary>
        /// Fine 2 Y actuator position
        /// </summary>
        protected float fine2YPos;
        /// <summary>
        /// Font lol
        /// </summary>
        protected Font font;

        /// <summary>
        /// Initialize panel view
        /// </summary>
        /// <param name="rccm">Reference to RCCM object</param>
        public PanelView(RCCMSystem rccm)
        {
            this.rccm = rccm;
            // Create rectangle for displaying panel
            this.panel = new RectangleF();
            this.panelImage = Image.FromFile((string)Program.Settings.json["panel"]["image file"]);
            // Create rectangle for displaying coarse stages
            float wCoarse = (float)Program.Settings.json["coarse stage"]["x travel"];
            float hCoarse = (float)Program.Settings.json["coarse stage"]["y travel"];
            this.coarse = new RectangleF(0, 0, wCoarse, hCoarse);
            // Create rectangles for displaying fine stages
            float wFine1 = (float)Program.Settings.json["fine 1"]["x travel"];
            float hFine1 = (float)Program.Settings.json["fine 1"]["y travel"];
            float wFine2 = (float)Program.Settings.json["fine 2"]["x travel"];
            float hFine2 = (float)Program.Settings.json["fine 2"]["y travel"];
            this.fine1 = new RectangleF(0, 0, wFine1, hFine1);
            this.fine2 = new RectangleF(0, 0, wFine2, hFine2);
            // Create brushes/pens to draw with
            this.font = new Font(FontFamily.GenericSansSerif, 4, FontStyle.Regular);
            this.fontBrush = new SolidBrush(Color.Black);
            this.coarseBrush = new SolidBrush(Color.FromArgb(128, Color.Gray));
            this.fineBrush = new SolidBrush(Color.FromArgb(128, Color.Green));
            this.pen = new Pen(Color.Black, 0);
            // Initialize matrix for holding coordinate system transform
            this.transform = new Matrix();
            this.updatePositions();
        }

        /// <summary>
        /// Paint panel, stages, and position crosshair graphics
        /// </summary>
        /// <param name="g">The graphics object representing the control on which to draw</param>
        /// <param name="rccm">Handle to RCCMSystem object. Will be used to get positions</param>
        public void Paint(Graphics g)
        {
            this.updatePositions();
            
            // Set axis limits
            g.Transform = this.transform;

            // Draw panel
            // Rotate drawing about pivot plate location (translate to location, rotate, translate back)
            this.rotateAt(g, (float)this.rccm.PanelAngle, this.panel.X, this.panel.Y);
            g.DrawImage(this.panelImage, this.panel);
            this.rotateAt(g, -(float)this.rccm.PanelAngle, this.panel.X, this.panel.Y);

            // Draw travel regions and crosshairs
            float size = (string)Program.Settings.json["units"] == "mm" ? 50 : 2;
            float x, y;
            // Coarse axis travel region
            g.FillRectangle(this.coarseBrush, this.coarse);
            // Fine 1 axis travel region
            float fine1x = this.coarseXPos + this.fine1Offset.Width;
            float fine1y = this.coarseYPos + this.fine1Offset.Height;
            this.rotateAt(g, (float)this.rccm.FineStageAngle, fine1x, fine1y);
            g.FillRectangle(this.fineBrush, this.fine1);
            g.DrawString("1", this.font, this.fontBrush, this.fine1.Left, this.fine1.Top);
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
            g.DrawString("2", this.font, this.fontBrush, this.fine2.Left, this.fine2.Top);
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
        public void SetTransform(Graphics g)
        {
            RectangleF bounds = g.VisibleClipBounds;
            float rccmXSize = 1.25f * this.panel.Width;
            float rccmYSize = 1.25f * this.panel.Height;
            float scaleX = bounds.Width / rccmXSize;
            float scaleY = bounds.Height / rccmYSize;
            float scale = Math.Max(0.00001f, Math.Min(scaleX, scaleY));
            g.ScaleTransform(scale, scale);
            g.TranslateTransform((g.VisibleClipBounds.Width - this.panel.Width) / 2, (g.VisibleClipBounds.Height - this.panel.Height) / 2);
            this.transform = g.Transform;
        }
        
        /// <summary>
        /// Helper function for updating RCCM actuator positions and settings variables
        /// </summary>
        private void updatePositions()
        {
            // Get positions from rccm
            this.coarseXPos = (float)this.rccm.motors["coarse X"].GetPos();
            this.coarseYPos = (float)this.rccm.motors["coarse Y"].GetPos();
            this.fine1XPos = (float)this.rccm.motors["fine 1 X"].GetPos();
            this.fine1YPos = (float)this.rccm.motors["fine 1 Y"].GetPos();
            this.fine2XPos = (float)this.rccm.motors["fine 2 X"].GetPos();
            this.fine2YPos = (float)this.rccm.motors["fine 2 Y"].GetPos();
            // Update panel dimensions
            this.panel.X = (float)this.rccm.PanelOffsetX;
            this.panel.Y = (float)this.rccm.PanelOffsetY;
            this.panel.Width = (float)this.rccm.PanelWidth;
            this.panel.Height = (float)this.rccm.PanelHeight;
            // Move fine stage rectangles
            PointF coarsePos = new PointF(this.coarseXPos, this.coarseYPos);
            float xFine1 = (float)this.rccm.NFOV1X;
            float yFine1 = (float)this.rccm.NFOV1Y;
            float xFine2 = (float)this.rccm.NFOV2X;
            float yFine2 = (float)this.rccm.NFOV2Y;
            PointF fine1Off = this.rccm.FineVectorToGlobalVector(xFine1, yFine1);
            PointF fine2Off = this.rccm.FineVectorToGlobalVector(xFine2, yFine2);
            this.fine1Offset = new SizeF(fine1Off.X, fine1Off.Y);
            this.fine2Offset = new SizeF(fine2Off.X, fine2Off.Y);
            this.fine1.Location = PointF.Add(coarsePos, this.fine1Offset);
            this.fine2.Location = PointF.Add(coarsePos, this.fine2Offset);
        }
    }
}
