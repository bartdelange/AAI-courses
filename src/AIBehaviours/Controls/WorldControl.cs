using System.Timers;
using System.Windows.Forms;
using AICore.Worlds;
using Timer = System.Timers.Timer;

namespace AIBehaviours.Controls
{
    public sealed class WorldControl : Panel
    {
        private const float TimeDelta = 0.8f;
        
        public IWorld World { get; set; }

        public WorldControl()
        {
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            
            Paint += WorldPanel_Paint;
            
            Focus();

            // Create timer that is used to invalidate _worldPanel every 16ms (60fps)
            var demoTimer = new Timer();
            demoTimer.Elapsed += Timer_Elapsed;
            demoTimer.Interval = 16;
            demoTimer.Enabled = true;
        }

        private void WorldPanel_Paint(object sender, PaintEventArgs e)
        {
            World?.Render(e.Graphics);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            World?.Update(TimeDelta);
            Invalidate();
        }
    }
}