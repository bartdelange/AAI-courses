#region

using System.Linq;
using System.Numerics;
using System.Timers;
using System.Windows.Forms;
using AICore;
using AICore.Behaviour.Individual;
using Timer = System.Timers.Timer;

#endregion

namespace AIBehaviours
{
    public partial class PathFollowingDemo : Form
    {
        private const float TimeDelta = 0.8f;

        private readonly World _world;

        public Menu menu;

        public PathFollowingDemo()
        {
            InitializeComponent();

            Width = 1000;
            Height = 800;
            worldPanel.Width = Width;
            worldPanel.Height = Height;

            _world = new World(worldPanel.Width, worldPanel.Height);

            var timer = new Timer();

            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 16;
            timer.Enabled = true;
        }


        #region event handlers

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _world.Update(TimeDelta);
            worldPanel.Invalidate();
        }

        private void WorldPanel_Paint(object sender, PaintEventArgs e)
        {
            _world.Render(e.Graphics);
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

        private void PathFollowingDemo_FormClosing(object sender, FormClosingEventArgs e)
        {
            menu.Show();
        }

        #endregion
    }
}