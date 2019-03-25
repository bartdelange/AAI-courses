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
    /// - Should stay near the opponents goal                 (ArriveBehaviour / WanderBehaviour)
    /// - Should follow other strikers                        (OffsetPursuit with (very) low weight)
    /// - Should avoid obstacles in the field                 (ObstacleAvoidance)
    /// - Should stay within the field                        (WallAvoidance)
    /// - Should move to tactical positions                   (Exploring / PathFollowingBehaviour)
    /// - Should kick the ball to the opponents goal
    /// - Should not move when tired                          (TiredModule)
    /// </summary>
    public class StrikerBehaviour : TiredModule, ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly ISteeringBehaviour _steeringBehaviour;

        public StrikerBehaviour(IPlayer striker, List<IPlayer> team, World world)
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