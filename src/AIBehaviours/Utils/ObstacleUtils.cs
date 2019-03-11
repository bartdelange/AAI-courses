using System;
using System.Collections.Generic;
using System.Numerics;
using AICore.Entity;

namespace AIBehaviours.Utils
{
    public static class ObstacleUtils
    {
        public static List<Obstacle> CreateObstacles(int width, int height, int clutter)
        {
            var random = new Random();

            // Create list instance to save obstacles
            var obstacles = new List<Obstacle>();

            var clutterRemaining = clutter == 0 ? Math.Min(width, height) : clutter;

            while (clutterRemaining > 0)
            {
                // Create a random radius between Obstacle._MinRadius and (Obstacle._MaxRadius || Clutter remaining)
                var maxRand = Math.Min(clutterRemaining, Obstacle.MaxRadius);
                var obstacleRadius = random.Next(Obstacle.MinRadius, Math.Max(maxRand, Obstacle.MinRadius));

                // Create a position while keeping the obs
                var randomX = Math.Min(width - obstacleRadius, random.Next(obstacleRadius, width));
                var randomY = Math.Min(height - obstacleRadius, random.Next(obstacleRadius, height));

                // Create new obstacle and subtract its size from the available clutter
                obstacles.Add(new Obstacle(new Vector2(randomX, randomY), null, obstacleRadius));

                // TODO add docs to describe what we do here
                clutterRemaining -= obstacleRadius;
            }

            return obstacles;
        }
    }
}