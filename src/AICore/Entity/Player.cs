using System;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.Entity
{
    public class Player : MovingEntity, IPlayer
    {        
        public Vector2 StartPosition { get; }
        public DateTime Attempt { get; set; } = DateTime.Now;

        public IMovingEntity BallEntity { get; set; }

        public float MaxEnergy { get; set; } = 50;
        public float MinEnergy { get; set; } = 10;   
        public string TeamName { get; set; } 


        public float Energy { get; set; }

        // Render properties
        private Brush _brush;

        public Player(Vector2 position, string teamName, Color? color = null) : base(position)
        {
            StartPosition = position;
            TeamName = teamName;

            _brush = new SolidBrush(color ?? Color.Black);
        }

        public override void Render(Graphics graphics)
        {
            base.Render(graphics);

            graphics.FillEllipse(
                _brush,
                Position.X - BoundingRadius,
                Position.Y - BoundingRadius,
                BoundingRadius * 2,
                BoundingRadius * 2
            );
        }

        public void KickBall(Ball ball, Vector2 position)
        {
            ball.Kicked(Vector2.Normalize(position));
        }

        public void Dribble(Ball ball)
        {
            // Might need to fix this equals check
            if (ball.OwnedBy == this)
            {
                // Should update it to be in front of the player
                ball.Position = Heading * ball.BoundingRadius + Position;
            }
        }

        public void Steal(Ball ball)
        {
            var random = new Random();
            
            // Might need to fix this equals check
            if (ball.OwnedBy == this) return;

            // If we do not add an attempt check it will get it every time (as this is parsed in update / every game tick)
            if (random.NextDouble() > 0.5d && Attempt.AddSeconds(5) <= DateTime.Now)
            {
                ball.OwnedBy = this;
            }
            
            Attempt = DateTime.Now;
        }
    }
}