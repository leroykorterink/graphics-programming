﻿using System;

namespace graphics_programming
{
    #pragma warning disable CS0661, CS0660
    public class Matrix4 : IEquatable<Matrix4>
    {
        public float P11 = 1;
        public float P12;
        public float P13;
        public float P14;

        public float P21;
        public float P22 = 1;
        public float P23;
        public float P24;

        public float P31;
        public float P32;
        public float P33 = 1;
        public float P34;

        public float P41;
        public float P42;
        public float P43;
        public float P44 = 1;

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

        public override string ToString()
        {
            return
                $"{P11}, {P12}, {P13}, {P14},\n" +
                $"{P21}, {P22}, {P23}, {P24},\n" +
                $"{P31}, {P32}, {P33}, {P34},\n" +
                $"{P41}, {P42}, {P43}, {P44}\n";
        }

        public bool Equals(Matrix4 other)
        {
            return (
                other.P11 == P11 &&
                other.P12 == P12 &&
                other.P13 == P13 &&
                other.P14 == P14 &&

                other.P21 == P21 &&
                other.P22 == P22 &&
                other.P23 == P23 &&
                other.P24 == P24 &&

                other.P31 == P31 &&
                other.P32 == P32 &&
                other.P33 == P33 &&
                other.P34 == P34 &&

                other.P41 == P41 &&
                other.P42 == P42 &&
                other.P43 == P43 &&
                other.P44 == P44
            );
        }

        #endregion
    }

    public static class Matrix4ExtensionMethods
    {
        public static Matrix4 RotateX(this Matrix4 matrix, float degrees)
        {
            var radians = degrees * Math.PI / 180.0;

            var sin = (float)Math.Sin(radians);
            var cos = (float)Math.Cos(radians);

            var rotationMatrix = new Matrix4
            {
                P22 = cos,
                P32 = sin,
                P23 = -sin,
                P33 = cos
            };

            return rotationMatrix * matrix;
        }

        public static Matrix4 RotateY(this Matrix4 matrix, float degrees)
        {
            var radians = degrees * Math.PI / 180.0;

            var sin = (float)Math.Sin(radians);
            var cos = (float)Math.Cos(radians);

            var rotationMatrix = new Matrix4
            {
                P11 = cos,
                P13 = sin,
                P31 = -sin,
                P33 = cos
            };

            return rotationMatrix * matrix;
        }

        public static Matrix4 RotateZ(this Matrix4 matrix, float degrees)
        {
            var radians = degrees * Math.PI / 180.0;

            var sin = (float)Math.Sin(radians);
            var cos = (float)Math.Cos(radians);

            var rotationMatrix = new Matrix4
            {
                P11 = cos,
                P21 = sin,
                P12 = -sin,
                P22 = cos
            };

            return rotationMatrix * matrix;
        }

        public static Matrix4 Translate(this Matrix4 matrix, Vector3 velocity)
        {
            var translationMatrix = new Matrix4
            {
                P14 = velocity.X,
                P24 = velocity.Y,
                P34 = velocity.Z
            };

            return translationMatrix * matrix;
        }
    }
}