using System.Drawing;
using System.Numerics;
using AICore.SteeringBehaviour;

namespace AICore.Behaviour
{
    /// <summary>
    /// Behaviour that is used by the striker entity
    ///
    /// Rules:
    /// - Should not move too close to the own goal                             (Arrive / WanderBehaviour)
    /// - Should pursuit to opponent when opponent is within a certain range    (PursuitBehaviour)
    /// - Should move on a similar defensive line as other defenders            (OffsetPursuit / Arrive)
    /// - Should avoid obstacles in the field                                   (ObstacleAvoidance)
    /// - Should stay within the playing field                                  (WallAvoidance)
    /// - Should kick the ball towards the strikers
    /// </summary>
    public class DefenderBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        public Vector2 Calculate(float deltaTime)
        {
            return Vector2.Zero;
        }

        public void Render(Graphics graphics)
        {
        }
    }
}