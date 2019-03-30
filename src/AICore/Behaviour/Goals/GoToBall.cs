using System;
using System.Collections.Generic;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.FuzzyLogic;
using AICore.FuzzyLogic.FuzzyHedges;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Aggregate;
using AICore.SteeringBehaviour.Individual;
using AICore.SteeringBehaviour.Util;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
{
    public class GoToBall : BaseGoal
    {
        private readonly FuzzyModule _fuzzyModule = new FuzzyModule();
        
        public GoToBall(IPlayer player, SoccerField soccerField): base(player, soccerField)
        {
            var distToBall = _fuzzyModule.CreateFlv("DistToBall");
            var ballClose = distToBall.AddLeftShoulderSet("BallClose", 0, 25, 150);
            var ballMedium = distToBall.AddTriangularSet("BallMedium", 25, 150, 300);
            var ballFar = distToBall.AddRightShoulderSet("BallFar", 150, 300, 500);

            var desirability = _fuzzyModule.CreateFlv("Desirability");
            var veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);

            _fuzzyModule.AddRule("ballClose -> very(undesirable)", ballClose, new FzVery(undesirable));
            _fuzzyModule.AddRule("ballMedium -> desirable", ballMedium, desirable);
            _fuzzyModule.AddRule("ballFar -> undesirable", ballFar, veryDesirable);
        }

        public override void Activate()
        {
            Player.SteeringBehaviour = new WeightedTruncatedRunningSumWithPrioritization(new List<WeightedSteeringBehaviour>
            {
                new WeightedSteeringBehaviour(new WallObstacleAvoidanceBehaviour(Player, SoccerField.Sidelines, SoccerField.Obstacles),10f),
                new WeightedSteeringBehaviour(new PursuitBehaviour(Player, SoccerField.Ball), 1f)
            }, Player.MaxSpeed);
        }

        public override void Update(IPlayer player)
        {
            SoccerField.Ball.TakeBall(Player);

            base.Update(player);
        }

        public override double CheckDesirability()
        {
            if (SoccerField.Ball.Owner == Player)
            {
                return 0;
            }
            
            var ballDist = Vector2.Distance(Player.Position, SoccerField.Ball.Position);
            _fuzzyModule.Fuzzify("DistToBall", ballDist);
            return _fuzzyModule.DeFuzzify("Desirability", FuzzyModule.DefuzzifyType.MaxAv);
        }
    }
}