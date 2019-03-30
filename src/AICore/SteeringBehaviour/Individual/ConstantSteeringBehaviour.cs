using System.Drawing;
using System.Numerics;

namespace AICore.SteeringBehaviour.Individual
{
    public class ConstantSteeringBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; }

        private readonly Vector2 _velocity;

        public ConstantSteeringBehaviour(Vector2 velocity)
        {
            _velocity = velocity;
        }

        public Vector2 Calculate(float deltaTime)
        {
            return _velocity;
        }

        public void Render(Graphics graphics)
        {
        }
    }
}