using AICore.Entity;
using AICore.Util;

namespace AICore.Behaviour.Individual
{
    public class PursuitBehaviour : SteeringBehaviour
    {
        public PursuitBehaviour(MovingEntity movingEntity, MovingEntity target, double weight)
            : base(movingEntity, target, weight)
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