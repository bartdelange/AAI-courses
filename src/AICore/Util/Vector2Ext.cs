using System.Drawing;
using System.Numerics;

namespace AICore.Util
{
    public static class Vector2Ext
    {
        public static PointF ToPointF(this Vector2 v)
        {
            return new PointF((int) v.X, (int) v.Y);
        }
        
        public static Point ToPoint(this Vector2 v)
        {
            return new Point((int) v.X, (int) v.Y);
        }

        public static Vector2 Minus(this Vector2 v, float value)
        {
            return v - new Vector2(value);
        }
        
        public static Vector2 Perpendicular(this Vector2 v)
        {
            return new Vector2(-v.Y, v.X);
        }

        public static Vector2 ApplyMatrix(this Vector2 vector2, Matrix3 matrix)
        {
            var x = matrix.P11 * vector2.X + matrix.P21 * vector2.Y + matrix.P13;
            var y = matrix.P12 * vector2.X + matrix.P22 * vector2.Y + matrix.P23;

            return new Vector2(x, y);
        }
    }
}