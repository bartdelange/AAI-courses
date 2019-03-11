using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Behaviour.Group;
using AICore.Entity;

namespace AICore.Behaviour.Aggregate
{
    public class FlockingBehaviour : ISteeringBehaviour
    {
        private readonly Dictionary<float, ISteeringBehaviour> _steeringBehaviours;

        public FlockingBehaviour(MovingEntity movingEntity)
        {
            _steeringBehaviours = new Dictionary<float, ISteeringBehaviour>
            {
                {0.3f, new AlignmentBehaviour(movingEntity)},
                {0.7f, new CohesionBehaviour(movingEntity)}
            };
        }

        public Vector2 Calculate(float deltaTime)
        {
            var steeringForce = new Vector2();
            
            foreach (var keyValuePair in _steeringBehaviours)
            {
                var weight = keyValuePair.Key;
                var steeringBehaviour = keyValuePair.Value;

                steeringForce += steeringBehaviour.Calculate(deltaTime) * weight;
            }

            return steeringForce / _steeringBehaviours.Count;
        }

        public void Draw(Graphics g)
        {
        }
    }
}