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
        private readonly Dictionary<Vector2, bool> _vectors = new Dictionary<Vector2, bool>();
        private readonly CoarseMapHelper _coarseMapHelper = new CoarseMapHelper();

        public CoarseMap(int w, int h, List<Obstacle> obstacles) : base (w, h, obstacles)
        {
            var x0y0 = new Vector2(Density, Density);
            GenerateEdges(x0y0);

            var xWyH = new Vector2(Width / Density * Density, Height / Density * Density);
            GenerateEdges(xWyH);
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

                if (deltaX * deltaX + deltaY * deltaY <= collisionZoneThreshold)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        public override void Render(Graphics g)
        {
            base.Render(g);
            
            foreach (var vector in _vectors)
            {
                g.DrawEllipse(new Pen(Color.Red), vector.Key.X - 1, vector.Key.Y - 1, 3, 3);
            }

            _coarseMapHelper.Draw(g);
        }

        public override Vector2 FindClosestVertex(Vector2 position)
        {
            var roundedPosition = new Vector2(
                (float) Math.Round(position.X / Density, MidpointRounding.AwayFromZero) * Density,
                (float) Math.Round(position.Y / Density, MidpointRounding.AwayFromZero) * Density
            );

            // STEPS MUST BE EVEN!
            var steps = 10;

            Vector2 currentVector;

            // Lazy search around till we find one (do this [steps] times
            for (var i = 0; i <= steps; i++)
            {
                for (var j = 0; j <= steps; j++)
                {
                    var currI = i % 2 == 0 ? i : -(i - 1);
                    var currJ = j % 2 == 0 ? j : -(j - 1);
                    
                    currentVector = roundedPosition + new Vector2(Density * currI, Density * currJ);

                    if (_vectors.ContainsKey(currentVector))
                    {
                        return currentVector;
                    }
                }
            }

            throw new VectorNotFoundException("No vector found at or close to given x and y in CoarseMap");
        }

        public override IEnumerable<Vector2> FindPath(Vector2 start, Vector2 destination)
        {
            var path = AStar(FindClosestVertex(start), FindClosestVertex(destination), new Manhattan());
            
            // Update CoarseMapHelper
            _coarseMapHelper.CurrentPath = path.Item1;
            _coarseMapHelper.SmoothedPath = SmoothPath(path.Item1);
            _coarseMapHelper.VisitedVertices = path.Item2;

            return _coarseMapHelper.SmoothedPath;
        }
        
        
        private IEnumerable<Vector2> SmoothPath(IEnumerable<Vector2> path)
        {
            // The start is always path[0].previous
            var smoothedPath = new List<Vector2>();
            var workingPath = path.ToList();
            var current = workingPath[0];
            var next = workingPath[1];
            
            foreach(var vector in path) {
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
    }

    public class VectorNotFoundException : Exception
    {
        public VectorNotFoundException(string message) : base(message)
        {
        }
    }
}