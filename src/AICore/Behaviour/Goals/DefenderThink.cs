using AICore.Entity.Contracts;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
{
    public class DefenderThink : Think
    {
        public DefenderThink(IPlayer player, SoccerField soccerField) : base(player, soccerField)
        {
            ActiveGoal = new RestGoal(Player, SoccerField);
            
            // Ball is close to the goal and player is close to its starting position
            Add(GoalNames.DefendGoal, new DefendGoal(player, soccerField));
            
            // Ball is close to player
            Add(GoalNames.TakeBall, new TakeBall(player, soccerField));
            
            // Player has the ball and is not near a player in its team
            // Shoot ball when very close to striker
            Add(GoalNames.DribbleTowardsNearestStriker, new DribbleTowardsNearestStriker(player, soccerField));
            
            // Player is tired or too far from its starting position
            Add(GoalNames.RestGoal, new RestGoal(player, soccerField));
        }
    }
}