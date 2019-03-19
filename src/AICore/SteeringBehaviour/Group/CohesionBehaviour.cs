using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AICore.SteeringBehaviour.Group
{
    public class CohesionBehaviour<T> : ISteeringBehaviour where T : IEntity
    {
        public bool Visible { get; set; } = true;
        
        private readonly IMovingEntity _movingEntity;
        private readonly IEnumerable<T> _neighbours;

        private readonly Brush _brush = new SolidBrush(Color.FromArgb(25, 255, 255, 0));
        private const int Radius = 50;

        public CohesionBehaviour(IMovingEntity movingEntity, IEnumerable<T> neighbours)
        {
            _movingEntity = movingEntity;
            _neighbours = neighbours;
        }

        public Vector2 Calculate(float deltaTime)
        {
            if (!_neighbours.Any())
            {
                return new Vector2();
            }

            var centerOfMass = _neighbours.Aggregate(
                new Vector2(),
                (position, neighbor) => position + neighbor.Position
            );

            var targetPosition = centerOfMass / _neighbours.Count();

            return Vector2.Normalize(targetPosition - _movingEntity.Position) * _movingEntity.MaxSpeed;
        }

        public void Render(Graphics graphics)
        {
            const int size = Radius * 2;

            graphics.FillEllipse(
                _brush,
                new Rectangle(
                    _movingEntity.Position.Minus(Radius).ToPoint(),
                    new Size(size, size)
                )
            );
        }
    }
}