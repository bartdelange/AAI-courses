using System.Drawing;
using System.Numerics;
using AICore.Util;

namespace AICore.Entity
{
    public class Vehicle : MovingEntity
    {
        private readonly Pen _objectPen = new Pen(Color.Black, 2);
        private readonly Pen _velocityPen = new Pen(Color.Red, 2);

        public Vehicle(Vector2 pos, World w) : base(pos, w)
        {
            Velocity = new Vector2(0, 0);
            Scale = 1;
        }

        public Vehicle(Vector2 pos, Color color, World w) : this(pos, w)
        {
            _objectPen = new Pen(color, 2);
        }


        public override void Render(Graphics g)
        {
            // Draw velocity	
            g.DrawLine(_velocityPen,
                Pos.ToPointF(),
                (Pos + Velocity).ToPointF()
            );

            var p1 = new Vector2(-8, 5);
            var p3 = new Vector2(5, 0);
            var p2 = new Vector2(-8, -5);

            var matrix = new Matrix();

            matrix.Rotate(Heading, Side);
            matrix.Translate(Pos.X, Pos.Y);

            // Transform the vector to world space	
            p1 = matrix.TransformVector2s(p1);
            p2 = matrix.TransformVector2s(p2);
            p3 = matrix.TransformVector2s(p3);

            // Create points that define polygon.	
            PointF[] curvePoints =
            {
                p1.ToPointF(),
                p2.ToPointF(),
                p3.ToPointF()
            };

            g.DrawPolygon(_objectPen, curvePoints);
        }

        public override string ToString()
        {
            return $"{_objectPen.Color}";
        }
    }
}