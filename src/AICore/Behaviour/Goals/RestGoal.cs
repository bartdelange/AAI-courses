using AICore.Entity.Contracts;
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
        }

        public override void Update(float deltaTim)
        {
            
        }

        public override double CheckDesirability()
        {
            return 0;
        }
    }
}