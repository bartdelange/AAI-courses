using System.Drawing;
using System.Numerics;
using AIBehaviours.Controls;
using AIBehaviours.Utils;
using AICore;
using AICore.Model;
using AICore.SteeringBehaviour.Aggregate;

namespace AIBehaviours.Demos
{
    public class ObstacleAvoidanceDemo : DemoForm
    {
        public ObstacleAvoidanceDemo(Size size) : base(size)
        {
            var bounds = new Bounds(new Vector2(), WorldSize);

            World = new World();
            
            World.Obstacles.AddRange(EntityUtils.CreateObstacles(bounds, 500));

            // Create walls
            const int margin = 20;
            World.Walls.AddRange(EntityUtils.CreateCage(
                new Bounds(Vector2.Zero, new Vector2(WorldSize.Width, WorldSize.Height)) - new Vector2(margin)
            ));

            var entities = EntityUtils.CreateVehicles(
                50,
                bounds,
                entity => entity.SteeringBehaviour = new WanderWallObstacleAvoidanceBehaviour(entity, World.Obstacles, World.Walls)
            );
            
            World.Entities.AddRange(entities);
        }
    }
}