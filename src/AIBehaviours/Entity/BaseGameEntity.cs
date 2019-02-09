using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AIBehaviours.Util;

namespace AIBehaviours.Entity
{
    public abstract class BaseGameEntity
    {
        public List<MovingEntity> Neighbors;

        protected BaseGameEntity(Vector2D pos, World w)
        {
            Pos = pos;
            MyWorld = w;
        }

        public Vector2D Pos { get; set; }

        protected float Scale { get; set; }

        protected World MyWorld { get; }

        public abstract void Update(float delta);

        public virtual void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Blue, new Rectangle((int) Pos.X, (int) Pos.Y, 10, 10));
        }

        protected void FindNeighbours(double radius)
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