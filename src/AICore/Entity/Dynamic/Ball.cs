using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Behaviour;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AICore.Entity.Dynamic
{
    public class Ball : MovingEntity
    {
        public IPlayer OwnedBy;
        
        private readonly IEnumerable<IWall> _walls;

        public Ball(Vector2 position, IEnumerable<IWall> walls) : base(position)
        {
            Position = position;

            Mass = Config.BallMass;
            MaxSpeed = Config.BallMaxSpeed;
            BoundingRadius = Config.BallRadius;

            // Walls are used to check for collision
            _walls = walls;

            Kick(new Vector2(.5f, 0), MaxSpeed);
        }

        public void Kick(Vector2 direction, float speed)
        {
            Velocity = (Vector2.Normalize(direction) * speed) / Mass;
        }

        public override void Update(float deltaTime)
        {
            const float squaredFriction = Config.BallFriction * Config.BallFriction;
            
            // Check for collisions, if ball has collision reflect velocity
            if (BallUtils.CheckForCollisions(this, _walls, out var normal))
            {
                Velocity = Vector2.Reflect(Velocity, normal);
            }
            
            // Simulate friction. Make sure the speed is positive
            if (Velocity.LengthSquared() <= squaredFriction)
            {
                return;
            }

            Velocity += Vector2.Normalize(Velocity) * Config.BallFriction;
            Position += Velocity;

            // Update heading
            Heading = Vector2.Normalize(Velocity);
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