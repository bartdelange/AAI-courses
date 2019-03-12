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
        public OffsetPursuitDemo(int width = 1000, int height = 800)
        {
            InitializeComponent();
            Width = width;
            Height = height;

            var world = new World(ClientSize.Width, ClientSize.Height);
            var worldBound = new Vector2(ClientSize.Width, ClientSize.Height);

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

            MovingEntity previousEntity = leader;
            foreach (var entity in entities)
            {                
                entity.SteeringBehaviour = new OffsetPursuit(
                    entity, 
                    previousEntity, 
                    new Vector2(-100, 0)
                );
                
                previousEntity = entity;
            }

            entities.Add(leader);

            // Populate world
            world.Entities = entities;
            world.Obstacles = ObstacleUtils.CreateObstacles(ClientSize.Width, ClientSize.Height, 250);
            
            var worldControl = new WorldControl(world);
            KeyPress += worldControl.WorldPanel_KeyPress;

            Controls.Add(worldControl);
        }
    }
}