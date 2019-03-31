using AICore.Entity.Contracts;
using AICore.SteeringBehaviour.Aggregate;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
{
    public class RestGoal : BaseGoal
    {
        public RestGoal(IPlayer player, SoccerField soccerField) : base(player, soccerField)
        {
        }

        public override void Enter()
        {
            Player.MaxSpeed = Config.RestSpeed;
            Player.SteeringBehaviour = new WanderNearPositionBehaviour(Player, SoccerField.Sidelines, SoccerField.Obstacles);
        }

        public override void Update(float deltaTim)
        {
        }

        public override void Leave()
        {
            Player.MaxSpeed = Config.MaxSpeed;
        }

        public override double CheckDesirability()
        {
            // If we own the ball we can't rest 
            return SoccerField.Ball.Owner == Player ? 0d : 50d;
        }
    }
}