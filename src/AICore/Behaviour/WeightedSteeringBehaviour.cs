using System.Collections.Generic;

namespace AICore.Behaviour
{
    /// <summary>
    /// Example weight values for steering behaviours
    /// 
    /// SeparationBehaviour weight          1.0
    /// AlignmentBehaviour weight           1.0
    /// CohesionBehaviour weight            2.0
    /// ObstacleAvoidanceBehaviour weight   10.0
    /// WallAvoidanceBehaviour weight       10.0
    /// WanderBehaviour weight              1.0
    /// SeekBehaviour weight                1.0
    /// FleeBehaviour weight                1.0
    /// ArriveBehaviour weight              1.0
    /// PursuitBehaviour weight             1.0
    /// OffsetPursuitBehaviour weight       1.0
    /// InterposeBehaviour weight           1.0
    /// HideBehaviour weight                1.0
    /// EvadeBehaviour weight               0.01
    /// FollowPathBehaviour weight          0.05
    /// </summary>
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