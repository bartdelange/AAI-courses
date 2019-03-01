using System.Numerics;
using AICore.Entity;

namespace AICore.Behaviour.Individual
{
    public class PursuitBehaviour : SteeringBehaviour
    {
        public PursuitBehaviour(MovingEntity movingEntity, MovingEntity target, float weight)
            : base(movingEntity, target, weight)
        {
        }

        public override Vector2 Calculate(float deltaTime)
        {
            var toEvader = Target.Pos - MovingEntity.Pos;

            var lookAheadTime = toEvader.Length();

            // Add turn around time
            const float coefficient = 0.5f;
            lookAheadTime += (Vector2.Dot(MovingEntity.Heading, Vector2.Normalize(toEvader)) - 1) * -coefficient;

            // Seek to predicted position
            return Seek(Target.Pos + MovingEntity.Velocity * lookAheadTime);
        }

        private Vector2 Seek(Vector2 targetPosition)
        {
            return Vector2.Normalize(targetPosition - MovingEntity.Pos) *
                   MovingEntity.MaxSpeed -
                   MovingEntity.Velocity;
        }
    }
}