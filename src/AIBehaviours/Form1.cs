using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using AIBehaviours.Behaviour.Group;
using AIBehaviours.Behaviour.Individual;
using AIBehaviours.Entity;
using AIBehaviours.Util;
using Timer = System.Timers.Timer;

namespace AIBehaviours
{
    public partial class Form1 : Form
    {
        private readonly World _world;
        private readonly Random _random = new Random();

        private const float TimeDelta = 0.8f;

        private readonly List<BehaviourItem> _steeringBehaviors = new List<BehaviourItem>()
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

        public Form1()
        {
            InitializeComponent();

            _world = new World(worldPanel.Width, worldPanel.Height);

            var timer = new Timer();

            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 20;
            timer.Enabled = true;

            CreateBehaviourControls();
            UpdateForm();

            // Select first item in entitiesList
            entityList.SetSelected(0, true);
        }

        private void CreateBehaviourControls()
        {
            var controlWidth = entityOverviewPanel.Width - SystemInformation.VerticalScrollBarWidth - 6;

            _steeringBehaviors.ForEach(behaviour =>
            {
                var control = new BehaviourControl(behaviour, entityList, _world)
                {
                    Width = controlWidth,
                };

                entityOverviewPanel.Controls.Add(control);
            });
        }

        private void UpdateForm()
        {
            var entitiesArray = _world.Entities.ToArray();

            entityList.Items.Clear();
            entityList.Items.AddRange(entitiesArray);

            foreach (var control in entityOverviewPanel.Controls)
            {
                if (!(control is BehaviourControl)) continue;

                ((BehaviourControl)control).UpdateEntities();
            }
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

        private void WorldPanel_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (MovingEntity entity in entityList.SelectedItems)
            {
                entity.Pos = new Vector2D(e.X, e.Y);
            }
        }

        private void AddVehicleButton_Click(object sender, EventArgs e)
        {
            var randomPosition = new Vector2D(_random.Next(0, worldPanel.Width), _random.Next(0, worldPanel.Height));

            var newVehicle = new Vehicle(
                randomPosition,
                Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256)),
                _world
            );

            _world.Entities.Add(newVehicle);

            UpdateForm();
        }

        private void RemoveVehicleButton_Click(object sender, EventArgs e)
        {
            if (entityList.SelectedItem == null) return;

            MovingEntity entity = (MovingEntity)entityList.SelectedItem;
            _world.Entities.Remove(entity);

            UpdateForm();
        }

        #endregion
    }
}