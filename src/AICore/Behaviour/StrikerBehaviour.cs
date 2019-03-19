using System.Drawing;
using System.Numerics;
using AICore.SteeringBehaviour;

namespace AICore.Behaviour
{
    /// <summary>
    /// Behaviour that is used by the striker entity
    ///
    /// Rules:
    /// - Should stay near the opponents goal     (ArriveBehaviour / WanderBehaviour)
    /// - Should follow other strikers            (OffsetPursuit with (very) low weight)
    /// - Should avoid obstacles in the field     (ObstacleAvoidance)
    /// - Should stay within the field            (WallAvoidance)
    /// - Should move to tactical positions       (Exploring / PathFollowingBehaviour)
    /// - Should not move when tired
    /// - Should kick the ball to the opponents goal
    /// </summary>
    public class StrikerBehaviour : ISteeringBehaviour
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