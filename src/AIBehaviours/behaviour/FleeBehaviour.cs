using AIBehaviours.entity;
using AIBehaviours.util;

namespace AIBehaviours.behaviour
{
    internal class FleeBehaviour : SteeringBehaviour
    {
        private const int Boundary = 100 * 100;

        public FleeBehaviour(MovingEntity self, MovingEntity target) : base(self, target)
        {
        }

        public override Vector2D Calculate(float deltaTime)
        {
            // Don't flee when target is far away
            var distance = MovingEntity.Pos.Clone().Subtract(Target.Pos).LengthSquared();

            if (distance > Boundary)
            {
                return new Vector2D();
            }

            return MovingEntity
                .Pos
                .Clone()
                .Subtract(Target.Pos)
                .Normalize()
                .Multiply(MovingEntity.MaxSpeed)
                .Subtract(MovingEntity.Velocity);
        }
    }
}
