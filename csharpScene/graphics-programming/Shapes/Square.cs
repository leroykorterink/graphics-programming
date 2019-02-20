using System.Collections.Generic;
using System.Drawing;

namespace graphics_programming.Shapes
{
    public class Square : Shape2
    {
        readonly Color color;
        private readonly int size;
        private readonly float weight;

        public Square(Color color, int size = 100, float weight = 3)
        {
            this.color = color;
            this.size = size;
            this.weight = weight;

            vectorBuffer = new List<Vector2>
            {
                new Vector2(-size, -size),
                new Vector2(size, -size),
                new Vector2(size, size),
                new Vector2(-size, size)
            };
        }

        public void Draw(Graphics g, List<Vector2> vb)
        {
            Pen pen = new Pen(color, weight);
            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[1].X, vb[1].Y);
            g.DrawLine(pen, vb[1].X, vb[1].Y, vb[2].X, vb[2].Y);
            g.DrawLine(pen, vb[2].X, vb[2].Y, vb[3].X, vb[3].Y);
            g.DrawLine(pen, vb[3].X, vb[3].Y, vb[0].X, vb[0].Y);
        }
    }
}
