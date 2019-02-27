using graphics_programming.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace graphics_programming
{
    public partial class Form3D : Form
    {
        private readonly AxisX axisX;
        private readonly AxisY axisY;
        private Cube cube = new Cube(Color.Purple);

        private CubeControls cubeControls = new CubeControls();

        public Form3D()
        {
            InitializeComponent();

            // Attach Invalidate method to the cubeControls onChange delegate
            cubeControls.OnChange += UpdateForm;
            
            // Attach keydown event handler that is used to update 3d view properties
            KeyDown += new KeyEventHandler(cubeControls.KeyDown);

            //
            Width = 800;
            Height = 600;

            axisX = new AxisX(200);
            axisY = new AxisY(200);
        }

        private void UpdateForm()
        {
            cube = new Cube(Color.Purple);

            var transformationMatrix = new Matrix4()
                .RotateX(cubeControls.Values.ThetaX)
                .RotateY(cubeControls.Values.ThetaY)
                .RotateZ(cubeControls.Values.ThetaZ)
                .Translate(
                    new Vector3(
                        cubeControls.Values.X,
                        cubeControls.Values.Y,
                        cubeControls.Values.Z
                    )
                );

            cube.ApplyMatrix(transformationMatrix);

            // Invalidate form to trigger a new paint
            Invalidate();
        }

        private List<Vector3> ViewportTransformation(float width, float height, List<Vector3> vectors)
        {
            var cameraPosition = new Vector3(5, 1, 5);

            var viewMatrix = new Matrix4()
                .Translate(cameraPosition)
                .RotateZ(cubeControls.Values.CameraPhi)
                .RotateX(cubeControls.Values.CameraTheta);

            // TODO Inverse matrix

            List<Vector3> result = new List<Vector3>();

            vectors.ForEach(vector => result.Add(viewMatrix * vector));

            return result;
        }

        private List<Vector2> ProjectionTransformation(float distance, List<Vector3> vectors)
        {
            List<Vector2> result = new List<Vector2>();

            var distanceInverse = -distance;

            vectors.ForEach(vector =>
            {
                var perspective = distanceInverse / vector.Z;

                result.Add(new Vector2(perspective * vector.X, perspective * vector.Y));
            });

            return result;
        }

        private List<Vector2> ViewingPipeline(List<Vector3> vectorBuffer)
        {
            var transformedVectorBuffer = ViewportTransformation(Width, Height, vectorBuffer);

            return ProjectionTransformation(cubeControls.Values.Distance, vectorBuffer);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //axisX.Draw(e.Graphics, ViewingPipeline(axisX3.vectorBuffer));
            //axisY.Draw(e.Graphics, ViewingPipeline(axisY3.vectorBuffer));

            // Render 3d cube
            cube.Draw(e.Graphics, ViewingPipeline(cube.vectorBuffer));

            // Draw viewport properties
            cubeControls.Draw(e.Graphics);
        }
    }
}
