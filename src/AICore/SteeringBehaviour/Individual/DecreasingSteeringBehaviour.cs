using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.SteeringBehaviour.Individual
{
    /// <summary>
    /// Steering behaviour that will apply a constant (updatable) steering force.
    /// </summary>
    public class DecreasingSteeringBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly IMovingEntity _movingEntity;

        public DecreasingSteeringBehaviour(IMovingEntity entity)
        {
            _movingEntity = entity;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var velocity = _movingEntity.Heading * _movingEntity.CurrentSpeed * deltaTime;
            _movingEntity.CurrentSpeed /= 1.01f;
            if (_movingEntity.CurrentSpeed < 0) _movingEntity.CurrentSpeed = 0;
            return velocity;
        }

        public void Render(Graphics graphics)
        {
        }
    }
}