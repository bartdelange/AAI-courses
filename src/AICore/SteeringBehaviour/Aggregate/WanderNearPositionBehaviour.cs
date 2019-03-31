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

        public WanderNearPositionBehaviour(
            IMovingEntity entity, 
            IEnumerable<IWall> walls,
            IEnumerable<IObstacle> obstacles
        )
        {
            var steeringBehaviours = new List<WeightedSteeringBehaviour>
            {
                new WeightedSteeringBehaviour(new WallObstacleAvoidanceBehaviour(entity, walls, obstacles), 10f),
                new WeightedSteeringBehaviour(new Seek(entity, entity.StartPosition), 3f),
                new WeightedSteeringBehaviour(new Wander(entity), 1f)
            };

            _steeringBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                steeringBehaviours,
                entity.MaxSpeed
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