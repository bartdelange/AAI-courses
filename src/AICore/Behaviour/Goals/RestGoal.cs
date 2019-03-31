using System;
using System.Linq;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Entity.Static;
using AICore.Graph.PathFinding;
using AICore.Model;
using AICore.Navigation;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Individual;
using AICore.Util;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
{
    public class RestGoal : BaseGoal
    {
        private PathFollowing _pathFollowing;
        
        public RestGoal(IPlayer player, SoccerField soccerField) : base(player, soccerField)
        {
        }

        public override void Enter()
        {
            Player.MaxSpeed = Config.RestSpeed;
        }

        public override void Update(float deltaTim)
        {   
            // if we are not done yet, do nothing
            if (_pathFollowing != null && Vector2.Distance(Player.Position, _pathFollowing.FinalWaypoint) > 10)
            {
                return;
            }
            
            var randomPos = GetValidRandomPosition();
            var path = SoccerField.Navigation.FindPath(Player.Position, randomPos, new AStar<Vector2>(), new PrecisePathSmoothing());
            _pathFollowing = new PathFollowing(path, Player);

            Player.SteeringBehaviour = _pathFollowing;
        }
        
        private Vector2 GetValidRandomPosition()
        {
            var random = Vector2ExtensionMethods.GetRandom(new Bounds(new Vector2(-100), new Vector2(100)));
            var newPos = Player.StartPosition + random;
            
            
            // Check it is inside bounds
            if (SoccerField.Sidelines.Any(w => w.IntersectsWithLine(Player.StartPosition, newPos)))
            {
                newPos = GetValidRandomPosition();
            }

            // Check if obstacle has avoidance
            if (SoccerField.Obstacles.Any(o => o.IntersectsWithPoint(newPos)))
            {
                newPos = GetValidRandomPosition();
            }

            // It can be visited, return it
            return newPos;
        } 

        public override void Leave()
        {
            Player.MaxSpeed = Config.MaxSpeed;
        }

        public override double CheckDesirability()
        {
            // If we own the ball we can't rest 
            return SoccerField.Ball.Owner == Player ? 0d : 50d;
        }
    }
}