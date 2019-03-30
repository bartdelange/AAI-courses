using System;
using AICore.Entity.Contracts;
using AICore.Exceptions;
using AICore.Worlds;

namespace AICore.Behaviour.Goals.StrikerGoals
{
    public abstract class Think : BaseGoal
    {
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

        public override void Update(IPlayer player)
        {
            var desirableGoal = ActiveGoal;
            var currentDesirability = desirableGoal.CheckDesirability();
            
            foreach (var goal in _goals)
            {
                if (goal.Value.CheckDesirability() > currentDesirability)
                {
                    desirableGoal = goal.Value;
                }
            }

            if (desirableGoal != ActiveGoal)
            {
                desirableGoal.Activate();                
                ActiveGoal = desirableGoal;
            }

            desirableGoal.Update(player);
        }

        public override void Activate()
        {
            
            throw new NotImplementedByException(); // Not needed
        }

        public override double CheckDesirability()
        {
            throw new NotImplementedByException(); // Not needed
        }
    }
}