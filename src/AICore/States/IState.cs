namespace AICore.States
{
    internal interface IState<T>
    {
        void Enter(EntityState<T> state);
        void Update(EntityState<T> state);
        void Leave(EntityState<T> state);
    }
}