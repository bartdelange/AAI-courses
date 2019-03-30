using AICore.Entity.Contracts;
using AICore.Worlds;

namespace AICore.Behaviour.Goals.StrikerGoals
{
    public class StrikerThink : Think
    {
        public StrikerThink(IPlayer player, SoccerField soccerField): base(player, soccerField)
        {
            ActiveGoal = new RestGoal(Player, SoccerField);
                
            Add(GoalNames.GoToBall, new GoToBall(player, soccerField));
            Add(GoalNames.DribbleToGoal, new DribbleToGoal(player, soccerField));
            Add(GoalNames.RestGoal, new RestGoal(player, soccerField));
        }
    }
}