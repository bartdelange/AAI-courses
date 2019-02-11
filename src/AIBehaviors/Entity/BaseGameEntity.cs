using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AIBehaviors.Util;

namespace AIBehaviors.Entity
{
    public abstract class BaseGameEntity : GameObject
    {
        public List<MovingEntity> _Neighbors;

        protected BaseGameEntity(Vector2D pos, World w) : base(pos, w)
        {
        }

        public abstract void Update(float delta);

        protected void FindNeighbors(double radius)
        {
            _Neighbors = MyWorld._Entities.Where(entity =>
            {
                var targetDistance = entity.Pos - Pos;
                var rangeSquared = Math.Pow(radius, 2);

                return entity != this && targetDistance.LengthSquared() < rangeSquared;
            }).ToList();
        }
    }
}