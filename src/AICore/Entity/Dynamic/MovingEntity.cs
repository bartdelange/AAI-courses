using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Util;
using AICore.Util;

namespace AICore.Entity.Dynamic
{
    public abstract class MovingEntity : IMovingEntity
    {
        private Vector2 _position;
        
        #region render properties

        public bool Visible { get; set; } = true;
        private readonly Brush _boundingCircleBrush = new SolidBrush(Color.FromArgb(50, Color.Red));

        #endregion

        #region entity properties

        private float _maxSpeed;
        private float _mass;
        private int _boundingRadius;

        public float MaxSpeed
        {
            get
            {
                if (_maxSpeed == default(float))
                    throw new ArgumentOutOfRangeException("MaxSpeed is not defined");

                return _maxSpeed;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("MaxSpeed must be higher than 0");

                _maxSpeed = value;
            }
        }

        public float Mass
        {
            get
            {
                if (_mass == default(float))
                    throw new ArgumentNullException("Mass is not defined");

                return _mass;
            }

            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("Mass must be higher than 1");

                _mass = value;
            }
        }

        public int BoundingRadius
        {
            get
            {
                if (_boundingRadius == default(int))
                    throw new ArgumentOutOfRangeException("BoundingRadius is not defined");

                return _boundingRadius;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("BoundingRadius must be higher than 0");

                _boundingRadius = value;
            }
        }

        #endregion

        #region behaviour properties

        public ISteeringBehaviour SteeringBehaviour { set; get; }
        public List<IMiddleware> Middlewares { get; set; } = new List<IMiddleware>();

        public Vector2 Position
        {
            get => _position;
            set
            {
                if (value.X == float.NaN || value.Y == float.NaN)
                    throw new ArgumentException("Invalid position");
                
                _position = value;
            }
        }

        public Vector2 StartPosition { get; }
        public Vector2 Velocity { get; set; } = Vector2.One;
        public Vector2 Heading { get; set; } = Vector2.One;
        public Vector2 SmoothHeading { get; set; } = Vector2.One;

        private readonly HeadingSmoother _headingSmoother;

        #endregion

        /// <summary>
        /// Base class that is used to create entities that can interact with the world
        /// </summary>
        /// <param name="position"></param>
        protected MovingEntity(Vector2 position)
        {
            StartPosition = position;
            Position = position;

            _headingSmoother = new HeadingSmoother(this, 15);
        }

        /// <summary>
        /// Method that is called when the timer interval elapses
        /// </summary>
        /// <param name="deltaTime"></param>
        public virtual void Update(float deltaTime)
        {
            var acceleration = (SteeringBehaviour?.Calculate(deltaTime) / Mass) ?? Vector2.Zero;

            Velocity = acceleration * deltaTime;

            Position += Velocity * deltaTime;

            if (Velocity.LengthSquared() > 0.000000001)
            {
                Heading = Vector2.Normalize(Velocity);

                _headingSmoother.Update();
            }

            // Apply middleware after steering force was applied
            Middlewares.ForEach(middleware => middleware.Update(deltaTime));
        }

        /// <summary>
        /// Visualizes the velocity, bounding radius and position
        /// </summary>
        /// <param name="graphics"></param>
        public virtual void Render(Graphics graphics)
        {
            if (!Config.Debug) return;

            // Render steering behaviour if it exists
            SteeringBehaviour?.RenderIfVisible(graphics);
            
            // visualize the velocity
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
        }
    }
}