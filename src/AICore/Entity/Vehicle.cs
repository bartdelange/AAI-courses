using System.Drawing;
using AIBehaviours.Util;

namespace AIBehaviours.Entity
{
    public class Vehicle : MovingEntity
    {
        private readonly Pen _objectPen = new Pen(Color.Black, 2);
        private readonly Pen _velocityPen = new Pen(Color.Red, 2);

        public Vehicle(Vector2D pos, World w) : base(pos, w)
        {
            Velocity = new Vector2D(0, 0);
            Scale = 1;
        }

        public Vehicle(Vector2D pos, Color color, World w) : this(pos, w)
        {
            _objectPen = new Pen(color, 2);
        }


        public override void Render(Graphics g)
        {
            // Draw velocity	
            g.DrawLine(_velocityPen,
                (PointF) Pos,
                (PointF) (Pos + Velocity)
            );

            var p1 = new Vector2D(-8, 5);
            var p3 = new Vector2D(5, 0);
            var p2 = new Vector2D(-8, -5);

            var matrix = new Matrix();

            matrix.Rotate(Heading, Side);
            matrix.Translate(Pos._X, Pos._Y);

            // Transform the vector to world space	
            p1 = matrix.TransformVector2Ds(p1);
            p2 = matrix.TransformVector2Ds(p2);
            p3 = matrix.TransformVector2Ds(p3);

            // Create points that define polygon.	
            PointF[] curvePoints =
            {
                (PointF) p1,
                (PointF) p2,
                (PointF) p3
            };

            g.DrawPolygon(_objectPen, curvePoints);
        }

        public override string ToString()
        {
            return $"{_objectPen.Color}";
        }
    }
}