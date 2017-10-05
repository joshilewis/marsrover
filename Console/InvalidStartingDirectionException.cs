using System;

namespace Console
{
    public class InvalidStartingDirectionException : Exception
    {
        public InvalidStartingDirectionException(string startingDirection)
            : base(startingDirection)
        {
            
        }
    }
}