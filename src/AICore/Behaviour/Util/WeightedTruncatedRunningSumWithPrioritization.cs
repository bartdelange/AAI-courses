using System.Collections.Specialized;
using System.Drawing;
using System.Numerics;

namespace AICore.Behaviour.Util
{
    public class WeightedTruncatedRunningSumWithPrioritization : ISteeringBehaviour
    {
        private readonly OrderedDictionary _steeringBehaviour;
        private readonly float _maxSpeed;

        public WeightedTruncatedRunningSumWithPrioritization(OrderedDictionary steeringBehaviour, float maxSpeed)
        {
            _steeringBehaviour = steeringBehaviour;
            _maxSpeed = maxSpeed;
        }
        
        public Vector2 Calculate(float deltaTime)
        {
            var enumerator = _steeringBehaviour.GetEnumerator();

            var steeringForceSum = new Vector2();
            var remainingSteeringForce = _maxSpeed;
            
            while(enumerator.Current != null)
            {
                // Return steering force when we cannot add more speed
                if (remainingSteeringForce <= 0) return steeringForceSum;
             
                var steeringBehaviour = (ISteeringBehaviour) enumerator.Key;
                var weight = (float) enumerator.Value;
                
                var steeringForce = steeringBehaviour.Calculate(deltaTime) * weight;
                var steeringForceMagnitude = steeringForce.Length();

                // Add steering force to steeringForceSum. Adds as much as possible when addition will exceed maxSpeed.
                steeringForceSum += (steeringForceMagnitude < remainingSteeringForce) 
                    ? steeringForce
                    :(Vector2.Normalize(steeringForce) * remainingSteeringForce);

                // Move to next steering behaviour
                enumerator.MoveNext();
            }

            return steeringForceSum;
        }

        public void Draw(Graphics g)
        {
            throw new System.NotImplementedException();
        }
    }
}