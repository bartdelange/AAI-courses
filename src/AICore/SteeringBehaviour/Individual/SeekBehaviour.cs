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
            var targetPosition = _targetPosition - _movingEntity.Position;

            // Can't divide by zero so we just return Vector2.Zero
            if (targetPosition == Vector2.Zero)
                return Vector2.Zero;
            
            return Vector2.Normalize(targetPosition) * _movingEntity.MaxSpeed;
        }

        public void Render(Graphics graphics)
        {
            throw new System.NotImplementedException();
        }
    }
}