#region

using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity;
using AICore.Util;

#endregion

namespace AICore.Behaviour.Group
{
    public class AlignmentBehaviour : SteeringBehaviour
    {
        private readonly Brush _brush = new SolidBrush(Color.FromArgb(25, 0, 0, 255));

        public AlignmentBehaviour(MovingEntity movingEntity, MovingEntity target, float weight)
            : base(movingEntity, target, weight)
        {
        }

        public override Vector2 Calculate(float deltaTime)
        {
            if (MovingEntity.Neighbors.Count < 1) return new Vector2();

            var averageHeading = MovingEntity.Neighbors.Aggregate(
                new Vector2(),
                (accumulator, neighbor) => accumulator + neighbor.Heading,

                // Get average of sum of headings
                headingSum => headingSum / MovingEntity.Neighbors.Count
            );

            return averageHeading;
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