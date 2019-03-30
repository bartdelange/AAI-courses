using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour.Individual;
using AICore.SteeringBehaviour.Util;

namespace AICore.SteeringBehaviour.Aggregate
{
    public class WanderNearPositionBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; }

        private readonly WeightedTruncatedRunningSumWithPrioritization _steeringBehaviour;

        public WanderNearPositionBehaviour(IPlayer playerEntity)
        {
            var steeringBehaviours = new List<WeightedSteeringBehaviour>
            {
                new WeightedSteeringBehaviour(new SeekBehaviour(playerEntity, playerEntity.StartPosition), 5f),
                new WeightedSteeringBehaviour(new WanderBehaviour(playerEntity), 1f)
            };

            _steeringBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                steeringBehaviours,
                playerEntity.MaxSpeed
            );
        }

        public Vector2 Calculate(float deltaTime)
        {
            return _steeringBehaviour.Calculate(deltaTime);
        }

        public void Render(Graphics graphics)
        {
        }
    }
}