using AICore.Entity;

namespace AICore.Goals
{
    interface IGoal
    {
        void Add(IGoal goal);
        void Remove(IGoal goal);

        void Update(MovingEntity movingEntity);
    }
}
