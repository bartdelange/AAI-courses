using AICore.Entity;
using System.Collections.Generic;

namespace AICore.Goals
{
    abstract class BaseGoal : IGoal
    {
        private List<IGoal> _goals = new List<IGoal>();

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
