using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.Entity
{
    public static class EntityExtensionMethods
    {
        public static bool IsPathObstructed(
            this IEnumerable<IObstacle> obstacles,
            Vector2 start,
            Vector2 target,
            int margin
        )
        {
            return obstacles.Any(obstacle => obstacle.IntersectsWithLine(start, target, margin));
        }
    }
}