using System.Drawing;

namespace AICore.Worlds
{
    public interface IWorld
    {
        void Update(float timeElapsed);

        void Render(Graphics graphics);
    }
}