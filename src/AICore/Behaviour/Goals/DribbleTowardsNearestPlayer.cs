using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Entity.Dynamic;
using AICore.FuzzyLogic;
using AICore.FuzzyLogic.FuzzyHedges;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Individual;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
{
    public class DribbleTowardsNearestPlayerWithStrategy : BaseGoal
    {
        private readonly FuzzyModule _fuzzyModule = new FuzzyModule();
        private IPlayer _nearestStriker;
        private readonly PlayerStrategy _strategy;

        public DribbleTowardsNearestPlayerWithStrategy(IPlayer player, SoccerField soccerField, PlayerStrategy strategy) : base(player, soccerField)
        {
            _strategy = strategy;
            
            var distToPlayer = _fuzzyModule.CreateFlv("DistToPlayer");
            var playerClose = distToPlayer.AddLeftShoulderSet("PlayerClose", 0, 50, 100);
            var playerMedium = distToPlayer.AddTriangularSet("PlayerMedium", 50, 100, 150);
            var playerFar = distToPlayer.AddRightShoulderSet("PlayerFar", 100, 150, 1000);

            var desirability = _fuzzyModule.CreateFlv("Desirability");
            var veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);

            _fuzzyModule.AddRule("playerClose -> veryDesirable", playerClose, veryDesirable);
            _fuzzyModule.AddRule("playerMedium -> desirable", playerMedium, desirable);
            _fuzzyModule.AddRule("playerFar -> undesirable", playerFar, undesirable);
        }

        public override void Enter()
        {
        }

        public override void Update(float deltaTime)
        {
            Player.SteeringBehaviour = CreateAvoidanceEnabledSteeringBehaviour(
                new WeightedSteeringBehaviour(new Pursuit(Player, _nearestStriker), 1f)
            );
            
            var distanceToStriker = Vector2.Distance(Player.Position, _nearestStriker.Position);
            
            if (distanceToStriker < 125 && Vector2.Dot(_nearestStriker.Position - Player.Position, Player.Position) < 0f)
            {
                SoccerField.Ball.Kick(Player, distanceToStriker);
            }
        }

        public override void Leave()
        {
        }

        public override double CheckDesirability()
        {
            if (SoccerField.Ball.Owner != Player)
            {
                return 0;
            }

            var minimalDistance = float.MaxValue;

            Player.Team.Players.ForEach(player =>
            {
                if (player.Strategy != _strategy)
                {
                    return;
                }

                var distance = Vector2.Distance(Player.Position, player.Position);
                if (minimalDistance <= distance)
                {
                    return;
                }

                // Save nearest player for later use
                _nearestStriker = player;

                minimalDistance = distance;
            });

            _fuzzyModule.Fuzzify("DistToPlayer", minimalDistance);
            return _fuzzyModule.DeFuzzify("Desirability", FuzzyModule.DefuzzifyType.MaxAv);
        }
    }
}