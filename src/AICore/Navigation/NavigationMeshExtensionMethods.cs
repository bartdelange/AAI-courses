using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.Navigation
{
    public static class NavigationExtensionMethods
    {
        /// <summary>
        /// Gets the vertex that is closest to the given position
        /// </summary>
        /// <param name="navigationMesh"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static Vector2 FindVertex(this INavigationMesh navigationMesh, Vector2 position)
        {
            var closestVector = new Vector2();
            var closestLength = float.MaxValue;

            foreach (var vector in navigationMesh.Mesh.Vertices)
            {
                var currentLength = Math.Abs((vector.Key - position).LengthSquared());

                if (!(currentLength < closestLength))
                {
                    continue;
                }

                closestLength = currentLength;
                closestVector = vector.Key;
            }

            return closestVector;
        }
    }
}