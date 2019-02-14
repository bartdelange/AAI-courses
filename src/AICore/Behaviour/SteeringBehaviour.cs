using System.Drawing;
using AICore.Entity;
using AICore.Util;

namespace AICore.Behaviour
{
    public abstract class SteeringBehaviour
    {
        public double Weight = 100;

        protected SteeringBehaviour(MovingEntity movingEntity, MovingEntity target, double weight)
        {
            Weight = weight;

            MovingEntity = movingEntity;
            Target = target;
        }

        public MovingEntity MovingEntity { get; }
        public MovingEntity Target { get; }

        public abstract Vector2D Calculate(float deltaTime);

        public virtual void Render(Graphics g)
        {
        }
    }
}