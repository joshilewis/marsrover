using System;

namespace Model
{
    public class InvalidStartingDirectionException : Exception
    {
        public InvalidStartingDirectionException(string startingDirection)
            : base(startingDirection)
        {
            
        }
    }
}