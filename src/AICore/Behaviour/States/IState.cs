using AICore.Entity.Contracts;

namespace AICore.Behaviour.States
{
    public interface IState<T> where T : IPlayer
    {
        void Enter(PlayerState<T> state);
        void Update(PlayerState<T> state, float deltaTime);
        void Leave(PlayerState<T> state);
    }
}