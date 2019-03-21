using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour.Individual;
using AICore.SteeringBehaviour.Util;

namespace AICore.SteeringBehaviour.Aggregate
{
    public class WanderWallAvoidanceBehaviour : ISteeringBehaviour
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
                    new WeightedSteeringBehaviour(wallAvoidanceBehaviour, 10f),
                    new WeightedSteeringBehaviour(wanderBehaviour, 1f)
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
            _aggregateBehaviour.RenderIfVisible(graphics);
        }
    }
}