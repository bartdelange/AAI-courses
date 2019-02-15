using AICore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
