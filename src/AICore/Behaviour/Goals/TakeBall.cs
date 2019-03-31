using System.Linq;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.FuzzyLogic;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Individual;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
{
    public class TakeBall : BaseGoal
    {
        private readonly FuzzyModule _fuzzyModule = new FuzzyModule();

        public TakeBall(IPlayer player, SoccerField soccerField) : base(player, soccerField)
        {
            var distToBall = _fuzzyModule.CreateFlv("DistToBall");
            var ballClose = distToBall.AddLeftShoulderSet("BallClose", 0, 50, 100);
            var ballMedium = distToBall.AddTriangularSet("BallMedium", 50, 100, 150);
            var ballFar = distToBall.AddRightShoulderSet("BallFar", 100, 150, 1000);
            
            var distFromPosition = _fuzzyModule.CreateFlv("DistFromPosition");
            var positionClose = distFromPosition.AddLeftShoulderSet("PositionClose", 0, 25, 50);
            var positionMedium = distFromPosition.AddTriangularSet("PositionMedium", 25, 50, 100);
            var positionFar = distFromPosition.AddRightShoulderSet("PositionFar", 100, 150, 1000);

            var desirability = _fuzzyModule.CreateFlv("Desirability");
            var veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);

            _fuzzyModule.AddRule("ballClose -> veryDesirable", ballClose, veryDesirable);
            _fuzzyModule.AddRule("ballMedium -> undesirable", ballMedium, undesirable);
            _fuzzyModule.AddRule("ballFar -> undesirable", ballFar, undesirable);

            _fuzzyModule.AddRule("positionClose -> veryDesirable", positionClose, veryDesirable);
            _fuzzyModule.AddRule("positionMedium -> undesirable", positionMedium, undesirable);
            _fuzzyModule.AddRule("positionFar -> undesirable", positionFar, undesirable);
        }

        public override void Enter()
        {
        }

        public override void Update(float deltaTime)
        {
            Player.SteeringBehaviour = CreateAvoidanceEnabledSteeringBehaviour(
                new WeightedSteeringBehaviour(new Pursuit(Player, SoccerField.Ball), 1f)
            );
            SoccerField.Ball.TakeBall(Player);
        }

        public override void Leave()
        {
        }

        public override double CheckDesirability()
        {
            if (Player.Team.Players.Any(player => player == SoccerField.Ball.Owner))
            {
                return 0;
            }
            
            var distanceToBall = Vector2.Distance(Player.Position, SoccerField.Ball.Position);
            var distanceFromPosition = Vector2.Distance(Player.Position, Player.StartPosition);
            
            _fuzzyModule.Fuzzify("DistToBall", distanceToBall);
            _fuzzyModule.Fuzzify("DistFromPosition", distanceFromPosition);
            return _fuzzyModule.DeFuzzify("Desirability", FuzzyModule.DefuzzifyType.MaxAv);
        }
    }
}