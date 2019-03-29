using System.Collections.Generic;
using AICore.Entity.Contracts;

namespace AICore.SteeringBehaviour.Util
{
    public class ZeroOverlapMiddleware : IMiddleware
    {
        private readonly IMovingEntity _movingEntity;
        private readonly IEnumerable<IEntity> _other;

        public ZeroOverlapMiddleware(IMovingEntity movingEntity, IEnumerable<IEntity> other)
        {
            _movingEntity = movingEntity;
            _other = other;
        }

        public void Update(float deltaTime)
        {
            foreach (var entity in _other)
            {
                // Ignore _movingEntity
                if (entity.Equals(_movingEntity))
                {
                    continue;
                }

                var toEntity = _movingEntity.Position - entity.Position;
                var distanceToEntity = toEntity.Length();

                var overlapAmount = (_movingEntity.BoundingRadius + entity.BoundingRadius) -
                                    distanceToEntity;

                // Ignore when not overlapping
                if (overlapAmount <= 0)
                {
                    continue;
                }

                // Ensure that entity won't overlap any other entity
                _movingEntity.Position = _movingEntity.Position + (toEntity / distanceToEntity * overlapAmount);
            }
        }
    }
}