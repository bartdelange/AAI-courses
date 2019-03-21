using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AIBehaviours.Utils;
using AICore;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.Graph.PathFinding;
using AICore.Navigation;
using AICore.SteeringBehaviour.Individual;

namespace AIBehaviours.Demos
{
    public partial class PathFollowingDemo : Form
    {
        private readonly World _world;
        
        public PathFollowingDemo(int width, int height)
        {
            InitializeComponent();
            
            Width = width;
            Height = height;

            var worldBounds = new Vector2(width, height);

            // Create new world instance
            _world = new World(worldBounds);

            // Populate world
            _world.Entities = new List<IMovingEntity> { new Vehicle(new Vector2(50, 50), worldBounds) };
            _world.Obstacles = EntityUtils.CreateObstacles(worldBounds, 500);

            // Create navigation layer
            _world.NavigationLayer = new NavigationLayer(
                new FineMesh(50, worldBounds, _world.Obstacles)
            );
            
            // Create world control and attach event handlers that are used in this demo
            var worldControl = new WorldControl(_world);
            worldControl.MouseClick += WorldPanel_MouseClick;

            // Create new world control that is used to render the World
            Controls.Add(worldControl);
        }

        private void WorldPanel_MouseClick(object sender, MouseEventArgs mouseEventArgs)
        {
            var movingEntity = _world.Entities.First();

            var path = _world.NavigationLayer.FindPath(
                movingEntity.Position,
                new Vector2(mouseEventArgs.X, mouseEventArgs.Y),
                new AStar<Vector2>(),
                new PrecisePathSmoothing()
            );

            movingEntity.SteeringBehaviour = new PathFollowingBehaviour(
                path,
                movingEntity
            );
        }
    }
}