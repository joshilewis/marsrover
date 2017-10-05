using System;

namespace Model
{
    public class RoverOutOfBoundsException : Exception
    {
        public RoverOutOfBoundsException(string message)
            : base(message)
        {
        }
    }
}