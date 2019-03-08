#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

#endregion

namespace AICore.Entity
{
    public abstract class BaseGameEntity : GameObject
    {
        public List<MovingEntity> Neighbors;

        protected BaseGameEntity(Vector2 pos, World w) : base(pos, w)
        {
        }

        public abstract void Update(float delta);

        protected void FindNeighbors(double radius)
        {
            Neighbors = MyWorld.Entities.Where(entity =>
            {
                var targetDistance = entity.Pos - Pos;
                var rangeSquared = Math.Pow(radius, 2);

                return entity != this && targetDistance.LengthSquared() < rangeSquared;
            }).ToList();
        }
    }
}