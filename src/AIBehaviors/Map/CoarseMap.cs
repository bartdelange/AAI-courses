using System.Collections.Generic;
using AIBehaviors.Entity;
using AIBehaviors.Util;

namespace AIBehaviors.Map
{
    public class CoarseMap : BaseMap
    {
        public int Width { get; }
        public int Height { get; }
        private const int Density = 25;
        private Dictionary<Vector2D, bool> vectorList = new Dictionary<Vector2D, bool>();


        public CoarseMap(int w, int h, List<Obstacle> obstacles)
        {
            Width = w;
            Height = h;

            var v1 = new Vector2D(Density, Density);
            GenerateEdges(v1); // Flood fill

            // Run dijkstra
            Dijkstra(v1);

            // TODO create A* algorithm
            // AStar(start, target)
        }

        public void GenerateEdges(Vector2D start)
        {
            if (vectorList.TryGetValue(start, out var processed))
                return;

            vectorList.Add(start, true);

            // Next horizontal edge
            if (!(start._X + Density >= Width))
            {
                var hV = new Vector2D(start._X + Density, start._Y);
                AddEdge(start, hV, 1);
                AddEdge(hV, start, 1);
                GenerateEdges(hV);
            }

            // Next vertical edge
            if (!(start._Y + Density >= Height))
            {
                var vV = new Vector2D(start._X, start._Y + Density);
                AddEdge(start, vV, 1);
                AddEdge(vV, start, 1);
                GenerateEdges(vV);
            }

            // Next forward diagonal edge
            if (!(start._X + Density >= Width) && !(start._Y + Density >= Height))
            {
                var vV = new Vector2D(start._X + Density, start._Y + Density);
                AddEdge(start, vV, 1);
                AddEdge(vV, start, 1);
                GenerateEdges(vV);
            }

            // Next backward diagonal edge
            if (!(start._X - Density <= 0) && !(start._Y + Density >= Height))
            {
                var vV = new Vector2D(start._X - Density, start._Y + Density);
                AddEdge(start, vV, 1);
                AddEdge(vV, start, 1);
                GenerateEdges(vV);
            }
        }
    }
}