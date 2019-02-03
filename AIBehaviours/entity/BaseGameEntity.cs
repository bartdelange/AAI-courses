using System.Drawing;
using AIBehaviours.util;
using AIBehaviours.world;

namespace AIBehaviours.entity
{
    internal abstract class BaseGameEntity
    {
        public BaseGameEntity(Vector2D pos, World w)
        {
            Pos = pos;
            MyWorld = w;
        }

        public Vector2D Pos { get; set; }

        public float Scale { get; set; }

        public World MyWorld { get; set; }

        public abstract void Update(float delta);

        public virtual void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Blue, new Rectangle((int) Pos.X, (int) Pos.Y, 10, 10));
        }
    }
}