using AICore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AICore.Graph
{
    class Manhattan : IHeuristic<Vector2>
    {
        public double Calculate(Vector2 v1, Vector2 v2)
        {
            return Math.Abs(v1.X - v2.X + (v1.Y - v2.Y));
        }
    }
}
