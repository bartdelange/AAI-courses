using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.SteeringBehaviour.Aggregate;
using AICore.SteeringBehaviour.Util;
using AICore.Util;
using AICore.Worlds;

namespace AICore.SteeringBehaviour.Individual
{
    public class DynamicSteering : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly IPlayer _player;
        private readonly SoccerField _soccerField;
        
        private readonly WeightedSteeringBehaviour _wallObstacleAvoidanceBehaviour;

        public bool MoveUp { get; set; }
        public bool MoveLeft { get; set; }
        public bool MoveDown { get; set; }
        public bool MoveRight { get; set; }

        public DynamicSteering(IPlayer player, SoccerField soccerField)
        {
            player.MaxSpeed = Config.MaxSpeed;
            
            _player = player;
            _soccerField = soccerField;

            _wallObstacleAvoidanceBehaviour = new WeightedSteeringBehaviour(
                new WallObstacleAvoidance(player, soccerField.Sidelines, soccerField.Obstacles),
                10f
            );
        }

        public Vector2 Calculate(float deltaTime)
        {
            var velocity = Vector2.Zero;

            if(MoveUp) velocity += new Vector2(0, -1);
            if(MoveLeft) velocity += new Vector2(-1, 0);
            if(MoveDown) velocity += new Vector2(0, 1);
            if(MoveRight) velocity += new Vector2(1, 0);
                        
            var weightedSteeringBehaviour = new WeightedTruncatedRunningSumWithPrioritization(
                new List<WeightedSteeringBehaviour>
                {
                    _wallObstacleAvoidanceBehaviour,
                    new WeightedSteeringBehaviour(new ConstantSteering(velocity * _player.MaxSpeed), 1f),
                },
                
                _player.MaxSpeed
            );

            // Take ball when player touches ball
            _soccerField.Ball.TakeBall(_player);
            
            return weightedSteeringBehaviour.Calculate(deltaTime);
        }

        public void Render(Graphics graphics)
        {
            var size = new Vector2(6, 6);

            graphics.FillEllipse(
                Brushes.PaleGreen,
                new Rectangle(
                    (_player.Position - (size / 2)).ToPoint(),
                    new Size(size.ToPoint())
                )
            );
        }
    }
}