using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AIBehaviours.Utils;
using AICore;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Individual;
using AICore.SteeringBehaviour.Util;
using AICore.Util;

namespace AIBehaviours.Demos
{
    public class WanderObstacleAvoidanceBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly IMovingEntity _entity;
        private readonly List<WeightedSteeringBehaviour> _steeringBehaviours;

        private readonly ISteeringBehaviour _aggregateBehaviour;

        public WanderObstacleAvoidanceBehaviour(IMovingEntity entity, IEnumerable<IObstacle> obstacles)
        {
            _entity = entity;
            var obstacleAvoidanceBehaviour = new ObstacleAvoidanceBehaviour(entity, obstacles, 40);
            var wanderBehaviour = new WanderBehaviour(entity);

            _steeringBehaviours = new List<WeightedSteeringBehaviour>
            {
                new WeightedSteeringBehaviour(obstacleAvoidanceBehaviour, 10f),
                new WeightedSteeringBehaviour(wanderBehaviour, 1f)
            };

            _aggregateBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                _steeringBehaviours,
                entity.MaxSpeed
            );
        }

        public Vector2 Calculate(float deltaTime)
        {
            return _aggregateBehaviour.Calculate(deltaTime);
        }

        public void Render(Graphics graphics)
        {
            _aggregateBehaviour.RenderIfVisible(graphics);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class ObstacleAvoidanceDemo : Form
    {
        private readonly World _world;

        public ObstacleAvoidanceDemo(int width, int height)
        {
            InitializeComponent();

            Width = width;
            Height = height;

            ClientSize = new Size(width, height);


            var worldBounds = new Vector2(ClientSize.Width, ClientSize.Height);
            _world = new World(worldBounds);

            _world.Obstacles = EntityUtils.CreateObstacles(worldBounds, 500);

            var entities = EntityUtils.CreateVehicles(
                50,
                worldBounds,
                entity => entity.SteeringBehaviour = new WanderObstacleAvoidanceBehaviour(entity, _world.Obstacles));

            // Populate world instance
            _world.Entities = entities;

            // Add world to form
            var worldControl = new WorldControl(_world);
            worldControl.MouseClick += HandleMouseClick;

            Controls.Add(worldControl);
        }

        private void HandleMouseClick(object sender, MouseEventArgs e)
        {
            var firstEntity = _world.Entities.First();

            firstEntity.Position = new Vector2(e.X, e.Y);
        }
    }
}