﻿using System.Numerics;
using AICore.Entity;

namespace AICore.Behaviour.Individual
{
    public class EvadeBehaviour : SteeringBehaviour
    {
        private const double ThreatRange = 100.0;

        public EvadeBehaviour(MovingEntity movingEntity, MovingEntity target, float weight)
            : base(movingEntity, target, weight)
        {
        }

        public override Vector2 Calculate(float deltaTime)
        {
            var toPursuer = Target.Pos - MovingEntity.Pos;

            // Only flee if the target is within 'panic distance'. Work in distance squared space.
            if (toPursuer.LengthSquared() > ThreatRange * ThreatRange)
                return new Vector2(0, 0);

            // The lookahead time is proportional to the distance between the pursuer
            // and the pursuer; and is inversely proportional to the sum of the
            // agents' velocities
            var lookAheadTime = toPursuer.Length() / (MovingEntity.MaxSpeed + Target.Velocity.Length());

            var predictedPosition = (Target.Pos + Target.Velocity) * lookAheadTime;

            //now flee away from predicted future position of the pursuer
            return Vector2.Normalize(MovingEntity.Pos - predictedPosition) *
                   MovingEntity.MaxSpeed -
                   MovingEntity.Velocity;
        }
    }
}