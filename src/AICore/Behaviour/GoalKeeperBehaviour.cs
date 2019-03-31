using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.FuzzyLogic;
using AICore.Model;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Individual;
using AICore.SteeringBehaviour.Util;
using AICore.Worlds;

namespace AICore.Behaviour
{
    /// <summary>
    /// Behaviour that is used by the goal keeper
    ///
    /// Rules:
    /// - Should stay near the goal                                (Fuzzy: Arrive / Wander)  
    /// - Should kick the ball away when too close to the goal     (Fuzzy: Pursuit / Ball.Kicked())
    /// - Should avoid obstacles in the field                      (ObstacleAvoidance)
    /// - Should stay within the playing field                     (WallAvoidance)
    /// - Should not move when tired                               (TiredModule)
    /// </summary>
    public class GoalKeeperBehaviour : TiredModule, ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        #region Behaviour properties
        
        private readonly IPlayer _entity;
        private readonly Team _team;
        private readonly SoccerField _soccerField;

        private readonly ISteeringBehaviour _wanderBehaviour;
        private readonly ISteeringBehaviour _targetedBehaviour;

        #endregion

        #region Fuzzy logic properties

        private readonly FuzzyModule _fmGoal = new FuzzyModule();
        private readonly FuzzyModule _fmBall = new FuzzyModule();

        #endregion
        
        public GoalKeeperBehaviour(IPlayer goalkeeper, Team team, SoccerField soccerField)
        {
            _entity = goalkeeper;
            _team = team;
            _soccerField = soccerField;

            // Wandering behaviour
            _wanderBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                new List<WeightedSteeringBehaviour>
                {
                    new WeightedSteeringBehaviour(new WallAvoidance(goalkeeper, _soccerField.Sidelines), 10f),
                    new WeightedSteeringBehaviour(new Seek(goalkeeper, goalkeeper.Position), 1f)
                },
                goalkeeper.MaxSpeed
            );

            // Running towards goal
            _targetedBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                new List<WeightedSteeringBehaviour>
                {
                    new WeightedSteeringBehaviour(new WallAvoidance(goalkeeper, _soccerField.Sidelines), 10f),
                    new WeightedSteeringBehaviour(new Arrive(goalkeeper, _entity.StartPosition), 1f)
                },
                goalkeeper.MaxSpeed
            );
            
            InitFuzzyModule();
        }

        public Vector2 Calculate(float deltaTime)
        {
            return CalculateDistanceToGoalDesirability(Vector2.Distance(_entity.StartPosition, _entity.Position)) > 50
                ? _wanderBehaviour.Calculate(deltaTime) 
                : _targetedBehaviour.Calculate(deltaTime);
        }

        public void InitFuzzyModule()
        {
            InitGoalModule();
            InitBallModule();
        }

        public void InitGoalModule()
        {
            var distToGoal = _fmGoal.CreateFlv("DistToGoal");
            var goalClose = distToGoal.AddLeftShoulderSet("GoalClose", 0, 25, 150);
            var goalMedium = distToGoal.AddTriangularSet("GoalMedium", 25, 150, 300);
            var goalFar = distToGoal.AddRightShoulderSet("GoalFar", 150, 300, 500);

            var desirability = _fmGoal.CreateFlv("Desirability");
            var veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);

            _fmGoal.AddRule("goalClose -> veryDesirable", goalClose, veryDesirable);
            _fmGoal.AddRule("goalMedium -> undesirable", goalMedium, undesirable);
            _fmGoal.AddRule("goalFar -> undesirable", goalFar, undesirable);
        }
        
        public void InitBallModule()
        {
            var distBallToGoal = _fmBall.CreateFlv("DistBallToGoal");
            var ballCloseToGoal = distBallToGoal.AddRightShoulderSet("BallCloseToGoal", 10, 30, 100);
            var ballMediumToGoal = distBallToGoal.AddTriangularSet("BallMediumToGoal", 0, 10, 30);
            var ballFarFromGoal = distBallToGoal.AddLeftShoulderSet("BallFarFromGoal", 0, 0, 10);

            var desirability = _fmBall.CreateFlv("Desirability");
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
        
        public void Render(Graphics graphics)
        {
        } 
    }
}