using System.Drawing;
using System.Numerics;
using AIBehaviours.Controls;
using AIBehaviours.Utils;
using AICore;
using AICore.SteeringBehaviour.Aggregate;

namespace AIBehaviours.Demos
{
    public class ObstacleAvoidanceDemo : DemoForm
    {
        public ObstacleAvoidanceDemo(Size size) : base(size)
        {
            var worldBounds = new Vector2(WorldSize.Width, WorldSize.Height);

            World = new World(worldBounds)
            {
                Obstacles = EntityUtils.CreateObstacles(worldBounds, 500)
            };

            var entities = EntityUtils.CreateVehicles(
                50,
                worldBounds,
                entity => entity.SteeringBehaviour = new WanderObstacleAvoidanceBehaviour(entity, World.Obstacles)
            );

            // Populate world instance
            World.Entities = entities;
        }
    }
}