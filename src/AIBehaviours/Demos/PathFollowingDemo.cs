using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AIBehaviours.Utils;
using AICore;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.Entity.Dynamic;
using AICore.Graph.PathFinding;
using AICore.Model;
using AICore.Navigation;
using AICore.SteeringBehaviour.Individual;
using AICore.Worlds;

namespace AIBehaviours.Demos
{
    public class PathFollowingDemo : DemoForm
    {
        private World _world;

        public PathFollowingDemo(Size size) : base(size)
        {
            var bounds = new Bounds(Vector2.Zero, WorldSize);

            // Create new world instance
            _world = new World();

            // Populate world
            _world.Entities.Add(new Vehicle(new Vector2(50, 50)));
            _world.Obstacles.AddRange(EntityUtils.CreateObstacles(bounds, 500));
            _world.NavigationLayer = new NavigationLayer(new FineMesh(50, bounds, _world.Obstacles));
            
            // Set world instance of demo form
            World = _world;
            
            // Create world control and attach event handlers that are used in this demo
            WorldControl.MouseClick += WorldPanel_MouseClick;
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