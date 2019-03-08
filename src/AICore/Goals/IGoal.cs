#region

using AICore.Entity;

#endregion

namespace AICore.Goals
{
    public interface IGoal
    {
        void Add(IGoal goal);
        void Remove(IGoal goal);

        void Update(MovingEntity movingEntity);
    }
}