#region

using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Graph;
using AICore.Util;

#endregion

namespace AICore.Map
{
    public abstract class BaseMap : Graph<Vector2>
    {
        private readonly Brush _brush = new SolidBrush(Color.LightSeaGreen);
        private readonly Brush _brushNotVisited = new SolidBrush(Color.FromArgb(128, Color.RoyalBlue));

        private readonly Brush _brushStart = new SolidBrush(Color.FromArgb(128, Color.Cyan));
        private readonly Brush _brushTarget = new SolidBrush(Color.FromArgb(128, Color.Red));
        private readonly Brush _brushVisited = new SolidBrush(Color.FromArgb(128, Color.DarkGreen));
        private readonly Pen _pen = new Pen(Color.DarkSeaGreen);
        private readonly Pen _penPath = new Pen(Color.DeepPink, 2);
        private readonly Pen _penSmoothedPath = new Pen(Color.Gold, 2);


        protected List<Obstacle> Obstacles;
        protected IEnumerable<Vertex<Vector2>> Path;
        protected IEnumerable<Vertex<Vector2>> SmoothedPath;

        protected Vector2 Start;
        protected Vector2 Target;

        protected BaseMap(int w, int h, List<Obstacle> obstacles)
        {
            Width = w;
            Height = h;
            Obstacles = obstacles;
        }

        protected int Width { get; }
        protected int Height { get; }

        public virtual void Render(Graphics g)
        {
            foreach (var edge in VertexMap)
            {
                foreach (var adjacentEdge in edge.Value.AdjacentVertices)
                    g.DrawLine(_pen, adjacentEdge.Value.Destination.Data.ToPoint(), edge.Value.Data.ToPoint());

                g.FillEllipse(_brush, new Rectangle(edge.Value.Data.Minus(2).ToPoint(), new Size(5, 5)));
            }
        }

        public abstract Vector2 FindClosestVertex(Vector2 position);

        public abstract IEnumerable<Vector2> FindPath(Vector2 start, Vector2 destination);
    }
}