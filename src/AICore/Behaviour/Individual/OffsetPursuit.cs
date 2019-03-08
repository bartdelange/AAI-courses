using AICore.Entity;
using System;
using System.Drawing;
using System.Numerics;

namespace AICore.Behaviour.Individual
{
    class OffsetPursuit : SteeringBehaviour
    {
        private MovingEntity _leader;
        private Vector2 _offset;

        protected OffsetPursuit(MovingEntity movingEntity, MovingEntity leader, Vector2 offset, float weight) : base(movingEntity, weight)
        {
            _leader = leader;
            _offset = offset;
        }

        public override Vector2 Calculate(float deltaTime)
        {
            var worldOffsetPosition = MovingEntity.GetPointToWorldSpace(_offset);
            var toOffset = worldOffsetPosition - MovingEntity.Pos;

            var lookAheadTime = toOffset.Length() / (MovingEntity.MaxSpeed + _leader.Velocity.Length());
            var targetPosition = worldOffsetPosition + _leader.Velocity * lookAheadTime;

            var arriveBehaviour = new ArriveBehaviour(MovingEntity, targetPosition, weight);

            return arriveBehaviour.Calculate(deltaTime);
        }

        public override void Render(Graphics g)
        {
        }
    }
}
