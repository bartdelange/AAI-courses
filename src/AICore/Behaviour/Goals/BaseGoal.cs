using System.Collections.Generic;
using AICore.Entity.Contracts;
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

        public abstract void Enter();

        public abstract void Update(float deltaTime);
        
        public abstract void Leave();

        public abstract double CheckDesirability();
    }
}