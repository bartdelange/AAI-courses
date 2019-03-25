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
    /// Behaviour that is used by the striker entity
    ///
    /// Rules:
    /// - Should not move too close to the own goal                             (Arrive / WanderBehaviour)
    /// - Should pursuit the opponent when opponent is within a certain range    (PursuitBehaviour)
    /// - Should move on a similar defensive line as other defenders            (OffsetPursuit / Arrive)
    /// - Should avoid obstacles in the field                                   (ObstacleAvoidance)
    /// - Should stay within the playing field                                  (WallAvoidance)
    /// - Should kick the ball towards the strikers
    /// </summary>
    public class DefenderBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;
        
        private FuzzyModule _fm = new FuzzyModule();
        private readonly ISteeringBehaviour _steeringBehaviour;

        public DefenderBehaviour(IPlayer striker, List<IPlayer> team, World world)
        {
            _steeringBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                new List<WeightedSteeringBehaviour>
                {
                    new WeightedSteeringBehaviour(new WallAvoidanceBehaviour(striker, world.Walls), 10f),
                    new WeightedSteeringBehaviour(new WanderBehaviour(striker), 1f)
                },
                striker.MaxSpeed
            );
        }

        public Vector2 Calculate(float deltaTime)
        {
            return _steeringBehaviour.Calculate(deltaTime);
        }

        public void InitFuzzyModule()
        {
            // TODO: Make the values a bit better
            var distToGoal = _fm.CreateFLV("DistToGoal");
            var goalClose = distToGoal.AddLeftShoulderSet("GoalClose", 0, 25, 150);
            var goalMedium = distToGoal.AddTriangularSet("GoalMedium", 25, 150, 300);
            var goalFar = distToGoal.AddRightShoulderSet("GoalFar", 150, 300, 500);

            var distToOpponent = _fm.CreateFLV("DistToOpponent");
            var opponentClose = distToOpponent.AddRightShoulderSet("OpponentClose", 10, 30, 100);
            var opponentMedium = distToOpponent.AddTriangularSet("OpponentMedium", 0, 10, 30);
            var opponentFar = distToOpponent.AddLeftShoulderSet("OpponentFar", 0, 0, 10);

            var desirability = _fm.CreateFLV("Desirability");
            var veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);

            _fm.AddRule("goalClose -> undesirable", goalClose, undesirable);
            _fm.AddRule("goalMedium -> veryDesirable", goalMedium, veryDesirable);
            _fm.AddRule("goalFar -> undesirable", goalFar, undesirable);
            _fm.AddRule("opponentFar -> undesirable", opponentFar, undesirable);
            _fm.AddRule("opponentMedium -> desirable", opponentMedium, desirable);
            _fm.AddRule("opponentClose -> veryDesirable", opponentClose, veryDesirable);
        }

        public double CalculateDistanceDesirability(double goalDist, double opponentDist)
        {
            _fm.Fuzzify("DistToGoal", goalDist);
            _fm.Fuzzify("DistToOpponent", opponentDist);
            
            //this method automatically processes the rules and defuzzifies //the inferred conclusion
            return _fm.DeFuzzify("Desirability", FuzzyModule.DefuzzifyType.MaxAv);
        }
        
        public void Render(Graphics graphics)
        {
        }
    }
}