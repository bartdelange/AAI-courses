using System.Drawing;
using System.Numerics;

namespace AICore.SteeringBehaviour.Individual
{
    public class ConstantSteering : ISteeringBehaviour
    {
        public bool Visible { get; set; }

        private readonly Vector2 _velocity;

        public ConstantSteering(Vector2 velocity)
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