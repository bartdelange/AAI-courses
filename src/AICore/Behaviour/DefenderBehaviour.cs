using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Individual;
using AICore.SteeringBehaviour.Util;

namespace AICore.Behaviour
{
    /// <summary>
    /// Behaviour that is used by the striker entity
    ///
    /// Rules:
    /// - Should not move too close to the own goal                             (Arrive / WanderBehaviour)
    /// - Should pursuit to opponent when opponent is within a certain range    (PursuitBehaviour)
    /// - Should move on a similar defensive line as other defenders            (OffsetPursuit / Arrive)
    /// - Should avoid obstacles in the field                                   (ObstacleAvoidance)
    /// - Should stay within the playing field                                  (WallAvoidance)
    /// - Should kick the ball towards the strikers
    /// </summary>
    public class DefenderBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly ISteeringBehaviour _steeringBehaviour;

        public DefenderBehaviour(IPlayer striker, List<IPlayer> team, World world)
        {
            _steeringBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                new List<WeightedSteeringBehaviour>
                {
                    new WeightedSteeringBehaviour(new WallAvoidanceBehaviour(striker, world.Walls), 10f),
                    new WeightedSteeringBehaviour(new WanderBehaviour(striker), 1f)
                },
                striker.MaxSpeed
            );
        }

        public Vector2 Calculate(float deltaTime)
        {
            return _steeringBehaviour.Calculate(deltaTime);
        }

        public void Render(Graphics graphics)
        {
        }
    }
}