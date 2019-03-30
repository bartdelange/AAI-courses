using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.Entity.Dynamic;
using AICore.Entity.Static;
using AICore.Model;
using AICore.Navigation;
using AICore.Util;

namespace AICore.Worlds
{
    public class SoccerField : IWorld
    {
        private readonly Bounds _playingFieldArea;

        // Dynamic entities
        public Ball Ball { get; set; }
        public readonly List<Team> Teams = new List<Team>();

        // Static entities
        public NavigationLayer Navigation;
        public readonly List<IWall> Sidelines;

        public List<IObstacle> Obstacles
        {
            get => _obstacles;
            set
            {
                _obstacles = value;

                var meshDensity = _playingFieldArea.Max.Y / 15 - 1;
                Navigation = new NavigationLayer(new FineMesh(meshDensity, _playingFieldArea, value));
            }
        }

        private List<IObstacle> _obstacles;

        public SoccerField(Bounds playingFieldArea)
        {
            _playingFieldArea = playingFieldArea;
            var center = _playingFieldArea.Center();
            
            Ball = new Ball(center, this);

            Obstacles = new List<IObstacle>
            {
                new CircleObstacle(center - new Vector2(-100, -125), 50),
                new CircleObstacle(center - new Vector2(100, 125), 50),
                new CircleObstacle(center - new Vector2(100, -125), 50),
                new CircleObstacle(center - new Vector2(-100, 125), 50)
            };

            Sidelines = EntityUtils.CreateCage(playingFieldArea);
        }

        public void Update(float timeElapsed)
        {
            Ball?.Update(timeElapsed);

            Teams?.ForEach(team =>
                team?.Players.ForEach(player =>
                    player.Update(timeElapsed)
                )
            );
        }

        public void Render(Graphics graphics)
        {
            Ball?.RenderIfVisible(graphics);

            Teams?.ForEach(team =>
            {
                team?.Goal.RenderIfVisible(graphics);
                team?.Players.ForEach(player => player.RenderIfVisible(graphics));
            });

            Obstacles?.ForEach(obstacle => obstacle.RenderIfVisible(graphics));
            Sidelines?.ForEach(wall => wall.RenderIfVisible(graphics));
            Navigation?.RenderIfVisible(graphics);
        }
    }
}