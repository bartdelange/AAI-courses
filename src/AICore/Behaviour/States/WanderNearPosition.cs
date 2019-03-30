using System.Numerics;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour.Aggregate;

namespace AICore.Behaviour.States
{
    public class WanderNearPosition : IState<IPlayer>
    {
        public void Enter(PlayerState<IPlayer> state)
        {
            state.Player.SteeringBehaviour = new WanderNearPositionBehaviour(state.Player);
        }

        public void Update(PlayerState<IPlayer> state, float deltaTime)
        {
            // Try to intercept the ball When player is near ball when:
            // - TODO Ball is near player
            // - Player is not too far away from its start position
            if (
                Vector2.Distance(state.Player.Position, state.Player.StartPosition) < 150
            )
            {
                state.SetState(new TryToInterceptBall());
            }
        }

        public void Leave(PlayerState<IPlayer> state)
        {
        }
    }
}