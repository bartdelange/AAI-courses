using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AIBehaviours.Utils;
using AICore;
using AICore.Behaviour.Individual;
using AICore.Entity;
using AICore.Map;

namespace AIBehaviours.Demos
{
    public partial class PathFollowingDemo : Form
    {
        private readonly World _world;
        
        public PathFollowingDemo(int width = 1000, int height = 800)
        {
            InitializeComponent();
            Width = width;
            Height = height;

            // Create new world instance
            _world = new World(ClientSize.Width, ClientSize.Height);

            // Populate world
            _world.Entities = new List<MovingEntity> { new Vehicle(new Vector2(50, 50), _world) };
            _world.Obstacles = ObstacleUtils.CreateObstacles(ClientSize.Width, ClientSize.Height, 2000);
            _world.Map = new CoarseMap(ClientSize.Width, ClientSize.Height, _world.Obstacles);
            
            // Create world control and attach event handlers that are used in this demo
            var worldControl = new WorldControl(_world);
            worldControl.MouseClick += WorldPanel_MouseClick;
            KeyPress += worldControl.WorldPanel_KeyPress;

            // Create new world control that is used to render the World
            Controls.Add(worldControl);
        }

        private void WorldPanel_MouseClick(object sender, MouseEventArgs mouseEventArgs)
        {
            var movingEntity = _world.Entities.First();

            var path = _world.Map.FindPath(
                movingEntity.Pos,
                new Vector2(mouseEventArgs.X, mouseEventArgs.Y)
            );

            movingEntity.SteeringBehaviour = new PathFollowingBehaviour(
                path,
                movingEntity
            );
        }
    }
}