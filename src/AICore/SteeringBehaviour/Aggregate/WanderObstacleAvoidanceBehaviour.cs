using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour.Individual;
using AICore.SteeringBehaviour.Util;

namespace AICore.SteeringBehaviour.Aggregate
{
    public class WanderObstacleAvoidanceBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly ISteeringBehaviour _aggregateBehaviour;

        public WanderObstacleAvoidanceBehaviour(IMovingEntity entity, IEnumerable<IObstacle> obstacles)
        {
            var steeringBehaviours = new List<WeightedSteeringBehaviour>
            {
                new WeightedSteeringBehaviour(new ObstacleAvoidanceBehaviour(entity, obstacles, 40), 2f),
                new WeightedSteeringBehaviour(new WanderBehaviour(entity, 50, 25, 25), 1f)
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