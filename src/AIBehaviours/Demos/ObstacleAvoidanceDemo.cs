using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AIBehaviours.Controls;
using AICore.Model;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Individual;
using AICore.SteeringBehaviour.Util;
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

            var entities = EntityUtils.CreateVehicles(
                50,
                bounds,
                entity =>
                {
                    entity.SteeringBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                        new List<WeightedSteeringBehaviour>
                        {
                            new WeightedSteeringBehaviour(new ObstacleAvoidance(entity, world.Obstacles, 30), 1f),
                            new WeightedSteeringBehaviour(new Wander(entity), 1f),
                        },
                        entity.MaxSpeed
                    );
                    
                    entity.Middlewares.Add(new WrapAroundMiddleware(entity, bounds));
                }
            );

            world.Entities.AddRange(entities);

            World = world;
        }
    }
}