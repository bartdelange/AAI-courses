using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Util;

namespace AICore.Behaviour.Util
{
    public class WeightedTruncatedSum : ISteeringBehaviour
    {
        private readonly Dictionary<ISteeringBehaviour, float> _steeringBehaviours;
        private readonly float _maxSpeed;

        public WeightedTruncatedSum(Dictionary<ISteeringBehaviour, float> steeringBehaviours, float maxSpeed)
        {
            _steeringBehaviours = steeringBehaviours;
            _maxSpeed = maxSpeed;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var steeringForce = Vector2.Zero;
            
            foreach (var keyValuePair in _steeringBehaviours)
            {
                var steeringBehaviour = keyValuePair.Key;
                var weight = keyValuePair.Value;

                steeringForce += steeringBehaviour.Calculate(deltaTime) * weight;
            }

            return steeringForce.Truncate(_maxSpeed);
        }

        public void Draw(Graphics g)
        {
            // Not implemented by design since this class shouldn't be used as a steering behaviour directly
            throw new System.NotImplementedException();
        }
    }
}