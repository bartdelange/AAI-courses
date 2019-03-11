using System.Drawing;
using System.Numerics;
using AICore.Entity;

namespace AICore.Behaviour.Individual
{
    public class PursuitBehaviour : ISteeringBehaviour
    {
        private readonly MovingEntity _movingEntity;
        private readonly MovingEntity _target;

        public PursuitBehaviour(MovingEntity movingEntity, MovingEntity target)
        {
            _target = target;
            _movingEntity = movingEntity;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var toEvader = _target.Pos - _movingEntity.Pos;

            var lookAheadTime = toEvader.Length();

            // Add turn around time33
            const float coefficient = 0.5f;
            lookAheadTime += (Vector2.Dot(_movingEntity.Heading, Vector2.Normalize(toEvader)) - 1) * -coefficient;

            var seekBehaviour = new SeekBehaviour(
                _movingEntity,
                _target.Pos + _movingEntity.Velocity * lookAheadTime
            );

            // Seek to predicted position
            return seekBehaviour.Calculate(deltaTime);
        }

        public void Draw(Graphics g)
        {
        }
    }
}