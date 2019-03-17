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
        
        /// <summary>
        /// Calculates the shortest distance to one of the line segments
        /// </summary>
        /// <param name="lineStart">Start of line</param>
        /// <param name="lineEnd">End of line</param>
        /// <param name="position">Position to check against</param>
        /// <returns></returns>
        public static float SquaredDistanceToLine(
            Vector2 lineStart, 
            Vector2 lineEnd, 
            Vector2 position
        )
        {
            var startDot = (position.X - lineStart.X) * (lineEnd.X - lineStart.X) +
                           (position.Y - lineStart.Y) * (lineEnd.Y - lineStart.Y);

            // If the angle between Position and A is obtuse and the angle
            // between A and B is obtuse then the closest vector must be A
            if (startDot <= 0)
            {
                return Vector2.DistanceSquared(lineStart, position);
            }
            
            var endDot = (position.X - lineEnd.X) * (lineStart.X - lineEnd.X) +
                         (position.Y - lineEnd.Y) * (lineStart.Y - lineEnd.Y);

            // If the angle between Position and B is obtuse and the angle
            // between B and A is obtuse then the closest vector must be B
            if (endDot <= 0)
            {
                return Vector2.DistanceSquared(lineEnd, position);
            }

            return Vector2.DistanceSquared(
                position,
                lineStart + ((lineEnd - lineStart) * startDot) / (startDot + endDot)
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
        /// RotateAroundOrigin
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector2 RotateAroundOrigin(this Vector2 vector, float angle)
        {
            // Create a transformation matrix
            var transformationMatrix = new Matrix3()
                .Rotate(angle);
	
            // Transform the vector
            return vector.ApplyMatrix(transformationMatrix);
        }

        /// <summary>
        /// When vector exceeds given bounds value will be wrapped
        /// </summary>
        /// <param name="position"></param>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public static Vector2 WrapToBounds(this Vector2 position, Vector2 bounds)
        {
            if (position.X > bounds.X)
                return new Vector2(0, position.Y);

            if (position.Y > bounds.Y)
                return new Vector2(position.X, 0);

            if (position.X < 0)
                return new Vector2(bounds.X, position.Y);

            if (position.Y < 0)
                return new Vector2(position.X, bounds.Y);

            return position;
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