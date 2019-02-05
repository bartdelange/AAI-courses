using AIBehaviours.entity;
using AIBehaviours.util;

namespace AIBehaviours.behaviour
{
    internal class EvadeBehaviour : SteeringBehaviour
    {
        private const double ThreatRange = 100.0;

        public EvadeBehaviour(MovingEntity movingEntity, MovingEntity target) : base(movingEntity, target)
        {
        }

        public override Vector2D Calculate(float deltaTime)
        {
            var toPursuer = Target.Pos.Clone().Subtract(MovingEntity.Pos);

            // Only flee if the target is within 'panic distance'. Work in distance squared space.
            if (toPursuer.LengthSquared() > ThreatRange * ThreatRange) return new Vector2D(0, 0);

            // The lookahead time is proportional to the distance between the pursuer
            // and the pursuer; and is inversely proportional to the sum of the
            // agents' velocities
            var lookAheadTime = toPursuer.Length() / (MovingEntity.MaxSpeed + Target.Velocity.Length());

            var predictedPosition = Target
                .Pos
                .Clone()
                .Add(Target.Velocity.Clone().Multiply(lookAheadTime));

            //now flee away from predicted future position of the pursuer
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