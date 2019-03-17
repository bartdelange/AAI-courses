using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AICore.Behaviour.Group
{
    public class SeparationBehaviour<T> : ISteeringBehaviour where T : IEntity
    {
        private readonly MovingEntity _movingEntity;
        private readonly IEnumerable<T> _neighbours;
        
        private readonly Brush _brush = new SolidBrush(Color.FromArgb(25, 255, 0, 0));

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

        public void Draw(Graphics g)
        {
            const int size = MovingEntity.Radius * 2;

            g.FillEllipse(
                _brush,
                new Rectangle(
                    _movingEntity.Position.Minus(MovingEntity.Radius).ToPoint(),
                    new Size(size, size)
                )
            );
        }
    }
}