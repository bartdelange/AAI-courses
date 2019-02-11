using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIBehaviors.Util;

namespace AIBehaviors.Entity
{
    public class Obstacle : GameObject
    {
        public Obstacle(Vector2D pos, World w) : base(pos, w)
        {
        }

        public int Radius { get; set; }

        public override void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Black, new Rectangle((int)Pos._X, (int)Pos._Y, Radius, Radius));
        }
    }
}
