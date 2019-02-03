using AIBehaviours.util;
using System;

namespace AIBehaviours.behaviour
{
    internal class WanderBehaviour : SteeringBehaviour
    {
        private const double WanderRadius = 25;

        private const double WanderDistance = WanderRadius + 25;

        private const double WanderJitter = 10;

        private static Random random = new Random();

        public WanderBehaviour(entity.MovingEntity movingEntity, entity.MovingEntity target) : base(movingEntity, target)
        {
        }

        public override Vector2D Calculate()
        {
            Vector2D wanderTarget = new Vector2D(
                RandomClamped() * WanderJitter,
                RandomClamped() * WanderJitter
            )
                .Normalize()
                .Multiply(WanderRadius);

            Vector2D localTarget = new Vector2D(WanderDistance, 0).Add(wanderTarget);
        }

        private double RandomClamped()
        {
            return random.NextDouble() * 2 - 1;
        }
    }
}
