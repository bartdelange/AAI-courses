using System;
using System.Drawing;

namespace AIBehaviours.Util
{
    public class Vector2D
    {
        /// <summary>
        ///     Position on x (horizontal) axis
        /// </summary>
        public readonly double _X;

        /// <summary>
        ///     Position on y (vertical) axis
        /// </summary>
        public readonly double _Y;

        public Vector2D()
        {
        }

        public Vector2D(double x, double y)
        {
            _X = x;
            _Y = y;
        }

        public double Length()
        {
            return Math.Sqrt(LengthSquared());
        }

        public double LengthSquared()
        {
            return _X * _X + _Y * _Y;
        }

        public double Dot(Vector2D target)
        {
            return _X * target._X + _Y * target._Y;
        }

        public Vector2D Normalize()
        {
            var length = Length();

            if (!(length > double.Epsilon))
                return this;

            return new Vector2D(_X / length, _Y / length);
        }

        public Vector2D Perpendicular()
        {
            return new Vector2D(-_Y, _X);
        }

        public Vector2D Truncate(double max)
        {
            if (!(Length() > max)) return this;

            return Normalize() * max;
        }

        /// <summary>
        ///     Convert vector to readable string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"({_X},{_Y})";
        }

        #region operator overrides

        /// <summary>
        ///     Subtract vector values
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector2D operator -(Vector2D vector, double value)
        {
            return new Vector2D(vector._X - value, vector._Y - value);
        }

        public static Vector2D operator -(Vector2D vector, float value)
        {
            return vector - (double) value;
        }

        public static Vector2D operator -(Vector2D vector, int value)
        {
            return vector - (double) value;
        }

        //
        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1._X - v2._X, v1._Y - v2._Y);
        }

        /// <summary>
        ///     Add vector values
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector2D operator +(Vector2D vector, double value)
        {
            return new Vector2D(vector._X + value, vector._Y + value);
        }

        public static Vector2D operator +(Vector2D vector, float value)
        {
            return vector + (double) value;
        }

        public static Vector2D operator +(Vector2D vector, int value)
        {
            return vector + (double) value;
        }

        //
        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1._X + v2._X, v1._Y + v2._Y);
        }

        /// <summary>
        ///     Multiply vector values
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector2D operator *(Vector2D vector, double scalar)
        {
            return new Vector2D(vector._X * scalar, vector._Y * scalar);
        }

        public static Vector2D operator *(Vector2D vector, float scalar)
        {
            return vector * (double) scalar;
        }

        public static Vector2D operator *(Vector2D vector, int scalar)
        {
            return vector * (double) scalar;
        }

        /// <summary>
        ///     Divide vector values
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector2D operator /(Vector2D vector, double scalar)
        {
            // Can't divide by zero
            if (scalar.Equals(0))
                return new Vector2D(vector._X, vector._Y);

            return new Vector2D(vector._X / scalar, vector._Y / scalar);
        }

        public static Vector2D operator /(Vector2D vector, float scalar)
        {
            return vector / (double) scalar;
        }

        public static Vector2D operator /(Vector2D vector, int scalar)
        {
            return vector / (double) scalar;
        }

        #endregion

        #region explicit operator cast

        /// <summary>
        ///     Cast to PointF
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static explicit operator PointF(Vector2D vector)
        {
            return new PointF((float) vector._X, (float) vector._Y);
        }

        /// <summary>
        ///     Cast to Point
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static explicit operator Point(Vector2D vector)
        {
            return new Point((int) vector._X, (int) vector._Y);
        }

        #endregion
    }
}