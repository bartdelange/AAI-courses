using System.Drawing;
using System.Linq;
using AIBehaviours.Entity;
using AIBehaviours.Util;

namespace AIBehaviours.Behaviour.Group
{
    public class CohesionBehaviour : SteeringBehaviour
    {
        private readonly Brush _brush = new SolidBrush(Color.FromArgb(25, 255, 255, 0));

        public CohesionBehaviour(MovingEntity movingEntity, MovingEntity target) : base(movingEntity, target)
        {
        }

        public override Vector2D Calculate(float deltaTime)
        {
            if (MovingEntity._Neighbors.Count < 1) return new Vector2D();

            var centerOfMass = MovingEntity._Neighbors.Aggregate(
                new Vector2D(),
                (position, neighbor) => position + neighbor.Pos
            );

            var targetPosition = centerOfMass / MovingEntity._Neighbors.Count;

            return (targetPosition - MovingEntity.Pos).Normalize() * MovingEntity.MaxSpeed;
        }

        public override void Render(Graphics g)
        {
            var size = MovingEntity.Radius * 2;

            g.FillEllipse(
                _brush,
                new Rectangle(
                    (Point) (MovingEntity.Pos - MovingEntity.Radius),
                    new Size(size, size)
                )
            );
        }
    }
}