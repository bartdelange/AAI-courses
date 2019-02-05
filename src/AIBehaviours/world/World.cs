using AIBehaviours.behaviour;
using AIBehaviours.entity;
using AIBehaviours.util;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AIBehaviours.world
{
    internal class World
    {
        private readonly Type _steeringBehaviour;

        private readonly Type _targetSteeringBehaviour;

        public readonly List<MovingEntity> Entities = new List<MovingEntity>();

        public int Width { get; }

        public int Height { get; }

        public World(int w, int h, Type steeringBehaviour = null)
            : this(w, h, steeringBehaviour, null)
        {
        }

        public World(int w, int h, Type steeringBehaviour, Type targetSteeringBehaviour)
        {
            _steeringBehaviour = steeringBehaviour ?? typeof(ArriveBehaviour);
            _targetSteeringBehaviour = targetSteeringBehaviour ?? typeof(WanderBehaviour);

            Width = w;
            Height = h;

            Populate();
        }

        private void Populate()
        {
            var target = new Vehicle(new Vector2D(100, 60), this)
            {
                VColor = Color.DarkRed,
                MaxSpeed = 5
            };

            var agent = new Vehicle(new Vector2D(250, 250), this)
            {
                VColor = Color.Blue
            };

            // Add behaviours
            target.SteeringBehaviours.AddRange(new []{
                (SteeringBehaviour) Activator.CreateInstance(_targetSteeringBehaviour, target, agent)
            });

            agent.SteeringBehaviours.Add(
                (SteeringBehaviour) Activator.CreateInstance(_steeringBehaviour, agent, target)
            );

            Entities.Add(agent);
            Entities.Add(target);
        }

        public void Update(float timeElapsed)
        {
            foreach (var entity in Entities)
                entity.Update(timeElapsed);
        }

        public void Render(Graphics g)
        {
            Entities.ForEach(e =>
            {
                e.Render(g);
                e.SteeringBehaviours.ForEach(sb => sb.Render(g));
            });
        }
    }
}