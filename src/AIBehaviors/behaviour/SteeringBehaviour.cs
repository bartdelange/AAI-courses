using System.Drawing;
using AIBehaviors.Entity;
using AIBehaviors.Util;

namespace AIBehaviors.Behaviour
{
    public abstract class SteeringBehaviour
    {
        protected SteeringBehaviour(MovingEntity movingEntity, MovingEntity target)
        {
            MovingEntity = movingEntity;
            Target = target;
        }

        public MovingEntity MovingEntity { get; set; }

        public MovingEntity Target { get; set; }

        public abstract Vector2D Calculate(float deltaTime);

        public virtual void Render(Graphics g)
        {
        }
    }
}