using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.Entity
{
    public static class EntityExtensionMethods
    {
        /// <summary>
        /// Render an entity when its visibility property is true
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="g"></param>
        public static void RenderIfVisible(this IRenderable instance, Graphics g)
        {
            if (!instance.Visible)
            {
                return;
            }

            instance.Render(g);
        }
        
        /// <summary>
        /// Checks if line between start and end arguments is intersecting with any obstacle
        /// </summary>
        /// <param name="obstacles"></param>
        /// <param name="start"></param>
        /// <param name="target"></param>
        /// <param name="margin"></param>
        /// <returns></returns>
        public static bool IsPathObstructed(
            this IEnumerable<IObstacle> obstacles, Vector2 start, Vector2 target, int margin
        )
        {
            return obstacles.Any(obstacle => obstacle.IntersectsWithLine(start, target, margin));
        }
    }
}