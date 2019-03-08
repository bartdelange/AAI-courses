#region

using System.Drawing;
using System.Numerics;
using AICore.Entity;

#endregion

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

            var seekBehaviour = new SeekBehaviour(
                MovingEntity,
                Target.Pos + MovingEntity.Velocity * lookAheadTime,
                Weight
            );

            // Seek to predicted position
            return seekBehaviour.Calculate(deltaTime);
        }

        public override void Render(Graphics g)
        {
        }
    }
}