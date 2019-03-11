using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AIBehaviours.Utils;
using AICore;
using AICore.Behaviour.Individual;
using AICore.Entity;
using AICore.Util;

namespace AIBehaviours.Demos
{
    public partial class OffsetPursuitDemo : Form
    {
        public OffsetPursuitDemo()
        {
            InitializeComponent();

            var world = new World(Width, Height);
            var worldBound = new Vector2(Width, Height);

            var leader = new Vehicle(Vector2Util.GetRandom(worldBound), Color.Red, world);
            leader.SteeringBehaviour = new WanderBehaviour(leader);

            var entities = new List<MovingEntity>
            {
                new Vehicle(Vector2Util.GetRandom(worldBound), world),
                new Vehicle(Vector2Util.GetRandom(worldBound), world),
                new Vehicle(Vector2Util.GetRandom(worldBound), world),
                new Vehicle(Vector2Util.GetRandom(worldBound), world),
                new Vehicle(Vector2Util.GetRandom(worldBound), world)
            };

            MovingEntity previousEntity = null;
            foreach (var entity in entities)
            {                
                entity.SteeringBehaviour = new OffsetPursuit(
                    entity, 
                    previousEntity ?? leader, 
                    new Vector2(20, 20)
                );
                
                previousEntity = entity;
            }

            entities.Add(leader);

            // Populate world
            world.Entities = entities;
            world.Obstacles = ObstacleUtils.CreateObstacles(Width, Height, 250);
            

            Controls.Add(new WorldControl(world));
        }
    }
}