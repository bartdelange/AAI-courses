using AIBehaviours.Graph;
using AIBehaviours.Util;
using System.Drawing;

namespace AIBehaviours.Map
{
    public abstract class BaseMap : Graph<Vector2D>
    {
        private readonly Brush _brush = new SolidBrush(Color.LightSeaGreen);
        private readonly Pen _pen = new Pen(Color.DarkSeaGreen);
        private readonly Font _font = new Font("Arial", 10);

        public void Render(Graphics g)
        {
            foreach (var edge in VertexMap)
            {
                edge.Value.AdjacentVertices.ForEach(vertex =>
                    g.DrawLine(_pen, (Point)vertex.Destination.Data, (Point)edge.Value.Data)
                );

                g.FillEllipse(_brush, new Rectangle((Point)(edge.Value.Data - 2), new Size(5, 5)));
                g.DrawString($"{edge.Value.Data}: {edge.Value.Distance}", _font, _brush, (Point)(edge.Value.Data + 5));
            }
        }
    }
}
