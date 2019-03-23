using System;
using System.Drawing;
using System.Numerics;
using AIBehaviours.Controls;
using AIBehaviours.Utils;
using AICore;
using AICore.SteeringBehaviour.Aggregate;
using AICore.SteeringBehaviour.Util;

namespace AIBehaviours.Demos
{
    /// <summary>
    /// 
    /// </summary>
    public class WallAvoidanceDemo : DemoForm
    {
        public WallAvoidanceDemo(Size size) : base(size)
        {
            var worldBounds = new Vector2(WorldSize.Width, WorldSize.Height);
            
            const int margin = 20;
            
            World = new World(worldBounds);
            
            // Create walls
            World.Walls = EntityUtils.CreateCage(
                new Tuple<Vector2, Vector2>(
                    new Vector2(margin, margin),
                    new Vector2(WorldSize.Width - margin, WorldSize.Height - margin)
                )
            );

            // Create vehicles
            World.Entities = EntityUtils.CreateVehicles(
                150,
                worldBounds,
                (entity) => entity.SteeringBehaviour = new WanderWallAvoidanceBehaviour(entity, World.Walls)
            );

            // Apply zero overlap middleware
            World.Entities.ForEach(entity =>
                entity.Middlewares = new[] {new ZeroOverlap(entity, World.Entities)}
            );
        }
    }
}