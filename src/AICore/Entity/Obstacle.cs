#region

using System.Drawing;
using System.Numerics;

#endregion

namespace AICore.Entity
{
    public class Obstacle : GameObject
    {
        public static int MinRadius = 10;
        public static int MaxRadius = 100;

        public Obstacle(Vector2 pos, World w) : this(pos, w, MinRadius)
        {
        }

        public Obstacle(Vector2 pos, World w, int radius) : base(pos, w)
        {
            Radius = radius;
        }

        public int Radius { get; set; }

        public override void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Black,
                new Rectangle((int) Pos.X - Radius, (int) Pos.Y - Radius, Radius * 2, Radius * 2));
        }
    }
}