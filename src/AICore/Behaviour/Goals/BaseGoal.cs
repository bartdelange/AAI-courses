using System.Collections.Generic;
using System.Linq;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Aggregate;
using AICore.SteeringBehaviour.Util;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
{
    public abstract class BaseGoal : IGoal
    {
        protected readonly Dictionary<GoalNames, IGoal> Goals = new Dictionary<GoalNames, IGoal>();
        protected SoccerField SoccerField { get; }
        protected IPlayer Player { get; }

        public BaseGoal(IPlayer player, SoccerField soccerField)
        {
            Player = player;
            SoccerField = soccerField;
        }

        public void Add(GoalNames goalName, IGoal goal)
        {
            Goals.Add(goalName, goal);
        }

        public void Remove(GoalNames goalName)
        {
            Goals.Remove(goalName);
        }

        protected ISteeringBehaviour CreateAvoidanceEnabledSteeringBehaviour(params WeightedSteeringBehaviour[] steeringBehaviours)
        {
            var behaviours = new List<WeightedSteeringBehaviour>
            {
                new WeightedSteeringBehaviour(
                    new WallObstacleAvoidance(Player, SoccerField.Sidelines, SoccerField.Obstacles),
                    1000f
                )
            };
            behaviours.AddRange(steeringBehaviours);
            return new WeightedTruncatedRunningSumWithPrioritization(behaviours, Player.MaxSpeed);
        }

        public abstract void Enter();

        public abstract void Update(float deltaTime);

        public abstract void Leave();

        public abstract double CheckDesirability();
    }
}