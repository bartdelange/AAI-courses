using System.Drawing;
using System.Numerics;

namespace AICore.Behaviour.Individual
{
    /// <summary>
    /// Steering behaviour that will apply a constant steering force.
    /// </summary>
    public class ConstantSteeringBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; }

        private readonly Vector2 _steeringForce;

        public ConstantSteeringBehaviour(Vector2 steeringForce)
        {
            _steeringForce = steeringForce;
        }
        
        public Vector2 Calculate(float deltaTime)
        {
            return _steeringForce;
        }
        
        public void Render(Graphics graphics)
        {
        }
    }
}