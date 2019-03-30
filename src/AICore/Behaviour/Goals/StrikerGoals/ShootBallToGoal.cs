using System;
using AICore.Entity.Contracts;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
{
    public class ShootBallToGoal : BaseGoal
    {
        public ShootBallToGoal(IPlayer player, SoccerField soccerField): base(player, soccerField)
        {
            // TODO: Tweak this speed
            soccerField.Ball.Kick(player, soccerField.Ball.MaxSpeed);
        }

        public override void Activate()
        {
            throw new NotImplementedException();
        }

        public override void Update(IPlayer player)
        {
            // Add go to ball to entity
            Console.WriteLine("Update ShootBallToGoal");

            base.Update(player);
        }

        public override double CheckDesirability()
        {
            return 0.0d;
        }
    }
}