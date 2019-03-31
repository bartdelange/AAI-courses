using System;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.FuzzyLogic;
using AICore.Worlds;

namespace AICore.Behaviour.Goals.StrikerGoals
{
    public class ShootBallToGoal : BaseGoal
    {
        private DateTime _kickTimeout = DateTime.MinValue;

        private readonly FuzzyModule _fuzzyModule = new FuzzyModule();

        public ShootBallToGoal(IPlayer player, SoccerField soccerField) : base(player, soccerField)
        {
            var distToGoal = _fuzzyModule.CreateFlv("DistToGoal");
            var goalClose = distToGoal.AddLeftShoulderSet("GoalClose", 0, 25, 50);
            var goalMedium = distToGoal.AddTriangularSet("GoalMedium", 25, 50, 150);
            var goalFar = distToGoal.AddRightShoulderSet("GoalFar", 50, 150, 1000);

            var desirability = _fuzzyModule.CreateFlv("Desirability");
            var veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);

            _fuzzyModule.AddRule("goalClose -> veryDesirable", goalClose, veryDesirable);
            _fuzzyModule.AddRule("goalMedium -> desirable", goalMedium, desirable);
            _fuzzyModule.AddRule("goalFar -> undesirable", goalFar, undesirable);
        }

        public override void Enter()
        {
            // Remove steering behaviour
            Player.SteeringBehaviour = null;
            
            // Can't kick the ball too often
            if (_kickTimeout.Subtract(DateTime.Now).TotalMilliseconds >= 0)
            {
                return;
            }
            
            // TODO: Update heading?
            SoccerField.Ball.Kick(Player, 1000);

            _kickTimeout = DateTime.Now.AddMilliseconds(Config.KickTimeout);
        }

        public override void Update(float deltaTim)
        {
            // Add go to ball to entity
            Console.WriteLine("Update ShootBallToGoal");
        }

        public override double CheckDesirability()
        {
            // If we don't own the ball we can't kick it
            if (SoccerField.Ball.Owner != Player) return 0;

            var distanceToGoal = Vector2.Distance(Player.Position, Player.Team.Goal.Position);
            _fuzzyModule.Fuzzify("DistToGoal", distanceToGoal);
            return _fuzzyModule.DeFuzzify("Desirability", FuzzyModule.DefuzzifyType.MaxAv);
        }
    }
}