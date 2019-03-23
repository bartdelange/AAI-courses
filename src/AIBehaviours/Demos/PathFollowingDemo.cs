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
using AICore.Graph.PathFinding;
using AICore.Navigation;
using AICore.SteeringBehaviour.Individual;

namespace AIBehaviours.Demos
{
    public class PathFollowingDemo : DemoForm
    {
        public PathFollowingDemo(Size size) : base(size)
        {
            var worldBounds = new Vector2(WorldSize.Width, WorldSize.Height);

            // Create new world instance
            World = new World(worldBounds);
            var entity = new Vehicle(new Vector2(50, 50), worldBounds);

            // Populate world
            World.Entities = new List<IMovingEntity> { entity };
            World.Obstacles = EntityUtils.CreateObstacles(worldBounds, 500);

            // Create navigation layer
            World.NavigationLayer = new NavigationLayer(
                new FineMesh(50, worldBounds, World.Obstacles)
            );
            
            // Create world control and attach event handlers that are used in this demo
            WorldControl.MouseClick += WorldPanel_MouseClick;
        }

        private void WorldPanel_MouseClick(object sender, MouseEventArgs mouseEventArgs)
        {
            var movingEntity = World.Entities.First();

            var path = World.NavigationLayer.FindPath(
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