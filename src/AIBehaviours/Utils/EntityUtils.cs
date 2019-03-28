using System;
using System.Collections.Generic;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.Model;
using AICore.Util;

namespace AIBehaviours.Utils
{
    public static class EntityUtils
    {
        public static List<IObstacle> CreateObstacles(Bounds bounds, int clutter)
        {
            var random = new Random();

            // Create list instance to save obstacles
            var obstacles = new List<IObstacle>();

            var clutterRemaining = clutter == 0
                ? Math.Min(bounds.Max.X, bounds.Max.Y)
                : clutter;

            while (clutterRemaining > 0)
            {
                // Create a random radius between Circle._MinRadius and (Circle._MaxRadius || Clutter remaining)
                var maxRand = Math.Min(clutterRemaining, CircleObstacle.MaxRadius);
                var obstacleRadius = random.Next(CircleObstacle.MinRadius,
                    (int) Math.Max(maxRand, CircleObstacle.MinRadius));

                // Create a position while keeping the obs
                var randomX = Math.Min(
                    bounds.Max.X - obstacleRadius,
                    random.Next((int) (bounds.Min.X + obstacleRadius), (int) bounds.Max.X)
                );

                var randomY = Math.Min(
                    bounds.Max.Y - obstacleRadius,
                    random.Next((int) (bounds.Min.Y + obstacleRadius), (int) bounds.Max.Y)
                );

                // Create new obstacle and subtract its size from the available clutter
                obstacles.Add(
                    new CircleObstacle(
                        new Vector2(randomX, randomY),
                        obstacleRadius
                    )
                );

                // Decrease clutter remaining until we reach 0
                clutterRemaining -= obstacleRadius;
            }

            return obstacles;
        }

        public static List<IMovingEntity> CreateVehicles(
            int count,
            Bounds bounds,
            Action<Vehicle> createEntityMiddleware
        )
        {
            var vehicles = new List<IMovingEntity>();

            for (var i = 0; i < count; i++)
            {
                var entity = new Vehicle(Vector2ExtensionMethods.GetRandom(bounds));

                // Apply middleware when argument is given
                createEntityMiddleware?.Invoke(entity);

                vehicles.Add(entity);
            }

            return vehicles;
        }

        public static List<IWall> CreateCage(Bounds bounds)
        {
            return new List<IWall>()
            {
                // Top border
                new Wall(
                    new Vector2(bounds.Min.X, bounds.Min.Y),
                    new Vector2(bounds.Max.X, bounds.Min.Y)
                ),

                // Right border
                new Wall(
                    new Vector2(bounds.Max.X, bounds.Min.Y),
                    new Vector2(bounds.Max.X, bounds.Max.Y)
                ),

                // Bottom border
                new Wall(
                    new Vector2(bounds.Max.X, bounds.Max.Y),
                    new Vector2(bounds.Min.X, bounds.Max.Y)
                ),

                // Left border
                new Wall(
                    new Vector2(bounds.Min.X, bounds.Max.Y),
                    new Vector2(bounds.Min.X, bounds.Min.Y)
                ),
            };
        }
    }
}