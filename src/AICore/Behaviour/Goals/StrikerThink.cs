using AICore.Entity.Contracts;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
{
    public class StrikerThink : Think
    {
        public StrikerThink(IPlayer player, SoccerField soccerField): base(player, soccerField)
        {
            ActiveGoal = new RestGoal(Player, SoccerField);
            ActiveGoal.Enter();
                
            // Ball is close to the player and the player is close to its starting position
            Add(GoalNames.GoToBall, new GoToBall(player, soccerField));
            
            // The player has the ball
            Add(GoalNames.DribbleToGoal, new DribbleToGoal(player, soccerField));
            
            // Shoot ball towards the goal when very close to it
            Add(GoalNames.ShootBallToGoal, new ShootBallToGoal(player, soccerField));
            
            // Player is tired or too far from its starting position
            Add(GoalNames.RestGoal, new RestGoal(player, soccerField));
        }
    }
}