using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AICore.Entity.Static
{
    public class Wall : IWall
    {
        public bool Visible { get; set; } = true;

        public Vector2 Position { get; set; }
        public Vector2 Normal { get; set; }

        public int BoundingRadius { get; set; }

        // Render properties
        private readonly Pen _wallPen = new Pen(Color.FromArgb(80, Color.Black), 1);

        // Wall properties
        private readonly Vector2 _startPosition;
        private readonly Vector2 _endPosition;

        public Wall(Vector2 startPosition, Vector2 endPosition)
        {
            var width = (endPosition - startPosition);


            Position = (width / 2) + startPosition;
            Normal = Vector2.Normalize(width).Perpendicular();

            _startPosition = startPosition;
            _endPosition = endPosition;
        }

        /// <summary>
        /// Checks whether the given line intersects with the wall. when it does the distance and intersectPoint refs are updated
        /// 
        /// Code from book, there's no explanation. Check this webpage for explanation of this code
        /// https://www.geeksforgeeks.org/check-if-two-given-line-segments-intersect/
        /// </summary>
        /// <param name="start"></param>
        /// <param name="target"></param>
        /// <param name="distance"></param>
        /// <param name="intersectPoint"></param>
        /// <returns></returns>
        public bool IntersectsWithLine(Vector2 start, Vector2 target, out double? distance, out Vector2? intersectPoint)
        {
            double rTop = (start.Y - _startPosition.Y) * (_endPosition.X - _startPosition.X) -
                          (start.X - _startPosition.X) * (_endPosition.Y - _startPosition.Y);

            double sTop = (start.Y - _startPosition.Y) * (target.X - start.X) -
                          (start.X - _startPosition.X) * (target.Y - start.Y);

            double bot = (target.X - start.X) * (_endPosition.Y - _startPosition.Y) -
                         (target.Y - start.Y) * (_endPosition.X - _startPosition.X);

            if (bot == 0) // Lines are parallel
            {
                distance = null;
                intersectPoint = null;

                return false;
            }

            var r = rTop / bot;
            var s = sTop / bot;

            // If true lines intersect
            if (!(r > 0) || !(r < 1) || !(s > 0) || !(s < 1))
            {
                distance = null;
                intersectPoint = null;

                return false;
            }

            distance = Vector2.Distance(start, target) * r;
            intersectPoint = (float) r * (target - start) + start;

            return true;
        }

        /// <summary>
        /// Checks whether the given line intersects with the wall.
        /// 
        /// Code from book, there's no explanation. Check this webpage for explanation of this code
        /// https://www.geeksforgeeks.org/check-if-two-given-line-segments-intersect/
        /// </summary>
        /// <param name="start"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool IntersectsWithLine(Vector2 start, Vector2 target)
        {
            double rTop = (start.Y - _startPosition.Y) * (_endPosition.X - _startPosition.X) -
                          (start.X - _startPosition.X) * (_endPosition.Y - _startPosition.Y);

            double sTop = (start.Y - _startPosition.Y) * (target.X - start.X) -
                          (start.X - _startPosition.X) * (target.Y - start.Y);

            double bot = (target.X - start.X) * (_endPosition.Y - _startPosition.Y) -
                         (target.Y - start.Y) * (_endPosition.X - _startPosition.X);

            if (bot == 0) // Lines are parallel
            {
                return false;
            }

            var r = rTop / bot;
            var s = sTop / bot;

            // If true lines intersect
            return (r > 0) && (r < 1) && (s > 0) && (s < 1);
        }

        /// <summary>
        /// Draws the wall as a line
        /// </summary>
        /// <param name="graphics"></param>
        public void Render(Graphics graphics)
        {
            if (Config.Debug)
            {
                // Draw normal
                graphics.DrawLine(
                    Pens.Red,
                    Position.ToPoint(),
                    (Position + Normal * 25).ToPoint()
                );

                // Draw wall position
                graphics.FillEllipse(
                    Brushes.Red,
                    new Rectangle((int) Position.X, (int) Position.Y, 5, 5)
                );
            }

            graphics.DrawLine(
                _wallPen,
                _startPosition.ToPoint(),
                _endPosition.ToPoint()
            );
        }
    }
}