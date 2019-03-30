using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.SteeringBehaviour
{
    public interface ISteeringBehaviour : IRenderable
    {        
        /// <summary>
        ///     Method used to calculate a new velocity
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <returns>New velocity</returns>
        Vector2 Calculate(float deltaTime);
    }
}