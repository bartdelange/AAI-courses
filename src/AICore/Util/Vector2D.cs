using System;
using System.Drawing;

namespace AICore.Util
{
    public class Vector2D
    {
        /// <summary>
        ///     Position on x (horizontal) axis
        /// </summary>
        public readonly double X;

        /// <summary>
        ///     Position on y (vertical) axis
        /// </summary>
        public readonly double Y;

        public Vector2D()
        {
        }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Length()
        {
            return Math.Sqrt(LengthSquared());
        }

        public double LengthSquared()
        {
            return X * X + Y * Y;
        }

        public double Dot(Vector2D target)
        {
            return X * target.X + Y * target.Y;
        }

        public Vector2D Normalize()
        {
            var length = Length();

            if (!(length > double.Epsilon))
                return this;

            return new Vector2D(X / length, Y / length);
        }

        public Vector2D Perpendicular()
        {
            return new Vector2D(-Y, X);
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
            return $"({X},{Y})";
        }


        public override bool Equals(object obj)
        {
            if (!(obj is Vector2D)) return false;

            return Math.Abs(((Vector2D) obj).X - X) < 0.0000001 && Math.Abs(((Vector2D) obj).Y - Y) < 0.0000001;
        }

        public override int GetHashCode()
        {
            return (X + Y).GetHashCode();
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
            return new Vector2D(vector.X - value, vector.Y - value);
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
            return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        /// <summary>
        ///     Add vector values
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector2D operator +(Vector2D vector, double value)
        {
            return new Vector2D(vector.X + value, vector.Y + value);
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
            return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        /// <summary>
        ///     Multiply vector values
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector2D operator *(Vector2D vector, double scalar)
        {
            return new Vector2D(vector.X * scalar, vector.Y * scalar);
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
                return new Vector2D(vector.X, vector.Y);

            return new Vector2D(vector.X / scalar, vector.Y / scalar);
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
            return new PointF((float) vector.X, (float) vector.Y);
        }

        /// <summary>
        ///     Cast to Point
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static explicit operator Point(Vector2D vector)
        {
            return new Point((int) vector.X, (int) vector.Y);
        }

        #endregion
    }
}