using AICore.Util;

namespace AICore.Entity
{
    internal class Coward : Vehicle
    {
        public int Strength = 10;

        public Coward(Vector2D position, World world)
            : base(position, world)
        {
        }
    }
}