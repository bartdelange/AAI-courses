using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Xml;
using AIBehaviours.Controls;
using AIBehaviours.Utils;
using AICore;
using AICore.Behaviour;
using AICore.Entity.Contracts;
using AICore.Entity.Dynamic;
using AICore.Entity.Static;
using AICore.Model;
using AICore.SteeringBehaviour.Util;
using AICore.Worlds;

namespace AIBehaviours.Demos
{
    public class WeirdSoccerGameDemo : DemoForm
    {
        public WeirdSoccerGameDemo(Size size) : base(size)
        {
            var soccerField = new SoccerField();

            var margin = new Vector2(15);

            var playingFieldArea = new Bounds(Vector2.Zero, WorldSize) - margin;
            var playingFieldAreaWalls = EntityUtils.CreateCage(playingFieldArea);

            // Create ball
            var ball = new Ball(playingFieldArea.Center(), playingFieldAreaWalls);

            // Create teams
            var teamRed = CreateTeam(Color.Red, playingFieldArea, soccerField);
            var teamBlue = CreateTeam(Color.DeepSkyBlue, playingFieldArea, soccerField, true);

            teamRed.Opponent = teamBlue;
            teamBlue.Opponent = teamRed;

            // Add zero overlap middleware to entities
            var players = new List<IPlayer>();
            players.AddRange(teamRed.Players);
            players.AddRange(teamBlue.Players);
            players.ForEach(entity => entity.Middlewares.Add(new ZeroOverlapMiddleware(entity, players)));

            // Populate soccerField
            soccerField.Teams.Add(teamBlue);
            soccerField.Teams.Add(teamRed);
            soccerField.Sidelines.AddRange(playingFieldAreaWalls);
            soccerField.Ball = ball;

            // Save soccer field to world instance
            World = soccerField;
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
                    new Vector2(center.X + (isOpponent ? 175 : -175),center.Y - 100),
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
            defenders.ForEach(defender => defender.SteeringBehaviour = new DefenderBehaviour(defender, team, soccerField));
            strikers.ForEach(striker => striker.SteeringBehaviour = new StrikerBehaviour(striker, team, soccerField));
            goalKeeper.SteeringBehaviour = new GoalKeeperBehaviour(goalKeeper, team, soccerField);

            return team;
        }

        private static IEnumerable<IMovingEntity> CreateCrowd(
            World world,
            Bounds grandstandArea
        )
        {
            return new List<IMovingEntity>();
        }
    }
}