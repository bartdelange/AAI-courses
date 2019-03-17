using System.Drawing;

namespace AICore.Entity.Contracts
{
    public interface IRenderable
    {
        bool Visible { get; set; }
        
        void Render(Graphics graphics);
    }
}