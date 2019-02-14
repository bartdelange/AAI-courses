using AIBehaviours.Entity;
using AILib.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AILib.States
{
    class Attack : IState<Coward>
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

            if(state.Entity.Strength < 5)
            {
                state.SetState(new Hide());
            }
        }
    }
}
