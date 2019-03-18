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
        public bool Visible { get; set; } = true;

        private readonly WeightedTruncatedRunningSumWithPrioritization _aggregateBehaviour;

        public MySteeringBehaviour(IMovingEntity entity, IEnumerable<IWall> walls)
        {
            var wallAvoidanceBehaviour = new WallAvoidanceBehaviour(entity, walls);
            var wanderBehaviour = new WanderBehaviour(entity);

            _aggregateBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                new List<WeightedSteeringBehaviour>
                {
                    new WeightedSteeringBehaviour(wallAvoidanceBehaviour, .5f),
                    new WeightedSteeringBehaviour(wanderBehaviour, .5f)
                },
                entity.MaxSpeed
            );
        }

        public Vector2 Calculate(float deltaTime)
        {
            return _aggregateBehaviour.Calculate(deltaTime);
        }

        public void Render(Graphics graphics)
        {
            _aggregateBehaviour.Render(graphics);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class WallAvoidanceDemo : Form
    {
        public WallAvoidanceDemo(int width, int height)
        {
            InitializeComponent();

            Width = width;
            Height = height;

            ClientSize = new Size(width, height);

            
            var worldBounds = new Vector2(ClientSize.Width, ClientSize.Height);
            var world = new World(worldBounds);

            var wallMargin = 20;
            var walls = new List<IWall>()
            {
                // Top border
                new Wall(new Vector2(wallMargin, wallMargin), new Vector2(Width - wallMargin, wallMargin)),

                // Right border
                new Wall(new Vector2(Width - wallMargin, wallMargin),
                    new Vector2(Width - wallMargin, Height - wallMargin)),

                // Bottom border
                new Wall(new Vector2(wallMargin, Height - wallMargin),
                    new Vector2(Width - wallMargin, Height - wallMargin)),

                // Left border
                new Wall(new Vector2(wallMargin, wallMargin), new Vector2(wallMargin, Height - wallMargin)),
            };

            world.Walls = walls;


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
            world.Entities = entities;

            // Add world to form
            var worldControl = new WorldControl(world);

            Controls.Add(worldControl);
        }
    }
}