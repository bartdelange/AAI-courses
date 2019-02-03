using System;
using System.Collections.Generic;
using System.Drawing;
using AIBehaviours.behaviour;
using AIBehaviours.entity;
using AIBehaviours.util;

namespace AIBehaviours.world
{
    internal class World
    {
        private readonly Type _steeringBehaviour;

        private readonly Type _targetSteeringBehaviour;
        public readonly List<MovingEntity> Entities = new List<MovingEntity>();

        public World(int w, int h) : this(w, h, null)
        {
        }

        public World(int w, int h, Type steeringBehaviour) : this(w, h, steeringBehaviour, null)
        {
        }

        public World(int w, int h, Type steeringBehaviour, Type targetSteeringBehaviour)
        {
            _steeringBehaviour = steeringBehaviour ?? typeof(ArriveBehaviour);
            _targetSteeringBehaviour = targetSteeringBehaviour ?? typeof(FleeBehaviour);

            Width = w;
            Height = h;

            populate();
        }

        public int Width { get; set; }

        public int Height { get; set; }

        private void populate()
        {
            var target = new Vehicle(new Vector2D(100, 60), this)
            {
                VColor = Color.DarkRed
            };

            var agent = new Vehicle(new Vector2D(250, 250), this)
            {
                VColor = Color.Blue
            };

            // Add behaviours
            target.SteeringBehaviours.Add(
                (SteeringBehaviour) Activator.CreateInstance(_targetSteeringBehaviour, target, agent));
            agent.SteeringBehaviours.Add(
                (SteeringBehaviour) Activator.CreateInstance(_steeringBehaviour, agent,
                    target)); // Apply steering behavior on initalization

            Entities.Add(agent);
            Entities.Add(target);
        }

        public void Update(float timeElapsed)
        {
            foreach (var entity in Entities) entity.Update(timeElapsed);
        }

        public void Render(Graphics g)
        {
            Entities.ForEach(e => e.Render(g));
        }
    }
}