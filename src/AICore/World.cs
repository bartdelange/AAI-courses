using System.Collections.Generic;
using System.Drawing;
using AICore.Entity;
using AICore.Map;

namespace AICore
{
    public class World
    {
        public List<MovingEntity> Entities;
        public List<Obstacle> Obstacles;
        public BaseMap Map;

        // TODO Remove obstacle list and baseMap dependency 
        public World(int width, int height)
        {
            Width = width;
            Height = height;
        }
        
        public int Width { get; }
        public int Height { get; }

        public void Update(float timeElapsed)
        {
            Entities?.ForEach(e => e.Update(timeElapsed));
        }

        public void Render(Graphics g, bool graphIsVisible)
        {
            Map?.Render(g, graphIsVisible);
            Obstacles?.ForEach(e => e.Render(g));
            Entities?.ForEach(e => e.Render(g));
        }
    }
}