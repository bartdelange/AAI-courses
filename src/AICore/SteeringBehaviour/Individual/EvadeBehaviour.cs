using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.SteeringBehaviour.Individual
{
    public class EvadeBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; }

        private const double ThreatRange = 100.0;

        private readonly IMovingEntity _movingEntity;
        private readonly IMovingEntity _target;

        public EvadeBehaviour(IMovingEntity movingEntity, IMovingEntity target)
        {
            _movingEntity = movingEntity;
            _target = target;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var toPursuer = _target.Position - _movingEntity.Position;

            // Only flee if the target is within 'panic distance'. Work in distance squared space.
            if (toPursuer.LengthSquared() > ThreatRange * ThreatRange)
                return new Vector2(0, 0);

            // The lookahead time is proportional to the distance between the pursuer
            // and the pursuer; and is inversely proportional to the sum of the
            // agents' velocities
            var lookAheadTime = toPursuer.Length() / (_movingEntity.MaxSpeed + _target.Velocity.Length());

            var predictedPosition = (_target.Position + _target.Velocity) * lookAheadTime;

            //now flee away from predicted future position of the pursuer
            return Vector2.Normalize(_movingEntity.Position - predictedPosition) * _movingEntity.MaxSpeed
                   - _movingEntity.Velocity;
        }

        public void Render(Graphics graphics)
        {
        }
    }
}