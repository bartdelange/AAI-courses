using System;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Model;
using AICore.Util;

namespace AICore.Entity
{
    public class SoccerGoal : IRenderable
    {
        public bool Visible { get; set; } = true;

        public Vector2 Position { get; }
        public string TeamName { get; set; }

        private readonly Bounds _bounds;
        private readonly Color _teamColor;

        public SoccerGoal(Vector2 position, Vector2 size, string teamName, Color teamColor)
        {
            Position = position;
            TeamName = teamName;
            
            _bounds = new Bounds(position - size / 2, position + size / 2);
            _teamColor = teamColor;
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
                Brushes.Black,
                (Rectangle) _bounds
            );
        }
    }
}