using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Util;

namespace AICore.Behaviour
{
    public abstract class SteeringBehaviour
    {
        public float Weight = 100;

        protected SteeringBehaviour(MovingEntity movingEntity, MovingEntity target, float weight)
        {
            Weight = weight;

            MovingEntity = movingEntity;
            Target = target;
        }

        public MovingEntity MovingEntity { get; }
        public MovingEntity Target { get; }

        public abstract Vector2 Calculate(float deltaTime);

        public virtual void Render(Graphics g)
        {
        }
    }
}