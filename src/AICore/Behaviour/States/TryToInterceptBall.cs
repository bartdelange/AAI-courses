using AICore.Entity.Contracts;
using AICore.SteeringBehaviour.Aggregate;

namespace AICore.Behaviour.States
{
    public class TryToInterceptBall : IState<IPlayer>
    {
        public void Enter(PlayerState<IPlayer> state)
        {
            state.Player.SteeringBehaviour = new InterceptBallBehaviour(state.Player);
        }

        public void Update(PlayerState<IPlayer> state, float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public void Leave(PlayerState<IPlayer> state)
        {
            throw new System.NotImplementedException();
        }
    }
}