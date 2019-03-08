using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AICore.Util
{
    public static class Vector2Util
    {
        private static readonly Random _random = new Random();

        public static Vector2 GetRandom(Vector2 max, Vector2 min = new Vector2())
        {
            return new Vector2(
                _random.Next((int)min.X, (int)max.X),
                _random.Next((int)min.Y, (int)max.Y)
            );
        }
    }
}
