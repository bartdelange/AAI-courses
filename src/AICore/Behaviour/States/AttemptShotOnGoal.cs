using System;
using AICore.Entity.Contracts;

namespace AICore.Behaviour.States
{
    public class AttemptShotAtGoal : IState<IPlayer>
    {
        public void Enter(PlayerState<IPlayer> state)
        {
            Console.WriteLine("Enter Attack");
        }

        public void Leave(PlayerState<IPlayer> state)
        {
            Console.WriteLine("Leave Attack");
        }

        public void Update(PlayerState<IPlayer> state, float deltaTime)
        {
            Console.WriteLine("Attacking...");
        }
    }
}