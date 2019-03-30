using System;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Model;

namespace AICore.Entity.Static
{
    public class SoccerGoal : IRenderable
    {
        public bool Visible { get; set; } = true;

        public Vector2 Position { get; }

        private readonly Bounds _bounds;
        private readonly Brush _goalBrush;

        public SoccerGoal(Vector2 position, Vector2 size, Color teamColor)
        {
            Position = position;
            
            _bounds = new Bounds(position - size / 2, position + size / 2);
            _goalBrush = new SolidBrush(teamColor);
        }

        /// <summary>
        /// Checks if given line intersects with the goal. this can be used to check if the ball will hit the goal in it's behaviour
        /// </summary>
        /// <param name="start"></param>
        /// <param name="target"></param>
        /// <param name="margin"></param>
        /// <returns></returns>
        public bool IntersectsWithLine(Vector2 start, Vector2 target, int margin = 0)
        {
            throw new NotImplementedException();
        }

        public void Render(Graphics graphics)
        {
            graphics.FillRectangle(
                _goalBrush,
                (Rectangle) _bounds
            );
        }
    }
}