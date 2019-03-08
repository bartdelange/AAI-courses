#region

using System.Drawing;
using System.Numerics;
using AICore.Entity;

#endregion

namespace AICore.Behaviour.Individual
{
    public class OffsetPursuit : SteeringBehaviour
    {
        private readonly MovingEntity _leader;
        private readonly Vector2 _offset;

        protected OffsetPursuit(MovingEntity movingEntity, MovingEntity leader, Vector2 offset, float weight) : base(
            movingEntity, weight)
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

            var arriveBehaviour = new ArriveBehaviour(MovingEntity, targetPosition, Weight);

            return arriveBehaviour.Calculate(deltaTime);
        }

        public override void Render(Graphics g)
        {
        }
    }
}