#region

using System.Numerics;

#endregion

namespace AICore.Entity
{
    public class Coward : Vehicle
    {
        public int Strength = 10;

        public Coward(Vector2 position, World world)
            : base(position, world)
        {
        }
    }
}