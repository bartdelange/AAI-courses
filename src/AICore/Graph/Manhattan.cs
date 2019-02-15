using AICore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AICore.Graph
{
    class Manhattan : IHeuristic<Vector2D>
    {
        public double Calculate(Vector2D v1, Vector2D v2)
        {
            return Math.Abs((v1.X - v2.X) + (v1.Y - v2.Y));
        }
    }
}
