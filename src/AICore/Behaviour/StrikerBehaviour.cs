using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Model;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Individual;
using AICore.SteeringBehaviour.Util;
using AICore.Worlds;

namespace AICore.Behaviour
{
    /// <summary>
    /// Behaviour that is used by the striker entity
    ///
    /// Rules:
    /// - Should move to tactical positions                   (Fuzzy: Exploring / PathFollowingBehaviour
    /// - Should stay near the opponents goal                 (Fuzzy: ArriveBehaviour / WanderBehaviour)
    ///   - Merged to allow tactical wandering (exploring around a fixed point) in range of goal  
    /// - Should kick the ball to the opponents goal          (Fuzzy)
    /// - Should follow other strikers                        (OffsetPursuit with (very) low weight)
    /// - Should avoid obstacles in the field                 (ObstacleAvoidance)
    /// - Should stay within the field                        (WallAvoidance))
    /// - Should not move when tired                          (TiredModule)
    /// </summary>
    public class StrikerBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly ISteeringBehaviour _steeringBehaviour;

        public StrikerBehaviour(IPlayer entity, Team team, SoccerField soccerField)
        {
            _steeringBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                new List<WeightedSteeringBehaviour>
                {
                    new WeightedSteeringBehaviour(new WallAvoidanceBehaviour(entity, soccerField.Sidelines), 10f),
                },
                entity.MaxSpeed
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