using System;
using System.Collections.Generic;
using System.Drawing;
using AICore.Entity;
using AICore.Graph;
using AICore.Util;

namespace AICore.Map
{
    public class CoarseMap : BaseMap
    {
        private const int Density = 20;
        private readonly List<Obstacle> _obstacles;
        private readonly Dictionary<Vector2D, bool> _vectors = new Dictionary<Vector2D, bool>();
        private readonly IEnumerable<Vertex<Vector2D>> _examplePath;


        private readonly Brush _brushStart = new SolidBrush(Color.FromArgb(128, Color.Cyan));
        private readonly Brush _brushTarget = new SolidBrush(Color.FromArgb(128, Color.Red));
        private readonly Brush _brushVisited = new SolidBrush(Color.FromArgb(128, Color.DarkGreen));
        private readonly Brush _brushNotVisited = new SolidBrush(Color.FromArgb(128, Color.RoyalBlue));
        private readonly Pen _pen = new Pen(Color.DeepPink, 2);

        private readonly Vector2D _start;
        private readonly Vector2D _target;

        public CoarseMap(int w, int h, List<Obstacle> obstacles)
        {
            Width = w;
            Height = h;
            _obstacles = obstacles;

            var x0y0 = new Vector2D(Density, Density);
            GenerateEdges(x0y0);

            var xWyH = new Vector2D(Width / Density * Density, Height / Density * Density);
            GenerateEdges(xWyH);

            _start = x0y0;
            _target = xWyH;

            _examplePath = AStar(_start, _target, new Manhattan());
        }

        public int Width { get; }
        public int Height { get; }

        #region Floodfilling
        public void GenerateEdges(Vector2D start)
        {
            if (_vectors.TryGetValue(start, out var processed))
                return;

            _vectors.Add(start, true);

            // Horizontal edges
            if (!(start.X + Density >= Width))
            {
                var next = new Vector2D(start.X + Density, start.Y);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }

            if (!(start.X - Density <= 0))
            {
                var next = new Vector2D(start.X - Density, start.Y);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }

            // Vertical edges
            if (!(start.Y + Density >= Height))
            {
                var next = new Vector2D(start.X, start.Y + Density);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }

            if (!(start.Y - Density <= 0))
            {
                var next = new Vector2D(start.X, start.Y - Density);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }

            // Diagonal edges
            if (!(start.X + Density >= Width) && !(start.Y + Density >= Height))
            {
                var next = new Vector2D(start.X + Density, start.Y + Density);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }

            if (!(start.X - Density <= 0) && !(start.Y + Density >= Height))
            {
                var next = new Vector2D(start.X - Density, start.Y + Density);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }

            if (!(start.X - Density <= 0) && !(start.Y - Density <= 0))
            {
                var next = new Vector2D(start.X - Density, start.Y - Density);

                if (!HasCollision(next))
                {
                    CreateTwoWayEdges(start, next);
                    GenerateEdges(next);
                }
            }

            if (!(start.X + Density >= Width) && !(start.Y - Density <= 0))
            {
                var next = new Vector2D(start.X + Density, start.Y - Density);

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
                var deltaX = obstacle.Pos.X - end.X;
                var deltaY = obstacle.Pos.Y - end.Y;
                var extraRadius = Density / 2;
                var collisionZoneThreshold = (obstacle.Radius + extraRadius) * (obstacle.Radius + extraRadius);

                if (deltaX * deltaX + deltaY * deltaY <= collisionZoneThreshold)
                    return true;
            }

            return false;
        }
        #endregion

        public override void Render(Graphics g)
        {
            base.Render(g);

            foreach (var edge in SearchedVertexMap)
            {
                g.FillEllipse(edge.Value.Visited ? _brushVisited : _brushNotVisited,
                    new Rectangle((Point) (edge.Value.Data - 5), new Size(10, 10)));
            }
            
            g.FillEllipse(_brushTarget,
                new Rectangle((Point) (_target - 5), new Size(10, 10)));
            g.FillEllipse(_brushStart,
                new Rectangle((Point) (_start - 5), new Size(10, 10)));


            foreach (var vertex in _examplePath)
            {
                g.DrawLine(_pen, (Point) vertex.PreviousVertex.Data, (Point) vertex.Data);
            }
        }
    }
}