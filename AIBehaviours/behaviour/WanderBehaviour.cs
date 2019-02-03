using System;
using AIBehaviours.entity;
using AIBehaviours.util;

namespace AIBehaviours.behaviour
{
    internal class WanderBehaviour : SteeringBehaviour
    {
        private const double WanderRadius = 50;

        private const double WanderDistance = 20;

        private const double WanderJitter = 10;

        private static readonly Random random = new Random();

        public WanderBehaviour(MovingEntity movingEntity, MovingEntity target) : base(movingEntity, target)
        {
        }

        public override Vector2D Calculate()
        {
            var wanderTarget = new Vector2D(
                    RandomClamped() * WanderJitter,
                    RandomClamped() * WanderJitter
                )
                .Normalize()
                .Multiply(WanderRadius);

            var localTarget = new Vector2D(WanderDistance, 0).Add(wanderTarget);
            return localTarget;
        }


        private double RandomClamped()
        {
            return random.NextDouble() * 2 - 1;
        }
    }
}