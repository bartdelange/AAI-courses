using AICore.Entity.Contracts;

namespace AICore.Behaviour.States
{
    public class PlayerState<T> where T : IPlayer
    {
        public IState<T> CurrentState { get; private set; }
        
        public T Player { get; }

        public PlayerState(T entity)
        {
            Player = entity;
        }

        public void SetState(IState<T> nextState)
        {
            CurrentState.Leave(this);
            nextState.Enter(this);

            CurrentState = nextState;
        }

        public void Update(float deltaTime)
        {
            CurrentState.Update(this, deltaTime);
        }
    }
}