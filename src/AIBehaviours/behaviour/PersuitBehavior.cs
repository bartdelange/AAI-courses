using AIBehaviours.entity;
using AIBehaviours.util;
using System;

namespace AIBehaviours.behaviour
{
    internal class PersuitBehavior : SteeringBehaviour
    {
        public PersuitBehavior(MovingEntity self, MovingEntity target) : base(self, target)
        {
        }

        public override Vector2D Calculate(float deltaTime)
        {
            var toEvader = Target
                .Pos
                .Clone()
                .Subtract(MovingEntity.Pos);

            var relativeHeading = MovingEntity.Heading.Dot(Target.Heading);

            if (
                toEvader.Dot(MovingEntity.Heading) > 0
                && relativeHeading < -0.95) // acot(0.95) = 18 deg;
            {
                Console.WriteLine("Ahead");

                // Seek
                return Seek(Target.Pos);
            }

            var lookAheadTime = toEvader.Length() / (MovingEntity.MaxSpeed + Target.Velocity.Length());

            var newPos = Target
                .Pos
                .Clone()
                .Add(
                    MovingEntity.Velocity.Multiply(lookAheadTime)
                );

            // Seek to predicted position
            return Seek(newPos);
        }

        private Vector2D Seek(Vector2D targetPosition)
        {
            return MovingEntity
                .Pos
                .Clone()
                .Add(targetPosition)
                .Normalize()
                .Multiply(MovingEntity.MaxSpeed);
        }
    }
}
