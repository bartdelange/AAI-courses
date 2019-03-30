using System.Collections.Generic;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Aggregate;
using AICore.SteeringBehaviour.Individual;
using AICore.SteeringBehaviour.Util;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
{
    public class RestGoal : BaseGoal
    {
        public RestGoal(IPlayer player, SoccerField soccerField) : base(player, soccerField)
        {
        }

        public override void Enter()
        {
            Player.SteeringBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                new List<WeightedSteeringBehaviour>
                {
                    new WeightedSteeringBehaviour(new WallObstacleAvoidanceBehaviour(Player, SoccerField.Sidelines, SoccerField.Obstacles), 1f),
                    new WeightedSteeringBehaviour(new ArriveBehaviour(Player, Player.StartPosition), 1f)
                },
                Player.MaxSpeed
            );
        }

        public override void Update(float deltaTim)
        {
            
        }

        public override double CheckDesirability()
        {
            return 0.1d;
        }
    }
}