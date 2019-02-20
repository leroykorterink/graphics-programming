using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace graphics_programming.Shapes
{
    public class AxisX : Shape2
    {
        public AxisX(int size=100)
        {
            vectorBuffer = new List<Vector2>
            {
                new Vector2(0, 0),
                new Vector2(size, 0)
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
