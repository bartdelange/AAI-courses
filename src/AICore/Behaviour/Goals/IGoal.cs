namespace AICore.Behaviour.Goals
{
    public enum GoalNames
    {
        GoToBall,
        DribbleToGoal,
        RestGoal,
        ShootBallToGoal,
        DribbleTowardsNearestStriker,
        ShootBallTowardNearestStriker,
        ShootBallTowardNearestDefender,
        TakeBall,
        DefendGoal
    }

    public interface IGoal
    {
        void Add(GoalNames goalName, IGoal goal);
        void Remove(GoalNames goalName);
        
        void Enter();
        void Update(float deltaTim);
        void Leave();
        double CheckDesirability();
    }
}