using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using AIBehaviours.Controls;
using AICore;
using AICore.Behaviour.Goals;
using AICore.Entity.Contracts;
using AICore.Model;
using AICore.SteeringBehaviour;
using AICore.SteeringBehaviour.Individual;
using AICore.SteeringBehaviour.Util;
using AICore.Worlds;

namespace AIBehaviours.Demos
{
    public class WeirdSoccerGameDemo : DemoForm
    {
        private readonly SoccerField _soccerField;
        private bool _kickActive;
        private DateTime _kickActiveTime;

        private IPlayer _activePlayer;
        private ISteeringBehaviour _activePlayerPreviousSteeringBehaviour;
        private DynamicSteering _activePlayerSteering;
        private Think _activePlayerPreviousGoal;

        public WeirdSoccerGameDemo(Size size) : base(size)
        {
            var margin = new Vector2(15);
            var playingFieldArea = new Bounds(Vector2.Zero, WorldSize) - margin;

            _soccerField = new SoccerField(playingFieldArea);

            // Create teams
            var teamRed = new Team(Color.Red, playingFieldArea, _soccerField);
            var teamBlue = new Team(Color.DeepSkyBlue, playingFieldArea, _soccerField, true);

            teamRed.Opponent = teamBlue;
            teamBlue.Opponent = teamRed;

            // Add zero overlap middleware to entities
            var entities = new List<IMovingEntity>();
            entities.AddRange(teamRed.Players);
            entities.AddRange(teamBlue.Players);
            entities.Add(_soccerField.Ball);
            entities.ForEach(entity => entity.Middlewares.Add(new ZeroOverlapMiddleware(entity, entities)));

            // Populate soccerField
            _soccerField.Teams.Add(teamBlue);
            _soccerField.Teams.Add(teamRed);

            // Setup game controls            
            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;

            // Save soccer field to world instance
            World = _soccerField;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    _activePlayerSteering.MoveUp = false;
                    break;

                case Keys.A:
                    _activePlayerSteering.MoveLeft = false;
                    break;

                case Keys.S:
                    _activePlayerSteering.MoveDown = false;
                    break;

                case Keys.D:
                    _activePlayerSteering.MoveRight = false;
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
                    _activePlayerSteering.MoveUp = true;
                    break;

                case Keys.A:
                    _activePlayerSteering.MoveLeft = true;
                    break;

                case Keys.S:
                    _activePlayerSteering.MoveDown = true;
                    break;

                case Keys.D:
                    _activePlayerSteering.MoveRight = true;
                    break;
                
                case Keys.R:
                    _soccerField.Reset();
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
                _activePlayer.ThinkGoal = _activePlayerPreviousGoal;
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
            _activePlayerPreviousGoal = _activePlayer.ThinkGoal;
            _activePlayerSteering = new DynamicSteering(_activePlayer, _soccerField);
            _activePlayer.SteeringBehaviour = _activePlayerSteering;
            _activePlayer.ThinkGoal = null;
        }
    }
}