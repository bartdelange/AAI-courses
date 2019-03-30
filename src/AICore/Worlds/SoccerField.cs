using System.Collections.Generic;
using System.Drawing;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.Entity.Dynamic;

namespace AICore.Worlds
{
    public class SoccerField : IWorld
    {
        // Static entities
        public readonly List<IWall> Sidelines = new List<IWall>();
        public readonly List<Team> Teams = new List<Team>();

        // Dynamic entities
        public Ball Ball { get; set; }

        public void Update(float timeElapsed)
        {
            Ball.Update(timeElapsed);

            Teams.ForEach(team =>
                team.Players.ForEach(player =>
                    player.Update(timeElapsed)
                )
            );
        }

        public void Render(Graphics graphics)
        {
            Ball.RenderIfVisible(graphics);

            Teams.ForEach(team =>
            {
                team.Goal.RenderIfVisible(graphics);
                team.Players.ForEach(player => player.RenderIfVisible(graphics));
            });

            Sidelines.ForEach(wall => wall.RenderIfVisible(graphics));
        }
    }
}