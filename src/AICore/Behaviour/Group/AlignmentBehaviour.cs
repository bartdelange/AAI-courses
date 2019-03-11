using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity;
using AICore.Util;

namespace AICore.Behaviour.Group
{
    public class AlignmentBehaviour : ISteeringBehaviour
    {
        private readonly Brush _brush = new SolidBrush(Color.FromArgb(25, 0, 0, 255));
        private readonly MovingEntity _movingEntity;

        public AlignmentBehaviour(MovingEntity movingEntity)
        {
            _movingEntity = movingEntity;
        }

        public Vector2 Calculate(float deltaTime)
        {
            if (_movingEntity.Neighbors.Count < 1) return new Vector2();

            return _movingEntity.Neighbors.Aggregate(
                new Vector2(),
                (accumulator, neighbor) => accumulator + neighbor.Heading,

                // Get average of sum of headings
                headingSum => headingSum / _movingEntity.Neighbors.Count
            );
        }

        public void Draw(Graphics g)
        {
            var size = _movingEntity.Radius * 2;

            g.FillEllipse(
                _brush,
                new Rectangle(
                    _movingEntity.Pos.Minus(_movingEntity.Radius).ToPoint(),
                    new Size(size, size)
                )
            );
        }
    }
}