using System;
using System.Collections.Generic;
using System.Drawing;
using AICore.Graph;
using AICore.Util;

namespace AICore.Map
{
    public abstract class BaseMap : Graph<Vector2D>
    {
        private readonly Brush _brush = new SolidBrush(Color.LightSeaGreen);
        private readonly Pen _pen = new Pen(Color.DarkSeaGreen);

        public virtual async void Render(Graphics g)
        {
            foreach (var edge in VertexMap)
            {
                foreach (var adjacentEdge in edge.Value.AdjacentVertices)
                {
                    g.DrawLine(_pen, (Point) adjacentEdge.Value.Destination.Data, (Point) edge.Value.Data);
                }

                g.FillEllipse(_brush, new Rectangle((Point) (edge.Value.Data - 2), new Size(5, 5)));
            }
        }
    }
}