using System.Collections.Generic;
using AICore.Entity;

namespace AICore.Behaviour.Goals
{
    public abstract class BaseGoal : IGoal
    {
        private readonly List<IGoal> _goals = new List<IGoal>();

        public void Add(IGoal goal)
        {
            _goals.Add(goal);
        }

        public void Remove(IGoal goal)
        {
            _goals.Remove(goal);
        }

        public virtual void Update(MovingEntity entity)
        {
            _goals.ForEach(goal => goal.Update(entity));
        }
    }
}