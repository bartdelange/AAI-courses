using System;

namespace AIBehaviours.util
{
    public class Vector2D
    {
        public Vector2D() : this(0, 0)
        {
        }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double Length()
        {
            return Math.Sqrt(LengthSquared());
        }

        public double LengthSquared()
        {
            return Math.Pow(X, 2) + Math.Pow(Y, 2);
        }

        public double Dot(Vector2D target)
        {
            return X * target.X + Y * target.Y;
        }

        public Vector2D Add(Vector2D v)
        {
            X += v.X;
            Y += v.Y;

            return this;
        }

        public Vector2D Subtract(Vector2D v)
        {
            X -= v.X;
            Y -= v.Y;

            return this;
        }

        public Vector2D Multiply(double value)
        {
            X *= value;
            Y *= value;

            return this;
        }

        public Vector2D Divide(double value)
        {
            // Can't divide by zero
            if (value == 0) return this;

            X /= value;
            Y /= value;

            return this;
        }

        public Vector2D Normalize()
        {
            var length = Length();
            Divide(length);

            return this;
        }

        public Vector2D Truncate(double max)
        {
            if (Length() > max)
            {
                Normalize();
                Multiply(max);
            }

            return this;
        }

        public Vector2D Clone()
        {
            return new Vector2D(X, Y);
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}