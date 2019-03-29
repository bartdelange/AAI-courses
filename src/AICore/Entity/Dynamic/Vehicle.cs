using System.Drawing;
using System.Numerics;
using AICore.Util;

namespace AICore.Entity.Dynamic
{
    public class Vehicle : MovingEntity
    {
        private readonly Brush _brush;

        public Vehicle(Vector2 position, Color? color = null) : base(position)
        {
            MaxSpeed = 50;
            Mass = 25;
            BoundingRadius = 15;
            
            _brush = new SolidBrush(color ?? Color.DodgerBlue);
        }

        public override void Render(Graphics g)
        {
            base.Render(g);

            var p1 = new Vector2(-8, 5);
            var p3 = new Vector2(5, 0);
            var p2 = new Vector2(-8, -5);

            var matrix = new Matrix3()
                .Rotate(Heading, SmoothHeading.Perpendicular())
                .Translate(Position);

            // Transform the vector to world space and create points that define polygon.	
            PointF[] curvePoints =
            {
                p1.ApplyMatrix(matrix).ToPointF(),
                p2.ApplyMatrix(matrix).ToPointF(),
                p3.ApplyMatrix(matrix).ToPointF()
            };

            g.FillPolygon(_brush, curvePoints);
        }
    }
}