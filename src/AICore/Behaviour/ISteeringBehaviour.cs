using System.Drawing;
using System.Numerics;

namespace AICore.Behaviour
{
    public interface ISteeringBehaviour
    {
        /// <summary>
        ///     Method used to calculate the new velocity
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <returns>New velocity</returns>
        Vector2 Calculate(float deltaTime);

        /// <summary>
        ///     Method used to visualize the steering behaviour
        /// </summary>
        /// <param name="g"></param>
        void Draw(Graphics g);
    }
}