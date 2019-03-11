using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity;
using AICore.Util;

namespace AICore.Behaviour.Group
{
    public class CohesionBehaviour : ISteeringBehaviour
    {
        private readonly Brush _brush = new SolidBrush(Color.FromArgb(25, 255, 255, 0));
        private readonly MovingEntity _movingEntity;
        private readonly int _radius;

        public CohesionBehaviour(MovingEntity movingEntity, MovingEntity target)
        {
            _movingEntity = movingEntity;

            _radius = (int) (movingEntity.Radius * 1.5);
        }

        public Vector2 Calculate(float deltaTime)
        {
            if (_movingEntity.Neighbors.Count < 1) return new Vector2();

            var centerOfMass = _movingEntity.Neighbors.Aggregate(
                new Vector2(),
                (position, neighbor) => position + neighbor.Pos
            );

            var targetPosition = centerOfMass / _movingEntity.Neighbors.Count;

            return Vector2.Normalize(targetPosition - _movingEntity.Pos) * _movingEntity.MaxSpeed;
        }

        public void Draw(Graphics g)
        {
            var size = _radius * 2;

            g.FillEllipse(
                _brush,
                new Rectangle(
                    _movingEntity.Pos.Minus(_radius).ToPoint(),
                    new Size(size, size)
                )
            );
        }
    }
}