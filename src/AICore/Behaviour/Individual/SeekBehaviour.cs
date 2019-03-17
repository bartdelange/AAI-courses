using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.Behaviour.Individual
{
    public class SeekBehaviour : ISteeringBehaviour
    {
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
            return Vector2.Normalize(_targetPosition - _movingEntity.Position) * (float) _movingEntity.MaxSpeed;
        }

        public void Draw(Graphics g)
        {
        }
    }
}