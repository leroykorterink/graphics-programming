using System.Collections.Generic;

namespace graphics_programming.Shapes
{
    public abstract class Shape3
    {
        public List<Vector3> vectorBuffer = new List<Vector3>();
    }

    public static class Shape3ExtensionMethods
    {
        public static Shape3 ApplyMatrix(this Shape3 shape, Matrix4 matrix)
        {
            var vectorBuffer = new List<Vector3>();

            shape.vectorBuffer.ForEach(vector =>
            {
                vectorBuffer.Add(matrix * vector);
            });

            // Replace vectors on square instance with the transformed vectors
            shape.vectorBuffer = vectorBuffer;

            // return square to allow method chaining
            return shape;
        }
    }
}
