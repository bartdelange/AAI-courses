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
            return (X * X) + (Y * Y);
        }

        public double Dot(Vector2D target)
        {
            return (X * target.X) + (Y * target.Y);
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

            if (!(length > double.Epsilon)) return this;
            
            X /= length;
            Y /= length;

            return this;
        }

        public Vector2D Perpendicular()
        {
            return new Vector2D(-Y, X);
        }

        public Vector2D Truncate(double max)
        {
            if (!(Length() > max)) return this;
            
            Normalize();
            Multiply(max);

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
