﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Timers;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AICore;
using AICore.Behaviour.Group;
using AICore.Behaviour.Individual;
using AICore.Entity;
using AICore.Model;
using Timer = System.Timers.Timer;

namespace AIBehaviours
{
    public partial class PlayGround : Form
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

        private readonly World _world;

        public Menu menu;

        public PlayGround()
        {
            InitializeComponent();

            Width = 1000;
            Height = 800;
            worldPanel.Width = Width - entityOverviewPanel.Width;
            worldPanel.Height = Height;

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
                    Width = controlWidth
                };

                entityOverviewPanel.Controls.Add(control);
            });
        }

        private void UpdateForm()
        {
            if (_world == null) return;

            entityList.Items.Clear();
            entityList.Items.AddRange(_world.Entities.ToArray());

            foreach (var control in entityOverviewPanel.Controls) (control as BehaviourControl)?.UpdateEntities();
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
            foreach (MovingEntity entity in entityList.SelectedItems) entity.Pos = new Vector2(e.X, e.Y);
        }

        private void AddVehicleButton_Click(object sender, EventArgs e)
        {
            var randomPosition = new Vector2(_random.Next(0, worldPanel.Width), _random.Next(0, worldPanel.Height));

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

            var entity = (MovingEntity) entityList.SelectedItem;
            _world.Entities.Remove(entity);

            UpdateForm();
        }

        private void PlayGround_FormClosing(object sender, FormClosingEventArgs e)
        {
            menu.Show();
        }

        #endregion
    }
}