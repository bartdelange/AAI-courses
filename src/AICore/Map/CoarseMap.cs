using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Graph;
using AICore.Util;

namespace AICore.Map
{
    public class CoarseMap : BaseMap
    {
        private const int Density = 20;
        private readonly List<Obstacle> _obstacles;
        private readonly Dictionary<Vector2, bool> _vectors = new Dictionary<Vector2, bool>();
        private IEnumerable<Vertex<Vector2>> _path;

        private Vector2 _start;
        private Vector2 _target;


        private readonly Brush _brushStart = new SolidBrush(Color.FromArgb(128, Color.Cyan));
        private readonly Brush _brushTarget = new SolidBrush(Color.FromArgb(128, Color.Red));
        private readonly Brush _brushVisited = new SolidBrush(Color.FromArgb(128, Color.DarkGreen));
        private readonly Brush _brushNotVisited = new SolidBrush(Color.FromArgb(128, Color.RoyalBlue));
        private readonly Pen _pen = new Pen(Color.DeepPink, 2);

        public CoarseMap(int w, int h, List<Obstacle> obstacles)
        {
            Width = w;
            Height = h;
            _obstacles = obstacles;

            var x0y0 = new Vector2(Density, Density);
            GenerateEdges(x0y0);

            var xWyH = new Vector2(Width / Density * Density, Height / Density * Density);
            GenerateEdges(xWyH);
            
            CalcPath(x0y0, xWyH);
        }

        private int Width { get; }
        private int Height { get; }

        #region Floodfilling
        public void GenerateEdges(Vector2 start)
        {
            if (_vectors.TryGetValue(start, out var processed))
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
                    new Rectangle( edge.Value.Data.Minus(5).ToPoint(), new Size(10, 10)));
            }
            
            g.FillEllipse(_brushTarget,
                new Rectangle( _target.Minus(5).ToPoint(), new Size(10, 10)));
            g.FillEllipse(_brushStart,
                new Rectangle( _start.Minus(5).ToPoint(), new Size(10, 10)));


            foreach (var vertex in _path)
            {
                g.DrawLine(_pen,  vertex.PreviousVertex.Data.ToPoint(),  vertex.Data.ToPoint());
            }
        }

        public override Vector2 FindVector(float x, float y)
        {
            if (x > Width || x < 0)
                throw new ArgumentOutOfRangeException(nameof(x));
            if (y > Height || y < 0)
                throw new ArgumentOutOfRangeException(nameof(y));
            
            var roundedX = (int) Math.Round (x, MidpointRounding.AwayFromZero) / Density * Density;
            var roundedY = (int) Math.Round (y, MidpointRounding.AwayFromZero) / Density * Density;
            
            var vector = new Vector2(roundedX, roundedY);

            if (_vectors.ContainsKey(vector))
            {
                return vector;
            }


            var steps = Math.Max(Height,Width)/Density;
            // Lazy search around till we find one (do this [steps] times
            for (var i = 0; i <= steps; i++)
            {
                var vectorTop = new Vector2(roundedX, roundedY + Density * i);
                var vectorRight = new Vector2(roundedX + Density * i, roundedY);
                var vectorBottom = new Vector2(roundedX, roundedY - Density * i);
                var vectorLeft = new Vector2(roundedX - Density * i, roundedY);

                if (_vectors.ContainsKey(vectorTop))
                    return vectorTop;
                if (_vectors.ContainsKey(vectorRight))
                    return vectorRight;
                if (_vectors.ContainsKey(vectorBottom))
                    return vectorBottom;
                if (_vectors.ContainsKey(vectorLeft))
                    return vectorLeft;
            }
            
            throw new IndexOutOfRangeException("No vector was found at or close to the given x and y");
        }

        public override void CalcPath(Vector2 start, Vector2 target)
        {
            _start = start;
            _target = target;
            _path = AStar(_start, _target, new Manhattan());
        }
    }
}