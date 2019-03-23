using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AIBehaviours.Utils;
using AICore;
using AICore.Entity;
using AICore.Entity.Contracts;

namespace AIBehaviours.Demos
{
    /// <summary>
    /// 
    /// </summary>
    public class WeirdSoccerGameDemo : DemoForm
    {
        public WeirdSoccerGameDemo(Size size) : base(size)
        {
            World = CreateWorld(WorldSize);
        }

        private static World CreateWorld(Size size)
        {
            const int margin = 15;

            var world = new World(new Vector2(size.Width, size.Height));

            var grandstandArea = new Tuple<Vector2, Vector2>(
                new Vector2(margin, margin),
                new Vector2(size.Width - margin, 150)
            );

            var playingFieldArea = new Tuple<Vector2, Vector2>(
                new Vector2(margin, grandstandArea.Item2.Y),
                new Vector2(size.Width - margin, size.Height - margin)
            );

            CreateSoccerField(world, playingFieldArea);

            var teamRed = CreateTeam(world, playingFieldArea, Color.Red);
            var teamBlue = CreateTeam(world, playingFieldArea, Color.DeepSkyBlue);

            var crowd = CreateCrowd(world, grandstandArea);

            var entities = new List<IMovingEntity>();

            entities.AddRange(teamRed);
            entities.AddRange(teamBlue);
            entities.AddRange(crowd);

            world.Entities = entities;

            return world;
        }

        private static IEnumerable<IMovingEntity> CreateTeam(
            World world,
            Tuple<Vector2, Vector2> playingFieldArea,
            Color red
        )
        {
            return new List<IMovingEntity>();
        }

        private static IEnumerable<IMovingEntity> CreateCrowd(
            World world,
            Tuple<Vector2, Vector2> grandstandArea
        )
        {
            return new List<IMovingEntity>();
        }

        private static void CreateSoccerField(World world, Tuple<Vector2, Vector2> playingFieldArea)
        {
            world.Walls = EntityUtils.CreateCage(playingFieldArea);

            #region playing field obstacles

            world.Obstacles = new List<IObstacle>
            {
            };

            #endregion
        }
    }
}