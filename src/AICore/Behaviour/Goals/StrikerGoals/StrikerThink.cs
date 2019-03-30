using System;
using AICore.Entity.Contracts;
using AICore.Exceptions;
using AICore.FuzzyLogic;
using AICore.Worlds;

namespace AICore.Behaviour.Goals.StrikerGoals
{
    public class StrikerThink : BaseGoal
    {
        private FuzzyModule _fmBall = new FuzzyModule();
        private FuzzyModule _fmPosition = new FuzzyModule();
        private TiredModule _tiredModule = new TiredModule();

        public StrikerThink(IPlayer player, SoccerField soccerField): base(player, soccerField)
        {
            Add(GoalNames.GoToBall, new GoToBall(player, soccerField));
            Add(GoalNames.DribbleToGoal, new DribbleToGoal(player, soccerField));
        }
        
        public override void Update(IPlayer player)
        {
            IGoal desirableGoal = new RestGoal(Player, SoccerField);
            var currentDesirability = desirableGoal.CheckDesirability();
            
            foreach (var goal in _goals)
            {
                if (goal.Value.CheckDesirability() > currentDesirability)
                {
                    desirableGoal = goal.Value;
                }
            }

            desirableGoal.Activate();
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