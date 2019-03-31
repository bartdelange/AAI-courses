using System;
using System.Linq;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.FuzzyLogic;
using AICore.Graph.PathFinding;
using AICore.Model;
using AICore.Navigation;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Aggregate;
using AICore.SteeringBehaviour.Individual;
using AICore.Util;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
{
    public class StayGoal : BaseGoal
    {
        private readonly FuzzyModule _fuzzyModule = new FuzzyModule();

        public StayGoal(IPlayer player, SoccerField soccerField) : base(player, soccerField)
        {
            var distToBall = _fuzzyModule.CreateFlv("DistToBall");
            var ballClose = distToBall.AddLeftShoulderSet("BallClose", 0, 50, 50);
            var ballMedium = distToBall.AddTriangularSet("BallMedium", 50, 150, 250);
            var ballFar = distToBall.AddRightShoulderSet("BallFar", 150, 250, 1000);

            var distToPosition = _fuzzyModule.CreateFlv("DistToPosition");
            var positionClose = distToPosition.AddLeftShoulderSet("PositionClose", 0, 50, 150);
            var positionMedium = distToPosition.AddTriangularSet("PositionMedium", 50, 150, 250);
            var positionFar = distToPosition.AddRightShoulderSet("PositionFar", 150, 250, 1000);

            var desirability = _fuzzyModule.CreateFlv("Desirability");
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);

            _fuzzyModule.AddRule("ballClose -> undesirable", ballClose, undesirable);
            _fuzzyModule.AddRule("ballMedium -> desirable", ballMedium, desirable);
            _fuzzyModule.AddRule("ballFar -> veryDesirable", ballFar, veryDesirable);

            _fuzzyModule.AddRule("positionClose -> undesirable", positionClose, undesirable);
            _fuzzyModule.AddRule("positionMedium -> desirable", positionMedium, desirable);
            _fuzzyModule.AddRule("positionFar -> veryDesirable", positionFar, veryDesirable);
        }

        public override void Enter()
        {
            Player.MaxSpeed = Config.RestSpeed / 2;
            Player.SteeringBehaviour = new WanderNearPositionBehaviour(Player, SoccerField.Sidelines, SoccerField.Obstacles);
        }

        public override void Update(float deltaTim)
        {
        }

        public override void Leave()
        {
            Player.MaxSpeed = Config.MaxSpeed;
        }

        public override double CheckDesirability()
        {
            if (SoccerField.Ball.Owner == Player)
            {
                return 0;
            }

            _fuzzyModule.Fuzzify("DistToBall", Vector2.Distance(Player.Position, SoccerField.Ball.Position));
            _fuzzyModule.Fuzzify("DistToPosition", Vector2.Distance(Player.Position, Player.StartPosition));

            // If we own the ball we can't rest 
            return _fuzzyModule.DeFuzzify("Desirability", FuzzyModule.DefuzzifyType.MaxAv);
        }
    }
}