using System.Drawing;
using System.Linq;
using AIBehaviours.Entity;
using AIBehaviours.Util;

namespace AIBehaviours.Behaviour.Group
{
    public class CohesionBehaviour : SteeringBehaviour
    {
        private readonly Brush _brush = new SolidBrush(Color.FromArgb(25, 255, 255, 0));

        private readonly int _radius;

        public CohesionBehaviour(MovingEntity movingEntity, MovingEntity target, double weight)
            : base(movingEntity, target, weight)
        {
            _radius = (int)(MovingEntity.Radius * 1.5);
        }

        public override Vector2D Calculate(float deltaTime)
        {
            if (MovingEntity.Neighbors.Count < 1) return new Vector2D();

            var centerOfMass = MovingEntity.Neighbors.Aggregate(
                new Vector2D(),
                (position, neighbor) => position + neighbor.Pos
            );

            var targetPosition = centerOfMass / MovingEntity.Neighbors.Count;

            return (targetPosition - MovingEntity.Pos).Normalize() * MovingEntity.MaxSpeed;
        }

        public override void Render(Graphics g)
        {
            var size = _radius * 2;

            g.FillEllipse(
                _brush,
                new Rectangle(
                    (Point) (MovingEntity.Pos - _radius),
                    new Size(size, size)
                )
            );
        }
    }
}