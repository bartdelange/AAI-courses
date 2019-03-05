using System;
using System.Numerics;

namespace AICore.Util
{
    #pragma warning disable CS0661, CS0660
    public class Matrix3 : IEquatable<Matrix3>
    {
        public float P11 = 1;
        public float P12;
        public float P13;
        public float P21;
        public float P22 = 1;
        public float P23;
        public float P31;
        public float P32;
        public float P33 = 1;

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
            P13 = p13;

            P21 = p21;
            P22 = p22;
            P23 = p23;

            P31 = p31;
            P32 = p32;
            P33 = p33;
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
                m1.P11 - m2.P11, m1.P12 - m2.P12, m1.P13 - m2.P13,
                m1.P21 - m2.P21, m1.P22 - m2.P22, m1.P23 - m2.P13,
                m1.P31 - m2.P31, m1.P32 - m2.P32, m1.P33 - m2.P13
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
            return m1.Equals(m2);
        }

        /// <summary>
        /// Checks if matrixes are not equal
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static bool operator !=(Matrix3 m1, Matrix3 m2)
        {
            return !m1.Equals(m2);
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            return
                $"{P11}, {P12}, {P13},\n" +
                $"{P21}, {P22}, {P23},\n" +
                $"{P31}, {P32}, {P33}\n";
        }

        public bool Equals(Matrix3 other)
        {
            return (
                other.P11 == P11 &&
                other.P12 == P12 &&
                other.P13 == P13 &&

                other.P21 == P21 &&
                other.P22 == P22 &&
                other.P23 == P23 &&

                other.P31 == P31 &&
                other.P32 == P32 &&
                other.P33 == P33
            );
        }

        #endregion
    }

    public static class Matrix3ExtensionMethods
    {
        /// <summary>
        ///     Rotates matrix by using the given vectors
        /// </summary>
        public static Matrix3 Rotate(this Matrix3 matrix, Vector2 heading, Vector2 side)
        {
            var rotateMatrix = new Matrix3()
            {
                P11 = heading.X,
                P12 = heading.Y,
                P21 = side.X,
                P22 = side.Y,
            };

            // Console.WriteLine("Rotate\n" + matrix);	

            return rotateMatrix * matrix;
        }
        
        public static Matrix3 Rotate(this Matrix3 matrix, float degrees)
        {
            var radians = Math.PI / 180.0 * degrees;

            var sin = (float)Math.Sin(radians);
            var cos = (float)Math.Cos(radians);

            var rotateMatrix = new Matrix3()
            {
                P11 = cos,
                P12 = sin,
                P21 = -sin,
                P22 = cos
            };

            return rotateMatrix * matrix;
        }

        public static Matrix3 Translate(this Matrix3 matrix, Vector2 vector)
        {
            var translateMatrix = new Matrix3()
            {
                P13 = vector.X,
                P23 = vector.Y
            };

            return translateMatrix * matrix;
        }
    }
}