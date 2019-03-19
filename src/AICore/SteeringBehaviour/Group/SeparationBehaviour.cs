using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AICore.SteeringBehaviour.Group
{
    public class SeparationBehaviour<T> : ISteeringBehaviour where T : IEntity
    {
        public bool Visible { get; set; } = true;

        private readonly MovingEntity _movingEntity;
        private readonly IEnumerable<T> _neighbours;
        
        private readonly Brush _brush = new SolidBrush(Color.FromArgb(25, Color.Red));

        public SeparationBehaviour(MovingEntity movingEntity, IEnumerable<T> neighbours)
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

            return _neighbours.Aggregate(
                new Vector2(),
                (steeringForce, neighbor) => steeringForce + (_movingEntity.Position - neighbor.Position),
                steeringForce => steeringForce / _neighbours.Count()
            );
        }

        public void Render(Graphics graphics)
        {
            var size = _movingEntity.BoundingRadius * 2;

            graphics.FillEllipse(
                _brush,
                new Rectangle(
                    _movingEntity.Position.Minus(_movingEntity.BoundingRadius).ToPoint(),
                    new Size(size, size)
                )
            );
        }
    }
}