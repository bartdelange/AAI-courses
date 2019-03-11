using AICore.Entity;

namespace AICore.Goals
{
    public interface IGoal
    {
        void Add(IGoal goal);
        void Remove(IGoal goal);

        void Update(MovingEntity movingEntity);
    }
}