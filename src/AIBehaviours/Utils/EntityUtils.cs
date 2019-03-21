using System;
using System.Collections.Generic;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.Util;

namespace AIBehaviours.Utils
{
    public static class EntityUtils
    {
        public static List<IObstacle> CreateObstacles(Vector2 worldBounds, int clutter)
        {
            var random = new Random();

            // Create list instance to save obstacles
            var obstacles = new List<IObstacle>();

            var clutterRemaining = clutter == 0
                ? Math.Min(worldBounds.X, worldBounds.Y)
                : clutter;

            while (clutterRemaining > 0)
            {
                // Create a random radius between Circle._MinRadius and (Circle._MaxRadius || Clutter remaining)
                var maxRand = Math.Min(clutterRemaining, CircleObstacle.MaxRadius);
                var obstacleRadius = random.Next(CircleObstacle.MinRadius,
                    (int) Math.Max(maxRand, CircleObstacle.MinRadius));

                // Create a position while keeping the obs
                var randomX = Math.Min(
                    worldBounds.X - obstacleRadius,
                    random.Next(obstacleRadius, (int) worldBounds.X)
                );

                var randomY = Math.Min(
                    worldBounds.Y - obstacleRadius,
                    random.Next(obstacleRadius, (int) worldBounds.Y)
                );

                // Create new obstacle and subtract its size from the available clutter
                obstacles.Add(
                    new CircleObstacle(
                        new Vector2(randomX, randomY),
                        obstacleRadius
                    )
                );

                // TODO add docs to describe what we do here
                clutterRemaining -= obstacleRadius;
            }

            return obstacles;
        }

        public static List<IMovingEntity> CreateVehicles(int count, Vector2 worldBounds, Action<Vehicle> createEntityMiddleware)
        {
            var vehicles = new List<IMovingEntity>();

            for (var i = 0; i < count; i++)
            {
                var entity = new Vehicle(Vector2ExtensionMethods.GetRandom(worldBounds), worldBounds);
                
                // Apply middleware when argument is given
                createEntityMiddleware?.Invoke(entity);

                vehicles.Add(entity);
            }

            return vehicles;
        }
    }
}