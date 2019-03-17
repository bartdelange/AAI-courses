using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AIBehaviours.Utils;
using AICore;
using AICore.Behaviour;
using AICore.Behaviour.Individual;
using AICore.Behaviour.Util;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AIBehaviours.Demos
{
    internal class MySteeringBehaviour : ISteeringBehaviour
    {
        private readonly WeightedTruncatedRunningSumWithPrioritization _aggregateBehaviour;

        public MySteeringBehaviour(IMovingEntity entity, IEnumerable<IWall> walls)
        {
            var wallAvoidanceBehaviour = new WallAvoidanceBehaviour(entity, walls);
            var wanderBehaviour = new WanderBehaviour(entity);

            _aggregateBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                new OrderedDictionary
                {
                    {wallAvoidanceBehaviour, .8f},
                    {wanderBehaviour, .8f}
                },
                entity.MaxSpeed
            );
        }

        public Vector2 Calculate(float deltaTime)
        {
            return _aggregateBehaviour.Calculate(deltaTime);
        }

        public void Draw(Graphics g)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class WallAvoidanceDemo : Form
    {
        public WallAvoidanceDemo(int width = 1000, int height = 800)
        {
            InitializeComponent();

            Width = width;
            Height = height;

            var worldBounds = new Vector2(ClientSize.Width, ClientSize.Height);
            var world = new World(worldBounds);

            var obstacles = ObstacleUtils.CreateObstacles(worldBounds, 250);

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
                entity.SteeringBehaviour = new MySteeringBehaviour(entity, world.Walls);
            }

            // Populate world instance
            world.Obstacles = obstacles;
            world.Entities = entities;

            // Add world to form
            var worldControl = new WorldControl(world);

            Controls.Add(worldControl);
        }
    }
}