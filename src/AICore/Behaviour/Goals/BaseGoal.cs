using System.Collections.Generic;
using AICore.Entity.Contracts;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
{
    
    public abstract class BaseGoal : IGoal
    {
        protected readonly Dictionary<GoalNames, IGoal> _goals = new Dictionary<GoalNames, IGoal>();
        protected SoccerField SoccerField { get; }
        protected IPlayer Player { get; }

        public BaseGoal(IPlayer player, SoccerField soccerField)
        {
            Player = player;
            SoccerField = soccerField;
        }

        public void Add(GoalNames goalName, IGoal goal)
        {
            _goals.Add(goalName, goal);
        }

        public void Remove(GoalNames goalName)
        {
            _goals.Remove(goalName);
        }

        public abstract void Activate();

        public virtual void Update(IPlayer player)
        {
        }

        public abstract double CheckDesirability();
    }
}