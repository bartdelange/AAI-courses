using System.Drawing;
using System.Numerics;
using AICore.Util;

namespace AICore.Entity
{
    public class Vehicle : MovingEntity
    {
        private readonly Pen _pen;

        public Vehicle(Vector2 position, Vector2 bounds) :
            this(position, bounds, new Pen(Color.DodgerBlue, 2))
        {
        }

        public Vehicle(Vector2 position, Vector2 bounds, Pen pen) : base(position, bounds, pen)
        {
            _pen = pen;
        }

        public override void Render(Graphics g)
        {
            base.Render(g);

            var p1 = new Vector2(-8, 5);
            var p3 = new Vector2(5, 0);
            var p2 = new Vector2(-8, -5);

            var matrix = new Matrix3()
                .Rotate(SmoothHeading, SmoothHeading.Perpendicular())
                .Translate(Position);

            // Transform the vector to world space and create points that define polygon.	
            PointF[] curvePoints =
            {
                p1.ApplyMatrix(matrix).ToPointF(),
                p2.ApplyMatrix(matrix).ToPointF(),
                p3.ApplyMatrix(matrix).ToPointF()
            };

            g.DrawPolygon(_pen, curvePoints);
        }

        public override string ToString()
        {
            return $"{_pen.Color} {base.ToString()}";
        }
    }
}