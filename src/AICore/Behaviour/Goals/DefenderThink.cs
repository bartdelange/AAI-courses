using AICore.Entity.Contracts;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
{
    public class DefenderThink : Think
    {
        public DefenderThink(IPlayer player, SoccerField soccerField) : base(player, soccerField)
        {
            ActiveGoal = new RestGoal(Player, SoccerField);
            
            Add(GoalNames.GoToBall, new GoToBall(player, soccerField));
            Add(GoalNames.DribbleToNearestStriker, new DribbleToGoal(player, soccerField));
            Add(GoalNames.ShootBallToStriker, new ShootBallToGoal(player, soccerField));
            Add(GoalNames.RestGoal, new RestGoal(player, soccerField));
        }
    }
}