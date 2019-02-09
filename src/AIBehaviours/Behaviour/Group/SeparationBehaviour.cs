using AIBehaviours.Entity;
using AIBehaviours.Util;

namespace AIBehaviours.Behaviour.Group
{
    public class SeparationBehaviour : SteeringBehaviour
    {
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
    }
}