using System;
using System.Drawing;
using System.Numerics;
using System.Threading;
using AICore.Entity.Contracts;
using AICore.Exceptions;
using AICore.Util;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
{
    public abstract class Think : BaseGoal, IRenderable
    {
        public bool Visible { get; set; } = true;

        protected IGoal ActiveGoal
        {
            get
            {
                if (_activeGoal == null)
                {
                    throw new ArgumentNullException(nameof(ActiveGoal), "Default goal not set.");
                }

                return _activeGoal;
            }

            set => _activeGoal = value;
        }

        private IGoal _activeGoal;

        protected Think(IPlayer player, SoccerField soccerField) : base(player, soccerField)
        {
        }

        public override void Update(float deltaTime)
        {
            var desirableGoal = ActiveGoal;
            var currentDesirability = desirableGoal.CheckDesirability();

            foreach (var goal in Goals)
            {
                var goalDesirability = goal.Value.CheckDesirability();
                if (goalDesirability > currentDesirability)
                {
                    currentDesirability = goalDesirability;
                    desirableGoal = goal.Value;
                }
            }

            if (desirableGoal != ActiveGoal)
            {
                ActiveGoal.Leave();
                ActiveGoal = desirableGoal;
                ActiveGoal.Enter();
            }

            ActiveGoal.Update(deltaTime);
        }

        public override void Enter()
        {
            throw new NotImplementedByDesignException(); // Not needed
        }

        public override double CheckDesirability()
        {
            throw new NotImplementedByDesignException(); // Not needed
        }

        public override void Leave()
        {
            throw new NotImplementedByDesignException(); // Not needed
        }

        public void Render(Graphics graphics)
        {
            const float margin = 2;
            var offset = (float) -Player.BoundingRadius;
            
            foreach (var keyValuePair in Goals)
            {
                var goal = keyValuePair.Value;

                var goalName = goal.GetType().Name;
                var font = new Font(SystemFonts.DefaultFont, goal == _activeGoal ? FontStyle.Bold : FontStyle.Regular);

                graphics.DrawString(
                    goalName, 
                    font, 
                    Brushes.Black, 
                    (Player.Position + new Vector2(Player.BoundingRadius + margin, offset)).ToPoint()
                );

                offset += graphics.MeasureString(goalName, font).Height + margin;
            }
        }
    }
}