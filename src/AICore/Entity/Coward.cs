using System.Numerics;
using AICore.Util;

namespace AICore.Entity
{
    internal class Coward : Vehicle
    {
        public int Strength = 10;

        public Coward(Vector2 position, World world)
            : base(position, world)
        {
        }
    }
}