using System.Collections.Generic;

namespace graphics_programming.Shapes
{
    public abstract class Shape2
    {
        public List<Vector2> vectorBuffer = new List<Vector2>();
    }

    public static class Shape2ExtensionMethods
    {
        public static Shape2 ApplyMatrix(this Shape2 shape, Matrix3 matrix)
        {
            var vectorBuffer = new List<Vector2>();

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
