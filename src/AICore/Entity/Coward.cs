using AIBehaviours;
using AIBehaviours.Entity;
using AIBehaviours.Util;

namespace AILib.Entity
{
    class Coward : Vehicle
    {
        public int Strength = 10;

        public Coward(Vector2D position, World world) 
            : base(position, world)
        { }
    }
}
