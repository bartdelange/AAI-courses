using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AICore.Entity
{
    public class Circle : IObstacle
    {
        public bool Visible { get; set; } = true;

        public Vector2 Position { get; set; }
        public int BoundingRadius { get; set; }

        public const int MinRadius = 10;
        public const int MaxRadius = 100;

        public Circle(Vector2 position, int radius)
        {
            Position = position;
            BoundingRadius = radius;
        }

        public void Render(Graphics g)
        {
            var ellipse = new Rectangle(
                (int) (Position.X - BoundingRadius),
                (int) (Position.Y - BoundingRadius),
                BoundingRadius * 2,
                BoundingRadius * 2
            );

            g.FillEllipse(
                new SolidBrush(Color.FromArgb(50, Color.Black)),
                ellipse
            );

            g.FillEllipse(
                Brushes.Red,
                new Rectangle((int) Position.X - 2, (int) Position.Y - 2, 5, 5)
            );
        }

        public bool IntersectsWithLine(Vector2 lineStart, Vector2 lineEnd, int margin = 0)
        {
            var circleCenter = new Vector2(Position.X, Position.Y);

            var squaredRadius = BoundingRadius * BoundingRadius;
            var squaredMargin = margin * margin;

            return Vector2ExtensionMethods.SquaredDistanceToLine(lineStart, lineEnd, circleCenter) <
                   squaredRadius + squaredMargin;
        }

        /// <summary>
        /// Compare radius of circle with distance of its center from given point
        /// </summary>
        /// <param name="point"></param>
        /// <param name="margin"></param>
        /// <returns></returns>
        public bool IntersectsWithPoint(Vector2 point, int margin = 0)
        {
            var circleCenter = new Vector2(Position.X, Position.Y);

            var distanceToCircleCenter = (point.X - circleCenter.X) * (point.X - circleCenter.X) +
                                         (point.Y - circleCenter.Y) * (point.Y - circleCenter.Y);

            var squaredRadius = BoundingRadius * BoundingRadius;
            var squaredMargin = margin * margin;

            return distanceToCircleCenter <= squaredRadius + squaredMargin;
        }
    }
}