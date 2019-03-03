using System;

namespace graphics_programming
{
    #pragma warning disable CS0661, CS0660
    public class Vector2 : IEquatable<Vector2>
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
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public static Vector2 operator+ (Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        /// <summary>
        /// Sutracts two vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public static Vector2 operator- (Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        /// <summary>
        /// Multiplies given vector by given amount
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="amount"></param>
        public static Vector2 operator* (Vector2 v1, float amount)
        {
            return new Vector2(v1.X * amount, v1.Y * amount);
        }

        /// <summary>
        /// Divides given vector by given amount
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="amount"></param>
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
        public Vector2 Clone()
        {
            return new Vector2(X, Y);
        }

        public override string ToString()
        {
            return (
                $"|{X}|\n" +
                $"|{Y}|"
            );
        }

        /// <summary>
        /// Checks if given Vector2 is equal to current Vector2
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Vector2 other)
        {
            return (
                other.X == X &&
                other.Y == Y
            );
        }
    }
}
