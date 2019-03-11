using System.Drawing;
using System.Numerics;
using AICore.Entity;

namespace AICore.Behaviour.Individual
{
    public class FleeBehaviour : ISteeringBehaviour
    {
        private const int Boundary = 100 * 100;

        private readonly MovingEntity _movingEntity;
        private readonly MovingEntity _target;

        public FleeBehaviour(MovingEntity movingEntity, MovingEntity target)
        {
            _movingEntity = movingEntity;
            _target = target;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var distance = (_movingEntity.Pos - _target.Pos).LengthSquared();

            // Only flee if the target is within 'panic distance'.
            if (distance > Boundary) return new Vector2();

            return Vector2.Normalize(_movingEntity.Pos - _target.Pos) * _movingEntity.MaxSpeed - _movingEntity.Velocity;
        }

        public void Draw(Graphics g)
        {
        }
    }
}