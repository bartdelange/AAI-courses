using AIBehaviours.Entity;
using AIBehaviours.Util;

namespace AIBehaviours.Behaviour.Individual
{
    internal class SeekBehaviour : SteeringBehaviour
    {
        public SeekBehaviour(MovingEntity self, MovingEntity target) : base(self, target)
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