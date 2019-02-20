using graphics_programming.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace graphics_programming
{
    public partial class Form1 : Form
    {
        AxisX axisX;
        AxisY axisY;
        Square square;
        Square squareTransformed;

        public Form1()
        {
            InitializeComponent();

            Vector2 v1 = new Vector2();
            Console.WriteLine(v1);
            Vector2 v2 = new Vector2(1, 2);
            Console.WriteLine(v2);
            Vector2 v3 = new Vector2(2, 6);
            Console.WriteLine(v3);
            Vector2 v4 = v2 + v3;
            Console.WriteLine(v4); // 3, 8

            Matrix2 m1 = new Matrix2();
            Console.WriteLine(m1); // 1, 0, 0, 1
            Matrix2 m2 = new Matrix2(
                2,  4,
                -1, 3
            );
            Console.WriteLine(m2);
            Console.WriteLine(m1 + m2); // 3, 4, -1, 4
            Console.WriteLine(m1 - m2); // -1, -4, 1, -2
            Console.WriteLine(m2 * m2); // 0, 20, -5, 5

            Console.WriteLine(m2 * v3); // 28, 16

            Width = 800;
            Height = 600;

            axisX = new AxisX(200);
            axisY = new AxisY(200);

            square = new Square(Color.Purple, 100);
            squareTransformed = new Square(Color.ForestGreen, 50);

            var transformation = new Matrix3();
            transformation.Rotate(25);
            transformation.Translate(new Vector2(200, 200));

            squareTransformed.ApplyMatrix(transformation);
        }

        private List<Vector2> ViewportTransformation(float width, float height, List<Vector2> vectors)
        {
            List<Vector2> result = new List<Vector2>();

            float dx = width / 2;
            float dy = height / 2;

            vectors.ForEach(vector =>
                result.Add(new Vector2(vector.X + dx, dy - vector.Y))
            );

            return result;
        }

        private List<Vector3> ViewportTransformation(float width, float height, List<Vector3> vectors)
        {
            List<Vector3> result = new List<Vector3>();

            float dx = width / 2;
            float dy = height / 2;

            vectors.ForEach(vector =>
                result.Add(new Vector3(vector.X + dx, dy - vector.Y))
            );

            return result;
        }

        private List<Vector2> ProjectionTransformation(float distance, List<Vector3> vectors)
        {
            List<Vector2> result = new List<Vector2>();

            var distanceInverse = -distance;

            vectors.ForEach(vector => result.Add(
                new Vector2(vector.X, distance * (vector.Y / vector.Z))
            ));

            return result;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            axisX.Draw(e.Graphics, ViewportTransformation(Width, Height, axisX.vectorBuffer));
            axisY.Draw(e.Graphics, ViewportTransformation(Width, Height, axisY.vectorBuffer));

            square.Draw(e.Graphics, ViewportTransformation(Width, Height, square.vectorBuffer));
            squareTransformed.Draw(e.Graphics, ViewportTransformation(Width, Height, squareTransformed.vectorBuffer));
        }

    }
}
