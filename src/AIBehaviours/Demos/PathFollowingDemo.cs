using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AICore;
using AICore.Behaviour.Individual;

namespace AIBehaviours.Demos
{
    public partial class PathFollowingDemo : Form
    {
        private readonly World _world;
        
        public PathFollowingDemo()
        {
            InitializeComponent();

            // Create new world instance
            _world = new World(Width, Height);
            var worldControl = new WorldControl(_world);
            
            // Attach event handlers that are used in this demo
            worldControl.MouseClick += WorldPanel_MouseClick;

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