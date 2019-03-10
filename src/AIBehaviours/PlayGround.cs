#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AICore;
using AICore.Behaviour.Group;
using AICore.Behaviour.Individual;
using AICore.Entity;
using AICore.Model;

#endregion

namespace AIBehaviours
{
    public partial class PlayGround : DemoBase
    {
        private const float TimeDelta = 0.8f;
        private readonly Random _random = new Random();

        private readonly List<BehaviourItem> _steeringBehaviors = new List<BehaviourItem>
        {
            new BehaviourItem {Type = typeof(ArriveBehaviour), Name = "Individual - Arrive"},
            new BehaviourItem {Type = typeof(FleeBehaviour), Name = "Individual - Flee"},
            new BehaviourItem {Type = typeof(SeekBehaviour), Name = "Individual - Seek"},
            new BehaviourItem {Type = typeof(PursuitBehaviour), Name = "Individual - Pursuit"},
            new BehaviourItem {Type = typeof(EvadeBehaviour), Name = "Individual - Evade"},
            new BehaviourItem {Type = typeof(WanderBehaviour), Name = "Individual - Wander"},
            new BehaviourItem {Type = typeof(SeparationBehaviour), Name = "Group - Separation"},
            new BehaviourItem {Type = typeof(AlignmentBehaviour), Name = "Group - Alignment"},
            new BehaviourItem {Type = typeof(CohesionBehaviour), Name = "Group - Cohesion"}
        };

        public PlayGround()
        {
            InitializeComponent();

            InitWorldTimer();
            InitWorld(worldPanel);

            CreateBehaviourControls();
            UpdateForm();

            // Select first item in entitiesList
            entityList.SetSelected(0, true);
        }

        protected override World CreateWorld()
        {
            return new World(worldPanel.Width, worldPanel.Height);
        }

        private void CreateBehaviourControls()
        {
            var controlWidth = entityOverviewPanel.Width - SystemInformation.VerticalScrollBarWidth - 6;

            _steeringBehaviors.ForEach(behaviour =>
            {
                var control = new BehaviourControl(behaviour, entityList, World)
                {
                    Width = controlWidth
                };

                entityOverviewPanel.Controls.Add(control);
            });
        }

        private void UpdateForm()
        {
            if (World == null) return;

            entityList.Items.Clear();
            entityList.Items.AddRange(World.Entities.ToArray());

            foreach (var control in entityOverviewPanel.Controls) (control as BehaviourControl)?.UpdateEntities();
        }

        #region event handlers

        private void WorldPanel_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (MovingEntity entity in entityList.SelectedItems) entity.Pos = new Vector2(e.X, e.Y);
        }

        private void AddVehicleButton_Click(object sender, EventArgs e)
        {
            var randomPosition = new Vector2(_random.Next(0, worldPanel.Width), _random.Next(0, worldPanel.Height));

            var newVehicle = new Vehicle(
                randomPosition,
                Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256)),
                World
            );

            World.Entities.Add(newVehicle);

            UpdateForm();
        }

        private void RemoveVehicleButton_Click(object sender, EventArgs e)
        {
            if (entityList.SelectedItem == null) return;

            var entity = (MovingEntity) entityList.SelectedItem;
            World.Entities.Remove(entity);

            UpdateForm();
        }

        #endregion
    }
}