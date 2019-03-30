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
            var ballClose = distToBall.AddLeftShoulderSet("BallClose", 0, 25, 50);
            var ballMedium = distToBall.AddTriangularSet("BallMedium", 25, 50, 150);
            var ballFar = distToBall.AddRightShoulderSet("BallFar", 50, 150, 1000);

            var desirability = _fuzzyModule.CreateFlv("Desirability");
            var veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);

            _fuzzyModule.AddRule("ballClose -> veryDesirable", ballClose, veryDesirable);
            _fuzzyModule.AddRule("ballMedium -> desirable", ballMedium, desirable);
            _fuzzyModule.AddRule("ballFar -> Very(undesirable)", ballFar, new FzVery(undesirable));
        }

        public override void Enter()
        {
            Player.SteeringBehaviour = new WeightedTruncatedRunningSumWithPrioritization(new List<WeightedSteeringBehaviour>
            {
                new WeightedSteeringBehaviour(new WallObstacleAvoidanceBehaviour(Player, SoccerField.Sidelines, SoccerField.Obstacles),10f),
                new WeightedSteeringBehaviour(new PursuitBehaviour(Player, SoccerField.Ball), 1f)
            }, Player.MaxSpeed);
        }

        public override void Update(float deltaTim)
        {
            SoccerField.Ball.TakeBall(Player);
        }

        public override double CheckDesirability()
        {
            if (SoccerField.Ball.Owner == Player) return 0;

            var ballDist = Vector2.Distance(Player.Position, SoccerField.Ball.Position);
            _fuzzyModule.Fuzzify("DistToBall", ballDist);
            return _fuzzyModule.DeFuzzify("Desirability", FuzzyModule.DefuzzifyType.MaxAv);
        }
    }
}