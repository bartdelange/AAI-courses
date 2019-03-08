#region

using System.Drawing;
using System.Numerics;
using AICore.Entity;

#endregion

namespace AICore.Behaviour.Individual
{
    public class OffsetPursuit : ISteeringBehaviour
    {
        private readonly MovingEntity _leader;
        private readonly MovingEntity _movingEntity;

        private readonly Vector2 _offset;

        protected OffsetPursuit(MovingEntity movingEntity, MovingEntity leader, Vector2 offset, float weight)
        {
            _leader = leader;
            _offset = offset;
            _movingEntity = movingEntity;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var worldOffsetPosition = _movingEntity.GetPointToWorldSpace(_offset);
            var toOffset = worldOffsetPosition - _movingEntity.Pos;

            var lookAheadTime = toOffset.Length() / (_movingEntity.MaxSpeed + _leader.Velocity.Length());
            var targetPosition = worldOffsetPosition + _leader.Velocity * lookAheadTime;

            var arriveBehaviour = new ArriveBehaviour(_movingEntity, targetPosition);

            return arriveBehaviour.Calculate(deltaTime);
        }

        public void Draw(Graphics g)
        {
        }
    }
}