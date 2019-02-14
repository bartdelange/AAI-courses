using System.Drawing;
using AICore.Util;

namespace AICore.Entity
{
    public class Obstacle : GameObject
    {
        public static int _MinRadius = 10;
        public static int _MaxRadius = 100;

        public Obstacle(Vector2D pos, World w) : this(pos, w, _MinRadius)
        {
        }

        public Obstacle(Vector2D pos, World w, int radius) : base(pos, w)
        {
            Radius = radius;
        }

        public int Radius { get; set; }

        public override void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Black,
                new Rectangle((int) Pos._X - Radius, (int) Pos._Y - Radius, Radius * 2, Radius * 2));
        }
    }
}