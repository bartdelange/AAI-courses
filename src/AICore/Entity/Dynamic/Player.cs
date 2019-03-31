using System;
using System.Drawing;
using System.Numerics;
using AICore.Behaviour.Goals;
using AICore.Entity.Contracts;
using AICore.Model;
using AICore.Util;

namespace AICore.Entity.Dynamic
{
    public enum PlayerStrategy
    {
        Defender,
        Striker,
        GoalKeeper
    }
    
    public class  Player : MovingEntity, IPlayer
    {
        public new Vector2 StartPosition { get; }
        public Team Team { get; set; }
        public PlayerStrategy Strategy { get; set; }
        
        public Think ThinkGoal { get; set; }

        public DateTime Attempt { get; set; } = DateTime.Now;

        public float MaxEnergy { get; set; } = 50;
        public float MinEnergy { get; set; } = 10;

        public float Energy { get; set; }

        // Render properties
        private readonly Brush _brush;

        public Player(PlayerStrategy playerStrategy, Vector2 startingPosition, Color color) : base(startingPosition)
        {
            StartPosition = startingPosition;
            Strategy = playerStrategy;

            // Set entity properties
            MaxSpeed = Config.MaxSpeed;
            Mass = 5;
            BoundingRadius = 10;

            // 
            _brush = new SolidBrush(color);
        }
        
        public override void Update(float deltaTime)
        {            
            base.Update(deltaTime);

            ThinkGoal?.Update(deltaTime);
        }

        public void Steal(Ball ball)
        {
            var random = new Random();

            // Might need to fix this equals check
            if (ball.Owner == this) return;

            // If we do not add an attempt check it will get it every time (as this is parsed in update / every game tick)
            if (random.NextDouble() > 0.5d && Attempt.AddSeconds(5) <= DateTime.Now)
            {
                ball.Owner = this;
            }

            Attempt = DateTime.Now;
        }

        public override void Render(Graphics graphics)
        {
            ThinkGoal?.RenderIfVisible(graphics);

            var strategyName = Strategy.ToString();

            var textSize = graphics.MeasureString(strategyName, SystemFonts.DefaultFont);

            graphics.DrawString(
                strategyName,
                SystemFonts.DefaultFont,
                Brushes.Black,
                (Position + new Vector2(-(textSize.Width / 2), -(BoundingRadius + 15))).ToPoint()
            );

            var rectangle = new Rectangle(
                (int) (Position.X - BoundingRadius),
                (int) (Position.Y - BoundingRadius),
                BoundingRadius * 2,
                BoundingRadius * 2
            );

            graphics.FillEllipse(_brush, rectangle);
            
            base.Render(graphics);
        }
    }
}