﻿using AICore.Entity;
using AICore.Util;

namespace AICore.Behaviour.Individual
{
    public class FleeBehaviour : SteeringBehaviour
    {
        private const int Boundary = 100 * 100;

        public FleeBehaviour(MovingEntity movingEntity, MovingEntity target, double weight)
            : base(movingEntity, target, weight)
        {
        }

        public override Vector2D Calculate(float deltaTime)
        {
            var distance = (MovingEntity.Pos - Target.Pos).LengthSquared();

            // Only flee if the target is within 'panic distance'. Work in distance squared space.
            if (distance > Boundary) return new Vector2D();

            return (MovingEntity.Pos - Target.Pos).Normalize() * MovingEntity.MaxSpeed - MovingEntity.Velocity;
        }
    }
}