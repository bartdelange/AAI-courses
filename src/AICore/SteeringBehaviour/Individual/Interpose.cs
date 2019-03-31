using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;

namespace AICore.SteeringBehaviour.Individual
{
    public class Interpose : ISteeringBehaviour
    {
        private readonly IMovingEntity _entity;

        private readonly IEntity _entityOne;
        private readonly IEntity _entityTwo;

        public Interpose(IMovingEntity entity, IEntity entityOne, IEntity entityTwo)
        {
            _entity = entity;
            _entityOne = entityOne;
            _entityTwo = entityTwo;
        }

        public bool Visible { get; set; } = true;

        public Vector2 Calculate(float deltaTime)
        {
            var arriveBehaviour = new Arrive(
                _entity,
                (_entityOne.Position + _entityTwo.Position) / 2
            );

            return arriveBehaviour.Calculate(deltaTime);
        }

        public void Render(Graphics graphics)
        {
        }
    }
}