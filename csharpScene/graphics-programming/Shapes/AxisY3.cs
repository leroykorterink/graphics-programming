using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace graphics_programming.Shapes
{
    public class AxisY3 : Shape3
    {
        public AxisY3(int size = 100)
        {
            vectorBuffer = new List<Vector3>
            {
                new Vector3(0, 0, 0),
                new Vector3(0, size, 0)
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
