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
    /// Manager for displaying NFOV image and overlaying cracks on a graphics object
    /// </summary>
    public class NFOVView
    {
        private readonly RCCMSystem rccm;
        private readonly List<MeasurementSequence> cracks;

        public NFOV NFOV1 { get; set; }
        public NFOV NFOV2 { get; set; }
        public int ActiveIndex { get; set; }
        private float displayScale = 0.25f;
        public bool Drawing { get; private set; }
        public PointF drawnLineStart;
        public PointF drawnLineEnd;

        /// <summary>
        /// Initialize NFOV display
        /// </summary>
        /// <param name="rccm">RCCMSystem object, needed for getting location and zoom status</param>
        /// <param name="cracks">List of cracks to display</param>
        public NFOVView(RCCMSystem rccm, List<MeasurementSequence> cracks)
        {
            this.rccm = rccm;
            this.cracks = cracks;
            this.Drawing = false;
            this.ActiveIndex = -1;
        }

        /// <summary>
        /// Draw NFOV image and cracks on given graphics object
        /// </summary>
        /// <param name="g">Canvas on which NFOV display will be drawn</param>
        public void paint(Graphics g)
        {
            g.ResetTransform(); // Reset transform to draw NFOV image
            // Get active camera
            NFOV nfov = this.rccm.ActiveStage == RCCMStage.RCCM1 ? this.rccm.NFOV1 : this.rccm.NFOV2;
            // Display live image from NFOV camera
            Bitmap liveImg = nfov.getLiveImage();
            if (liveImg != null)
            {
                Bitmap img = new Bitmap(liveImg, new Size(612, 512));
                g.DrawImage(img, 0, 0);
                img.Dispose();
            }

            // Now transform to world coordinates
            
            // Move image center to origin and rotate
            RectangleF bounds = g.VisibleClipBounds;
            g.TranslateTransform(bounds.Width / 2, bounds.Height / 2);
            g.RotateTransform((float)rccm.FineStageAngle);
            g.TranslateTransform(-bounds.Width / 2, -bounds.Height / 2);
            // Scale coordinate system by pixel to mm scaling
            float scaleX = bounds.Width / (float) nfov.Width;
            float scaleY = bounds.Height / (float) nfov.Height;
            g.ScaleTransform(scaleX, scaleY);
            // Move to NFOV location (first move origin to image center)
            g.TranslateTransform((float) nfov.Width / 2, (float)nfov.Height / 2);
            PointF pos = this.rccm.getNFOVLocation(this.rccm.ActiveStage);
            g.TranslateTransform(-pos.X, -pos.Y);
            
            // Draw each crack on the image
            foreach (MeasurementSequence crack in cracks)
            {
                crack.plot(g);
            }
            // Draw segment that user is creating with mouse
            if (ActiveIndex >= 0 && this.Drawing)
            {
                Color c = cracks[ActiveIndex].Color;
                g.DrawLine(new Pen(Color.FromArgb(128, c), 0), this.drawnLineStart, this.drawnLineEnd);
            }
        }
        
        /// <summary>
        /// Create segment that was being drawn by user with mouse input. This will add a point or segment to the active MeasurementSequence
        /// </summary>
        public void createSegment()
        {
            NFOV nfov = this.rccm.ActiveStage == RCCMStage.RCCM1 ? this.rccm.NFOV1 : this.rccm.NFOV2;
            PointF pos = this.rccm.getNFOVLocation(this.rccm.ActiveStage);
            // Add measurements for start and end if user is drawing both
            if (this.cracks[this.ActiveIndex].CountPoints == 0)
            {
                // Convert pixel coordinates to global coordinates
                Measurement p0 = new Measurement(this.rccm, this.rccm.ActiveStage, this.drawnLineStart.X - pos.X, 
                                                                                   this.drawnLineStart.Y - pos.Y);
                this.cracks[ActiveIndex].addPoint(p0);
            }
            // Add end point (in all situations)
            Measurement p1 = new Measurement(this.rccm, this.rccm.ActiveStage, this.drawnLineEnd.X - pos.X,
                                                                               this.drawnLineEnd.Y - pos.Y);
            this.cracks[ActiveIndex].addPoint(p1);

            this.Drawing = false;
        }

        /// <summary>
        /// Move the end point of the line segment that user is currently drawing
        /// </summary>
        /// <param name="x">Mouse x location in pixels</param>
        /// <param name="y">Mouse y location in pixels</param>
        /// <param name="w">Canvas width in pixels</param>
        /// <param name="h">Canvas height in pixels</param>
        public void moveDrawnLineEnd(int x, int y, int w, int h)
        {
            if (this.Drawing)
            {
                // Get position of NFOV camera
                NFOV nfov = this.rccm.ActiveStage == RCCMStage.RCCM1 ? this.rccm.NFOV1 : this.rccm.NFOV2;
                PointF pos = this.rccm.getNFOVLocation(this.rccm.ActiveStage);
                // Convert mouse coordinates to global coordinate system units
                double pixX = nfov.Scale * (x - w / 2.0) / this.displayScale;
                double pixY = nfov.Scale * (y - h / 2.0) / this.displayScale;
                // Rotate pixel vector into global position vector
                PointF pix = this.rccm.fineVectorToGlobalVector(pixX, pixY);
                this.drawnLineEnd.X = pos.X + pix.X;
                this.drawnLineEnd.Y = pos.Y + pix.Y;
            }
        }

        /// <summary>
        /// Begin drawing new segment in active MeasurementSequence from user mouse input
        /// </summary>
        /// /// <param name="x">Mouse x location in pixels</param>
        /// <param name="y">Mouse y location in pixels</param>
        /// <param name="w">Canvas width in pixels</param>
        /// <param name="h">Canvas height in pixels</param>
        public void createDrawnLine(int x, int y, int w, int h)
        {
            if(this.ActiveIndex >= 0)
            {
                this.Drawing = true;
                // Get mouse location in global coordinates
                NFOV nfov = this.rccm.ActiveStage == RCCMStage.RCCM1 ? this.rccm.NFOV1 : this.rccm.NFOV2;
                PointF pos = this.rccm.getNFOVLocation(this.rccm.ActiveStage);
                double pixX = nfov.Scale * (x - w / 2.0) / this.displayScale;
                double pixY = nfov.Scale * (y - h / 2.0) / this.displayScale;
                PointF pix = this.rccm.fineVectorToGlobalVector(pixX, pixY);
                // Create start point - use last crack vertex if active crack is started.
                if (this.cracks[this.ActiveIndex].CountPoints > 0)
                {
                    Measurement p0 = this.cracks[this.ActiveIndex].getLastPoint();
                    this.drawnLineStart.X = (float) p0.X;
                    this.drawnLineStart.Y = (float) p0.Y;
                }
                else
                {
                    this.drawnLineStart.X = pos.X + pix.X;
                    this.drawnLineStart.Y = pos.Y + pix.Y;
                }
                // Create end point
                this.drawnLineEnd.X = pos.X + pix.X;
                this.drawnLineEnd.Y = pos.Y + pix.Y;
            }
        }
    }
}
