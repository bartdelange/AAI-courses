using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.FuzzyLogic;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Individual;
using AICore.SteeringBehaviour.Util;

namespace AICore.Behaviour
{
    /// <summary>
    /// Behaviour that is used by the goal keeper
    ///
    /// Rules:
    /// - Should stay near the goal                                (Fuzzy: ArriveBehaviour / WanderBehaviour)  
    /// - Should kick the ball away when too close to the goal     (Fuzzy)
    /// - Should avoid obstacles in the field                      (ObstacleAvoidanceBehaviour)
    /// - Should stay within the playing field                     (WallAvoidanceBehaviour)
    /// - Should not move when tired                               (TiredModule)
    /// </summary>
    public class GoalKeeperBehaviour : TiredModule, ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private FuzzyModule _fmGoal = new FuzzyModule();
        private FuzzyModule _fmBall = new FuzzyModule();
        private readonly ISteeringBehaviour _steeringBehaviour;

        public GoalKeeperBehaviour(IPlayer goalkeeper, List<IPlayer> team, World world)
        {
            _steeringBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                new List<WeightedSteeringBehaviour>
                {
                    new WeightedSteeringBehaviour(new WallAvoidanceBehaviour(goalkeeper, world.Walls), 10f),
                    new WeightedSteeringBehaviour(new WanderBehaviour(goalkeeper), 1f)
                },
                goalkeeper.MaxSpeed
            );
            
            InitFuzzyModule();
        }

        public Vector2 Calculate(float deltaTime)
        {
            return _steeringBehaviour.Calculate(deltaTime);
        }

        public void Render(Graphics graphics)
        {
        } 

        public void InitFuzzyModule()
        {
            initGoalModule();
            InitBallModule();
        }

        public void initGoalModule()
        {
            var distToGoal = _fmGoal.CreateFLV("DistToGoal");
            var goalClose = distToGoal.AddLeftShoulderSet("GoalClose", 0, 25, 150);
            var goalMedium = distToGoal.AddTriangularSet("GoalMedium", 25, 150, 300);
            var goalFar = distToGoal.AddRightShoulderSet("GoalFar", 150, 300, 500);

            var desirability = _fmGoal.CreateFLV("Desirability");
            var veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);

            _fmGoal.AddRule("goalClose -> veryDesirable", goalClose, veryDesirable);
            _fmGoal.AddRule("goalMedium -> undesirable", goalMedium, undesirable);
            _fmGoal.AddRule("goalFar -> undesirable", goalFar, undesirable);
        }
        
        public void InitBallModule()
        {
            var distBallToGoal = _fmBall.CreateFLV("DistBallToGoal");
            var ballCloseToGoal = distBallToGoal.AddRightShoulderSet("BallCloseToGoal", 10, 30, 100);
            var ballMediumToGoal = distBallToGoal.AddTriangularSet("BallMediumToGoal", 0, 10, 30);
            var ballFarFromGoal = distBallToGoal.AddLeftShoulderSet("BallFarFromGoal", 0, 0, 10);

            var desirability = _fmBall.CreateFLV("Desirability");
            var veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);
            
            _fmBall.AddRule("ballFarFromGoal -> veryDesirable", ballFarFromGoal, veryDesirable);
            _fmBall.AddRule("ballMediumToGoal -> desirable", ballMediumToGoal, desirable);
            _fmBall.AddRule("ballCloseToGoal -> undesirable", ballCloseToGoal, undesirable);
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
    }
}