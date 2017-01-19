﻿using System;
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

        public void plot(Graphics axes, Region bounds)
        {
            if (this.points.Count > 1)
            {
                axes.Clip = bounds;
                PointF p0, p1;
                
                p0 = measurementToPoint(this.points[0]);
                for (int i = 1; i < this.points.Count; i++)
                {
                    p1 = measurementToPoint(this.points[i]);
                    axes.DrawLine(new Pen(this.color), p0, p1);
                    p0 = p1;                    
                }
            }            
        }

        private PointF measurementToPoint(Measurement pt)
        {
            return new PointF((float) pt.getX(), (float) pt.getY());
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
    }
}
