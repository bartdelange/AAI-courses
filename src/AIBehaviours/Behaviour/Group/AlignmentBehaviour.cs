using System.Drawing;
using System.Linq;
using AIBehaviours.Entity;
using AIBehaviours.Util;

namespace AIBehaviours.Behaviour.Group
{
    public class AlignmentBehaviour : SteeringBehaviour
    {
        private readonly Brush _brush = new SolidBrush(Color.FromArgb(25, 0, 0, 255));

        public AlignmentBehaviour(MovingEntity movingEntity, MovingEntity target) : base(movingEntity, target)
        {
        }

        public override Vector2D Calculate(float deltaTime)
        {
            if (MovingEntity.Neighbors.Count < 1) return new Vector2D();

            var averageHeading = MovingEntity.Neighbors.Aggregate(
                new Vector2D(),
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
                    (Point)(MovingEntity.Pos - MovingEntity.Radius), 
                    new Size(size, size)
                )
            );
        }
    }
}