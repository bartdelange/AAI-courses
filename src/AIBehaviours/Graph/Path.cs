using System;

namespace AIBehaviours.Graph
{
    public class Path<T> : IComparable
    {
        public Vertex<T> Destination;
        public double Cost;

        public Path(Vertex<T> destination, double cost)
        {
            Destination = destination;
            Cost = cost;
        }

        /// <summary>
        /// Implementation of Comparable interface 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            var path = (Path<T>) obj;

            return Cost < path.Cost
                ? -1
                : Cost > path.Cost
                    ? 1
                    : 0;
        }
    }
}