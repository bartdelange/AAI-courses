﻿using System;
using System.Numerics;

namespace AICore.Graph
{
    class Manhattan : IHeuristic<Vector2>
    {
        public double Calculate(Vector2 v1, Vector2 v2)
        {
            return Math.Abs(v1.X - v2.X) + Math.Abs(v1.Y - v2.Y);
        }
    }
}
