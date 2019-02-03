using AIBehaviours.entity;
using AIBehaviours.util;

namespace AIBehaviours.behaviour
{
    internal abstract class SteeringBehaviour
    {
        public MovingEntity MovingEntity { get; set; }

        public MovingEntity Target { get; set; }

        public abstract Vector2D Calculate();

        public SteeringBehaviour(MovingEntity movingEntity, MovingEntity target)
        {
            MovingEntity = movingEntity;
            Target = target;
        }
    }
}
