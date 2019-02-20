using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace graphics_programming.Shapes
{
    public class AxisY : Shape2
    {
        public AxisY(int size = 100)
        {
            vectorBuffer = new List<Vector2>
            {
                new Vector2(0, 0),
                new Vector2(0, size)
            };
        }

        public void Draw(Graphics g, List<Vector2> vb)
        {
            Pen pen = new Pen(Color.Green, 2f);
            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[1].X, vb[1].Y);
            Font font = new Font("Arial", 10);
            PointF p = new PointF(vb[1].X, vb[1].Y);
            g.DrawString("y", font, Brushes.Green, p);
        }
    }
}
