using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    public class PanelView
    {
        protected Pen pen;
        protected RectangleF panel;
        protected RectangleF fine1;
        protected RectangleF fine2;
        protected PointF[] plgpts;
        protected float w;
        protected float h;

        public PanelView(Settings settings)
        {
            // Create rectangle for displaying panel
            this.w = (float)settings.json["panel"]["width"];
            this.h = (float)settings.json["panel"]["height"];
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
            this.fine1 = new RectangleF(xFine1, yFine1, wFine1, hFine1);
            this.fine2 = new RectangleF(xFine2, yFine2, wFine2, hFine2);

            this.pen = new Pen(Color.Black);

        }

        public void paint(Graphics g)
        {
            RectangleF bounds = g.VisibleClipBounds;
            g.ScaleTransform(bounds.Width / (1.2f * this.w), bounds.Height / (1.2f * this.h));
            g.TranslateTransform(0.1f * this.w, 0.1f * this.h);
            g.DrawRectangles(this.pen, new RectangleF[] { this.panel, this.fine1, this.fine2 });
            Console.WriteLine(g.VisibleClipBounds.ToString());
        }
    }
}
