using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Timers;
using System.Windows.Forms;
using AICore;
using AICore.Behaviour.Individual;
using AICore.Entity;
using AICore.Util;
using Timer = System.Timers.Timer;

namespace AIBehaviours
{
    public partial class OffsetPursuitDemo : Form
    {
        private const float TimeDelta = 0.8f;

        private readonly World _world;

        public OffsetPursuitDemo()
        {
            InitializeComponent();

            Width = 1000;
            Height = 800;
            worldPanel.Width = 1000;
            worldPanel.Height = 800;

            _world = CreateWorld(Width, Height);

            var timer = new Timer();

            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 16;
            timer.Enabled = true;
        }

        World CreateWorld(int width, int height)
        {
            var world = new World(worldPanel.Width, worldPanel.Height);

            var max = new Vector2(width, height);
            var leader = new Vehicle(Vector2Util.GetRandom(max), _world);

            var entities = new List<MovingEntity>
            {
                new Vehicle(Vector2Util.GetRandom(max), _world),
                new Vehicle(Vector2Util.GetRandom(max), _world),
                new Vehicle(Vector2Util.GetRandom(max), _world),
                new Vehicle(Vector2Util.GetRandom(max), _world),
                new Vehicle(Vector2Util.GetRandom(max), _world)
            };

            for (int i = 0; i < entities.Count(); i++)
            {
                var entity = entities[i]; 

                entity.SteeringBehaviour = new PursuitBehaviour(entity, leader);
            }

            return world;
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

        #endregion
    }
}