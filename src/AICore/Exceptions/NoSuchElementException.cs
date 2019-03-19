using System;

namespace AICore.Exceptions
{
    public class NoSuchElementException : Exception
    {
        public NoSuchElementException()
        {
        }

        public NoSuchElementException(string message) : base(message)
        {
        }
    }
}