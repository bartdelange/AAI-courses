using System;
using System.Collections.Generic;
using System.Drawing;
using AIBehaviours.Behaviour;
using AIBehaviours.Behaviour.Individual;
using AIBehaviours.Entity;
using AIBehaviours.Map;
using AIBehaviours.Util;

namespace AIBehaviours
{
    public class World
    {
        private readonly Type _blueSteeringBehaviour;

        private readonly BaseMap _map = new CoarseMap();

        private readonly Type _redSteeringBehaviour;

        public readonly List<MovingEntity> _Entities = new List<MovingEntity>();

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

            Populate();
        }

        public int Width { get; }

        public int Height { get; }

        private void Populate()
        {
            var target = new Vehicle(new Vector2D(100, 60), Color.DarkRed, this);
            var agent = new Vehicle(new Vector2D(250, 250), Color.Blue, this);

            // Add behaviours
            target.SteeringBehaviours.AddRange(new[]
            {
                (SteeringBehaviour) Activator.CreateInstance(_redSteeringBehaviour, target, agent)
            });

            agent.SteeringBehaviours.Add(
                (SteeringBehaviour) Activator.CreateInstance(_blueSteeringBehaviour, agent, target)
            );

            _Entities.Add(agent);
            _Entities.Add(target);
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

            _map.Render(g);
        }
    }
}