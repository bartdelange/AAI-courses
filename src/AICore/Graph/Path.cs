using System;

namespace AICore.Graph
{
    public class Path<T> : IComparable
    {
        public double Cost;
        public Vertex<T> Destination;

        public Path(Vertex<T> destination, double cost)
        {
            Destination = destination;
            Cost = cost;
        }

        /// <summary>
        ///     Implementation of Comparable interface
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            var path = (Path<T>) obj;

            if (Cost < path.Cost)
                return -1;

            if (Cost > path.Cost)
                return 1;
            
            return 0;
        }
    }
}