using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Behaviour.Individual;
using AICore.Behaviour.Util;
using AICore.Entity.Contracts;

namespace AICore.Behaviour.Aggregate
{
    public class WanderWallAvoidanceBehaviour: ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly WeightedTruncatedRunningSumWithPrioritization _aggregateBehaviour;

        public WanderWallAvoidanceBehaviour(IMovingEntity entity, IEnumerable<IWall> walls)
        {
            var wallAvoidanceBehaviour = new WallAvoidanceBehaviour(entity, walls);
            var wanderBehaviour = new WanderBehaviour(entity);

            _aggregateBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                new List<WeightedSteeringBehaviour>
                {
                    new WeightedSteeringBehaviour(wallAvoidanceBehaviour, .5f),
                    new WeightedSteeringBehaviour(wanderBehaviour, .5f)
                },
                entity.MaxSpeed
            );
        }

        public Vector2 Calculate(float deltaTime)
        {
            return _aggregateBehaviour.Calculate(deltaTime);
        }

        public void Render(Graphics graphics)
        {
            _aggregateBehaviour.Render(graphics);
        }
    }
}