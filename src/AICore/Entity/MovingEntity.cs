using System.Drawing;
using System.Numerics;
using AICore.Behaviour;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AICore.Entity
{
    public abstract class MovingEntity : IMovingEntity
    {
        // Render properties
        public bool Visible { get; set; } = true;

        // Entity properties
        public const float Mass = 20;
        public const int Radius = 100;

        public Vector2 Position { get; set; }
        private Vector2 Bounds { get; }
        public int BoundingRadius { get; set; } = 150;

        //
        public Vector2 Velocity { get; set; } = new Vector2(1, 1);
        public Vector2 Heading { get; set; } = new Vector2(1, 1);
        public Vector2 Side { get; set; } = new Vector2(1, 1);

        // Behaviour properties
        public ISteeringBehaviour SteeringBehaviour { set; get; }
        public float MaxSpeed { get; set; } = 100;

        // Render properties
        private readonly Pen _pen;

        protected MovingEntity(Vector2 position, Vector2 bounds, Pen pen)
        {
            Position = position;
            Bounds = bounds;

            _pen = pen;
        }

        public void Update(float delta)
        {
            var acceleration = (SteeringBehaviour?.Calculate(delta) / Mass) ?? new Vector2();

            Velocity = acceleration * delta;
            Position += Velocity * delta;

            if (Velocity.LengthSquared() > 0.000000001)
            {
                Heading = Vector2.Normalize(Velocity);
                Side = Heading.Perpendicular();
            }

            Position = Position.WrapToBounds(Bounds);
        }

        public virtual void Render(Graphics graphics)
        {
            graphics.DrawLine(
                Pens.Red,
                Position.ToPoint(),
                (Position + Velocity).ToPoint()
            );

            graphics.DrawString(
                $"{Velocity.ToString("##.##")}\n{Position.ToString("##.##")}",
                SystemFonts.DefaultFont,
                Brushes.Black,
                Position.ToPoint()
            );
            
            SteeringBehaviour?.Render(graphics);
        }
    }
}