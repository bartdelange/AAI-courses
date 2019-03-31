using System.Drawing;
using System.Numerics;

namespace AICore.Model
{
    public class Bounds
    {
        public Vector2 Min { get; }
        public Vector2 Max { get; }

        public Bounds(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }

        public Bounds(Vector2 min, Size max)
        {
            Min = min;
            Max = new Vector2(max.Width, max.Height);
        }

        /// <summary>
        /// Subtract vector from bounds
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Bounds operator -(Bounds bounds, Vector2 v)
        {
            return new Bounds(bounds.Min + v, bounds.Max - v);
        }

        /// <summary>
        /// Cast operator overload that creates a rectangle for given bounds
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public static explicit operator Rectangle(Bounds bounds)
        {
            var size = bounds.Max - bounds.Min;

            return new Rectangle(
                (int) bounds.Min.X,
                (int) bounds.Min.Y,
                (int) size.X,
                (int) size.Y
            );
        }
    }

    public static class BoundsExtensionMethods
    {
        public static Vector2 Center(this Bounds bounds)
        {
            return (bounds.Max - bounds.Min) / 2 + bounds.Min;
        }
    }
}