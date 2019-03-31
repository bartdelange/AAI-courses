using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.SteeringBehaviour.Individual
{
    public class Flee : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private const int Boundary = 100 * 100;

        private readonly IMovingEntity _movingEntity;
        private readonly IMovingEntity _target;

        public Flee(IMovingEntity movingEntity, IMovingEntity target)
        {
            _movingEntity = movingEntity;
            _target = target;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var distance = (_movingEntity.Position - _target.Position).LengthSquared();

            // Only flee if the target is within 'panic distance'.
            if (distance > Boundary)
            {
                return Vector2.Zero;
            }

            return Vector2.Normalize(_movingEntity.Position - _target.Position) * _movingEntity.MaxSpeed -
                   _movingEntity.Velocity;
        }

        public void Render(Graphics graphics)
        {
        }
    }
}