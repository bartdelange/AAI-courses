using AICore.Entity;

namespace AICore.Behaviour.Goals
{
    public interface IGoal
    {
        void Add(IGoal goal);
        void Remove(IGoal goal);

        void Update(MovingEntity movingEntity);
    }
}