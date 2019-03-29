﻿using System;
using System.Drawing;
using System.Numerics;
using AIBehaviours.Controls;
using AIBehaviours.Utils;
using AICore;
using AICore.Entity.Contracts;
using AICore.Model;
using AICore.SteeringBehaviour.Aggregate;
using AICore.SteeringBehaviour.Util;

namespace AIBehaviours.Demos
{
    /// <summary>
    /// 
    /// </summary>
    public class WallAvoidanceDemo : DemoForm
    {
        public WallAvoidanceDemo(Size size) : base(size)
        {
            var bounds = new Bounds(Vector2.Zero, WorldSize);

            // Create walls
            const int margin = 20;
            World.Walls.AddRange(EntityUtils.CreateCage(
                new Bounds(Vector2.Zero, new Vector2(WorldSize.Width, WorldSize.Height)) - new Vector2(margin)
            ));

            // Create vehicles
            var vehicles = EntityUtils.CreateVehicles(
                150,
                bounds,
                (entity) => entity.SteeringBehaviour = new WanderWallAvoidanceBehaviour(entity, World.Walls)
            );

            World.Entities.AddRange(vehicles);

            // Apply zero overlap middleware
            World.Entities.ForEach(entity =>
            {
                entity.Middlewares.Add(new ZeroOverlapMiddleware(entity, World.Entities));
                entity.Middlewares.Add(new ZeroOverlapMiddleware(entity, World.Entities));
            });
        }
    }
}