using System;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.Entity
{
    public class Player : MovingEntity, IPlayer
    {
        public Vector2 StartPosition { get; set; }

        public IMovingEntity BallEntity { get; set; }

        public float MaxEnergy { get; set; } = 50;
        public float MinEnergy { get; set; } = 10;

        public float Energy { get; set; }

        // Render properties
        private Brush _brush;

        public Player(Vector2 position, Color? color = null) : base(position)
        {
            StartPosition = position;

            _brush = new SolidBrush(color ?? Color.Black);
        }

        public override void Render(Graphics graphics)
        {
            base.Render(graphics);

            graphics.FillEllipse(
                _brush,
                Position.X - BoundingRadius,
                Position.Y - BoundingRadius,
                BoundingRadius * 2,
                BoundingRadius * 2
            );
        }
    }
}