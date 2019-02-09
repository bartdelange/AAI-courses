using System.Drawing;
using AIBehaviours.Entity;
using AIBehaviours.Util;

namespace AIBehaviours.Behaviour.Group
{
    public class SeparationBehaviour : SteeringBehaviour
    {
        private Brush _brush = new SolidBrush(Color.FromArgb(25, 255, 0, 0));
        
        public SeparationBehaviour(MovingEntity movingEntity, MovingEntity target) : base(movingEntity, target)
        {
        }

        public override Vector2D Calculate(float deltaTime)
        {
            var steeringForce = new Vector2D();
            
            MovingEntity.Neighbors?.ForEach(neighbor =>
            {
                var targetDistance = MovingEntity.Pos - neighbor.Pos;

                steeringForce += targetDistance.Normalize() / targetDistance.Length();
            });

            return steeringForce;
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