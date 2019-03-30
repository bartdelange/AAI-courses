using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AIBehaviours.Utils;
using AICore;
using AICore.Behaviour;
using AICore.Entity.Contracts;
using AICore.Entity.Dynamic;
using AICore.Entity.Static;
using AICore.Model;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Aggregate;
using AICore.SteeringBehaviour.Util;
using AICore.Util;
using AICore.Worlds;

namespace AIBehaviours.Demos
{
    class ConstantSteeringBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; }

        private readonly Vector2 _velocity;

        public ConstantSteeringBehaviour(Vector2 velocity)
        {
            _velocity = velocity;
        }

        public Vector2 Calculate(float deltaTime)
        {
            return _velocity;
        }

        public void Render(Graphics graphics)
        {
        }
    }

    class DynamicSteeringBehaviour : ISteeringBehaviour
    {
        public bool Visible { get; set; } = true;

        private readonly IPlayer _player;
        private readonly WeightedSteeringBehaviour _wallObstacleAvoidanceBehaviour;

        public bool MoveUp { get; set; }
        public bool MoveLeft { get; set; }
        public bool MoveDown { get; set; }
        public bool MoveRight { get; set; }

        public DynamicSteeringBehaviour(IPlayer player, SoccerField soccerField)
        {
            _player = player;

            _wallObstacleAvoidanceBehaviour = new WeightedSteeringBehaviour(
                new WallObstacleAvoidanceBehaviour(player, soccerField.Sidelines, soccerField.Obstacles),
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
                    new WeightedSteeringBehaviour(new ConstantSteeringBehaviour(velocity * _player.MaxSpeed), 1f),
                },
                
                _player.MaxSpeed
            );
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

    public class WeirdSoccerGameDemo : DemoForm
    {
        private readonly SoccerField _soccerField;
        private bool _kickActive;
        private DateTime _kickActiveTime;

        private IPlayer _activePlayer;
        private ISteeringBehaviour _activePlayerPreviousSteeringBehaviour;
        private DynamicSteeringBehaviour _activePlayerSteeringBehaviour;

        public WeirdSoccerGameDemo(Size size) : base(size)
        {
            _soccerField = new SoccerField();

            var margin = new Vector2(15);

            var playingFieldArea = new Bounds(Vector2.Zero, WorldSize) - margin;
            var playingFieldAreaWalls = EntityUtils.CreateCage(playingFieldArea);

            // Create ball
            var ball = new Ball(playingFieldArea.Center(), playingFieldAreaWalls);

            // Create teams
            var teamRed = CreateTeam(Color.Red, playingFieldArea, _soccerField);
            var teamBlue = CreateTeam(Color.DeepSkyBlue, playingFieldArea, _soccerField, true);

            teamRed.Opponent = teamBlue;
            teamBlue.Opponent = teamRed;

            // Add zero overlap middleware to entities
            var players = new List<IPlayer>();
            players.AddRange(teamRed.Players);
            players.AddRange(teamBlue.Players);
            players.ForEach(entity => entity.Middlewares.Add(new ZeroOverlapMiddleware(entity, players)));

            // Populate soccerField
            _soccerField.Teams.Add(teamBlue);
            _soccerField.Teams.Add(teamRed);
            _soccerField.Sidelines.AddRange(playingFieldAreaWalls);
            _soccerField.Ball = ball;

            // Setup game controls            
            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;

            SwitchPlayer();

            // Save soccer field to world instance
            World = _soccerField;
        }

        private static Team CreateTeam(
            Color teamColor,
            Bounds playingFieldArea,
            SoccerField soccerField,
            bool isOpponent = false
        )
        {
            var center = playingFieldArea.Center();

            // Create goals
            var soccerGoal = new SoccerGoal(
                playingFieldArea.Center() - new Vector2(isOpponent ? -350 : 350, 0),
                new Vector2(10, 150),
                teamColor
            );

            var goalKeeper = new Player("Goalkeeper",
                new Vector2(center.X + (isOpponent ? 300 : -300), center.Y), teamColor);

            var defenders = new List<IPlayer>
            {
                new Player(
                    "Left defender",
                    new Vector2(center.X + (isOpponent ? 175 : -175), center.Y - 100),
                    teamColor
                ),
                new Player(
                    "Central defender",
                    new Vector2(center.X + (isOpponent ? 225 : -225), center.Y),
                    teamColor
                ),
                new Player(
                    "Right defender",
                    new Vector2(center.X + (isOpponent ? 175 : -175), center.Y + 100),
                    teamColor
                )
            };

            var strikers = new List<IPlayer>
            {
                new Player(
                    "Left striker",
                    new Vector2(center.X + (isOpponent ? 75 : -75), center.Y - 150),
                    teamColor
                ),
                new Player(
                    "Central striker",
                    new Vector2(center.X + (isOpponent ? 25 : -25), center.Y),
                    teamColor
                ),
                new Player(
                    "Right striker",
                    new Vector2(center.X + (isOpponent ? 75 : -75), center.Y + 150),
                    teamColor
                )
            };

            var players = new List<IPlayer>();
            players.AddRange(defenders);
            players.AddRange(strikers);
            players.Add(goalKeeper);

            var team = new Team(soccerGoal, players);

            // Add behaviours to the entities
            defenders.ForEach(defender =>
                defender.SteeringBehaviour = new DefenderBehaviour(defender, team, soccerField));
            strikers.ForEach(striker => striker.SteeringBehaviour = new StrikerBehaviour(striker, team, soccerField));
            goalKeeper.SteeringBehaviour = new GoalKeeperBehaviour(goalKeeper, team, soccerField);

            return team;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    _activePlayerSteeringBehaviour.MoveUp = false;
                    break;

                case Keys.A:
                    _activePlayerSteeringBehaviour.MoveLeft = false;
                    break;

                case Keys.S:
                    _activePlayerSteeringBehaviour.MoveDown = false;
                    break;

                case Keys.D:
                    _activePlayerSteeringBehaviour.MoveRight = false;
                    break;

                case Keys.Space:
                    var timespan = DateTime.Now.Subtract(_kickActiveTime).TotalMilliseconds;
                    
                    _soccerField.Ball.Kick(
                        _activePlayer,
                        (float) timespan / Config.BallBuildupRatio
                    );

                    _kickActive = false;
                    break;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // Switch to another player
                case Keys.Tab:
                    SwitchPlayer();
                    break;

                case Keys.W:
                    _activePlayerSteeringBehaviour.MoveUp = true;
                    break;

                case Keys.A:
                    _activePlayerSteeringBehaviour.MoveLeft = true;
                    break;

                case Keys.S:
                    _activePlayerSteeringBehaviour.MoveDown = true;
                    break;

                case Keys.D:
                    _activePlayerSteeringBehaviour.MoveRight = true;
                    break;

                case Keys.Space:
                    if (!_kickActive)
                        _kickActiveTime = DateTime.Now;
                    
                    _kickActive = true;
                    break;
            }
        }

        private void SwitchPlayer()
        {
            if (_activePlayer != null)
            {
                _activePlayer.SteeringBehaviour = _activePlayerPreviousSteeringBehaviour;
            }

            var newPlayers = _soccerField
                .Teams
                .First()
                .Players
                .Where(
                    player => player != _activePlayer
                )
                .ToList();

            _activePlayer = _soccerField.Ball.FindClosestPlayer(newPlayers);

            // Change steering behaviour to allow keyboard controls for active player
            _activePlayerPreviousSteeringBehaviour = _activePlayer.SteeringBehaviour;
            _activePlayerSteeringBehaviour = new DynamicSteeringBehaviour(_activePlayer, _soccerField);
            _activePlayer.SteeringBehaviour = _activePlayerSteeringBehaviour;
        }
    }
}