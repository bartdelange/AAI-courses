using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Util;
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
        public float CurrentSpeed { get; set; } = 150;

        public float Mass { get; set; } = 20;

        public int BoundingRadius { get; set; } = 10;

        #endregion

        #region behaviour properties

        public ISteeringBehaviour SteeringBehaviour { set; get; }

        public IEnumerable<IMiddleware> Middlewares { get; set; }

        #endregion

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; } = Vector2.One;
        public Vector2 Heading { get; set; } = Vector2.One;
        public Vector2 SmoothHeading { get; set; } = Vector2.One;

        private Vector2 WorldBounds { get; }

        private readonly HeadingSmoother _headingSmoother;

        /// <summary>
        /// Base class that is used to create entities that can interact with the world
        /// </summary>
        /// <param name="position"></param>
        /// <param name="bounds"></param>
        protected MovingEntity(Vector2 position)
        {
            Position = position;

            _headingSmoother = new HeadingSmoother(this, 15);
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

                _headingSmoother.Update();
            }

            if (Middlewares == null) return;
            
            // Apply middleware after steering force was applied
            foreach (var middleware in Middlewares)
                middleware.Update();
        }

        /// <summary>
        /// Visualizes the velocity, bounding radius and position
        /// </summary>
        /// <param name="graphics"></param>
        public virtual void Render(Graphics graphics)
        {
            if (!Config.Debug) return;

            // visualize the velocity
            graphics.DrawLine(
                Pens.Red,
                Position.ToPoint(),
                (Position + (Velocity * 10)).ToPoint()
            );

            // visualize the bounding circle
            graphics.FillEllipse(
                _boundingCircleBrush,
                new Rectangle(
                    (Position - new Vector2(BoundingRadius)).ToPoint(),
                    new Size(BoundingRadius * 2, BoundingRadius * 2)
                )
            );

            // Render steering behaviour if it exists
            SteeringBehaviour?.RenderIfVisible(graphics);
        }
    }
}