using System.Timers;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AICore;
using Timer = System.Timers.Timer;

namespace AIBehaviours.Demos
{
    public abstract class DemoBase : Form
    {
        private const float TimeDelta = 0.8f;
        
        protected World World;
        
        public readonly DoubleBufferedPanel WorldPanel = new DoubleBufferedPanel()
        {
            Dock = DockStyle.Fill
        };
        
        protected DemoBase()
        {
            Width = 1000;
            Height = 800;
        }

        protected virtual void CreateWorld()
        {
            var timer = new Timer();
            
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 16;
            timer.Enabled = true;

            WorldPanel.Width = Width;
            WorldPanel.Height = Height;
        }
        
        #region event handlers
        
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            World?.Update(TimeDelta);
            
            WorldPanel.Invalidate();
        }
        
        #endregion
    }
}