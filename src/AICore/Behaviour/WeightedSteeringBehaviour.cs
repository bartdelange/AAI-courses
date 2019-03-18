using System.Collections.Generic;

namespace AICore.Behaviour
{
    public struct WeightedSteeringBehaviour
    {
        public ISteeringBehaviour SteeringBehaviour { get; }
        public float Weight { get; }
        
        public float Probability { get; }

        public WeightedSteeringBehaviour(ISteeringBehaviour steeringBehaviour, float weight, float probability = 1)
        {
            SteeringBehaviour = steeringBehaviour;
            Weight = weight;
            Probability = probability;
        }
    }
}