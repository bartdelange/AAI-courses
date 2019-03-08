#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity;
using AICore.Graph.Heuristics;

#endregion

namespace AICore.Map
{
    public class CoarseMap : BaseMap
    {
        private const int Density = 50;
        private readonly CoarseMapHelper _coarseMapHelper = new CoarseMapHelper();

        private readonly Dictionary<Vector2, bool> _vectors = new Dictionary<Vector2, bool>();

        public CoarseMap(int w, int h, List<Obstacle> obstacles) : base(w, h, obstacles)
        {
            var x0y0 = new Vector2(Density, Density);
            GenerateEdges(x0y0);

            var xWyH = new Vector2(Width / Density * Density, Height / Density * Density);
            GenerateEdges(xWyH);
        }

        public override void Render(Graphics g)
        {
            base.Render(g);

            foreach (var vector in _vectors)
                g.DrawEllipse(new Pen(Color.Red), vector.Key.X - 1, vector.Key.Y - 1, 3, 3);

            _coarseMapHelper.Draw(g);
        }

        private Vector2? HasVector(Vector2 v)
        {
            Console.WriteLine(v);

            if (_vectors.ContainsKey(v)) return v;

            return null;
        }

        public override Vector2 FindClosestVertex(Vector2 position)
        {
            Vector2 closestVector = new Vector2();
            float closestLength = float.MaxValue;

            foreach(var vector in _vectors)
            {
                var currentLength = Math.Abs((vector.Key - position).LengthSquared());

                if(currentLength < closestLength)
                {
                    closestLength = currentLength;
                    closestVector = vector.Key;
                }
            }

            return closestVector;
        }

        public override IEnumerable<Vector2> FindPath(Vector2 start, Vector2 destination)
        {
            var path = AStar(FindClosestVertex(start), FindClosestVertex(destination), new Manhattan());

            var fullPath = path.Item1.ToList();
            fullPath.Insert(0, start);
            fullPath.Add(destination);

            var smoothedPath = SmoothPath(fullPath);

            // Update CoarseMapHelper
            _coarseMapHelper.VisitedVertices = path.Item2;

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

                for (var i = 0; i * Obstacle.MinRadius <= Vector2.Distance(start, target); i++)
                {
                    newVector += direction * Obstacle.MinRadius;

                    // If we don't have a collision proceed to next interval
                    if (!HasCollision(newVector)) continue;

                    // We have a collision, so a path to this vector is not possible
                    pathPossible = false;
                }

                next = new Vector2(vector.X, vector.Y);

                if (!pathPossible)
                {
                    current = new Vector2(vector.X, vector.Y);
                    smoothedPath.Add(current);
                }
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

        public void CreateTwoWayEdges(Vector2 start, Vector2 end)
        {
            AddEdge(start, end, Vector2.Distance(start, end));
            AddEdge(end, start, Vector2.Distance(start, end));
        }

        protected bool HasCollision(Vector2 end)
        {
            const int extraRadius = Density / 2;

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