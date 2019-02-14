using System.Drawing;
using AIBehaviours.Entity;
using AIBehaviours.Util;

namespace AIBehaviours.Behaviour
{
    public abstract class SteeringBehaviour
    {
        public double Weight = 100;

        public MovingEntity MovingEntity { get; }
        public MovingEntity Target { get; }

        protected SteeringBehaviour(MovingEntity movingEntity, MovingEntity target, double weight)
        {
            Weight = weight;

            MovingEntity = movingEntity;
            Target = target;
        }

        public abstract Vector2D Calculate(float deltaTime);

        public virtual void Render(Graphics g)
        {
        }
    }
}