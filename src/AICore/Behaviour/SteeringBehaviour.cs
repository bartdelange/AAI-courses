using System;
using System.Drawing;
using System.Numerics;
using AICore.Entity;

namespace AICore.Behaviour
{
    public abstract class SteeringBehaviour
    {
        protected MovingEntity MovingEntity { get; }
        
        [Obsolete("Target is deprecated, the target should be defined by using the constructor of the behaviour that needs it")]
        protected MovingEntity Target { get; }

        public float Weight { get; }

        protected SteeringBehaviour(MovingEntity movingEntity, float weight)
        {
            Weight = weight;
            MovingEntity = movingEntity;
        }
        
        // TODO Remove targetEntity dependency from base steering behaviour 
        protected SteeringBehaviour(MovingEntity movingEntity, MovingEntity target, float weight) 
            : this(movingEntity, weight)
        {
            Target = target;
        }

        public abstract Vector2 Calculate(float deltaTime);

        public abstract void Render(Graphics g);
    }
}