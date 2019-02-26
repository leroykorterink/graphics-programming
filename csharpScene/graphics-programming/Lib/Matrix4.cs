using System;

namespace graphics_programming
{
    public class Matrix4
    {
        private float P11 = 1;
        private float P12;
        private float P13;
        private float P14;

        private float P21;
        private float P22 = 1;
        private float P23;
        private float P24;

        private float P31;
        private float P32;
        private float P33 = 1;
        private float P34;

        private float P41;
        private float P42;
        private float P43;
        private float P44 = 1;

        public Matrix4()
        {
        }

        public Matrix4(Vector3 vector)
        {
            P11 = vector.X;
            P22 = vector.Y;
            P33 = vector.Z;
        }

        public Matrix4(
            float p11, float p12, float p13, float p14,
            float p21, float p22, float p23, float p24,
            float p31, float p32, float p33, float p34,
            float p41, float p42, float p43, float p44
        )
        {
            P11 = p11;
            P12 = p12;
            P13 = p13;
            P14 = p14;

            P21 = p21;
            P22 = p22;
            P23 = p23;
            P24 = p24;

            P31 = p31;
            P32 = p32;
            P33 = p33;
            P34 = p34;

            P41 = p41;
            P42 = p42;
            P43 = p43;
            P44 = p44;
        }

        public void RotateX(float degrees)
        {
            var radians = degrees * Math.PI / 180.0;

            var sin = (float)Math.Sin(radians);
            var cos = (float)Math.Cos(radians);

            P22 = cos;
            P32 = sin;
            P23 = -sin;
            P33 = cos;
        }

        public void RotateY(float degrees)
        {
            var radians = Math.PI * degrees / 180.0;

            var sin = (float)Math.Sin(radians);
            var cos = (float)Math.Cos(radians);

            P11 = cos;
            P13 = sin;
            P31 = -sin;
            P33 = cos;
        }

        public void RotateZ(float degrees)
        {
            var radians = Math.PI * degrees / 180.0;

            var sin = (float)Math.Sin(radians);
            var cos = (float)Math.Cos(radians);

            P11 = cos;
            P21 = sin;
            P12 = -sin;
            P22 = cos;
        }

        public void Translate(Vector3 v)
        {
            P14 = v.X;
            P24 = v.Y;
            P34 = v.Z;
        }

        #region arethmic operations

        public static Matrix4 operator +(Matrix4 m1, Matrix4 m2)
        {
            return new Matrix4(
                m1.P11 + m2.P11, m1.P12 + m2.P12, m1.P13 + m2.P13, m1.P14 + m2.P14,
                m1.P21 + m2.P21, m1.P22 + m2.P22, m1.P23 + m2.P13, m1.P24 + m2.P24,
                m1.P31 + m2.P31, m1.P32 + m2.P32, m1.P33 + m2.P13, m1.P34 + m2.P34,
                m1.P31 + m2.P31, m1.P32 + m2.P32, m1.P33 + m2.P13, m1.P44 + m2.P44
            );
        }

        public static Matrix4 operator -(Matrix4 m1, Matrix4 m2)
        {
            return new Matrix4(
                m1.P11 - m2.P11, m1.P12 - m2.P12, m1.P13 - m2.P13, m1.P14 - m2.P14,
                m1.P21 - m2.P21, m1.P22 - m2.P22, m1.P23 - m2.P13, m1.P24 - m2.P24,
                m1.P31 - m2.P31, m1.P32 - m2.P32, m1.P33 - m2.P13, m1.P34 - m2.P34,
                m1.P31 - m2.P31, m1.P32 - m2.P32, m1.P33 - m2.P13, m1.P44 - m2.P44
            );
        }

        public static Matrix4 operator *(Matrix4 m1, float scalar)
        {
            return new Matrix4(
               m1.P11 * scalar, m1.P12 * scalar, m1.P13 * scalar, m1.P14 * scalar,
               m1.P21 * scalar, m1.P22 * scalar, m1.P23 * scalar, m1.P24 * scalar,
               m1.P31 * scalar, m1.P32 * scalar, m1.P33 * scalar, m1.P34 * scalar,
               m1.P41 * scalar, m1.P42 * scalar, m1.P43 * scalar, m1.P44 * scalar
           );
        }  

