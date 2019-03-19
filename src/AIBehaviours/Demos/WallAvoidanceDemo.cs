using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AICore;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour.Aggregate;
using AICore.Util;

namespace AIBehaviours.Demos
{
    /// <summary>
    /// 
    /// </summary>
    public partial class WallAvoidanceDemo : Form
    {
        public WallAvoidanceDemo(int width, int height)
        {
            InitializeComponent();

            Width = width;
            Height = height;

            ClientSize = new Size(width, height);

            
            var worldBounds = new Vector2(ClientSize.Width, ClientSize.Height);
            var world = new World(worldBounds);

            var wallMargin = 20;
            var walls = new List<IWall>()
            {
                // Top border
                new Wall(new Vector2(wallMargin, wallMargin), new Vector2(Width - wallMargin, wallMargin)),

                // Right border
                new Wall(new Vector2(Width - wallMargin, wallMargin),
                    new Vector2(Width - wallMargin, Height - wallMargin)),

                // Bottom border
                new Wall(new Vector2(wallMargin, Height - wallMargin),
                    new Vector2(Width - wallMargin, Height - wallMargin)),

                // Left border
                new Wall(new Vector2(wallMargin, wallMargin), new Vector2(wallMargin, Height - wallMargin)),
            };

            world.Walls = walls;


            var entities = new List<IMovingEntity>
            {
                new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds),
                new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds),
                new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds),
                new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds),
                new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds),
                new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds)
            };

            foreach (var entity in entities)
            {
                entity.SteeringBehaviour = new WanderWallAvoidanceBehaviour(entity, world.Walls);
            }

            // Populate world instance
            world.Entities = entities;

            // Add world to form
            var worldControl = new WorldControl(world);

            Controls.Add(worldControl);
        }
    }
}