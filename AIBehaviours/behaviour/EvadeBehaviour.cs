using AIBehaviours.entity;
using AIBehaviours.util;

namespace AIBehaviours.behaviour
{
    internal class EvadeBehaviour : SteeringBehaviour
    {
        private const int Boundary = 100 * 100;

        public EvadeBehaviour(MovingEntity movingEntity, MovingEntity target) : base(movingEntity, target)
        {
        }

        public override Vector2D Calculate()
        {
            // Don't flee when target is far away
            var distance = MovingEntity.Pos.Clone().Subtract(Target.Pos).LengthSquared();

            if (distance > Boundary) return new Vector2D();

            var toPersuer = Target
                .Pos
                .Clone()
                .Subtract(MovingEntity.Pos);

            var lookAheadTime = toPersuer.Length() / (MovingEntity.MaxSpeed + Target.Velocity.Length());

            var predictedPosition = Target.Pos.Clone().Add(Target.Velocity.Clone().Multiply(lookAheadTime));

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