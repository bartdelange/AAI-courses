using System.Drawing;
using AIBehaviours.Graph;
using AIBehaviours.Util;

namespace AIBehaviours.Map
{
    public abstract class BaseMap : Graph<Vector2D>
    {
        private readonly Brush _brush = new SolidBrush(Color.LightSeaGreen);
        private readonly Font _font = new Font("Arial", 10);
        private readonly Pen _pen = new Pen(Color.DarkSeaGreen);

        public void Render(Graphics g)
        {
            foreach (var edge in _VertexMap)
            {
                edge.Value._AdjacentVertices.ForEach(vertex =>
                    g.DrawLine(_pen, (Point) vertex._Destination._Data, (Point) edge.Value._Data)
                );

                g.FillEllipse(_brush, new Rectangle((Point) (edge.Value._Data - 2), new Size(5, 5)));
                g.DrawString($"{edge.Value._Data}: {edge.Value._Distance}", _font, _brush, (Point) (edge.Value._Data + 5));
            }
        }
    }
}