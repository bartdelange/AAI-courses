using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Util;

namespace AICore.SteeringBehaviour.Util
{
    /// <summary>
    /// Probability coefficient examples for steering behaviours
    /// 
    /// WallAvoidance             0.5
    /// ObstacleAvoidance         0.5
    /// Separation                0.2
    /// Alignment                 0.3
    /// Cohesion                  0.6
    /// Wander                    0.8
    /// Seek                      0.8
    /// Flee                      0.6
    /// Evade                     1.0
    /// Hide                      0.8
    /// Arrive                    0.5
    /// </summary>
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
                weightedSteeringBehaviour => weightedSteeringBehaviour.SteeringBehaviour.RenderIfVisible(graphics)
            );
        }
    }
}