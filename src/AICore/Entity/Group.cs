using System.Collections.Generic;
using System.Drawing;
using AICore.Entity.Contracts;

namespace AICore.Entity
{
    public class Group : IRenderable
    {
        public bool Visible { get; set; } = true;

        private IEnumerable<IRenderable> Children { get; set; }

        public Group(IEnumerable<IRenderable> children)
        {
            Children = children;
        }

        public void Render(Graphics graphics)
        {
            foreach (var entity in Children)
            {
                entity.Render(graphics);
            }
        }
    }
}