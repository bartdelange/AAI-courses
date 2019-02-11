using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIBehaviors.Util;

namespace AIBehaviors.Entity
{
    public class GameObject
    {
        protected GameObject(Vector2D pos, World w)
        {
            Pos = pos;
            MyWorld = w;
        }

        public Vector2D Pos { get; set; }

        protected float Scale { get; set; }

        protected World MyWorld { get; }

        public virtual void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Black, new Rectangle((int)Pos._X, (int)Pos._Y, 10, 10));
        }
    }
}
