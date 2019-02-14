using System;

namespace AICore.Graph
{
    public class Path<T> : IComparable
    {
        public double _Cost;
        public Vertex<T> _Destination;

        public Path(Vertex<T> destination, double cost)
        {
            _Destination = destination;
            _Cost = cost;
        }

        /// <summary>
        ///     Implementation of Comparable interface
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            var path = (Path<T>) obj;

            return _Cost < path._Cost
                ? -1
                : _Cost > path._Cost
                    ? 1
                    : 0;
        }
    }
}