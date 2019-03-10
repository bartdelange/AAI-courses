#region

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using AICore.Graph;
using AICore.Util;

#endregion

namespace AICore.Map
{
    public interface IHelper
    {
        void Draw(Graphics graphics);
    }

    public class CoarseMapHelper : IHelper
    {
        public IEnumerable<Vector2> CurrentPath;
        public IEnumerable<Vector2> SmoothedPath;
        public Dictionary<Vector2, Vertex<Vector2>> VisitedVertices;

        public void Draw(Graphics graphics)
        {
            if (CurrentPath == null || VisitedVertices == null || !CurrentPath.Any()) return;

            var start = CurrentPath.First();
            var destination = CurrentPath.Last();

            var brushStart = new SolidBrush(Color.FromArgb(128, Color.Cyan));
            var brushTarget = new SolidBrush(Color.FromArgb(128, Color.Red));
            var brushVisited = new SolidBrush(Color.FromArgb(128, Color.DarkGreen));
            var brushNotVisited = new SolidBrush(Color.FromArgb(128, Color.RoyalBlue));
            var pen = new Pen(Color.DeepPink, 2);
            var smoothedPathPen = new Pen(Color.Gold, 2);

            foreach (var edge in VisitedVertices)
                graphics.FillEllipse(
                    edge.Value.Visited ? brushVisited : brushNotVisited,
                    new Rectangle(edge.Value.Data.Minus(5).ToPoint(), new Size(10, 10))
                );

            graphics.FillEllipse(
                brushTarget,
                new Rectangle(destination.Minus(5).ToPoint(), new Size(10, 10))
            );

            graphics.FillEllipse(
                brushStart,
                new Rectangle(start.Minus(5).ToPoint(), new Size(10, 10))
            );

            var previousVector2 = start;
            foreach (var vector2 in CurrentPath)
            {
                graphics.DrawLine(pen, previousVector2.ToPoint(), vector2.ToPoint());
                previousVector2 = vector2;
            }

            previousVector2 = start;
            foreach (var vector2 in SmoothedPath)
            {
                graphics.DrawLine(smoothedPathPen, previousVector2.ToPoint(), vector2.ToPoint());
                previousVector2 = vector2;
            }
        }
    }
}