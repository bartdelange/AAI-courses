using System.Drawing;
using System.Numerics;
using AICore.Behaviour;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AICore.Entity
{
    public abstract class MovingEntity : IMovingEntity
    {
        #region render properties
        
        public bool Visible { get; set; } = true;
        
        #endregion

        #region entity properties
        
        public float MaxSpeed { get; set; } = 100;

        public float Mass { get; set; } = 20;
        
        public int BoundingRadius { get; set; } = 150;

        #endregion

        // Behaviour
        public ISteeringBehaviour SteeringBehaviour { set; get; }
                
        // 
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; } = new Vector2(1, 1);
        public Vector2 Heading { get; set; } = new Vector2(1, 1);
        public Vector2 Side { get; set; } = new Vector2(1, 1);
        
        //
        private Vector2 WorldBounds { get; }

        // Render properties
        private readonly Pen _pen;

        protected MovingEntity(Vector2 position, Vector2 bounds, Pen pen)
        {
            Position = position;
            WorldBounds = bounds;

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

            Position = Position.WrapToBounds(WorldBounds);
        }

        public virtual void Render(Graphics graphics)
        {
            graphics.DrawLine(
                Pens.Red,
                Position.ToPoint(),
                (Position + Velocity).ToPoint()
            );

            graphics.FillEllipse(
                new SolidBrush(Color.FromArgb(50, Color.Red)),
                new Rectangle(
                    (Position - new Vector2(BoundingRadius)).ToPoint(),
                    new Size(BoundingRadius * 2, BoundingRadius * 2)
                )
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