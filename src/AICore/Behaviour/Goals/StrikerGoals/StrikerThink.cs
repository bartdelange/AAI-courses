using System;
using AICore.Entity.Contracts;
using AICore.FuzzyLogic;
using AICore.Worlds;

namespace AICore.Behaviour.Goals
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
            IGoal desireableGoal = new RestGoal(Player, SoccerField);
            var currDesirability = desireableGoal.CheckDesirability();
            
            foreach (var goal in _goals)
            {
                if (goal.Value.CheckDesirability() > currDesirability)
                {
                    desireableGoal = goal.Value;
                }
            }

            desireableGoal?.Activate();
            desireableGoal?.Update(player);
        }

        public override void Activate()
        {
            throw new NotImplementedException(); // Not needed
        }

        public override double CheckDesirability()
        {
            throw new NotImplementedException(); // Not needed
        }
    }
}