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

            var entities = EntityUtils.CreateVehicles(
                50,
                bounds,
                entity => entity.SteeringBehaviour = new WanderObstacleAvoidanceBehaviour(entity, World.Obstacles)
            );
            
            World.Entities.AddRange(entities);
        }
    }
}