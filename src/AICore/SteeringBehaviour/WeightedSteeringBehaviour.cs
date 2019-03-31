namespace AICore.SteeringBehaviour
{
    /// <summary>
    /// Example weight values for steering behaviours
    /// 
    /// SeparationBehaviour weight          1.0
    /// AlignmentBehaviour weight           1.0
    /// CohesionBehaviour weight            2.0
    /// ObstacleAvoidance weight   10.0
    /// WallAvoidance weight       10.0
    /// Wander weight              1.0
    /// Seek weight                1.0
    /// Flee weight                1.0
    /// Arrive weight              1.0
    /// Pursuit weight             1.0
    /// OffsetPursuitBehaviour weight       1.0
    /// Interpose weight           1.0
    /// HideBehaviour weight                1.0
    /// Evade weight               0.01
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