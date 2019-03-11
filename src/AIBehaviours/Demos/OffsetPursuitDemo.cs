using System.Collections.Generic;
using System.Numerics;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AICore;
using AICore.Behaviour.Individual;
using AICore.Entity;
using AICore.Util;

namespace AIBehaviours.Demos
{
    public partial class OffsetPursuitDemo : Form
    {
        public OffsetPursuitDemo()
        {
            InitializeComponent();
            
            
            var world = new World(Width, Height);

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

            Controls.Add(new WorldControl(world));
        }
    }
}