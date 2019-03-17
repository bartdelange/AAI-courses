using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AICore.Behaviour.Group
{
    public class AlignmentBehaviour<T> : ISteeringBehaviour where T : IMovingEntity
    {
        private readonly IMovingEntity _movingEntity;
        private readonly IEnumerable<T> _neighbours;
        
        private readonly Brush _brush = new SolidBrush(Color.FromArgb(25, 0, 0, 255));

        public AlignmentBehaviour(IMovingEntity movingEntity, IEnumerable<T> neighbours)
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
                (accumulator, neighbor) => accumulator + neighbor.Heading,

                // Get average of sum of headings
                headingSum => headingSum / _neighbours.Count()
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