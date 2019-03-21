using System;
using AICore.Entity;

namespace AICore.Behaviour.States
{
    public class Hide : IState<Coward>
    {
        public void Enter(EntityState<Coward> state)
        {
            Console.WriteLine("Enter Hide");
        }

        public void Leave(EntityState<Coward> state)
        {
            Console.WriteLine("Leave Hide");
        }

        public void Update(EntityState<Coward> state)
        {
            Console.WriteLine("Hiding...");
            state.Entity.Strength += 1;

            if (true) state.SetState(new Patrol());
        }
    }
}