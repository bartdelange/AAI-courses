using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AICore.Entity.Contracts;
using AICore.Navigation;

namespace AICore
{
    public class World
    {
        public int Width { get; }
        public int Height { get; }

        // Entities
        public List<IMovingEntity> Entities;
        public List<IObstacle> Obstacles;
        public List<IWall> Walls;
        public NavigationLayer NavigationLayer;

        // TODO Remove obstacle list and baseMap dependency 
        public World(Vector2 worldBounds)
        {
            Width = (int) worldBounds.X;
            Height = (int) worldBounds.Y;
        }

        public void Update(float timeElapsed)
        {
            Entities?.ForEach(e =>
            {
                e.Update(timeElapsed);
            });
        }

        public void Render(Graphics graphics, bool graphIsVisible)
        {
            NavigationLayer?.Render(graphics);
            
            Obstacles?.ForEach(obstacle => obstacle.Render(graphics));
            Entities?.ForEach(entity => entity.Render(graphics));
            Walls?.ForEach(wall => wall.Render(graphics));
        }
    }
}