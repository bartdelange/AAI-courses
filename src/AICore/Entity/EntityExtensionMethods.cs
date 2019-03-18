using System;
using System.Collections.Generic;
using System.Drawing;
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
        
        public static void Render(this IRenderable renderable, Graphics g)
        {
            if (renderable == null || !renderable.Visible)
            {
                return;
            }

            renderable.Render(g);
        }
    }
}