﻿using graphics_programming.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace graphics_programming
{
    public partial class Form3D : Form
    {
        private readonly AxisX3 axisX3;
        private readonly AxisY3 axisY3;
        private readonly AxisZ3 axisZ3;
        private Cube cube = new Cube(Color.Purple);

        private CubeControls cubeControls = new CubeControls();

        public Form3D()
        {
            DoubleBuffered = true;

            InitializeComponent();

            // Attach Invalidate method to the cubeControls onChange delegate
            cubeControls.OnChange += UpdateForm;
            
            // Attach keydown event handler that is used to update 3d view properties
            KeyDown += new KeyEventHandler(cubeControls.KeyDown);

            //
            Width = 1000;
            Height = 1000;

            axisX3 = new AxisX3(200);
            axisY3 = new AxisY3(200);
            axisZ3 = new AxisZ3(200);
        }

        private void UpdateForm()
        {
            cube = new Cube(Color.Black);

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

        public List<Vector3> ViewportTransformation(float width, float height, List<Vector3> vectors)
        {
            //var viewMatrix = new Matrix4()
            //    .RotateZ(cubeControls.Values.CameraPhi)
            //    .RotateX(cubeControls.Values.CameraTheta);

            var thetaDegrees = Math.PI / 180 * cubeControls.Values.CameraTheta;
            var phiDegrees = Math.PI / 180 * cubeControls.Values.CameraPhi;

            var thetaSin = (float)Math.Sin(thetaDegrees);
            var phiSin = (float)Math.Sin(phiDegrees);
            var thetaCos = (float)Math.Cos(thetaDegrees);
            var phiCos = (float)Math.Cos(phiDegrees);

            var viewMatrix = new Matrix4(
                -thetaSin, thetaCos, 0, 0,
                -thetaCos * phiCos, -phiCos * thetaSin, phiSin, 0,
                thetaCos * phiSin, thetaSin * phiSin, phiCos, cubeControls.Values.Distance,
                0, 0, 0, 1
            );
            //.Translate(cameraPosition);

            List<Vector3> result = new List<Vector3>();

            vectors.ForEach(vector => result.Add(viewMatrix * vector));

            return result;
        }

        public List<Vector2> ProjectionTransformation(float distance, List<Vector3> vectors)
        {
            List<Vector2> result = new List<Vector2>();

            vectors.ForEach(vector =>
            {
                var perspective = distance / vector.Z;

                var projectionMatrix = new Matrix3(
                    perspective, 0, 0,
                    0, perspective, 0,
                    0, 0, 1
                );

                result.Add(projectionMatrix * new Vector2(vector.X, vector.Y));
            });

            return result;
        }

        private List<Vector2> ViewingPipeline(List<Vector3> vectorBuffer)
        {
            var verticalCenter = Height / 2;
            var horizontalCenter = Width / 2;

            var transformedVectorBuffer = ViewportTransformation(Width, Height, vectorBuffer);

            return ProjectionTransformation(cubeControls.Values.Distance, vectorBuffer);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            axisX3.Draw(e.Graphics, ViewingPipeline(axisX3.vectorBuffer));
            axisY3.Draw(e.Graphics, ViewingPipeline(axisY3.vectorBuffer));
            axisZ3.Draw(e.Graphics, ViewingPipeline(axisZ3.vectorBuffer));

            // Render 3d cube
            cube.Draw(e.Graphics, ViewingPipeline(cube.vectorBuffer));

            // Draw viewport properties
            cubeControls.Draw(e.Graphics);
        }
    }
}
