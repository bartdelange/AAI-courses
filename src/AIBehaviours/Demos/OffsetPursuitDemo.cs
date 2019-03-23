using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AICore;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour.Individual;
using AICore.Util;

namespace AIBehaviours.Demos
{
    public class OffsetPursuitDemo : DemoForm
    {
        public OffsetPursuitDemo(Size size) : base(size)
        {
            var worldBounds = new Vector2(WorldSize.Width, WorldSize.Height);
            World = new World(worldBounds);

            // Create leader entity
            var leader = new Vehicle(
                Vector2ExtensionMethods.GetRandom(worldBounds),
                worldBounds,
                new Pen(Color.Red)
            );

            // Add steering behaviour to leader entity
            leader.SteeringBehaviour = new WanderBehaviour(leader);

            // Create follower entities
            var followers = new List<IMovingEntity>
            {
                new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds),
                new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds),
                new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds),
                new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds),
                new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds)
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

            // Populate world
            World.Entities = followers;
        }
    }
}