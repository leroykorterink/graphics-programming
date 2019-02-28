namespace graphics_programming
{
    public class Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x = 0, float y = 0)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Adds to vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector2 operator+ (Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        /// <summary>
        /// Sutracts two vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector2 operator- (Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        /// <summary>
        /// Multiplies given vector by given amount
        /// </summary>
        /// <param name="a"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static Vector2 operator* (Vector2 v1, float amount)
        {
            return new Vector2(v1.X * amount, v1.Y * amount);
        }

        /// <summary>
        /// Divides given vector by given amount
        /// </summary>
        /// <param name="a"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static Vector2 operator/ (Vector2 v1, float amount)
        {
            if (amount == 0)
            {
                return v1.Clone();
            }

            return new Vector2(v1.X / amount, v1.Y / amount);
        }

        /// <summary>
        /// Creates a new Vector2D
        /// </summary>
        /// <returns></returns>
        public Vector2 Clone()
        {
            return new Vector2(X, Y);
        }

        public override bool Equals(object obj)
        {
            var vector = (Vector2)obj;

            return (
                vector.X == X &&
                vector.Y == Y
            );
        }

        public override string ToString()
        {
            return $"|{X}|\n|{Y}|";
        }
    }
}
