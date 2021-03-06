﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace graphics_programming.Shapes
{
    public class AxisX3 : Shape3
    {
        public AxisX3(float size = 3F)
        {
            vectorBuffer = new List<Vector3>
            {
                new Vector3(0, 0, 0),
                new Vector3(size, 0, 0)
            };
        }

        public void Draw(Graphics g, List<Vector2> vb)
        {
            Pen pen = new Pen(Color.Red, 2f);

            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[1].X, vb[1].Y);

            Font font = new Font("Arial", 10);
            PointF p = new PointF(vb[1].X, vb[1].Y);

            g.DrawString("x", font, Brushes.Red, p);
        }
    }
}
