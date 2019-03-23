using System;
using AICore.Entity.Contracts;

namespace AICore.Behaviour.States
{
    public class Rest : IState<IPlayer>
    {
        public void Enter(PlayerState<IPlayer> state)
        {
            Console.WriteLine("Enter Rest");
        }

        public void Leave(PlayerState<IPlayer> state)
        {
            Console.WriteLine("Leave Rest");
        }

        public void Update(PlayerState<IPlayer> state, float deltaTime)
        {
            Console.WriteLine("Resting...");
            state.Player.Energy += 1;

            if (state.Player.Energy >= state.Player.MaxEnergy)
            {
                state.SetState(new WanderNearPosition());
            }
        }
    }
}