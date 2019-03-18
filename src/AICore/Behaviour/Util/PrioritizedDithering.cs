using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Numerics;
using AICore.Util;

namespace AICore.Behaviour.Util
{
    public class PrioritizedDithering : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly List<WeightedSteeringBehaviour> _weightedSteeringBehaviours;
        private readonly float _maxSpeed;

        public PrioritizedDithering(List<WeightedSteeringBehaviour> weightedSteeringBehaviours, float maxSpeed)
        {
            _weightedSteeringBehaviours = weightedSteeringBehaviours;
            _maxSpeed = maxSpeed;
        }
        
        public Vector2 Calculate(float deltaTime)
        {
            var steeringForceSum = Vector2.Zero;
            var remainingSteeringForce = _maxSpeed;
            
            foreach (var weightedSteeringBehaviour in _weightedSteeringBehaviours)
            {
                // Return steering force when we cannot add more speed
                if (remainingSteeringForce <= 0) return steeringForceSum;
                             
                var steeringForce = weightedSteeringBehaviour.SteeringBehaviour.Calculate(deltaTime) * weightedSteeringBehaviour.Weight;
                var steeringForceMagnitude = steeringForce.Length();
                
                // Ignore this steering behaviour when magnitude is zero
                if (steeringForceMagnitude > 0)
                {
                    return steeringForce.Truncate(_maxSpeed) * weightedSteeringBehaviour.Weight / weightedSteeringBehaviour.Probability;
                }
            }
            
            return Vector2.Zero;
        }

        public void Render(Graphics graphics)
        {
            _weightedSteeringBehaviours.ForEach(
                weightedSteeringBehaviour => weightedSteeringBehaviour.SteeringBehaviour.Render(graphics)
            );
        }
    }
}