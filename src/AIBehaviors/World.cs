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

        private readonly BaseMap _map;

        private readonly Type _redSteeringBehaviour;

        public readonly List<MovingEntity> _Entities = new List<MovingEntity>();

        public readonly List<Obstacle> _Obstacles = new List<Obstacle>();

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
            _map = new CoarseMap(w, h, null);

            Populate();
        }

        private void GenerateRandomObstacles()
        {

        }

        public int Width { get; }

        public int Height { get; }

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

            _Obstacles.ForEach(e =>
            {
                e.SteeringBehaviours.ForEach(sb => sb.Render(g));
                e.Render(g);
            });

            _map.Render(g);
        }
    }
}