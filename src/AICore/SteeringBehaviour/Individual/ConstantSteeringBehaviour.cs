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

        private readonly IMovingEntity _movingEntity;
        private readonly Vector2 _direction;

        public ConstantSteeringBehaviour(IMovingEntity movingEntity, Vector2 direction)
        {
            _movingEntity = movingEntity;
            _direction = direction;
        }

        public Vector2 Calculate(float deltaTime)
        {
            return _movingEntity.Position + _direction;
        }

        public void Render(Graphics graphics)
        {
        }
    }
}