using System.Collections.Generic;
using AICore.Entity;
using AICore.Entity.Contracts;

namespace AICore.Behaviour.Util
{
    public class ZeroOverlap<T> where T : IEntity
    {
        private readonly MovingEntity _movingEntity;
        private readonly IEnumerable<T> _other;

        public ZeroOverlap(MovingEntity movingEntity, IEnumerable<T> other)
        {
            _movingEntity = movingEntity;
            _other = other;
        }

        public void CheckOverlap()
        {
            var enumerator = _other.GetEnumerator();

            while (enumerator.Current != null)
            {
                var entity = enumerator.Current;
                
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

                // Ensure MovingEntity won't overlap any other entity
                _movingEntity.Position = _movingEntity.Position + ((toEntity / distanceToEntity) * overlapAmount);
                
                enumerator.MoveNext();
            }
            
            enumerator.Dispose();
        }
    }
}