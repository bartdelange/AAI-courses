using System.Drawing;
using AIBehaviours.util;
using AIBehaviours.world;

namespace AIBehaviours.entity
{
    internal class Vehicle : MovingEntity
    {
        public Vehicle(Vector2D pos, World w) : base(pos, w)
        {
            Velocity = new Vector2D(0, 0);
            Scale = 5;

            VColor = Color.Black;
        }

        public Color VColor { get; set; }

        public override void Render(Graphics g)
        {
            var leftCorner = Pos.X - Scale;
            var rightCorner = Pos.Y - Scale;
            double size = Scale * 2;

            var p = new Pen(VColor, 2);
            g.DrawEllipse(p, new Rectangle((int) leftCorner, (int) rightCorner, (int) size, (int) size));
            g.DrawLine(p, (int) Pos.X, (int) Pos.Y, (int) Pos.X + (int) (Velocity.X * 2),
                (int) Pos.Y + (int) (Velocity.Y * 2));
        }
    }
}