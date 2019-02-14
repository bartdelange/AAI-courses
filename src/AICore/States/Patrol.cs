using System;
using AICore.Entity;

namespace AICore.States
{
    internal class Patrol : IState<Coward>
    {
        public void Enter(EntityState<Coward> state)
        {
            Console.WriteLine("Enter Patrol");
        }

        public void Leave(EntityState<Coward> state)
        {
            Console.WriteLine("Leave Patrol");
        }

        public void Update(EntityState<Coward> state)
        {
            Console.WriteLine("Patrolling...");
            state.Entity.Strength += 1;

            if (state.Entity.Strength > 10) // && enemyClose
                state.SetState(new Attack());
        }
    }
}