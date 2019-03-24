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
    /// Behaviour that is used by the goal keeper
    ///
    /// Rules:
    /// - Should stay near the goal                                (ArriveBehaviour / WanderBehaviour)
    /// - Should avoid obstacles in the field                      (ObstacleAvoidanceBehaviour)
    /// - Should stay within the playing field                     (WallAvoidanceBehaviour)
    /// - Should not move when tired                               
    /// - Should kick the ball away when too close to the goal     
    /// </summary>
    public class GoalKeeperBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly ISteeringBehaviour _steeringBehaviour;

        public GoalKeeperBehaviour(IPlayer goalkeeper, List<IPlayer> team, World world)
        {
            _steeringBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                new List<WeightedSteeringBehaviour>
                {
                    new WeightedSteeringBehaviour(new WallAvoidanceBehaviour(goalkeeper, world.Walls), 10f),
                    new WeightedSteeringBehaviour(new WanderBehaviour(goalkeeper), 1f)
                },
                goalkeeper.MaxSpeed
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