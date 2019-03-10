#region

using System.Collections.Generic;
using System.Numerics;
using AICore;
using AICore.Behaviour.Individual;
using AICore.Entity;
using AICore.Util;

#endregion

namespace AIBehaviours
{
    public partial class OffsetPursuitDemo : DemoBase
    {
        public OffsetPursuitDemo()
        {
            InitializeComponent();
            
            InitWorldTimer();
            InitWorld(worldPanel);
        }

        protected override World CreateWorld()
        {
            var world = new World(worldPanel.Width, worldPanel.Height);

            var max = new Vector2(Width, Height);
            var leader = new Vehicle(Vector2Util.GetRandom(max), world);

            var entities = new List<MovingEntity>
            {
                new Vehicle(Vector2Util.GetRandom(max), world),
                new Vehicle(Vector2Util.GetRandom(max), world),
                new Vehicle(Vector2Util.GetRandom(max), world),
                new Vehicle(Vector2Util.GetRandom(max), world),
                new Vehicle(Vector2Util.GetRandom(max), world)
            };

            foreach (var entity in entities)
            {
                entity.SteeringBehaviour = new PursuitBehaviour(entity, leader);
            }

            return world;
        }
    }
}