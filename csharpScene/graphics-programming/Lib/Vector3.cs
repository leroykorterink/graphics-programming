namespace graphics_programming
{
    public class Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Adds to vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        /// <summary>
        /// Sutracts two vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        /// <summary>
        /// Multiplies given vector by given amount
        /// </summary>
        /// <param name="a"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 v1, float amount)
        {
            return new Vector3(v1.X * amount, v1.Y * amount, v1.Z * amount);
        }

        /// <summary>
        /// Divides given vector by given amount
        /// </summary>
        /// <param name="a"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static Vector3 operator /(Vector3 v1, float amount)
        {
            if (amount == 0)
            {
                return v1.Clone();
            }

            return new Vector3(v1.X / amount, v1.Y / amount, v1.Z / amount);
        }

        /// <summary>
        /// Creates a new Vector2D
        /// </summary>
        /// <returns></returns>
        public Vector3 Clone()
        {
            return new Vector3(X, Y, Z);
        }

        #region overrides

        public override bool Equals(object obj)
        {
            var vector = (Vector3)obj;

            return (
                vector.X == X &&    
                vector.Y == Y &&    
                vector.Z == Z
            );
        }

        public override string ToString()
        {
            return (
                $"|{X}|\n" +
                $"|{Y}|\n" +
                $"|{Z}|"
            );
        }

        #endregion
    }
}
