using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Behaviour;
using AICore.Entity.Contracts;
using AICore.Entity.Dynamic;
using AICore.Entity.Static;
using AICore.Worlds;

namespace AICore.Model
{
    public class Team
    {
        private Team _opponent;

        public readonly SoccerGoal Goal;
        public readonly List<IPlayer> Players = new List<IPlayer>();

        public Team Opponent
        {
            get
            {
                if (_opponent == null)
                    throw new ArgumentNullException("Opponent is not defined");

                return _opponent;
            }
            
            set => _opponent = value;
        }

        public Team(
            Color teamColor,
            Bounds playingFieldArea,
            SoccerField soccerField,
            bool isOpponent = false
        )
        {
            var center = playingFieldArea.Center();

            // Create goals
            Goal = new SoccerGoal(
                playingFieldArea.Center() - new Vector2(isOpponent ? -455 : 455, 0),
                new Vector2(30, 150),
                teamColor
            );

            var goalKeeper = new Player("Goalkeeper",
                new Vector2(center.X + (isOpponent ? 400 : -400), center.Y), teamColor);

            var defenders = new List<IPlayer>
            {
                new Player(
                    "Left defender",
                    new Vector2(center.X + (isOpponent ? 225 : -225), center.Y - 150),
                    teamColor
                ),
                new Player(
                    "Central defender",
                    new Vector2(center.X + (isOpponent ? 300: -300), center.Y),
                    teamColor
                ),
                new Player(
                    "Right defender",
                    new Vector2(center.X + (isOpponent ? 225 : -225), center.Y + 150),
                    teamColor
                )
            };

            var strikers = new List<IPlayer>
            {
                new Player(
                    "Left striker",
                    new Vector2(center.X + (isOpponent ? 75 : -75), center.Y - 250),
                    teamColor
                ),
                new Player(
                    "Central striker",
                    new Vector2(center.X + (isOpponent ? 75 : -75), center.Y),
                    teamColor
                ),
                new Player(
                    "Right striker",
                    new Vector2(center.X + (isOpponent ? 75 : -75), center.Y + 250),
                    teamColor
                )
            };

            Players.AddRange(defenders);
            Players.AddRange(strikers);
            Players.Add(goalKeeper);

            // Add team to player entity
            Players.ForEach(player => player.Team = this);
            
            // Add behaviours to the entities
            defenders.ForEach(defender => defender.SteeringBehaviour = new DefenderBehaviour(defender, this, soccerField));
            strikers.ForEach(striker => striker.SteeringBehaviour = new StrikerBehaviour(striker, this, soccerField));
            goalKeeper.SteeringBehaviour = new GoalKeeperBehaviour(goalKeeper, this, soccerField);
        }

        public void Reset()
        {
            Players.ForEach(player =>
            {
                player.Position = player.StartPosition;
            });
        }
    }
}