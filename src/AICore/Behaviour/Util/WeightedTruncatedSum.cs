using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace AICore.Behaviour.Util
{
    public class WeightedTruncatedSum : ISteeringBehaviour
    {
        private readonly Dictionary<ISteeringBehaviour, float> _steeringBehaviours;
            
        public WeightedTruncatedSum(Dictionary<ISteeringBehaviour, float> steeringBehaviours)
        {
            _steeringBehaviours = steeringBehaviours;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var steeringForce = new Vector2();
            
            foreach (var keyValuePair in _steeringBehaviours)
            {
                var steeringBehaviour = keyValuePair.Key;
                var weight = keyValuePair.Value;

                steeringForce += steeringBehaviour.Calculate(deltaTime) * weight;
            }

            return (steeringForce / _steeringBehaviours.Count);
        }

        public void Draw(Graphics g)
        {
            // Not implemented by design since this class shouldn't be used as a steering behaviour directly
            throw new System.NotImplementedException();
        }
    }
}