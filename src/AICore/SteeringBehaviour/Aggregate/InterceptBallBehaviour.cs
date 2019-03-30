using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour.Individual;

namespace AICore.SteeringBehaviour.Aggregate
{
    public class InterceptBallBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; }

        private readonly PursuitBehaviour _steeringBehaviour;

        public InterceptBallBehaviour(IPlayer player)
        {
            // _steeringBehaviour = new PursuitBehaviour(player);
        }

        public Vector2 Calculate(float deltaTime)
        {
            return Vector2.Zero;
//            return _steeringBehaviour.Calculate(deltaTime);
        }

        public void Render(Graphics graphics)
        {
        }
    }
}