using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AICore.Shapes
{
    public interface ICircle : IEntity
    {
    }

    public static class ICircleExtensionMethods
    {
        public static bool LineIntersectsWithCircle(this ICircle circle, Vector2 lineStart, Vector2 lineEnd, float margin)
        {
            var circleCenter = new Vector2(circle.Position.X, circle.Position.Y);

            var squaredRadius = circle.BoundingRadius * circle.BoundingRadius;
            var squaredMargin = margin * margin;

            return Vector2ExtensionMethods.SquaredDistanceToLine(lineStart, lineEnd, circleCenter) <
                   squaredRadius + squaredMargin;
        }

        public static bool PolyIntersectsWithCircle(this ICircle circle, List<Vector2> polygon, float margin)
        {
            var polygonLength = polygon.Count();

            for (var i = 0; i < polygonLength; i++)
            {
                var lineStart = polygon[i];
                
                // Next position in list is used to "draw" a line
                var lineEnd = polygon[(i + 1) % polygonLength];
                
                if (LineIntersectsWithCircle(circle, lineStart, lineEnd, margin))
                {
                    return true;
                }
            }

            return false;
        }
    }
}