using System.Numerics;

namespace AICore.Entity
{
    public class Coward : Vehicle
    {
        public int Strength = 10;

        public Coward(Vector2 position, Vector2 bounds) : base(position, bounds)
        {
        }
    }
}