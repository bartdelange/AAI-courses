using System.Timers;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AICore;
using Timer = System.Timers.Timer;

namespace AIBehaviours
{
    public abstract class DemoBase : Form
    {
        private const float TimeDelta = 0.8f;
        protected World World;
        private DoubleBufferedPanel _worldPanel;
        public Menu menu;
        private bool _graphIsVisible;

        protected DemoBase()
        {
            Width = 1000;
            Height = 800;
        }

        protected void InitWorldTimer()
        {
            var timer = new Timer();
            
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 16;
            timer.Enabled = true;

            FormClosing += OnFormClosing;
            KeyPress += OnKeyPress;
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'g')
            {
                _graphIsVisible = !_graphIsVisible;
            }
        }

        protected void InitWorld(DoubleBufferedPanel worldPanel)
        {
            _worldPanel = worldPanel;
            _worldPanel.Width = Width;
            _worldPanel.Height = Height;
            _worldPanel.Paint += WorldPanel_Paint;

            World = CreateWorld();
        }

        protected abstract World CreateWorld();
        
        #region event handlers
        
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            World.Update(TimeDelta);
            _worldPanel.Invalidate();
        }

        private void WorldPanel_Paint(object sender, PaintEventArgs e)
        {
            World.Render(e.Graphics, _graphIsVisible);
        }
        
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            menu.Show();
        }
        
        #endregion
    }
}