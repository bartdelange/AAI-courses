using System.Collections.Generic;
using System.Linq;
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
            var ballClose = distToBall.AddLeftShoulderSet("BallClose", 0, 50, 100);
            var ballMedium = distToBall.AddTriangularSet("BallMedium", 50, 100, 150);
            var ballFar = distToBall.AddRightShoulderSet("BallFar", 100, 150, 1000);

            var desirability = _fuzzyModule.CreateFlv("Desirability");
            var veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);

            _fuzzyModule.AddRule("ballClose -> veryDesirable", ballClose, veryDesirable);
            _fuzzyModule.AddRule("ballMedium -> desirable", ballMedium, desirable);
            _fuzzyModule.AddRule("ballFar -> undesirable", ballFar, undesirable);
        }

        public override void Enter()
        {
            Player.SteeringBehaviour = new WeightedTruncatedRunningSumWithPrioritization(new List<WeightedSteeringBehaviour>
            {
                new WeightedSteeringBehaviour(new WallObstacleAvoidance(Player, SoccerField.Sidelines, SoccerField.Obstacles),10f),
                new WeightedSteeringBehaviour(new Pursuit(Player, SoccerField.Ball), 1f)
            }, Player.MaxSpeed);
        }

        public override void Update(float deltaTime)
        {
            Player.Energy -= 0.5f*deltaTime;
            SoccerField.Ball.TakeBall(Player);
        }

        public override double CheckDesirability()
        {
            if (SoccerField.Ball.Owner == Player)
            {
                return 0;
            }
            
            if (Player.Team.Players.Any(player => player == SoccerField.Ball.Owner))
            {
                return 0;
            }
            
            var distanceToBall = Vector2.Distance(Player.Position, SoccerField.Ball.Position);
            
            _fuzzyModule.Fuzzify("DistToBall", distanceToBall);
            return _fuzzyModule.DeFuzzify("Desirability", FuzzyModule.DefuzzifyType.MaxAv);
        }

        public override void Leave()
        {
            
        }
    }
}