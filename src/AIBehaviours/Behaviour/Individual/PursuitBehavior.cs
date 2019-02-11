using AIBehaviours.Entity;
using AIBehaviours.Util;

namespace AIBehaviours.Behaviour.Individual
{
    internal class PursuitBehavior : SteeringBehaviour
    {
        public PursuitBehavior(MovingEntity self, MovingEntity target) : base(self, target)
        {
        }

        public override Vector2D Calculate(float deltaTime)
        {
            var toEvader = Target.Pos - MovingEntity.Pos;

            var lookAheadTime = toEvader.Length();

            // Add turn around time
            const double coefficient = 0.5;
            lookAheadTime += (MovingEntity.Heading.Dot(toEvader.Normalize()) - 1) * -coefficient;

            // Seek to predicted position
            return Seek(Target.Pos + MovingEntity.Velocity * lookAheadTime);
        }

        private Vector2D Seek(Vector2D targetPosition)
        {
            return (targetPosition - MovingEntity.Pos).Normalize() *
                   MovingEntity.MaxSpeed -
                   MovingEntity.Velocity;
        }
    }
}