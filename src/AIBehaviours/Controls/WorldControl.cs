using System.Diagnostics;
using System.Timers;
using System.Windows.Forms;
using AICore;
using Timer = System.Timers.Timer;

namespace AIBehaviours.Controls
{
    public sealed class WorldControl : Panel
    {
        private const float TimeDelta = 0.8f;
        private readonly World _world;
        private bool _graphIsVisible = true;

        public WorldControl(World world)
        {
            Width = world.Width;
            Height = world.Height;
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            
            Paint += WorldPanel_Paint;
            
            Focus();

            // Save world instance for later use
            _world = world;

            // Create timer that is used to invalidate _worldPanel every 16ms (60fps)
            var demoTimer = new Timer();
            demoTimer.Elapsed += Timer_Elapsed;
            demoTimer.Interval = 16;
            demoTimer.Enabled = true;
        }

        public void WorldPanel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 'g') return;
            
            _graphIsVisible = !_graphIsVisible;
            Invalidate();
        }

        private void WorldPanel_Paint(object sender, PaintEventArgs e)
        {
            _world.Render(e.Graphics, _graphIsVisible);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _world.Update(TimeDelta);
            Invalidate();
        }
    }
}