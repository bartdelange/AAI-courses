using System.Drawing;
using System.Numerics;

namespace AICore.Entity
{
    public class GameObject
    {
        protected GameObject(Vector2 pos, World w)
        {
            Pos = pos;
            MyWorld = w;
        }

        public Vector2 Pos { get; set; }

        protected float Scale { get; set; }

        protected World MyWorld { get; }

        public virtual void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Black, new Rectangle((int) Pos.X, (int) Pos.Y, 10, 10));
        }
    }
}