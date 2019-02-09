using AIBehaviours.Behaviour;
using AIBehaviours.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using AIBehaviours.Behaviour.Individual;
using AIBehaviours.Util;

namespace AIBehaviours
{
    public class World
    {
        private readonly Type _blueSteeringBehaviour;

        private readonly Type _redSteeringBehaviour;

        public readonly List<MovingEntity> Entities = new List<MovingEntity>();

        public int Width { get; }

        public int Height { get; }

        public World(int w, int h, Type steeringBehaviour = null)
            : this(w, h, steeringBehaviour, null)
        {
        }

        public World(int w, int h, Type blueSteeringBehaviour, Type targetSteeringBehaviour)
        {
            _blueSteeringBehaviour = blueSteeringBehaviour ?? typeof(WanderBehaviour);
            _redSteeringBehaviour = targetSteeringBehaviour ?? typeof(WanderBehaviour);

            Width = w;
            Height = h;

            Populate();
        }

        private void Populate()
        {
            var target = new Vehicle(new Vector2D(100, 60), Color.DarkRed, this);
            var agent = new Vehicle(new Vector2D(250, 250), Color.Blue, this);
                
            // Add behaviours
            target.SteeringBehaviours.AddRange(new []{
                (SteeringBehaviour) Activator.CreateInstance(_redSteeringBehaviour, target, agent)
            });

            agent.SteeringBehaviours.Add(
                (SteeringBehaviour) Activator.CreateInstance(_blueSteeringBehaviour, agent, target)
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