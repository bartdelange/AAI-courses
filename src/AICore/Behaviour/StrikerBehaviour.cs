using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.FuzzyLogic;
using AICore.FuzzyLogic.FuzzyHedges;
using AICore.Model;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Individual;
using AICore.SteeringBehaviour.Util;
using AICore.Worlds;

namespace AICore.Behaviour
{
    /// <summary>
    /// Behaviour that is used by the striker entity
    ///
    /// Rules:
    /// - Should move to tactical positions                   (Fuzzy: Exploring / PathFollowingBehaviour
    /// - Should stay near the opponents goal                 (Fuzzy: ArriveBehaviour / WanderBehaviour)
    ///   - Merged to allow tactical wandering (exploring around a fixed point) in range of goal  
    /// - Should kick the ball to the opponents goal          (Fuzzy)
    /// - Should follow other strikers                        (OffsetPursuit with (very) low weight)
    /// - Should avoid obstacles in the field                 (ObstacleAvoidance)
    /// - Should stay within the field                        (WallAvoidance))
    /// - Should not move when tired                          (TiredModule)
    /// </summary>
    public class StrikerBehaviour : TiredModule, ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly ISteeringBehaviour _steeringBehaviour;
        private FuzzyModule _fmBall = new FuzzyModule();
        private FuzzyModule _fmPosition = new FuzzyModule();

        public StrikerBehaviour(IPlayer entity, Team team, SoccerField soccerField)
        {
            _steeringBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                new List<WeightedSteeringBehaviour>
                {
                    new WeightedSteeringBehaviour(new WallAvoidanceBehaviour(entity, soccerField.Sidelines), 10f),
                },
                entity.MaxSpeed
            );
            
            InitFuzzyModule();
        }

        public Vector2 Calculate(float deltaTime)
        {
            return _steeringBehaviour.Calculate(deltaTime);
        }
       
        public void InitFuzzyModule()
        {
            InitGoalModule();
            InitBallModule();
        }

        public void InitGoalModule()
        {
            var distToGoal = _fmPosition.CreateFLV("DistToGoal");
            var goalClose = distToGoal.AddLeftShoulderSet("GoalClose", 0, 25, 150);
            var goalMedium = distToGoal.AddTriangularSet("GoalMedium", 25, 150, 300);
            var goalFar = distToGoal.AddRightShoulderSet("GoalFar", 150, 300, 500);
            
            var distToOptimal = _fmPosition.CreateFLV("DistToOptimal");
            var optimalClose = distToOptimal.AddLeftShoulderSet("GoalClose", 0, 25, 150);
            var optimalMedium = distToOptimal.AddTriangularSet("GoalMedium", 25, 150, 300);
            var optimalFar = distToOptimal.AddRightShoulderSet("GoalFar", 150, 300, 500);

            var desirability = _fmPosition.CreateFLV("Desirability");
            var veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);

            _fmPosition.AddRule("goalClose -> desirable", goalClose, veryDesirable);
            _fmPosition.AddRule("goalMedium -> veryDesirable", goalMedium, desirable);
            _fmPosition.AddRule("goalFar -> undesirable", goalFar, new FzVery(undesirable));
            _fmPosition.AddRule("optimalClose -> undesirable", optimalClose, undesirable);
            _fmPosition.AddRule("optimalMedium -> desirable", optimalMedium, desirable);
            _fmPosition.AddRule("optimalFar -> veryDesirable", optimalFar, veryDesirable);
        }

        public void InitBallModule()
        {
            var distToBall = _fmBall.CreateFLV("DistToBall");
            var ballClose = distToBall.AddLeftShoulderSet("BallClose", 0, 25, 150);
            var ballMedium = distToBall.AddTriangularSet("BallMedium", 25, 150, 300);
            var ballFar = distToBall.AddRightShoulderSet("BallFar", 150, 300, 500);

            var desirability = _fmBall.CreateFLV("Desirability");
            var veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);

            _fmBall.AddRule("ballClose -> undesirable", ballClose, veryDesirable);
            _fmBall.AddRule("ballMedium -> veryDesirable", ballMedium, desirable);
            _fmBall.AddRule("ballFar -> undesirable", ballFar, undesirable);
        }

        public double CalculateDistanceDesirability(double goalDist, double optimalPositionDist)
        {
            _fmPosition.Fuzzify("DistToGoal", goalDist);
            _fmPosition.Fuzzify("DistToOptimal", optimalPositionDist);
            return _fmPosition.DeFuzzify("Desirability", FuzzyModule.DefuzzifyType.MaxAv);
        }

        public double CalculateDistanceToBallDesirability(double ballDist)
        {
            _fmBall.Fuzzify("DistToBall", ballDist);
            return _fmBall.DeFuzzify("Desirability", FuzzyModule.DefuzzifyType.MaxAv);
        }

        public void Render(Graphics graphics)
        {
        }
    }
}