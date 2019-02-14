namespace AICore.States
{
    internal class EntityState<T>
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