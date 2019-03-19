using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.SteeringBehaviour.Individual
{
    public class PursuitBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; }

        private readonly IMovingEntity _movingEntity;
        private readonly IMovingEntity _target;

        public PursuitBehaviour(IMovingEntity movingEntity, IMovingEntity target)
        {
            _target = target;
            _movingEntity = movingEntity;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var toEvader = _target.Position - _movingEntity.Position;

            var lookAheadTime = toEvader.Length();

            // Add turn around time33
            const float coefficient = 0.5f;
            lookAheadTime += (Vector2.Dot(_movingEntity.Heading, Vector2.Normalize(toEvader)) - 1) * -coefficient;

            var seekBehaviour = new SeekBehaviour(
                _movingEntity,
                _target.Position + _movingEntity.Velocity * lookAheadTime
            );

            // Seek to predicted position
            return seekBehaviour.Calculate(deltaTime);
        }

        public void Render(Graphics graphics)
        {
        }
    }
}