using AICore.Entity.Contracts;
using AICore.Entity.Dynamic;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
{
    public class GoalkeeperThink : Think
    {
        public GoalkeeperThink(IPlayer player, SoccerField soccerField) : base(player, soccerField)
        {
            ActiveGoal = new StayGoal(Player, SoccerField);
            ActiveGoal.Enter();
            
            // Ball is close to the goal and player is close to its starting position
            Add(GoalNames.DefendGoal, new DefendGoalOnLine(player, soccerField));
            
            // Ball is close to player
            Add(GoalNames.TakeBall, new TakeBall(player, soccerField));
            
            // Player has the ball and is not near a player in its team
            // Shoot ball when very close to striker
            Add(GoalNames.DribbleTowardsNearestStriker, new DribbleTowardsNearestPlayerWithStrategy(player, soccerField, PlayerStrategy.Defender));
            
            // Player is tired or too far from its starting position
            Add(GoalNames.RestGoal, new StayGoal(player, soccerField));
        }
    }
}