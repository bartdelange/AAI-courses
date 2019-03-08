#region

using System;
using System.Numerics;

#endregion

namespace AICore.Util
{
    public static class Vector2Util
    {
        private static readonly Random _random = new Random();

        public static Vector2 GetRandom(Vector2 max, Vector2 min = new Vector2())
        {
            return new Vector2(
                _random.Next((int) min.X, (int) max.X),
                _random.Next((int) min.Y, (int) max.Y)
            );
        }
    }
}