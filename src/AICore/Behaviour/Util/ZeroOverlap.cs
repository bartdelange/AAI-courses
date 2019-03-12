using System.Collections.Generic;
using AICore.Entity;

namespace AICore.Behaviour.Util
{
    public class ZeroOverlap
    {
        private readonly MovingEntity _movingEntity;
        private readonly IEnumerable<BaseGameEntity> _other;

        public ZeroOverlap(MovingEntity movingEntity, IEnumerable<BaseGameEntity> other)
        {
            _movingEntity = movingEntity;
            _other = other;
        }

        public void CheckOverlap()
        {
            var enumerator = _other.GetEnumerator();

            while (enumerator.Current != null)
            {
                // Ignore _movingEntity
                if (enumerator.Current == _movingEntity)
                {
                    continue;
                }

                var toEntity = _movingEntity.Pos - enumerator.Current.Pos;
                var distanceToEntity = toEntity.Length();

                var overlapAmount = (BaseGameEntity.BoundingRadius + BaseGameEntity.BoundingRadius) -
                                    distanceToEntity;

                // Ignore when not overlapping
                if (overlapAmount <= 0)
                {
                    continue;
                }

                // Ensure MovingEntity won't overlap any other BaseGameEntity
                _movingEntity.Pos = _movingEntity.Pos + ((toEntity / distanceToEntity) * overlapAmount);
                
                enumerator.MoveNext();
            }
            
            enumerator.Dispose();
        }
    }
}