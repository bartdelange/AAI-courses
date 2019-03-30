using System.Drawing;
using System.Numerics;
using AIBehaviours.Controls;
using AICore.Model;
using AICore.SteeringBehaviour.Aggregate;
using AICore.Util;
using AICore.Worlds;

namespace AIBehaviours.Demos
{
    public class ObstacleAvoidanceDemo : DemoForm
    {
        public ObstacleAvoidanceDemo(Size size) : base(size)
        {
            var bounds = new Bounds(Vector2.Zero, WorldSize);

            var world = new World();
            
            world.Obstacles.AddRange(EntityUtils.CreateObstacles(bounds, 500));

            // Create walls
            const int margin = 20;
            world.Walls.AddRange(EntityUtils.CreateCage(
                new Bounds(Vector2.Zero, new Vector2(WorldSize.Width, WorldSize.Height)) - new Vector2(margin)
            ));

            var entities = EntityUtils.CreateVehicles(
                50,
                bounds,
                entity => entity.SteeringBehaviour = new WanderWallObstacleAvoidanceBehaviour(entity, world.Obstacles, world.Walls)
            );
            
            world.Entities.AddRange(entities);

            World = world;
        }
    }
}