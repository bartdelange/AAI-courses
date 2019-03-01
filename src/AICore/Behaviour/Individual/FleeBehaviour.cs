using System.Numerics;
using AICore.Entity;

namespace AICore.Behaviour.Individual
{
    public class FleeBehaviour : SteeringBehaviour
    {
        private const int Boundary = 100 * 100;

        public FleeBehaviour(MovingEntity movingEntity, MovingEntity target, float weight)
            : base(movingEntity, target, weight)
        {
        }

        public override Vector2 Calculate(float deltaTime)
        {
            var distance = (MovingEntity.Pos - Target.Pos).LengthSquared();

            // Only flee if the target is within 'panic distance'. Work in distance squared space.
            if (distance > Boundary) return new Vector2();

            return Vector2.Normalize(MovingEntity.Pos - Target.Pos) * MovingEntity.MaxSpeed - MovingEntity.Velocity;
        }
    }
}