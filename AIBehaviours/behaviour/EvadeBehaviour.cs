using AIBehaviours.util;

namespace AIBehaviours.behaviour
{
    internal class EvadeBehaviour : SteeringBehaviour
    {
        private const int Boundary = 100 * 100;

        public EvadeBehaviour(entity.MovingEntity movingEntity, entity.MovingEntity target) : base(movingEntity, target)
        {
        }

        public override Vector2D Calculate()
        {
            // Don't flee when target is far away
            double distance = MovingEntity.Pos.Clone().Subtract(Target.Pos).LengthSquared();

            if (distance > Boundary)
            {
                return new Vector2D();
            }

            Vector2D toPersuer = Target
                .Pos
                .Clone()
                .Subtract(MovingEntity.Pos);

            double lookAheadTime = toPersuer.Length() / (MovingEntity.MaxSpeed + Target.Velocity.Length());

            Vector2D predictedPosition = Target.Pos.Clone().Add(Target.Velocity.Clone().Multiply(lookAheadTime));

            return MovingEntity
                .Pos
                .Clone()
                .Subtract(predictedPosition)
                .Normalize()
                .Multiply(MovingEntity.MaxSpeed)
                .Subtract(MovingEntity.Velocity);
        }
    }
}
