using System.Windows.Forms;

namespace graphics_programming
{
    class Matrix2
    {
        private float P11;
        private float P12;
        private float P21;
        private float P22;

        public Matrix2()
        {
        }

        public Matrix2(Vector2 vector)
        {
            P11 = vector.X;
            P22 = vector.Y;
        }

        public Matrix2(float p11, float p12, float p21, float p22)
        {
            P11 = p11;
            P12 = p12;
            P21 = p21;
            P22 = p22;
        }

        #region arethmic operations

        public static Matrix2 operator +(Matrix2 m1, Matrix2 m2)
        {
            return new Matrix2(
                m1.P11 + m2.P11, m1.P12 + m2.P12,
                m1.P21 + m2.P21, m1.P22 + m2.P22
            );
        }

        public static Matrix2 operator -(Matrix2 m1, Matrix2 m2)
        {
            return new Matrix2(
                m1.P11 - m2.P11, m1.P12 - m2.P12,
                m1.P21 - m2.P21, m1.P22 - m2.P22
            );
        }

        public static Matrix2 operator *(Matrix2 m1, float scalar)
        {
            return new Matrix2(
               m1.P11 * scalar, m1.P12 * scalar,
               m1.P21 * scalar, m1.P22 * scalar
           );
        }

        public static Matrix2 operator *(float scalar, Matrix2 m1)
        {
            return m1 * scalar;
        }

        public static Matrix2 operator *(Matrix2 m1, Matrix2 m2)
        {
            return new Matrix2(
                m1.P11 * m2.P11 + m1.P12 * m2.P21,
                m1.P11 * m2.P12 + m1.P12 * m2.P22,
                m1.P21 * m2.P11 + m1.P22 * m2.P21,
                m1.P21 * m2.P12 + m1.P22 * m2.P22
            );
        }
        
        public static Vector2 operator *(Matrix2 m1, Vector2 vector)
        {
            return new Vector2(
                m1.P11 * vector.X + m1.P12 * vector.Y,
                m1.P21 * vector.X + m1.P22 * vector.Y
            );
        }

        #endregion
    }
}