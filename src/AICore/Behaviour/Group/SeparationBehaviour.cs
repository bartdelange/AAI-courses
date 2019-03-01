using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity;
using AICore.Util;

namespace AICore.Behaviour.Group
{
    public class SeparationBehaviour : SteeringBehaviour
    {
        private readonly Brush _brush = new SolidBrush(Color.FromArgb(25, 255, 0, 0));

        public SeparationBehaviour(MovingEntity movingEntity, MovingEntity target, float weight)
            : base(movingEntity, target, weight)
        {
        }

        public override Vector2 Calculate(float deltaTime)
        {
            if (MovingEntity.Neighbors.Count < 1) return new Vector2();

            var force = MovingEntity.Neighbors.Aggregate(
                new Vector2(),
                (steeringForce, neighbor) => steeringForce + (MovingEntity.Pos - neighbor.Pos),
                steeringForce => steeringForce / MovingEntity.Neighbors.Count
            );

            return force;
        }

        public override void Render(Graphics g)
        {
            var size = MovingEntity.Radius * 2;

            g.FillEllipse(
                _brush,
                new Rectangle(
                    MovingEntity.Pos.Minus(MovingEntity.Radius).ToPoint(),
                    new Size(size, size)
                )
            );
        }
    }
}