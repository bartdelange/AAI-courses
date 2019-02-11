using System;
using System.Timers;
using System.Windows.Forms;
using AIBehaviors.Behaviour.Group;
using AIBehaviors.Behaviour.Individual;
using AIBehaviors.Util;
using Timer = System.Timers.Timer;

namespace AIBehaviors
{
    public partial class Form1 : Form
    {
        private const float TimeDelta = 0.8f;

        private World _world;

        public Form1()
        {
            InitializeComponent();

            _world = new World(worldPanel.Width, worldPanel.Height);

            BehaviourItem[] steeringBehaviors =
            {
                new BehaviourItem {Type = typeof(ArriveBehaviour), Name = "Individual - Arrive"},
                new BehaviourItem {Type = typeof(FleeBehaviour), Name = "Individual - Flee"},
                new BehaviourItem {Type = typeof(SeekBehaviour), Name = "Individual - Seek"},
                new BehaviourItem {Type = typeof(PursuitBehavior), Name = "Individual - Pursuit"},
                new BehaviourItem {Type = typeof(EvadeBehaviour), Name = "Individual - Evade"},
                new BehaviourItem {Type = typeof(WanderBehaviour), Name = "Individual - Wander"},
                new BehaviourItem {Type = typeof(SeparationBehaviour), Name = "Group - Separation"},
                new BehaviourItem {Type = typeof(AlignmentBehaviour), Name = "Group - Alignment"},
                new BehaviourItem {Type = typeof(CohesionBehaviour), Name = "Group - Cohesion"}
            };


            blueBehaviourSelect.DisplayMember = "Name";
            blueBehaviourSelect.ValueMember = "Type";
            blueBehaviourSelect.DataSource = steeringBehaviors.Clone();

            redBehaviourSelect.DisplayMember = "Name";
            redBehaviourSelect.ValueMember = "Type";
            redBehaviourSelect.DataSource = steeringBehaviors.Clone();

            var timer = new Timer();

            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 20;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _world.Update(TimeDelta);
            worldPanel.Invalidate();
        }

        private void dbPanel1_Paint(object sender, PaintEventArgs e)
        {
            _world.Render(e.Graphics);
        }

        private void dbPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                _world._Entities[0].Pos = new Vector2D(e.X, e.Y);
            else
                _world._Entities[1].Pos = new Vector2D(e.X, e.Y);
        }

        private void ChangeBehaviour(object sender, EventArgs e)
        {
            var blueBehaviour = ((BehaviourItem) blueBehaviourSelect.SelectedItem)?.Type ?? typeof(WanderBehaviour);
            var redBehaviour = ((BehaviourItem) redBehaviourSelect.SelectedItem)?.Type ?? typeof(WanderBehaviour);

            _world = new World(
                worldPanel.Width,
                worldPanel.Height,
                blueBehaviour,
                redBehaviour
            );
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            var blueBehaviour = ((BehaviourItem)blueBehaviourSelect.SelectedItem)?.Type ?? typeof(WanderBehaviour);
            var redBehaviour = ((BehaviourItem)redBehaviourSelect.SelectedItem)?.Type ?? typeof(WanderBehaviour);

            _world = new World(
                worldPanel.Width,
                worldPanel.Height,
                blueBehaviour,
                redBehaviour
            );
        }
    }
}