using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Model;
using AICore.Shapes;
using AICore.Util;
using AICore.Worlds;

namespace AICore.Entity.Dynamic
{
    public class Ball : MovingEntity, ICircle
    {
        private readonly SoccerField _soccerField;
        private readonly Brush _ballBrush;

        /// <summary>
        /// Player that is currently in possession of the ball
        /// </summary>
        public IPlayer Owner;
        
        public Ball(Vector2 position, SoccerField soccerField) : base(position)
        {
            Position = position;

            Mass = Config.BallMass;
            MaxSpeed = Config.BallMaxSpeed;
            BoundingRadius = Config.BallRadius;

            // Walls are used to check for collision
            _soccerField = soccerField;
            _ballBrush = Brushes.Black;
        }

        public void Kick(IPlayer player, float speed)
        {
            // Cannot kick the ball when not in possession of the ball
            if (player != Owner)
            {
                return;
            }
            
            // Set velocity of ball by using player heading and given speed
            Velocity = (Vector2.Normalize(Position - player.Position) * speed / Mass).Truncate(MaxSpeed);
            Owner = null;
        }

        public void TakeBall(IPlayer player)
        {
            var interceptDistance = Math.Pow(player.BoundingRadius * 2, 2) + Math.Pow(BoundingRadius, 2);
            var distance = Vector2.DistanceSquared(Position, player.Position);

            if (distance < interceptDistance)
            {
                Owner = player;
            }
        }

        public override void Update(float deltaTime)
        {
            CheckGoalCollision();

            // Ball should follow owner when it has one
            if (Owner != null)
            {
                const float margin = 15;
                
                // Should update it to be in front of the player
                Position = Owner.Position + Owner.Heading * (Owner.BoundingRadius + BoundingRadius + margin);
                return;
            }
            
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

            HandleGoal(affectedTeam);
        }

        private void HandleGoal(Team affectedTeam)
        {
            affectedTeam.Goal.Score -= 1;

            _soccerField.Reset();
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