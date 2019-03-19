using System.Collections.Generic;
using System.Drawing;
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
        private readonly IMovingEntity _entity;
        private List<WeightedSteeringBehaviour> _steeringBehaviours;

        public bool Visible { get; set; } = true;

        private readonly ISteeringBehaviour _aggregateBehaviour;

        public WanderObstacleAvoidanceBehaviour(IMovingEntity entity, IEnumerable<IObstacle> obstacles)
        {
            _entity = entity;
            var obstacleAvoidanceBehaviour = new ObstacleAvoidanceBehaviour(entity, obstacles, 50);
            var constantSteeringBehaviour = new ConstantSteeringBehaviour(new Vector2(50, 50));

            _steeringBehaviours = new List<WeightedSteeringBehaviour>
            {
                new WeightedSteeringBehaviour(obstacleAvoidanceBehaviour, 5f),
                new WeightedSteeringBehaviour(constantSteeringBehaviour, .5f)
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

            var sbv = "";
            _steeringBehaviours.ForEach(sb => sbv += $"{sb.SteeringBehaviour} > {sb.SteeringBehaviour.Calculate(1)}\n");

            graphics.DrawString(
                sbv,
                SystemFonts.DefaultFont,
                Brushes.Black,
                (_entity.Position + new Vector2(25, 25)).ToPoint()
            );
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class ObstacleAvoidanceDemo : Form
    {
        public ObstacleAvoidanceDemo(int width, int height)
        {
            InitializeComponent();

            Width = width;
            Height = height;

            ClientSize = new Size(width, height);


            var worldBounds = new Vector2(ClientSize.Width, ClientSize.Height);
            var world = new World(worldBounds);

            world.Obstacles = ObstacleUtils.CreateObstacles(worldBounds, 1000);

            var entities = new List<IMovingEntity>
            {
                new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds),
                new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds),
                new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds),
                new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds),
                new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds),
                new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds)
            };

            foreach (var entity in entities)
            {
                entity.SteeringBehaviour = new WanderObstacleAvoidanceBehaviour(entity, world.Obstacles);
            }

            // Populate world instance
            world.Entities = entities;

            // Add world to form
            var worldControl = new WorldControl(world);

            Controls.Add(worldControl);
        }
    }
}