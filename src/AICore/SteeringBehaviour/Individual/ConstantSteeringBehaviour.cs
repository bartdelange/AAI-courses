using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.SteeringBehaviour.Individual
{
    /// <summary>
    /// Steering behaviour that will apply a constant steering force.
    /// </summary>
    public class ConstantSteeringBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; }

        private readonly Vector2 _direction;

        public ConstantSteeringBehaviour(Vector2 direction)
        {
            _direction = direction;
        }

        public Vector2 Calculate(float deltaTime)
        {
            return _direction;
        }

        public void Render(Graphics graphics)
        {
        }
    }
}