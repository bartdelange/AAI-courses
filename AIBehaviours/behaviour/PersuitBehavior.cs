using AIBehaviours.entity;
using AIBehaviours.util;

namespace AIBehaviours.behaviour
{
    internal class PersuitBehavior : SteeringBehaviour
    {
        public PersuitBehavior(MovingEntity self, MovingEntity target) : base(self, target)
        {
        }

        public override Vector2D Calculate()
        {
            Vector2D toEvader = Target
                .Pos
                .Clone()
                .Subtract(MovingEntity.Pos);

            double relativeHeading = MovingEntity.Heading.Dot(Target.Heading);

            if (
                toEvader.Dot(MovingEntity.Heading) > 0
                && relativeHeading < -0.95) // acot(0.95) = 18 deg;
            {
                System.Console.WriteLine("Ahead");

                // Seek
                return Seek(Target.Pos);
            }

            double lookAheadTime = toEvader.Length() / (MovingEntity.MaxSpeed + Target.Velocity.Length());

            Vector2D newPos = Target
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
