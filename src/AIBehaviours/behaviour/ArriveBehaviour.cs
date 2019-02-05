using AIBehaviours.entity;
using AIBehaviours.util;
using System;

namespace AIBehaviours.behaviour
{
    internal class ArriveBehaviour : SteeringBehaviour
    {
        private const double DecelerationSpeed = 1;

        public ArriveBehaviour(MovingEntity self, MovingEntity target) : base(self, target)
        {
        }

        public override Vector2D Calculate(float deltaTime)
        {
            var toTarget = Target
                .Pos
                .Clone()
                .Subtract(MovingEntity.Pos);

            var distance = toTarget.Length();

            if (distance <= 0)
            {
                return new Vector2D(0, 0);
            }

            var speed = Math.Min(distance / DecelerationSpeed, MovingEntity.MaxSpeed);

            return toTarget
                .Multiply(speed)
                .Divide(distance)
                .Subtract(MovingEntity.Velocity);
        }
    }
}
