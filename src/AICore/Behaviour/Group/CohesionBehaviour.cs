using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity;
using AICore.Util;

namespace AICore.Behaviour.Group
{
    public class CohesionBehaviour : SteeringBehaviour
    {
        private readonly Brush _brush = new SolidBrush(Color.FromArgb(25, 255, 255, 0));

        private readonly int _radius;

        public CohesionBehaviour(MovingEntity movingEntity, MovingEntity target, float weight)
            : base(movingEntity, target, weight)
        {
            _radius = (int) (MovingEntity.Radius * 1.5);
        }

        public override Vector2 Calculate(float deltaTime)
        {
            if (MovingEntity.Neighbors.Count < 1) return new Vector2();

            var centerOfMass = MovingEntity.Neighbors.Aggregate(
                new Vector2(),
                (position, neighbor) => position + neighbor.Pos
            );

            var targetPosition = centerOfMass / MovingEntity.Neighbors.Count;

            return Vector2.Normalize(targetPosition - MovingEntity.Pos) * MovingEntity.MaxSpeed;
        }

        public override void Render(Graphics g)
        {
            var size = _radius * 2;

            g.FillEllipse(
                _brush,
                new Rectangle(
                    MovingEntity.Pos.Minus(_radius).ToPoint(),
                    new Size(size, size)
                )
            );
        }
    }
}