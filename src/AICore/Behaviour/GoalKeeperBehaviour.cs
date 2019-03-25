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
    /// - Should stay near the goal                                (ArriveBehaviour / WanderBehaviour)  
    /// - Should kick the ball away when too close to the goal
    ///   - Merge top two together so we can have one fuzzy calculation each [interval]
    /// - Should avoid obstacles in the field                      (ObstacleAvoidanceBehaviour)
    /// - Should stay within the playing field                     (WallAvoidanceBehaviour)
    /// - Should not move when tired                               (TiredModule)
    /// </summary>
    public class GoalKeeperBehaviour : TiredModule, ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private FuzzyModule _fm = new FuzzyModule();
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
            // Distance module
            var distToGoal = _fm.CreateFLV("DistToGoal");
            var goalClose = distToGoal.AddLeftShoulderSet("GoalClose", 0, 25, 150);
            var goalMedium = distToGoal.AddTriangularSet("GoalMedium", 25, 150, 300);
            var goalFar = distToGoal.AddRightShoulderSet("GoalFar", 150, 300, 500);

            var distBallToGoal = _fm.CreateFLV("DistBallToGoal");
            var ballCloseToGoal = distBallToGoal.AddRightShoulderSet("BallCloseToGoal", 10, 30, 100);
            var ballMediumToGoal = distBallToGoal.AddTriangularSet("BallMediumToGoal", 0, 10, 30);
            var ballFarFromGoal = distBallToGoal.AddLeftShoulderSet("BallFarFromGoal", 0, 0, 10);

            var desirability = _fm.CreateFLV("Desirability");
            var veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);

            _fm.AddRule("goalClose -> veryDesirable", goalClose, veryDesirable);
            _fm.AddRule("goalMedium -> undesirable", goalMedium, undesirable);
            _fm.AddRule("goalFar -> undesirable", goalFar, undesirable);
            _fm.AddRule("ballFarFromGoal -> veryDesirable", ballFarFromGoal, veryDesirable);
            _fm.AddRule("ballMediumToGoal -> desirable", ballMediumToGoal, desirable);
            _fm.AddRule("ballCloseToGoal -> undesirable", ballCloseToGoal, undesirable);
        }

        public double CalculateDistanceDesirability(double goalDist, double opponentDist)
        {
            _fm.Fuzzify("DistToGoal", goalDist);
            _fm.Fuzzify("DistToOpponent", opponentDist);
            return _fm.DeFuzzify("Desirability", FuzzyModule.DefuzzifyType.MaxAv);
        }
    }
}