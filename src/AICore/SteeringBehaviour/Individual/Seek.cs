using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AICore.SteeringBehaviour.Individual
{
    public class Seek : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly IMovingEntity _movingEntity;
        private readonly Vector2 _targetPosition;

        public Seek(IMovingEntity movingEntity, Vector2 targetPosition)
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
            var rectangle = new Rectangle(
                _movingEntity.Position.Minus(2).ToPoint(),
                new Size(3, 3)
            );
            
            graphics.FillEllipse(Brushes.Red, rectangle);
        }
    }
}