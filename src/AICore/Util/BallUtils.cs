using System.Collections.Generic;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.Util
{
    public static class BallUtils
    {
        public static bool CheckForCollisions(
            IMovingEntity movingEntity, 
            IEnumerable<IWall> walls, 
            out Vector2 normal
        )
        {
            var feelers = CreateFeelers(movingEntity, 5);

            IWall closestWall = null;
            double? closestDistance = null;

            foreach (var feeler in feelers)
            {
                foreach (var wall in walls)
                {
                    if (!wall.IntersectsWithLine(
                        movingEntity.Position,
                        feeler,
                        out var distance,
                        out var intersectPoint
                    ))
                        continue;

                    // Ignore it intersection if distance is longer than previous feeler
                    if (distance >= closestDistance) continue;

                    closestDistance = distance;
                    closestWall = wall;
                }
            }

            if (closestWall == null)
            {
                normal = Vector2.Zero;                
                return false;
            }

            var n = Vector2.Normalize(closestWall.Normal);
            var d = Vector2.Normalize(movingEntity.Heading);
            
            // Calculate normal of collision
            normal = d - 2 * Vector2.Dot(d, n) * n;
            return true;
        }

        /// <summary>
        /// Creates the antenna utilized by WallAvoidance
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Vector2> CreateFeelers(IMovingEntity movingEntity, float feelerLength)
        {
            var speed = movingEntity.Velocity.Length();
            
            var sideFeelerLength = feelerLength / 2;
            var feelers = new Vector2[1];

            // Forward pointing feeler
            // Left pointing feeler
            feelers[0] = movingEntity.Position +
                         sideFeelerLength * movingEntity.Heading.RotateAroundOrigin(35) * speed;

            // Right pointing feeler

            return feelers;
        }
    }
}