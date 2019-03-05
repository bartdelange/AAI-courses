using System;
using System.Collections;
using System.Collections.Generic;

namespace AICore.Graph
{
    public class Path<T> : IComparable<Path<T>>, IEnumerable<Vertex<T>>
    {
        private readonly double _cost;
        
        public readonly Vertex<T> Destination;

        public Path(Vertex<T> destination, double cost)
        {
            _cost = cost;
            
            Destination = destination;
        }

        /// <summary>
        /// Implementation of Comparable interface
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Path<T> other)
        {
            if (_cost < other._cost)
            {
                return -1;                
            }

            if (_cost > other._cost)
            {                
                return 1;
            }
            
            return 0;
        }

        public IEnumerator<Vertex<T>> GetEnumerator()
        {
            var currentWaypoint = Destination;

            // Continue iteration when previousVertex is not empty
            while (currentWaypoint.PreviousVertex != null)
            {
                yield return currentWaypoint;
                
                // Set next waypoint
                currentWaypoint = Destination;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}