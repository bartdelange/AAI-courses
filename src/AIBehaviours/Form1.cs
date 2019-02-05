using System;
using System.Timers;
using System.Windows.Forms;
using AIBehaviours.behaviour;
using AIBehaviours.util;
using AIBehaviours.world;
using Timer = System.Timers.Timer;

namespace AIBehaviours
{
    public partial class Form1 : Form
    {
        private const float TimeDelta = 0.8f;

        private readonly Timer _timer;
        private World _world;

        public Form1()
        {
            InitializeComponent();

            _world = new World(dbPanel1.Width, dbPanel1.Height);

            object[] steeringBehaviours = {
                typeof(ArriveBehaviour),
                typeof(FleeBehaviour),
                typeof(SeekBehaviour),
                typeof(PersuitBehavior),
                typeof(EvadeBehaviour),
                typeof(WanderBehaviour)
            };

            comboBox1.Items.AddRange(steeringBehaviours);
            comboBox2.Items.AddRange(steeringBehaviours);

            _timer = new Timer();
            _timer.Elapsed += Timer_Elapsed;
            _timer.Interval = 20;
            _timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _world.Update(TimeDelta);
            dbPanel1.Invalidate();
        }

        private void dbPanel1_Paint(object sender, PaintEventArgs e)
        {
            _world.Render(e.Graphics);
        }

        private void dbPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                _world.Entities[0].Pos = new Vector2D(e.X, e.Y);
            else
                _world.Entities[1].Pos = new Vector2D(e.X, e.Y);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _world = new World(
                dbPanel1.Width,
                dbPanel1.Height,
                (Type) comboBox1.SelectedItem,
                (Type) comboBox2.SelectedItem
            );
        }
    }
}