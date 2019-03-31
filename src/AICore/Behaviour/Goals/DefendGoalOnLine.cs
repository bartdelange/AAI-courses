using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Entity.Static;
using AICore.FuzzyLogic;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Individual;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
{
    public class DefendGoalOnLine : BaseGoal
    {
        private readonly FuzzyModule _fuzzyModule = new FuzzyModule();

        public DefendGoalOnLine(IPlayer player, SoccerField soccerField) : base(player, soccerField)
        {
            var distToBall = _fuzzyModule.CreateFlv("DistToBall");
            var ballClose = distToBall.AddLeftShoulderSet("BallClose", 0, 50, 50);
            var ballMedium = distToBall.AddTriangularSet("BallMedium", 50, 150, 250);
            var ballFar = distToBall.AddRightShoulderSet("BallFar", 150, 250, 1000);

            var desirability = _fuzzyModule.CreateFlv("Desirability");
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);

            _fuzzyModule.AddRule("ballClose -> undesirable", ballClose, undesirable);
            _fuzzyModule.AddRule("ballMedium -> veryDesirable", ballMedium, desirable);
            _fuzzyModule.AddRule("ballFar -> undesirable", ballFar, undesirable);
        }

        public override void Enter()
        {
        }

        public override void Update(float deltaTime)
        {
            Player.SteeringBehaviour = CreateAvoidanceEnabledSteeringBehaviour(
                new WeightedSteeringBehaviour(new Interpose(Player, SoccerField.Ball, Player.Team.Goal), 1f)
            );
        }

        public override void Leave()
        {
        }

        public override double CheckDesirability()
        {
            if (SoccerField.Ball.Owner == null || SoccerField.Ball.Owner == Player)
            {
                return 0;
            }
            
            var distanceToBall = Vector2.Distance(Player.Position, SoccerField.Ball.Position);
            
            _fuzzyModule.Fuzzify("DistToBall", distanceToBall);
            return _fuzzyModule.DeFuzzify("Desirability", FuzzyModule.DefuzzifyType.MaxAv);
        }
    }
}