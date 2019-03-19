using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace AICore.SteeringBehaviour.Util
{
    public class WeightedTruncatedRunningSumWithPrioritization : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;
        
        private readonly List<WeightedSteeringBehaviour> _weightedSteeringBehaviours;
        private readonly float _maxSpeed;

        public WeightedTruncatedRunningSumWithPrioritization(
            List<WeightedSteeringBehaviour> steeringBehaviour,
            float maxSpeed
        )
        {
            _weightedSteeringBehaviours = steeringBehaviour;
            _maxSpeed = maxSpeed;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var steeringForceSum = new Vector2();
            var remainingSteeringForce = _maxSpeed;

            foreach (var weightedSteeringBehaviour in _weightedSteeringBehaviours)
            {
                // Return steering force when we cannot add more speed
                if (steeringForceSum.Length() > _maxSpeed)
                {
                    return steeringForceSum;
                }

                var steeringForce = weightedSteeringBehaviour.SteeringBehaviour.Calculate(deltaTime) *
                                    weightedSteeringBehaviour.Weight;

                var steeringForceMagnitude = steeringForce.Length();

                // Add steering force to steeringForceSum. Adds as much as possible when addition will exceed maxSpeed.
                steeringForceSum += (steeringForceMagnitude < remainingSteeringForce)
                    ? steeringForce
                    : (Vector2.Normalize(steeringForce) * remainingSteeringForce);
            }

            return steeringForceSum;
        }

        public void Render(Graphics graphics)
        {
            _weightedSteeringBehaviours.ForEach(
                weightedSteeringBehaviour => weightedSteeringBehaviour.SteeringBehaviour.Render(graphics)
            );
        }
    }
}