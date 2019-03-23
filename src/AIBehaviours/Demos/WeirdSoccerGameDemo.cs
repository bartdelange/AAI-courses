using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AICore;
using AICore.Entity;
using AICore.Entity.Contracts;

namespace AIBehaviours.Demos
{
    /// <summary>
    /// 
    /// </summary>
    public partial class WeirdSoccerGameDemo : Form
    {
        public WeirdSoccerGameDemo(int width, int height)
        {
            Width = width;
            Height = height;

            InitializeComponent();

            // Add world to form
            var worldControl = new WorldControl(CreateWorld());

            Controls.Add(worldControl);
        }

        World CreateWorld()
        {
            const int margin = 15;

            var world = new World(new Vector2(ClientSize.Width, ClientSize.Height));

            var grandstandArea = new Tuple<Vector2, Vector2>(
                new Vector2(margin, margin),
                new Vector2(ClientSize.Width - margin, 150)
            );

            var playingFieldArea = new Tuple<Vector2, Vector2>(
                new Vector2(margin, grandstandArea.Item2.Y),
                new Vector2(ClientSize.Width - margin, ClientSize.Height - margin)
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

        private static IEnumerable<IMovingEntity> CreateTeam(World world, Tuple<Vector2, Vector2> playingFieldArea,
            Color red)
        {
            return new List<IMovingEntity>();
        }

        private static IEnumerable<IMovingEntity> CreateCrowd(World world, Tuple<Vector2, Vector2> grandstandArea)
        {
            return new List<IMovingEntity>();
        }

        private static void CreateSoccerField(World world, Tuple<Vector2, Vector2> playingFieldArea)
        {
            var soccerFieldStart = playingFieldArea.Item1;
            var soccerFieldEnd = playingFieldArea.Item2;

            #region playing field walls

            world.Walls = new List<IWall>()
            {
                // Top border
                new Wall(
                    new Vector2(soccerFieldStart.X, soccerFieldStart.Y),
                    new Vector2(soccerFieldEnd.X, soccerFieldStart.Y)
                ),

                // Right border
                new Wall(
                    new Vector2(soccerFieldEnd.X, soccerFieldStart.Y),
                    new Vector2(soccerFieldEnd.X, soccerFieldEnd.Y)
                ),

                // Bottom border
                new Wall(
                    new Vector2(soccerFieldEnd.X, soccerFieldEnd.Y),
                    new Vector2(soccerFieldStart.X, soccerFieldEnd.Y)
                ),

                // Left border
                new Wall(
                    new Vector2(soccerFieldStart.X, soccerFieldEnd.Y),
                    new Vector2(soccerFieldStart.X, soccerFieldStart.Y)
                ),
            };

            #endregion

            #region playing field obstacles

            world.Obstacles = new List<IObstacle>
            {
            };

            #endregion
        }
    }
}