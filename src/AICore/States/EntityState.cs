using AIBehaviours.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AILib.States
{
    class EntityState<T>
    {
        public IState<T> CurrentState { get; private set; }
        public T Entity { get; }

        public void SetState(IState<T> nextState)
        {
            CurrentState.Leave(this);
            nextState.Enter(this);

            CurrentState = nextState;
        }

        public void Update()
        {
            CurrentState.Update(this);
        }
    }
}
