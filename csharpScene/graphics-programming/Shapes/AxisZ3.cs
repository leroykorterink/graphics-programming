using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace graphics_programming.Shapes
{
    public class AxisZ3 : Shape3
    {
        public AxisZ3(int size = 100)
        {
            vectorBuffer = new List<Vector3>
            {
                new Vector3(0, 0, 0),
                new Vector3(0, 0, size)
            };
        }

        public void Draw(Graphics g, List<Vector2> vb)
        {
            Pen pen = new Pen(Color.Blue, 2f);
            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[1].X, vb[1].Y);
            Font font = new Font("Arial", 10);
            PointF p = new PointF(vb[1].X, vb[1].Y);
            g.DrawString("z", font, Brushes.Blue, p);
        }
    }
}
