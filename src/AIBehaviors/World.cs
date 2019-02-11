using System;
using System.Collections.Generic;
using System.Drawing;
using AIBehaviors.Behaviour;
using AIBehaviors.Behaviour.Individual;
using AIBehaviors.Entity;
using AIBehaviors.Map;
using AIBehaviors.Util;

namespace AIBehaviors
{
    public class World
    {
        private readonly Type _blueSteeringBehaviour;

        public readonly List<MovingEntity> _Entities = new List<MovingEntity>();

        private readonly BaseMap _map;

        public readonly List<Obstacle> _Obstacles = new List<Obstacle>();

        private readonly Type _redSteeringBehaviour;

        public World(int w, int h, Type steeringBehaviour = null)
            : this(w, h, steeringBehaviour, null)
        {
        }

        public World(int w, int h, Type blueSteeringBehaviour, Type redSteeringBehaviour)
        {
            _blueSteeringBehaviour = blueSteeringBehaviour ?? typeof(WanderBehaviour);
            _redSteeringBehaviour = redSteeringBehaviour ?? typeof(WanderBehaviour);

            Width = w;
            Height = h;

            GenerateRandomObstacles();
            _map = new CoarseMap(w, h, _Obstacles);

            Populate();
        }

        public int Width { get; }

        public int Height { get; }

        private void GenerateRandomObstacles()
        {
            var clutterRemaining = Math.Min(Width, Height);
            var rand = new Random();

            while (clutterRemaining > 0)
            {
                // Create a random radius between Obstacle._MinRadius and (Obstacle._MaxRadius || Clutter remaining)
                var maxRand = Math.Min(clutterRemaining, Obstacle._MaxRadius);
                var obstacleRadius = rand.Next(Obstacle._MinRadius, Math.Max(maxRand, Obstacle._MinRadius));

                // Create a position while keeping the obs
                var randX = Math.Min(Width - obstacleRadius, rand.Next(obstacleRadius, Width));
                var randY = Math.Min(Height - obstacleRadius, rand.Next(obstacleRadius, Height));

                // Add obstacle to the world and subtract its size from the available clutter
                _Obstacles.Add(new Obstacle(new Vector2D(randX, randY), this, obstacleRadius));
                clutterRemaining -= obstacleRadius;
            }
        }

        private void Populate()
        {
            var red = new Vehicle(new Vector2D(100, 60), Color.DarkRed, this);
            var blue = new Vehicle(new Vector2D(250, 250), Color.Blue, this);

            // Add behaviours
            red.SteeringBehaviours.AddRange(new[]
            {
                (SteeringBehaviour) Activator.CreateInstance(_redSteeringBehaviour, red, blue)
            });

            blue.SteeringBehaviours.Add(
                (SteeringBehaviour) Activator.CreateInstance(_blueSteeringBehaviour, blue, red)
            );

            _Entities.Add(blue);
            _Entities.Add(red);
        }

        public void Update(float timeElapsed)
        {
            foreach (var entity in _Entities)
                entity.Update(timeElapsed);
        }

        public void Render(Graphics g)
        {
            _Entities.ForEach(e =>
            {
                e.SteeringBehaviours.ForEach(sb => sb.Render(g));
                e.Render(g);
            });

            _Obstacles.ForEach(e => { e.Render(g); });

            _map.Render(g);
        }
    }
}