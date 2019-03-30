using System.Collections.Generic;
using System.Drawing;
using AICore.Entity;
using AICore.Entity.Contracts;
using AICore.Entity.Dynamic;
using AICore.Entity.Static;
using AICore.Navigation;

namespace AICore.Worlds
{
    public class World : IWorld
    {
        // Entities
        public Ball Ball;
        public NavigationLayer NavigationLayer;
        
        public readonly List<IMovingEntity> Entities = new List<IMovingEntity>();
        public readonly List<IObstacle> Obstacles = new List<IObstacle>();
        public readonly List<IWall> Walls = new List<IWall>();
        public readonly List<SoccerGoal> SoccerGoals = new List<SoccerGoal>();

        public void Update(float timeElapsed)
        {
            Entities?.ForEach(e => { e.Update(timeElapsed); });
            Ball?.Update(timeElapsed);
        }

        public void Render(Graphics graphics)
        {
            NavigationLayer?.RenderIfVisible(graphics);

            Obstacles?.ForEach(obstacle => obstacle.RenderIfVisible(graphics));
            Entities?.ForEach(entity => entity.RenderIfVisible(graphics));
            Walls?.ForEach(wall => wall.RenderIfVisible(graphics));
            SoccerGoals?.ForEach(goal => goal.RenderIfVisible(graphics));
            Ball?.RenderIfVisible(graphics);
        }
    }
}