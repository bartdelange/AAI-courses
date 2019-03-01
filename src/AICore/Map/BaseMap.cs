using System.Drawing;
using System.Numerics;
using AICore.Graph;
using AICore.Util;

namespace AICore.Map
{
    public abstract class BaseMap : Graph<Vector2>
    {
        private readonly Brush _brush = new SolidBrush(Color.LightSeaGreen);
        private readonly Pen _pen = new Pen(Color.DarkSeaGreen);

        public virtual void Render(Graphics g)
        {
            foreach (var edge in VertexMap)
            {
                foreach (var adjacentEdge in edge.Value.AdjacentVertices)
                {
                    g.DrawLine(_pen,  adjacentEdge.Value.Destination.Data.ToPoint(), edge.Value.Data.ToPoint());
                }

                g.FillEllipse(_brush, new Rectangle( edge.Value.Data.Minus(2).ToPoint(), new Size(5, 5)));
            }
        }

        public abstract Vector2 FindVector(float x, float y);
    }
}