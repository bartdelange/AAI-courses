using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Graph;
using AICore.Util;

namespace AICore.Map
{
    public abstract class BaseMap : Graph<Vector2>
    {
        private readonly Brush _brush = new SolidBrush(Color.DarkSeaGreen);
        private readonly Pen _pen = new Pen(Color.DarkSeaGreen);


        protected readonly List<Obstacle> Obstacles;

        protected BaseMap(int w, int h, List<Obstacle> obstacles)
        {
            Width = w;
            Height = h;
            Obstacles = obstacles;
        }

        protected int Width { get; }
        protected int Height { get; }

        public virtual void Render(Graphics g, bool graphIsVisible)
        {
            if (!graphIsVisible) return;

            foreach (var edge in VertexMap)
            {
                foreach (var adjacentEdge in edge.Value.AdjacentVertices)
                    g.DrawLine(_pen, adjacentEdge.Value.Destination.Data.ToPoint(), edge.Value.Data.ToPoint());
    
                g.FillEllipse(_brush, new Rectangle(edge.Value.Data.Minus(2).ToPoint(), new Size(4, 4)));
            }
        }

        public abstract IEnumerable<Vector2> FindPath(Vector2 start, Vector2 destination);
    }
}