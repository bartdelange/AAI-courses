using AIBehaviours.entity;
using AIBehaviours.util;

namespace AIBehaviours.behaviour
{
    internal class SeekBehaviour : SteeringBehaviour
    {
        public SeekBehaviour(MovingEntity self, MovingEntity target) : base(self, target)
        {
        }

        /// <summary>
        /// Set a velocity that will make the agent move the world target
        /// </summary>
        public override Vector2D Calculate()
        {
            return Target
                .Pos
                .Clone()
                .Subtract(MovingEntity.Pos)
                .Normalize()
                .Multiply(MovingEntity.MaxSpeed);
        }
    }
}
