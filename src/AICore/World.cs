#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Map;

#endregion

namespace AICore
{
    public class World
    {
        public readonly List<MovingEntity> Entities = new List<MovingEntity>();

        public readonly BaseMap Map;
        public readonly List<Obstacle> Obstacles = new List<Obstacle>();

        public World(int width, int height)
        {
            Width = width;
            Height = height;

            Entities.Add(new Vehicle(new Vector2(100, 60), Color.DarkRed, this));

            GenerateRandomObstacles();

            Map = new CoarseMap(Width, Height, Obstacles);
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
                var maxRand = Math.Min(clutterRemaining, Obstacle.MaxRadius);
                var obstacleRadius = rand.Next(Obstacle.MinRadius, Math.Max(maxRand, Obstacle.MinRadius));

                // Create a position while keeping the obs
                var randX = Math.Min(Width - obstacleRadius, rand.Next(obstacleRadius, Width));
                var randY = Math.Min(Height - obstacleRadius, rand.Next(obstacleRadius, Height));

                // Add obstacle to the world and subtract its size from the available clutter
                Obstacles.Add(new Obstacle(new Vector2(randX, randY), this, obstacleRadius));

                clutterRemaining -= obstacleRadius;
            }
        }

        public void Update(float timeElapsed)
        {
            foreach (var entity in Entities)
                entity.Update(timeElapsed);
        }

        public void Render(Graphics g, bool showGraph)
        {
            Obstacles.ForEach(e => e.Render(g));

            Map.Render(g, showGraph);

            Entities.ForEach(e =>
            {
                e.SteeringBehaviour?.Draw(g);
                e.Render(g);
            });
        }
    }
}