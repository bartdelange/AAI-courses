using AICore.Entity;
using System;

namespace AICore.Goals
{
    class Think : BaseGoal
    {
        public override void Update(MovingEntity movingEntity)
        {
            Console.WriteLine("Update think");

            base.Update(movingEntity);
        }
    }
}
