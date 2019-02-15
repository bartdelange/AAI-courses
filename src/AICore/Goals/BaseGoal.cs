using AICore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
