using System;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Model;
using AICore.Util;

namespace AICore.SteeringBehaviour.Util
{
    public class WrapAroundMiddleware : IMiddleware
    {
        private readonly IMovingEntity _movingEntity;
        private readonly Bounds _bounds;

        public WrapAroundMiddleware(IMovingEntity movingEntity, Bounds bounds)
        {
            _movingEntity = movingEntity;
            _bounds = bounds;
        }

        public void Update(float deltaTime)
        {
            _movingEntity.Position = _movingEntity.Position.WrapToBounds(_bounds);
        }
    }
}