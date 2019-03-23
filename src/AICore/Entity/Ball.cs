using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.Entity
{
    public class Ball : IEntity
    {
        public bool Visible { get; set; }

        public Vector2 Position { get; set; }
        public int BoundingRadius { get; set; }

        public Ball(Vector2 position, int ballSize = 20)
        {
            Position = position;
            BoundingRadius = ballSize;
        }

        public void Render(Graphics graphics)
        {
            graphics.FillEllipse(
                Brushes.CadetBlue,
                Position.X - BoundingRadius,
                Position.Y - BoundingRadius,
                BoundingRadius * 2,
                BoundingRadius * 2
            );
        }
    }
}