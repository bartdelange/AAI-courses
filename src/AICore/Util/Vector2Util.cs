using System;
using System.Numerics;

namespace AICore.Util
{
    public static class Vector2Util
    {
        private static readonly Random Random = new Random();

        public static Vector2 GetRandom(Vector2 max, Vector2 min = new Vector2())
        {
            return new Vector2(
                Random.Next((int) min.X, (int) max.X),
                Random.Next((int) min.Y, (int) max.Y)
            );
        }
    }
}