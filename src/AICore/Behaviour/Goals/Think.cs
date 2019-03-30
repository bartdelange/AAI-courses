using System;
using System.Drawing;
using System.Numerics;
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
            private get
            {
                if (_activeGoal == null)
                {
                    throw new ArgumentNullException("Default goal not set.");
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
                if (goal.Value.CheckDesirability() > currentDesirability)
                {
                    desirableGoal = goal.Value;
                }
            }

            if (desirableGoal != ActiveGoal)
            {
                desirableGoal.Enter();
                ActiveGoal = desirableGoal;
            }

            desirableGoal.Update(deltaTime);
        }

        public override void Enter()
        {
            throw new NotImplementedByException(); // Not needed
        }

        public override double CheckDesirability()
        {
            throw new NotImplementedByException(); // Not needed
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