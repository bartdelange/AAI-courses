using System.Drawing;
using System.Linq;
using AIBehaviors.Entity;
using AIBehaviors.Util;

namespace AIBehaviors.Behaviour.Group
{
    public class SeparationBehaviour : SteeringBehaviour
    {
        private readonly Brush _brush = new SolidBrush(Color.FromArgb(25, 255, 0, 0));

        public SeparationBehaviour(MovingEntity movingEntity, MovingEntity target) : base(movingEntity, target)
        {
        }

        public override Vector2D Calculate(float deltaTime)
        {
            if (MovingEntity._Neighbors.Count < 1) return new Vector2D();

            return MovingEntity._Neighbors.Aggregate(
                new Vector2D(),
                (steeringForce, neighbor) =>
                {
                    var targetDistance = MovingEntity.Pos - neighbor.Pos;
                    return steeringForce + targetDistance.Normalize() / targetDistance.Length();
                }
            );
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