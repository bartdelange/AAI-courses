using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Numerics;
using AICore.Util;

namespace AICore.Behaviour.Util
{
    public class PrioritizedDithering : ISteeringBehaviour
    {
        private readonly OrderedDictionary _steeringBehaviour;
        private readonly float _maxSpeed;

        public PrioritizedDithering(OrderedDictionary steeringBehaviour, float maxSpeed)
        {
            _steeringBehaviour = steeringBehaviour;
            _maxSpeed = maxSpeed;
        }
        
        public Vector2 Calculate(float deltaTime)
        {
            var enumerator = _steeringBehaviour.GetEnumerator();

            var steeringForceSum = Vector2.Zero;
            var remainingSteeringForce = _maxSpeed;
            
            while(enumerator.Current != null)
            {
                // Return steering force when we cannot add more speed
                if (remainingSteeringForce <= 0) return steeringForceSum;
             
                var steeringBehaviour = (ISteeringBehaviour) enumerator.Key;
                var properties = (Tuple<float, float>) enumerator.Value;

                var weight = properties.Item1;
                var probability = properties.Item1;
                
                var steeringForce = steeringBehaviour.Calculate(deltaTime) * weight;
                var steeringForceMagnitude = steeringForce.Length();
                
                // Ignore this steering behaviour when magnitude is zero
                if (steeringForceMagnitude > 0)
                {
                    return steeringForce.Truncate(_maxSpeed) * weight / probability;
                }
                
                enumerator.MoveNext();
            }
            
            return Vector2.Zero;
        }

        public void Draw(Graphics g)
        {
            throw new System.NotImplementedException();
        }
    }
}