using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.FuzzyLogic;
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
    /// - Should not move too close to the own goal                             (Fuzzy: Arrive / WanderBehaviour)
    /// - Should intercept the ball when ball is within a certain range         (Fuzzy: PursuitBehaviour)
    /// - Should move on a similar defensive line as other defenders            (OffsetPursuit / Arrive)
    /// - Should avoid obstacles in the field                                   (ObstacleAvoidance)
    /// - Should stay within the playing field                                  (WallAvoidance)
    /// - Should kick the ball towards the strikers                             (Ball.Kicked())
    /// - Should not move when tired                                            (TiredModule)
    /// </summary>
    public class DefenderBehaviour : TiredModule, ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;
        
        private FuzzyModule _fmGoal = new FuzzyModule();
        private FuzzyModule _fmBall = new FuzzyModule();
        private readonly ISteeringBehaviour _steeringBehaviour;

        public DefenderBehaviour(IPlayer striker, Team team, SoccerField soccerField)
        {
            _steeringBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                new List<WeightedSteeringBehaviour>
                {
                    new WeightedSteeringBehaviour(new WallAvoidanceBehaviour(striker, soccerField.Sidelines), 10f),
                    new WeightedSteeringBehaviour(new WanderBehaviour(striker), 1f)
                },
                striker.MaxSpeed
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
            var distToGoal = _fmGoal.CreateFLV("DistToGoal");
            var goalClose = distToGoal.AddLeftShoulderSet("GoalClose", 0, 25, 150);
            var goalMedium = distToGoal.AddTriangularSet("GoalMedium", 25, 150, 300);
            var goalFar = distToGoal.AddRightShoulderSet("GoalFar", 150, 300, 500);

            var desirability = _fmGoal.CreateFLV("Desirability");
            var veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);

            _fmGoal.AddRule("goalClose -> undesirable", goalClose, desirable);
            _fmGoal.AddRule("goalMedium -> veryDesirable", goalMedium, veryDesirable);
            _fmGoal.AddRule("goalFar -> undesirable", goalFar, undesirable);
        }
        
        public void InitBallModule()
        {
            var distBallToGoal = _fmBall.CreateFLV("DistToBall");
            var ballClose = distBallToGoal.AddRightShoulderSet("BallClose", 10, 30, 100);
            var ballMedium = distBallToGoal.AddTriangularSet("BallMedium", 0, 10, 30);
            var ballFar = distBallToGoal.AddLeftShoulderSet("BallFar", 0, 0, 10);

            var desirability = _fmBall.CreateFLV("Desirability");
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);
            
            _fmBall.AddRule("ballClose -> undesirable", ballClose, veryDesirable);
            _fmBall.AddRule("ballMedium -> desirable", ballMedium, desirable);
            _fmBall.AddRule("ballFar -> veryDesirable", ballFar, undesirable);
        }

        public double CalculateDistanceToGoalDesirability(double goalDist)
        {
            _fmGoal.Fuzzify("DistToGoal", goalDist);
            return _fmGoal.DeFuzzify("Desirability", FuzzyModule.DefuzzifyType.MaxAv);
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