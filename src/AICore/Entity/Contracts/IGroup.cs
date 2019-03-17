using System.Collections.Generic;

namespace AICore.Entity.Contracts
{
    public interface IGroup : IEntity
    {
        IEnumerable<IRenderable> Children { get; set; }
    }
}