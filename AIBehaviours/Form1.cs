using AIBehaviours.behaviour;
using AIBehaviours.util;
using AIBehaviours.world;
using System;
using System.Windows.Forms;

namespace AIBehaviours
{
    public partial class Form1 : Form
    {
        private World world;

        private System.Timers.Timer timer;

        public const float timeDelta = 0.8f;

        public Form1()
        {
            InitializeComponent();

            world = new World(w: dbPanel1.Width, h: dbPanel1.Height);

            Type[] steeringBehaviours = new Type[] {
                typeof(ArriveBehaviour),
                typeof(FleeBehaviour),
                typeof(SeekBehaviour),
                typeof(PersuitBehavior),
                typeof(EvadeBehaviour),
            };

            comboBox1.Items.AddRange(steeringBehaviours);
            comboBox2.Items.AddRange(steeringBehaviours);

            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 20;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
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
            {
                world.Entities[0].Pos = new Vector2D(e.X, e.Y);
            }
            else
            {
                world.Entities[1].Pos = new Vector2D(e.X, e.Y);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            world = new World(dbPanel1.Width, dbPanel1.Height, (Type)comboBox1.SelectedItem, (Type)comboBox2.SelectedItem);
        }
    }
}
