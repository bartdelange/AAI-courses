using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using AICore;
using AICore.Behaviour.Individual;

namespace AIBehaviours.Demos
{
    public partial class PathFollowingDemo : DemoBase
    {
        public PathFollowingDemo()
        {
            InitializeComponent();

            InitWorld(worldPanel);
            InitWorldTimer();
        }

        protected override World CreateWorld()
        {
            return new World(worldPanel.Width, worldPanel.Height);
        }

        #region event handlers

        private void WorldPanel_MouseClick(object sender, MouseEventArgs mouseEventArgs)
        {
            var movingEntity = World.Entities.First();

            var path = World.Map.FindPath(
                movingEntity.Pos,
                new Vector2(mouseEventArgs.X, mouseEventArgs.Y)
            );

            movingEntity.SteeringBehaviour = new PathFollowingBehaviour(
                path,
                movingEntity
            );
        }

        #endregion
    }
}