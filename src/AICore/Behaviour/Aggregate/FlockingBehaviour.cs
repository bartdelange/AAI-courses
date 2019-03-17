using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Numerics;
using AICore.Behaviour.Group;
using AICore.Behaviour.Util;
using AICore.Entity.Contracts;

namespace AICore.Behaviour.Aggregate
{
    public class FlockingBehaviour : ISteeringBehaviour
    {
        private readonly WeightedTruncatedRunningSumWithPrioritization _steeringBehaviour;

        public FlockingBehaviour(IMovingEntity movingEntity, IEnumerable<IMovingEntity> neighbours)
        {
            _steeringBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                new OrderedDictionary
                {
                    {0.3f, new AlignmentBehaviour<IMovingEntity>(movingEntity, neighbours)},
                    {0.7f, new CohesionBehaviour<IEntity>(movingEntity, neighbours)}
                },
                movingEntity.MaxSpeed
            );
        }

        public Vector2 Calculate(float deltaTime)
        {
            return _steeringBehaviour.Calculate(deltaTime);
        }

        public void Draw(Graphics g)
        {
        }
    }
}