using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Graph.Heuristics;

namespace AICore.Map
{
    public class CoarseMap : BaseMap
    {
        private const int Density = 20;

        private readonly List<Obstacle> _obstacles;
        private readonly Dictionary<Vector2, bool> _vectors = new Dictionary<Vector2, bool>();

        private readonly CoarseMapHelper _coarseMapHelper = new CoarseMapHelper();

        public CoarseMap(int w, int h, List<Obstacle> obstacles)
        {
            Width = w;
            Height = h;
            _obstacles = obstacles;

            var x0Y0 = new Vector2(Density, Density);
            GenerateEdges(x0Y0);

            var xWyH = new Vector2(Width / Density * Density, Height / Density * Density);
            GenerateEdges(xWyH);
        }

        private int Width { get; }
        private int Height { get; }

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

        public bool HasCollision(Vector2 end)
        {
            const int extraRadius = Density / 2;

            foreach (var obstacle in _obstacles)
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
            _coarseMapHelper.VisitedVertices = path.Item2;

            return path.Item1;
        }
    }

    public class VectorNotFoundException : Exception
    {
        public VectorNotFoundException(string message) : base(message)
        {
        }
    }
}