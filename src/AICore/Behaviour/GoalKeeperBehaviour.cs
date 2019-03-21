using System.Drawing;
using System.Numerics;
using AICore.SteeringBehaviour;

namespace AICore.Behaviour
{
    /// <summary>
    /// Behaviour that is used by the goal keeper
    ///
    /// Rules:
    /// - Should stay near the goal                                (ArriveBehaviour / WanderBehaviour)
    /// - Should avoid obstacles in the field                      (ObstacleAvoidanceBehaviour)
    /// - Should stay within the playing field                     (WallAvoidanceBehaviour)
    /// - Should not move when tired                               
    /// - Should kick the ball away when too close to the goal     
    /// </summary>
    public class GoalKeeperBehaviour : ISteeringBehaviour
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