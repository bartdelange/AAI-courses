using System.Drawing;
using System.Numerics;
using AICore.SteeringBehaviour;

namespace AICore.Behaviour
{
    /// <summary>
    /// Behaviour that is used by the striker entity
    ///
    /// Rules:
    /// - Should follow ball while watching from the sideline    (AlignmentBehaviour) // Should not update the velocity, just the instances' Heading
    /// - Should avoid obstacles                                 (ObstacleAvoidance)
    /// - Should stay out of the playing field                   (WallAvoidance)
    /// </summary>
    public class CrowdBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; }

        public Vector2 Calculate(float deltaTime)
        {
            return Vector2.Zero;
        }
        
        public void Render(Graphics graphics)
        {
        }
    }
}