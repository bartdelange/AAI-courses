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
            var distance = MovingEntity
                .Pos
                .Clone()
                .Subtract(Target.Pos)
                .LengthSquared();

            // Only flee if the target is within 'panic distance'. Work in distance squared space.
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
