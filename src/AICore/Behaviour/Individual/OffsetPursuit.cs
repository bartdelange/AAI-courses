using System.Drawing;
using System.Numerics;
using AICore.Entity;

namespace AICore.Behaviour.Individual
{
    public class OffsetPursuit : ISteeringBehaviour
    {
        private readonly MovingEntity _leader;
        private readonly MovingEntity _movingEntity;

        private readonly Vector2 _offset;

        /// <param name="movingEntity"></param>
        /// <param name="leader"></param>
        /// <param name="offset"></param>
        public OffsetPursuit(MovingEntity movingEntity, MovingEntity leader, Vector2 offset)
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