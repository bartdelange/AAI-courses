using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Static;

namespace AICore.Navigation
{
    public class PrecisePathSmoothing : IPathSmoothing
    {
        public IEnumerable<Vector2> CreateSmoothPath(INavigationMesh navigationMesh, List<Vector2> path)
        {
            if (path.Count() <= 1) return path;

            // The start is always path[0].previous
            var smoothedPath = new List<Vector2>();
            var workingPath = path.ToList();

            var current = workingPath[0];
            var next = workingPath[1];

            foreach (var vector in path)
            {
                // Skip if we are on _current_
                if (current == vector || next == vector) continue;

                // This in combination with the above if always skips the direct neighbour vector as this path is already possible
                var start = current;
                var target = vector;

                var direction = Vector2.Normalize(target - start);
                var newVector = start;
                var pathPossible = true;

                const int stepDistance = CircleObstacle.MinRadius / 4;
                const int margin = 10;
                
                for (var i = 0; i * stepDistance <= Vector2.Distance(start, target); i++)
                {
                    newVector += direction * stepDistance;

                    // If we don't have a collision proceed to next interval
                    if (!navigationMesh.Obstacles.Any(obstacle => obstacle.IntersectsWithPoint(newVector, margin)))
                    {
                        continue;
                    }

                    // We have a collision, so a path to this vector is not possible
                    pathPossible = false;
                }

                if (!pathPossible)
                {
                    current = new Vector2(next.X, next.Y);
                    smoothedPath.Add(current);
                }

                next = new Vector2(vector.X, vector.Y);
            }

            current = new Vector2(next.X, next.Y);
            smoothedPath.Add(current);
            
            return smoothedPath;
            
            /* Potentially better algorithm
            // We can't apply path smoothing on a path with one or less nodes
            if (path.Count() <= 1)
            {
                return path;
            }

            var reversedPath = new List<Vector2>(path);
            reversedPath.Reverse();

            var smoothPath = new List<Vector2>
            {
                path.First()
            };

            var previousNode = reversedPath.Last();
            foreach (var node in reversedPath)
            {
                var start = smoothPath.Last();

                // We're done with path smoothing when current node is the same as the last node in the smoothed path
                if (node == start)
                {
                    smoothPath.Add(node);

                    smoothPath.Reverse();

                    return smoothPath;
                }

                var pathIntersectsWithObstacles = navigationMesh
                    .Obstacles
                    .Any(obstacle => obstacle.IntersectsWithLine(start, node, 50));

                if (pathIntersectsWithObstacles)
                {
                    smoothPath.Add(previousNode);
                    previousNode = node;
                }
            }

            Console.WriteLine("Halloo");

            return smoothPath;
            */
        }
    }
}