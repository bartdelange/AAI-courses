using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour.Individual;
using AICore.SteeringBehaviour.Util;

namespace AICore.SteeringBehaviour.Aggregate
{
    public class WanderWallObstacleAvoidanceBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly ISteeringBehaviour _aggregateBehaviour;

        public WanderWallObstacleAvoidanceBehaviour(IMovingEntity entity, IEnumerable<IObstacle> obstacles, IEnumerable<IWall> walls)
        {
            var steeringBehaviours = new List<WeightedSteeringBehaviour>
            {
                new WeightedSteeringBehaviour(new ObstacleAvoidance(entity, obstacles, 40), 20f),
                new WeightedSteeringBehaviour(new WallAvoidance(entity, walls), 10f),
                new WeightedSteeringBehaviour(new Wander(entity, 50, 25, 25), 1f)
            };

            _aggregateBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                steeringBehaviours,
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