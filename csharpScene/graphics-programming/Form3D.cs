using graphics_programming.Shapes;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace graphics_programming
{
    public partial class Form3D : Form
    {
        AxisX axisX;
        AxisY axisY;
        Cube cube;

        public Form3D()
        {
            InitializeComponent();

            Width = 800;
            Height = 600;

            axisX = new AxisX(200);
            axisY = new AxisY(200);

            
            cube = new Cube(Color.Purple);

            //var transformationMatrix = new Matrix4();

            //cube.ApplyMatrix(transformationMatrix);
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

            var distance = 1;

            axisX.Draw(e.Graphics, ViewportTransformation(Width, Height, axisX.vectorBuffer));
            axisY.Draw(e.Graphics, ViewportTransformation(Width, Height, axisY.vectorBuffer));

            // Render 3d cube
            var transformedCube = ProjectionTransformation(distance, cube.vectorBuffer);

            cube.Draw(e.Graphics, ViewportTransformation(Width, Height, transformedCube));
        }
    }
}
