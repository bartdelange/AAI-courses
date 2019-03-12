using System;
using System.Drawing;
using System.Numerics;

namespace AICore.Util
{
    public static class Vector2ExtensionMethods
    {
        private static readonly Random Random = new Random();

        #region static methods
        
        public static Vector2 GetRandom(Vector2 max, Vector2 min = new Vector2())
        {
            return new Vector2(
                Random.Next((int) min.X, (int) max.X),
                Random.Next((int) min.Y, (int) max.Y)
            );
        }
        
        #endregion
        
        #region extension methods
        
        /// <summary>
        /// Converts vector to PointF
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static PointF ToPointF(this Vector2 vector)
        {
            return new PointF((int) vector.X, (int) vector.Y);
        }

        /// <summary>
        /// Converts vector to Point
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Point ToPoint(this Vector2 vector)
        {
            return new Point((int) vector.X, (int) vector.Y);
        }

        /// <summary>
        /// Subtracts float from vectors x and y values. Vector2 is a build-in type for which we can't write operator overloads
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector2 Minus(this Vector2 vector, float value)
        {
            return vector - new Vector2(value);
        }

        /// <summary>
        /// Gets perpendicular vector from given vector by inverting the Y value
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector2 Perpendicular(this Vector2 vector)
        {
            return new Vector2(-vector.Y, vector.X);
        }

        /// <summary>
        /// Truncates given vector when magnitude exceeds given value
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector2 Truncate(this Vector2 vector, float max)
        {
            if (vector.Length() <= max)
            {
                return vector;
            }
            
            return Vector2.Normalize(vector) * max;
        }

        /// <summary>
        /// Transforms given vector with given matrix
        /// </summary>
        /// <param name="vector2"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Vector2 ApplyMatrix(this Vector2 vector2, Matrix3 matrix)
        {
            var x = matrix.P11 * vector2.X + matrix.P21 * vector2.Y + matrix.P13;
            var y = matrix.P12 * vector2.X + matrix.P22 * vector2.Y + matrix.P23;

            return new Vector2(x, y);
        }
        
        #endregion
    }
}