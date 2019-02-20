using System;
using System.Windows.Forms;

namespace graphics_programming
{
    public class Matrix3
    {
        private float P11 = 1;
        private float P12;
        private float P13;
        private float P21;
        private float P22 = 1;
        private float P23;
        private float P31;
        private float P32;
        private float P33 = 1;

        public Matrix3()
        {
        }

        public Matrix3(Vector2 vector)
        {
            P11 = vector.X;
            P22 = vector.Y;
        }

        public Matrix3(float p11, float p12, float p21, float p22)
        {
            P11 = p11;
            P12 = p12;
            P21 = p21;
            P22 = p22;
        }

        public Matrix3(
            float p11, float p12, float p13,
            float p21, float p22, float p23,
            float p31, float p32, float p33
        )
        {
            P11 = p11;
            P12 = p12;
            P21 = p21;
            P22 = p22;
        }

        public void Rotate(float degrees)
        {
            var radians = Math.PI / 180.0 * degrees;

            var sin = (float)Math.Sin(radians);
            var cos = (float)Math.Cos(radians);

            P11 = cos;
            P12 = sin;
            P21 = -sin;
            P22 = cos;
        }

        public void Translate(Vector2 v)
        {
            P13 += v.X;
            P23 += v.Y;
        }

        #region arethmic operations

        public static Matrix3 operator +(Matrix3 m1, Matrix3 m2)
        {
            return new Matrix3(
                m1.P11 + m2.P11, m1.P12 + m2.P12, m1.P13 + m2.P13,
                m1.P21 + m2.P21, m1.P22 + m2.P22, m1.P23 + m2.P13,
                m1.P31 + m2.P31, m1.P32 + m2.P32, m1.P33 + m2.P13
            );
        }

        public static Matrix3 operator -(Matrix3 m1, Matrix3 m2)
        {
            return new Matrix3(
                m1.P11 + m2.P11, m1.P12 + m2.P12, m1.P13 + m2.P13,
                m1.P21 + m2.P21, m1.P22 + m2.P22, m1.P23 + m2.P13,
                m1.P31 + m2.P31, m1.P32 + m2.P32, m1.P33 + m2.P13
            );
        }

        public static Matrix3 operator *(Matrix3 m1, float scalar)
        {
            return new Matrix3(
               m1.P11 * scalar, m1.P12 * scalar, m1.P13 * scalar,
               m1.P21 * scalar, m1.P22 * scalar, m1.P23 * scalar,
               m1.P33 * scalar, m1.P32 * scalar, m1.P33 * scalar
           );
        }

        public static Matrix3 operator *(Matrix3 m1, Matrix3 m2)
        {
            return new Matrix3(
                m1.P11 * m2.P11 + m1.P12 * m2.P21 + m1.P13 * m2.P31,
                m1.P11 * m2.P12 + m1.P12 * m2.P22 + m1.P13 * m2.P32,
                m1.P11 * m2.P13 + m1.P12 * m2.P23 + m1.P13 * m2.P33,

                m1.P21 * m2.P11 + m1.P22 * m2.P21 + m1.P23 * m2.P31,
                m1.P21 * m2.P12 + m1.P22 * m2.P22 + m1.P23 * m2.P32,
                m1.P21 * m2.P13 + m1.P22 * m2.P23 + m1.P23 * m2.P33,

                m1.P31 * m2.P11 + m1.P32 * m2.P21 + m1.P33 * m2.P31,
                m1.P31 * m2.P12 + m1.P32 * m2.P22 + m1.P33 * m2.P32,
                m1.P31 * m2.P13 + m1.P32 * m2.P23 + m1.P33 * m2.P33
            );
        }
        
        public static Vector2 operator *(Matrix3 m1, Vector2 vector)
        {
            return new Vector2(
                (m1.P11 * vector.X + m1.P12 * vector.Y) + m1.P13,
                (m1.P21 * vector.X + m1.P22 * vector.Y) + m1.P23
            );
        }

        /// <summary>
        /// Checks if matrixes are equal
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static bool operator ==(Matrix3 m1, Matrix3 m2)
        {
            return m2.Equals(m2);
        }

        /// <summary>
        /// Checks if matrixes are not equal
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static bool operator !=(Matrix3 m1, Matrix3 m2)
        {
            return !m2.Equals(m2);
        }

        #endregion

        #region overrides

        public override bool Equals(object obj)
        {
            var matrix = (Matrix3)obj;

            return (
                matrix.P11 == P11 &&
                matrix.P12 == P12 &&
                matrix.P13 == P13 &&

                matrix.P21 == P21 &&
                matrix.P22 == P22 &&
                matrix.P23 == P23 &&

                matrix.P31 == P31 &&
                matrix.P32 == P32 &&
                matrix.P33 == P33
            );
        }

        public override string ToString()
        {
            return
                $"{P11}, {P12}, {P13},\n" +
                $"{P21}, {P22}, {P23},\n" +
                $"{P31}, {P32}, {P33}\n";
        }


        #endregion
    }
}