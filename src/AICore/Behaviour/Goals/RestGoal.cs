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
            Player.SteeringBehaviour = new WanderNearPositionBehaviour(Player, SoccerField.Sidelines, SoccerField.Obstacles);
        }

        public override void Update(float deltaTim)
        {
        }

        public override double CheckDesirability()
        {
            return 0.1d;
        }
    }
}