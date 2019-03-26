using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.Entity
{
    public class Ball : MovingEntity
    {
        public Ball(Vector2 position, int ballSize = 10) : base(position)
        {
            Position = position;
            BoundingRadius = ballSize;
        }

        public void Kicked(Vector2 heading)
        {
            Heading = heading;
            CurrentSpeed = MaxSpeed;
        }

        public override void Render(Graphics graphics)
        {
            base.Render(graphics);
            
            graphics.FillEllipse(
                Brushes.CadetBlue,
                Position.X - (BoundingRadius),
                Position.Y - (BoundingRadius),
                BoundingRadius * 2,
                BoundingRadius * 2
            );
        }
    }
}