using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Shapes;
using AICore.Util;
using AICore.Worlds;

namespace AICore.Entity.Dynamic
{
    public class Ball : MovingEntity, ICircle
    {
        public IPlayer OwnedBy;
        
        private readonly SoccerField _soccerField;

        public Ball(Vector2 position, SoccerField soccerField) : base(position)
        {
            Position = position;

            Mass = Config.BallMass;
            MaxSpeed = Config.BallMaxSpeed;
            BoundingRadius = Config.BallRadius;

            // Walls are used to check for collision
            _soccerField = soccerField;
        }

        public void Kick(IPlayer player, float speed)
        {
            // Can't kick the ball when player is too far away
            var distanceToBall = Vector2.DistanceSquared(player.Position, Position);

            const float ballWithinRange = 50 * 50;
            if (distanceToBall > ballWithinRange) return;
            
            // Set velocity of ball by using player heading and given speed
            Velocity = (Vector2.Normalize(Position - player.Position) * speed / Mass).Truncate(MaxSpeed);
        }

        public override void Update(float deltaTime)
        {
            CheckGoalCollision();
            
            const float squaredFriction = Config.BallFriction * Config.BallFriction;
            
            // Check for collisions, if ball has collision reflect velocity
            if (BallUtils.CheckForCollisions(this, _soccerField.Sidelines, out var normal))
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

        private void CheckGoalCollision()
        {
            var affectedTeam = _soccerField.Teams.Find(team =>
                this.PolyIntersectsWithCircle(team.Goal.Polygons,0)
            );

            // No collision happened, do nothing
            if (affectedTeam == null)
            {
                return;
            }

            affectedTeam.Goal.Score -= 1;
        }
        
        public IPlayer FindClosestPlayer(List<IPlayer> players)
        {
            var smallestDistance = float.MaxValue;

            IPlayer closestPlayer = null;
            
            players.ForEach(player =>
            {
                var distance = Vector2.DistanceSquared(player.Position, Position);

                if (!(distance < smallestDistance)) return;
                
                closestPlayer = player;
                smallestDistance = distance;
            });

            return closestPlayer;
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