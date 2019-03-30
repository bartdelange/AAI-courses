using AICore.Entity.Contracts;

namespace AICore.Behaviour.Goals
{
    public enum GoalNames
    {
        GoToBall,
        DribbleToGoal
    }

    public interface IGoal
    {
        void Add(GoalNames goalName, IGoal goal);
        void Remove(GoalNames goalName);
        
        void Activate();
        void Update(IPlayer player);
        double CheckDesirability();
    }
}