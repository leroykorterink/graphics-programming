using graphics_programming.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace graphics_programming
{
    public partial class Form2D : Form
    {
        AxisX axisX;
        AxisY axisY;
        Square square;
        Square squareTransformed;

        public Form2D()
        {
            InitializeComponent();

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
