using System;
using System.Collections.Generic;
using System.Text;

namespace AILib.States
{
    interface IState<T>
    {
        void Enter(EntityState<T> state);
        void Update(EntityState<T> state);
        void Leave(EntityState<T> state);
    }
}
