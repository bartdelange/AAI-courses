using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;

namespace AICore.SteeringBehaviour.Individual
{
    public class OffsetPursuit : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly IMovingEntity _leader;
        private readonly IMovingEntity _movingEntity;

        private readonly Vector2 _offset;

        /// <param name="movingEntity"></param>
        /// <param name="leader"></param>
        /// <param name="offset"></param>
        public OffsetPursuit(IMovingEntity movingEntity, IMovingEntity leader, Vector2 offset)
        {
            _leader = leader;
            _offset = offset;
            _movingEntity = movingEntity;
        }

        public Vector2 Calculate(float deltaTime)
        {
            var worldOffsetPosition = _leader.GetPointToWorldSpace(_offset);
            var toOffset = worldOffsetPosition - _movingEntity.Position;

            var lookAheadTime = toOffset.Length() / (_movingEntity.MaxSpeed + _leader.Velocity.Length());
            var targetPosition = worldOffsetPosition + (_leader.Velocity * lookAheadTime);

            var arriveBehaviour = new ArriveBehaviour(_movingEntity, targetPosition);

            return arriveBehaviour.Calculate(deltaTime);
        }

        public void Render(Graphics graphics)
        {
        }
    }
}