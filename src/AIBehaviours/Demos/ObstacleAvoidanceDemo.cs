using System.Numerics;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AIBehaviours.Utils;
using AICore;

namespace AIBehaviours.Demos
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ObstacleAvoidanceDemo : Form
    {
        public ObstacleAvoidanceDemo(int width, int height)
        {
            InitializeComponent();

            Width = width;
            Height = height;

            var worldBounds = new Vector2(ClientSize.Width, ClientSize.Height);

            var world = new World(worldBounds)
            {
                Obstacles = EntityUtils.CreateObstacles(worldBounds, 500)
            };

            var entities = EntityUtils.CreateVehicles(
                50,
                worldBounds,
                entity => entity.SteeringBehaviour = new WanderObstacleAvoidanceBehaviour(entity, world.Obstacles)
            );

            // Populate world instance
            world.Entities = entities;

            // Add world to form
            var worldControl = new WorldControl(world);

            Controls.Add(worldControl);
        }
    }
}