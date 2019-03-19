using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Util;

namespace AICore.SteeringBehaviour.Util
{
    public class WeightedTruncatedSum : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly List<WeightedSteeringBehaviour> _weightedSteeringBehaviours;
        private readonly float _maxSpeed;

        public WeightedTruncatedSum(List<WeightedSteeringBehaviour> weightedSteeringBehaviours, float maxSpeed)
        {
            _weightedSteeringBehaviours = weightedSteeringBehaviours;
            _maxSpeed = maxSpeed;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var steeringForce = Vector2.Zero;
            
            foreach (var weightedSteeringBehaviour in _weightedSteeringBehaviours)
            {
                steeringForce += weightedSteeringBehaviour.SteeringBehaviour.Calculate(deltaTime) * weightedSteeringBehaviour.Weight;
            }

            return steeringForce.Truncate(_maxSpeed);
        }

        public void Render(Graphics graphics)
        {
            _weightedSteeringBehaviours.ForEach(
                weightedSteeringBehaviour => weightedSteeringBehaviour.SteeringBehaviour.RenderIfVisible(graphics)
            );
        }
    }
}