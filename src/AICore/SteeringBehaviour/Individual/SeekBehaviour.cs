using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.SteeringBehaviour.Individual
{
    public class SeekBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly IMovingEntity _movingEntity;
        private readonly Vector2 _targetPosition;

        public SeekBehaviour(IMovingEntity movingEntity, Vector2 targetPosition)
        {
            _targetPosition = targetPosition;
            _movingEntity = movingEntity;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Set a velocity that will make the agent move the world target
        /// </summary>
        public Vector2 Calculate(float deltaTime)
        {
            return Vector2.Normalize(_targetPosition - _movingEntity.Position) * _movingEntity.MaxSpeed;
        }

        public void Render(Graphics graphics)
        {
            throw new System.NotImplementedException();
        }
    }
}