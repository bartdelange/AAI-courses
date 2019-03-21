namespace AICore.Behaviour.States
{
    public class EntityState<T>
    {
        public EntityState(T entity)
        {
            Entity = entity;
        }

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