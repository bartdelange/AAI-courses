using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Model;
using AICore.Shapes;
using AICore.Util;

namespace AICore.Entity.Static
{
    public class SoccerGoal : IRenderable, IPolygon
    {
        public bool Visible { get; set; } = true;

        public Vector2 Position { get; }

        public int Score { get; set; } = Config.InitialScore;

        private readonly Bounds _bounds;
        
        // Rendering properties
        private readonly Brush _goalBrush;
        private readonly Brush _scoreBrush;
        private readonly Font _scoreFont = new Font(FontFamily.GenericSansSerif,18f, FontStyle.Bold);

        /// <summary>
        /// Returns a list of vectors using the size and position of the goal
        /// </summary>
        public List<Vector2> Polygons { get; private set; }

        public SoccerGoal(Vector2 position, Vector2 size, Color teamColor)
        {
            _goalBrush = new SolidBrush(teamColor);
            _scoreBrush = new SolidBrush(Color.FromArgb(teamColor.ToArgb() ^ 0xffffff));

            
            Position = position;
            _bounds = new Bounds(position - size / 2, position + size / 2);

            // Set polygons
            Polygons = new List<Vector2>
            {
                new Vector2(_bounds.Min.X, _bounds.Min.Y), // Top left
                new Vector2(_bounds.Max.X, _bounds.Min.Y), // Top right
                new Vector2(_bounds.Max.X, _bounds.Max.Y), // Bottom right
                new Vector2(_bounds.Min.X, _bounds.Max.Y) // Bottom left
            };
        }

        public void Render(Graphics graphics)
        {
            graphics.FillRectangle(
                _goalBrush,
                (Rectangle) _bounds
            );
                        
            graphics.DrawString(
                Score.ToString(),
                _scoreFont,
                _scoreBrush, 
                Position.Minus(_scoreFont.Size / 2).ToPoint()
            );
        }
    }
}