        public static Matrix4 operator *(Matrix4 m1, Matrix4 m2)
        {
            return new Matrix4(
                m1.P11 * m2.P11 + m1.P12 * m2.P21 + m1.P13 * m2.P31 + m1.P14 * m2.P41,
                m1.P11 * m2.P12 + m1.P12 * m2.P22 + m1.P13 * m2.P32 + m1.P14 * m2.P42,
                m1.P11 * m2.P13 + m1.P12 * m2.P23 + m1.P13 * m2.P33 + m1.P14 * m2.P43,
                m1.P11 * m2.P14 + m1.P12 * m2.P24 + m1.P13 * m2.P34 + m1.P14 * m2.P44,

                m1.P21 * m2.P11 + m1.P22 * m2.P21 + m1.P23 * m2.P31 + m1.P24 * m2.P41,
                m1.P21 * m2.P12 + m1.P22 * m2.P22 + m1.P23 * m2.P32 + m1.P24 * m2.P42,
                m1.P21 * m2.P13 + m1.P22 * m2.P23 + m1.P23 * m2.P33 + m1.P24 * m2.P43,
                m1.P21 * m2.P14 + m1.P22 * m2.P24 + m1.P23 * m2.P34 + m1.P24 * m2.P44,

                m1.P31 * m2.P11 + m1.P32 * m2.P21 + m1.P33 * m2.P31 + m1.P34 * m2.P41,
                m1.P31 * m2.P12 + m1.P32 * m2.P22 + m1.P33 * m2.P32 + m1.P34 * m2.P42,
                m1.P31 * m2.P13 + m1.P32 * m2.P23 + m1.P33 * m2.P33 + m1.P34 * m2.P43,
                m1.P31 * m2.P14 + m1.P32 * m2.P24 + m1.P33 * m2.P34 + m1.P34 * m2.P44,

                m1.P41 * m2.P11 + m1.P42 * m2.P21 + m1.P43 * m2.P31 + m1.P44 * m2.P41,
                m1.P41 * m2.P12 + m1.P42 * m2.P22 + m1.P43 * m2.P32 + m1.P44 * m2.P42,
                m1.P41 * m2.P13 + m1.P42 * m2.P23 + m1.P43 * m2.P33 + m1.P44 * m2.P43,
                m1.P41 * m2.P14 + m1.P42 * m2.P24 + m1.P43 * m2.P34 + m1.P44 * m2.P44
            );
        }
        
        public static Vector3 operator *(Matrix4 m1, Vector3 vector)
        {
            return new Vector3(
                (m1.P11 * vector.X + m1.P12 * vector.Y + m1.P13 * vector.Z) + m1.P14,
                (m1.P21 * vector.X + m1.P22 * vector.Y + m1.P23 * vector.Z) + m1.P24,
                (m1.P31 * vector.X + m1.P32 * vector.Y + m1.P33 * vector.Z) + m1.P34
            );
        }

        /// <summary>
        /// Checks if matrixes are equal
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static bool operator ==(Matrix4 m1, Matrix4 m2)
        {
            return m1.Equals(m2);
        }

        /// <summary>
        /// Checks if matrixes are not equal
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static bool operator !=(Matrix4 m1, Matrix4 m2)
        {
            return !m1.Equals(m2);
        }

        #endregion

        #region overrides

        public override bool Equals(object obj)
        {
            var matrix = (Matrix4)obj;

            return (
                matrix.P11 == P11 &&
                matrix.P12 == P12 &&
                matrix.P13 == P13 &&
                matrix.P14 == P14 &&

                matrix.P21 == P21 &&
                matrix.P22 == P22 &&
                matrix.P23 == P23 &&
                matrix.P24 == P24 &&

                matrix.P31 == P31 &&
                matrix.P32 == P32 &&
                matrix.P33 == P33 &&
                matrix.P34 == P34 &&

                matrix.P41 == P41 &&
                matrix.P42 == P42 &&
                matrix.P43 == P43 &&
                matrix.P44 == P44
            );
        }

        public override string ToString()
        {
            return
                $"{P11}, {P12}, {P13}, {P14},\n" +
                $"{P21}, {P22}, {P23}, {P24},\n" +
                $"{P31}, {P32}, {P33}, {P34},\n" +
                $"{P41}, {P42}, {P43}, {P44}\n";
        }

        #endregion
    }
}