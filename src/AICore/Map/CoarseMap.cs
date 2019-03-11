using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity;
using AICore.Graph;
using AICore.Graph.Heuristics;

namespace AICore.Map
{
    public class CoarseMap : BaseMap
    {
        private const int Density = 20;
        private readonly CoarseMapHelper _coarseMapHelper = new CoarseMapHelper();

        private readonly Dictionary<Vector2, bool> _vectors = new Dictionary<Vector2, bool>();

        public CoarseMap(int w, int h, List<Obstacle> obstacles) : base(w, h, obstacles)
        {
            var topRight = new Vector2(10, 10);
            GenerateEdges(topRight);

            var workingWidth = Width - 10;
            var workingHeight = Height - 10;
            var bottomLeft = new Vector2(Width - workingWidth % Density, Height - workingHeight % Density);
            GenerateEdges(bottomLeft);
        }

        public override void Render(Graphics g, bool graphIsVisible)
        {
            base.Render(g, graphIsVisible);

            _coarseMapHelper.Draw(g);
        }

        private Vector2? HasVector(Vector2 v)
        {
            if (_vectors.ContainsKey(v)) return v;

            return null;
        }

        public override Vector2 FindClosestVertex(Vector2 position)
        {
            var closestVector = new Vector2();
            var closestLength = float.MaxValue;

            foreach (var vector in _vectors)
            {
                var currentLength = Math.Abs((vector.Key - position).LengthSquared());

                if (!(currentLength < closestLength)) continue;

                closestLength = currentLength;
                closestVector = vector.Key;
            }

        return closestVector;
    }

        public override IEnumerable<Vector2> FindPath(Vector2 start, Vector2 destination)
        {
            var closestStart = FindClosestVertex(start);
            var closestDest = FindClosestVertex(destination);

            var path = default(Tuple<IEnumerable<Vector2>, Dictionary<Vector2, Vertex<Vector2>>>);

            if (closestStart != closestDest)
                try
                {
                    path = AStar(closestStart, closestDest, new Manhattan());
                }
                catch (NoSuchElementException)
                {
                }

            var fullPath = new List<Vector2>();

            if (path != null && path.Item1.Any())
                fullPath = path.Item1.ToList();

            fullPath.Insert(0, start);
            fullPath.Add(destination);

            var smoothedPath = SmoothPath(fullPath);

            // Update CoarseMapHelper
            _coarseMapHelper.VisitedVertices = path?.Item2 ?? new Dictionary<Vector2, Vertex<Vector2>>();

            _coarseMapHelper.CurrentPath = fullPath;
            _coarseMapHelper.SmoothedPath = smoothedPath;

            return smoothedPath;
        }

        private IEnumerable<Vector2> SmoothPath(IEnumerable<Vector2> path)
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

                var stepDistance = Obstacle.MinRadius / 4;
                for (var i = 0; i * stepDistance <= Vector2.Distance(start, target); i++)
                {
                    newVector += direction * stepDistance;

                    // If we don't have a collision proceed to next interval
                    if (!HasCollision(newVector, 0)) continue;

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
        }

        #region Floodfilling

        public void GenerateEdges(Vector2 start)
        {
            if (_vectors.TryGetValue(start, out _))
                return;

            _vectors.Add(start, true);

            // Horizontal edges
            if (!(start.X + Density >= Width))
            {
                var next = new Vector2(start.X + Density, start.Y);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }

            if (!(start.X - Density <= 0))
            {
                var next = new Vector2(start.X - Density, start.Y);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }

            // Vertical edges
            if (!(start.Y + Density >= Height))
            {
                var next = new Vector2(start.X, start.Y + Density);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }

            if (!(start.Y - Density <= 0))
            {
                var next = new Vector2(start.X, start.Y - Density);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }

            // Diagonal edges
            if (!(start.X + Density >= Width) && !(start.Y + Density >= Height))
            {
                var next = new Vector2(start.X + Density, start.Y + Density);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }

            if (!(start.X - Density <= 0) && !(start.Y + Density >= Height))
            {
                var next = new Vector2(start.X - Density, start.Y + Density);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }

            if (!(start.X - Density <= 0) && !(start.Y - Density <= 0))
            {
                var next = new Vector2(start.X - Density, start.Y - Density);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }

            if (!(start.X + Density >= Width) && !(start.Y - Density <= 0))
            {
                var next = new Vector2(start.X + Density, start.Y - Density);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }
        }

        private void CreateTwoWayEdges(Vector2 start, Vector2 end)
        {
            AddEdge(start, end, Vector2.Distance(start, end));
            AddEdge(end, start, Vector2.Distance(start, end));
        }

        private bool HasCollision(Vector2 end, int extraRadius = int.MaxValue)
        {
            extraRadius = extraRadius == int.MaxValue ? Math.Max(Density, Density) / 2 : extraRadius;
            foreach (var obstacle in Obstacles)
            {
                var deltaX = obstacle.Pos.X - end.X;
                var deltaY = obstacle.Pos.Y - end.Y;

                var collisionZoneThreshold = (obstacle.Radius + extraRadius) * (obstacle.Radius + extraRadius);

                if (deltaX * deltaX + deltaY * deltaY <= collisionZoneThreshold) return true;
            }

            return false;
        }

        #endregion
    }

    public class VectorNotFoundException : Exception
    {
        public VectorNotFoundException(string message) : base(message)
        {
        }
    }
}