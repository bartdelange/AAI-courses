using System.Drawing;
using System.Numerics;
using AIBehaviours.Controls;
using AIBehaviours.Utils;
using AICore.Model;
using AICore.SteeringBehaviour.Aggregate;
using AICore.SteeringBehaviour.Util;
using AICore.Worlds;

namespace AIBehaviours.Demos
{
    /// <summary>
    /// 
    /// </summary>
    public class WallAvoidanceDemo : DemoForm
    {
        public WallAvoidanceDemo(Size size) : base(size)
        {
            var world = new World();
            
            var bounds = new Bounds(Vector2.Zero, WorldSize);

            // Create walls
            const int margin = 20;
            world.Walls.AddRange(EntityUtils.CreateCage(
                new Bounds(Vector2.Zero, new Vector2(WorldSize.Width, WorldSize.Height)) - new Vector2(margin)
            ));

            // Create vehicles
            var vehicles = EntityUtils.CreateVehicles(
                150,
                bounds,
                (entity) => entity.SteeringBehaviour = new WanderWallAvoidanceBehaviour(entity, world.Walls)
            );

            world.Entities.AddRange(vehicles);

            // Apply zero overlap middleware
            world.Entities.ForEach(entity =>
            {
                entity.Middlewares.Add(new ZeroOverlapMiddleware(entity, world.Entities));
                entity.Middlewares.Add(new ZeroOverlapMiddleware(entity, world.Entities));
            });

            World = world;
        }
    }
}