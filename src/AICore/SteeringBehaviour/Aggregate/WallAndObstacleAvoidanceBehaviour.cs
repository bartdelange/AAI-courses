using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour.Individual;
using AICore.SteeringBehaviour.Util;

namespace AICore.SteeringBehaviour.Aggregate
{
    public class WallObstacleAvoidanceBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly WeightedTruncatedRunningSumWithPrioritization _aggregateBehaviour;

        public WallObstacleAvoidanceBehaviour(IMovingEntity entity, IEnumerable<IWall> walls, IEnumerable<IObstacle> obstacles, float detectionLength = 20)
        {
            var wallAvoidanceBehaviour = new WallAvoidanceBehaviour(entity, walls, detectionLength);
            var obstacleAvoidanceBehaviour = new ObstacleAvoidanceBehaviour(entity, obstacles, detectionLength);

            _aggregateBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                new List<WeightedSteeringBehaviour>
                {
                    new WeightedSteeringBehaviour(wallAvoidanceBehaviour, 1f),
                    new WeightedSteeringBehaviour(obstacleAvoidanceBehaviour, 1f),
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