using System;
using AICore.Entity;

namespace AICore.Goals
{
    public class Think : BaseGoal
    {
        public override void Update(MovingEntity movingEntity)
        {
            Console.WriteLine("Update think");

            base.Update(movingEntity);
        }
    }
}