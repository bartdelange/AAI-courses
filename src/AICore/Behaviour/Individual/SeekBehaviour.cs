using System.Numerics;
using AICore.Entity;

namespace AICore.Behaviour.Individual
{
    public class SeekBehaviour : SteeringBehaviour
    {
        public SeekBehaviour(MovingEntity movingEntity, MovingEntity target, float weight)
            : base(movingEntity, target, weight)
        {
        }

        /// <summary>
        ///     Set a velocity that will make the agent move the world target
        /// </summary>
        public override Vector2 Calculate(float deltaTime)
        {
            return Vector2.Normalize(Target.Pos - MovingEntity.Pos) * MovingEntity.MaxSpeed;
        }
    }
}