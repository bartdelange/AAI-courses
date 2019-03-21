using System;
using AICore.Entity;

namespace AICore.Behaviour.States
{
    public class Attack : IState<Coward>
    {
        public void Enter(EntityState<Coward> state)
        {
            Console.WriteLine("Enter Attack");
        }

        public void Leave(EntityState<Coward> state)
        {
            Console.WriteLine("Leave Attack");
        }

        public void Update(EntityState<Coward> state)
        {
            Console.WriteLine("Attacking...");
            state.Entity.Strength -= 1;

            if (state.Entity.Strength < 5) state.SetState(new Hide());
        }
    }
}