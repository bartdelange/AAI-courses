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
        public const float timeDelta = 0.8f;

        private readonly Timer timer;
        private World world;

        public Form1()
        {
            InitializeComponent();

            world = new World(dbPanel1.Width, dbPanel1.Height);

            var steeringBehaviours = new[]
            {
                typeof(ArriveBehaviour),
                typeof(FleeBehaviour),
                typeof(SeekBehaviour),
                typeof(PersuitBehavior),
                typeof(EvadeBehaviour),
                typeof(WanderBehaviour)
            };

            comboBox1.Items.AddRange(steeringBehaviours);
            comboBox2.Items.AddRange(steeringBehaviours);

            timer = new Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 20;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            world.Update(timeDelta);
            dbPanel1.Invalidate();
        }

        private void dbPanel1_Paint(object sender, PaintEventArgs e)
        {
            world.Render(e.Graphics);
        }

        private void dbPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                world.Entities[0].Pos = new Vector2D(e.X, e.Y);
            else
                world.Entities[1].Pos = new Vector2D(e.X, e.Y);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            world = new World(dbPanel1.Width, dbPanel1.Height, (Type) comboBox1.SelectedItem,
                (Type) comboBox2.SelectedItem);
        }
    }
}