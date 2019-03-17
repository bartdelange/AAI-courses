using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AICore.Navigation
{
    public class NavigationHelper : IRenderable
    {
        public bool Visible { get; set; } = true;

        public PathValues<Vector2> PathValues;

        // Pens
        private readonly SolidBrush _startBrush = new SolidBrush(Color.Cyan);
        private readonly SolidBrush _destinationBrush = new SolidBrush(Color.Red);

        private readonly SolidBrush _visitedVertexBrush = new SolidBrush(Color.FromArgb(128, Color.DarkGreen));
        private readonly SolidBrush _skippedVertexBrush = new SolidBrush(Color.FromArgb(128, Color.RoyalBlue));

        private readonly Pen _pathPen = new Pen(Color.DeepPink);
        private readonly Pen _smoothPathPen = new Pen(Color.Gold,2);

        public void Render(Graphics graphics)
        {
            if (PathValues == null)
            {
                return;
            }
            
            var start = PathValues.Path.First();
            var destination = PathValues.Path.Last();

            DrawMesh(graphics);
            DrawPath(graphics);
            DrawSmoothPath(graphics);

            graphics.FillEllipse(
                _destinationBrush,
                new Rectangle(destination.Minus(5).ToPoint(), new Size(10, 10))
            );

            graphics.FillEllipse(
                _startBrush,
                new Rectangle(start.Minus(5).ToPoint(), new Size(10, 10))
            );
        }

        private void DrawMesh(Graphics graphics)
        {
            foreach (var edge in PathValues.VisitedVertices)
            {
                graphics.FillEllipse(
                    edge.Visited ? _visitedVertexBrush : _skippedVertexBrush,
                    new Rectangle(edge.Value.Minus(5).ToPoint(), new Size(10, 10))
                );
            }
        }

        private void DrawPath(Graphics graphics)
        {
            var previousVector = PathValues.Path.First();

            foreach (var vector2 in PathValues.Path)
            {
                graphics.DrawLine(_pathPen, previousVector.ToPoint(), vector2.ToPoint());
                previousVector = vector2;
            }
        }

        private void DrawSmoothPath(Graphics graphics)
        {
            var previousVector = PathValues.SmoothPath.First();

            foreach (var vector2 in PathValues.SmoothPath)
            {
                graphics.DrawLine(_smoothPathPen, previousVector.ToPoint(), vector2.ToPoint());
                previousVector = vector2;
            }
        }
    }
}