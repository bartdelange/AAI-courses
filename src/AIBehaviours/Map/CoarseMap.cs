using System.Collections.Generic;
using AIBehaviours.Entity;
using AIBehaviours.Util;

namespace AIBehaviours.Map
{
    public class CoarseMap : BaseMap
    {
        public int Width { get; }
        public int Height { get; }
        private const int Density = 40;
        private readonly Dictionary<Vector2D, bool> _vectors = new Dictionary<Vector2D, bool>();
        private readonly List<Obstacle> _obstacles;


        public CoarseMap(int w, int h, List<Obstacle> obstacles)
        {
            Width = w;
            Height = h;
            _obstacles = obstacles;

            var x0y0 = new Vector2D(Density, Density);
            GenerateEdges(x0y0);

            var xWyH = new Vector2D((Width / Density) * Density, (Height / Density) * Density);
            GenerateEdges(xWyH);

            // Run dijkstra
            //Dijkstra(v1);

            // TODO create A* algorithm
            // AStar(start, target)
        }

        public void GenerateEdges(Vector2D start)
        {
            if (_vectors.TryGetValue(start, out var processed))
                return;

            _vectors.Add(start, true);

            // Horizontal edges
            if (!(start._X + Density >= Width))
            {
                var next = new Vector2D(start._X + Density, start._Y);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }
            if (!(start._X - Density <= 0))
            {
                var next = new Vector2D(start._X - Density, start._Y);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }

            // Vertical edges
            if (!(start._Y + Density >= Height))
            {
                var next = new Vector2D(start._X, start._Y + Density);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }
            if (!(start._Y - Density <= 0))
            {
                var next = new Vector2D(start._X, start._Y - Density);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }

            // Diagonal edges
            if (!(start._X + Density >= Width) && !(start._Y + Density >= Height))
            {
                var next = new Vector2D(start._X + Density, start._Y + Density);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }
            if (!(start._X - Density <= 0) && !(start._Y + Density >= Height))
            {
                var next = new Vector2D(start._X - Density, start._Y + Density);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }
            if (!(start._X - Density <= 0) && !(start._Y - Density <= 0))
            {
                var next = new Vector2D(start._X - Density, start._Y - Density);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }
            if (!(start._X + Density >= Width) && !(start._Y - Density <= 0))
            {
                var next = new Vector2D(start._X + Density, start._Y - Density);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }
        }

        public void CreateTwoWayEdges(Vector2D start, Vector2D end)
        {
            AddEdge(start, end, 1);
            AddEdge(end, start, 1);
        }

        public bool HasCollision(Vector2D end)
        {
            foreach (var obstacle in _obstacles)
            {
                var deltaX = obstacle.Pos._X - end._X;
                var deltaY = obstacle.Pos._Y - end._Y;
                var extraRadius = Density / 2;
                var collisionZoneThreshold = (obstacle.Radius + extraRadius) * (obstacle.Radius + extraRadius);

                if (deltaX * deltaX + deltaY * deltaY <= collisionZoneThreshold)
                    return true;
            }

            return false;
        }
    }
}