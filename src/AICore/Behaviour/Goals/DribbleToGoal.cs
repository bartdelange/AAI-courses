using System;
using System.Collections.Generic;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Aggregate;
using AICore.SteeringBehaviour.Individual;
using AICore.SteeringBehaviour.Util;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
{
    public class DribbleToGoal : BaseGoal
    {
        public DribbleToGoal(IPlayer player, SoccerField soccerField) : base(player, soccerField)
        {
        }

        public override void Enter()
        {
            Player.SteeringBehaviour = CreateAvoidanceEnabledSteeringBehaviour(
                new WeightedSteeringBehaviour(new Seek(Player, Player.Team.Opponent.Goal.Position), 1f)
            );
        }

        public override void Update(float deltaTim)
        {
            Console.WriteLine("Update DribbleToGoal");
        }

        public override double CheckDesirability()
        {
            // If we don't own the ball we can dribble
            return SoccerField.Ball.Owner != Player ? 0d : 50d;
        }

        public override void Leave()
        {
            // 
        }
    }
}