using AIBehaviours.Graph;
using AIBehaviours.Util;
using System.Drawing;

namespace AIBehaviours.Map
{
    public abstract class BaseMap : Graph<Vector2D>
    {
        private Brush _brush = new SolidBrush(Color.LightSeaGreen);
        private Pen _pen = new Pen(Color.DarkSeaGreen);

        public void Render(Graphics g)
        {
            foreach (var edge in VertexMap)
            {
                edge.Value.AdjacentVertices.ForEach(vertex =>
                    g.DrawLine(_pen, (Point)vertex.Destination.Data, (Point)edge.Value.Data)
                );

                g.FillEllipse(_brush, new Rectangle((Point)(edge.Value.Data - 2), new Size(5, 5)));
            }
        }
    }
}
