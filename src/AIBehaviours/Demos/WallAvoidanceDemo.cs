using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AIBehaviours.Utils;
using AICore;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour.Aggregate;
using AICore.SteeringBehaviour.Util;
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

            const int wallMargin = 20;

            var walls = new List<IWall>()
            {
                // Top border
                new Wall(new Vector2(wallMargin, wallMargin), new Vector2(Width - wallMargin, wallMargin)),

                // Right border
                new Wall(new Vector2(Width - wallMargin, wallMargin),
                    new Vector2(Width - wallMargin, Height - wallMargin)),

                // Bottom border
                new Wall(new Vector2(Width - wallMargin, Height - wallMargin),
                    new Vector2(wallMargin, Height - wallMargin)),

                // Left border
                new Wall(new Vector2(wallMargin, Height - wallMargin), new Vector2(wallMargin, wallMargin)),
            };

            var worldBounds = new Vector2(ClientSize.Width, ClientSize.Height);
            var world = new World(worldBounds);

            world.Walls = walls;

            // Populate world instance
            world.Entities = EntityUtils.CreateVehicles(
                150,
                worldBounds,
                (entity) => entity.SteeringBehaviour = new WanderWallAvoidanceBehaviour(entity, world.Walls)
            );

            world.Entities.ForEach(entity =>
                entity.Middlewares = new[] {new ZeroOverlap(entity, world.Entities)}
            );

            // Add world to form
            var worldControl = new WorldControl(world);

            Controls.Add(worldControl);
        }
    }
}