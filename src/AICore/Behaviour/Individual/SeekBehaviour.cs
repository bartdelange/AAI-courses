using AICore.Entity;
using AICore.Util;

namespace AICore.Behaviour.Individual
{
    public class SeekBehaviour : SteeringBehaviour
    {
        public SeekBehaviour(MovingEntity movingEntity, MovingEntity target, double weight)
            : base(movingEntity, target, weight)
        {
        }

        /// <summary>
        ///     Set a velocity that will make the agent move the world target
        /// </summary>
        public override Vector2D Calculate(float deltaTime)
        {
            return (Target.Pos - MovingEntity.Pos).Normalize() * MovingEntity.MaxSpeed;
        }
    }
}