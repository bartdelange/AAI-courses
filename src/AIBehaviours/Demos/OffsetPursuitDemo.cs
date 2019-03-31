using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AIBehaviours.Controls;
using AICore.Entity.Contracts;
using AICore.Entity.Dynamic;
using AICore.Model;
using AICore.SteeringBehaviour.Individual;
using AICore.SteeringBehaviour.Util;
using AICore.Util;
using AICore.Worlds;

namespace AIBehaviours.Demos
{
    public class OffsetPursuitDemo : DemoForm
    {
        public OffsetPursuitDemo(Size size) : base(size)
        {
            var world = new World(); 
            var bounds = new Bounds(Vector2.Zero, WorldSize);

            // Create leader entity
            var leader = new Vehicle(
                Vector2ExtensionMethods.GetRandom(bounds),
                Color.Red
            );

            // Add steering behaviour to leader entity
            leader.SteeringBehaviour = new Wander(leader);

            // Create follower entities
            var followers = new List<IMovingEntity>
            {
                new Vehicle(Vector2ExtensionMethods.GetRandom(bounds)),
                new Vehicle(Vector2ExtensionMethods.GetRandom(bounds)),
                new Vehicle(Vector2ExtensionMethods.GetRandom(bounds)),
                new Vehicle(Vector2ExtensionMethods.GetRandom(bounds)),
                new Vehicle(Vector2ExtensionMethods.GetRandom(bounds))
            };

            // Add steering behaviour to followers
            IMovingEntity previousEntity = leader;
            followers.ForEach(entity =>
            {
                entity.SteeringBehaviour = new OffsetPursuit(entity, previousEntity, new Vector2(-100, 0));
                previousEntity = entity;
            });

            // Add leader to list of entities
            followers.Add(leader);

            followers.ForEach(entity =>
            {
                entity.Middlewares.Add(new ZeroOverlapMiddleware(entity, followers));
                entity.Middlewares.Add(new WrapAroundMiddleware(entity, bounds));
            });

            // Populate world
            world.Entities.AddRange(followers);

            World = world;
        }
    }
}