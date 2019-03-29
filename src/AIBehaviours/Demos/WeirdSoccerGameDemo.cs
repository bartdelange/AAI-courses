using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AIBehaviours.Controls;
using AIBehaviours.Utils;
using AICore;
using AICore.Behaviour;
using AICore.Entity.Contracts;
using AICore.Entity.Dynamic;
using AICore.Entity.Static;
using AICore.Model;
using AICore.SteeringBehaviour.Util;

namespace AIBehaviours.Demos
{
    public class WeirdSoccerGameDemo : DemoForm
    {
        public WeirdSoccerGameDemo(Size size) : base(size)
        {
            var margin = new Vector2(15);

            var playingFieldArea = new Bounds(Vector2.Zero, WorldSize) - margin;
            var playingFieldAreaWalls = EntityUtils.CreateCage(playingFieldArea);
            World.Walls.AddRange(playingFieldAreaWalls);

            var playingFieldCenter = playingFieldArea.Center();

            var teamOne = Color.Red;
            var teamTwo = Color.DeepSkyBlue;

            // Create goals
            var teamOneGoal = new SoccerGoal(playingFieldCenter - new Vector2(350, 0), new Vector2(10, 150), teamOne);
            var teamTwoGoal = new SoccerGoal(playingFieldCenter - new Vector2(-350, 0), new Vector2(10, 150), teamTwo);

            // Create the goals
            World.SoccerGoals.Add(teamOneGoal);
            World.SoccerGoals.Add(teamTwoGoal);

            // Create ball
            var ball = new Ball(playingFieldCenter, World.Walls);
            World.Ball = ball;
            
            // Create walls to contain players

            // Create teams
            var teamRed = CreateTeam(World, playingFieldArea, teamOne);
            var teamBlue = CreateTeam(World, playingFieldArea, teamTwo, true);

            // Add zero overlap middleware to entities
            var entities = new List<IMovingEntity>();
            entities.AddRange(teamRed);
            entities.AddRange(teamBlue);
            entities.ForEach(entity => entity.Middlewares.Add(new ZeroOverlapMiddleware(entity, entities)));

            // Add entities to World
            World.Entities.AddRange(entities);
        }

        private static IEnumerable<IMovingEntity> CreateTeam(
            World world,
            Bounds playingField,
            Color teamColor,
            bool isOpponent = false
        )
        {
            var size = playingField.Max - playingField.Min;
            var center = size / 2 + playingField.Min;

            var goalKeeper = new Player(new Vector2(center.X + (isOpponent ? 300 : -300), center.Y), teamColor);

            var defenders = new List<IPlayer>
            {
                new Player(new Vector2(center.X + (isOpponent ? 175 : -175), center.Y - 100), teamColor),
                new Player(new Vector2(center.X + (isOpponent ? 225 : -225), center.Y), teamColor),
                new Player(new Vector2(center.X + (isOpponent ? 175 : -175), center.Y + 100), teamColor)
            };

            var strikers = new List<IPlayer>
            {
                new Player(new Vector2(center.X + (isOpponent ? 75 : -75), center.Y - 150), teamColor),
                new Player(new Vector2(center.X + (isOpponent ? 25 : -25), center.Y), teamColor),
                new Player(new Vector2(center.X + (isOpponent ? 75 : -75), center.Y + 150), teamColor)
            };

            var team = new List<IPlayer>();

            team.AddRange(defenders);
            team.AddRange(strikers);
            team.Add(goalKeeper);

            // Add behaviours to the entities
            defenders.ForEach(defender => defender.SteeringBehaviour = new DefenderBehaviour(defender, team, world));
            strikers.ForEach(striker => striker.SteeringBehaviour = new StrikerBehaviour(striker, team, world));
            goalKeeper.SteeringBehaviour = new GoalKeeperBehaviour(goalKeeper, team, world);

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