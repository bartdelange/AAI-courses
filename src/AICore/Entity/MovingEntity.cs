using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour;
using AICore.Util;

namespace AICore.Entity
{
    public abstract class MovingEntity : IMovingEntity
    {
        #region render properties
        
        public bool Visible { get; set; } = true;
        
        private readonly Brush _boundingCircleBrush = new SolidBrush(Color.FromArgb(50, Color.Red));
        
        #endregion

        #region entity properties
        
        public float MaxSpeed { get; set; } = 150;

        public float Mass { get; set; } = 20;
        
        public int BoundingRadius { get; set; } = 10;

        #endregion

        public ISteeringBehaviour SteeringBehaviour { set; get; }
               
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; } = new Vector2(1, 1);
        public Vector2 Heading { get; set; } = new Vector2(1, 1);
        public Vector2 Side { get; set; } = new Vector2(1, 1);
        
        //
        private Vector2 WorldBounds { get; }

        /// <summary>
        /// Base class that is used to create entities that can interact with the world
        /// </summary>
        /// <param name="position"></param>
        /// <param name="bounds"></param>
        /// <param name="pen"></param>
        protected MovingEntity(Vector2 position, Vector2 bounds, Pen pen)
        {
            Position = position;
            WorldBounds = bounds;
        }

        /// <summary>
        /// Method that is called when the timer interval elapses
        /// </summary>
        /// <param name="delta"></param>
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

        /// <summary>
        /// Visualizes the velocity, bounding radius and position
        /// </summary>
        /// <param name="graphics"></param>
        public virtual void Render(Graphics graphics)
        {
            // visualize the instance position
            graphics.DrawLine(
                Pens.Red,
                Position.ToPoint(),
                (Position + Velocity).ToPoint()
            );

            // visualize the bounding circle
            graphics.FillEllipse(
                _boundingCircleBrush,
                new Rectangle(
                    (Position - new Vector2(BoundingRadius)).ToPoint(),
                    new Size(BoundingRadius * 2, BoundingRadius * 2)
                )
            );

            // Print the velocity and position 
            graphics.DrawString(
                $"{Velocity.ToString("##.##")}\n{Position.ToString("##.##")}",
                SystemFonts.DefaultFont,
                Brushes.Black,
                Position.ToPoint()
            );

            // Render steering behaviour if it exists
            SteeringBehaviour?.RenderIfVisible(graphics);
        }
    }
}