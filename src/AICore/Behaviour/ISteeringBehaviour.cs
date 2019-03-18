using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.Behaviour
{
    public interface ISteeringBehaviour : IRenderable
    {
        /// <summary>
        ///     Method used to calculate the new velocity
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <returns>New velocity</returns>
        Vector2 Calculate(float deltaTime);
    }
}