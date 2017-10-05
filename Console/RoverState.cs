using System;
using System.Collections.Generic;

namespace Console
{
    public class RoverState
    {
        private readonly Dictionary<string, string> rMapping = new Dictionary<string, string>()
        {
            {"N", "E"},
            {"E", "S"},
            {"S", "W"},
            {"W", "N"},
        };

        private readonly Dictionary<string, string> lMapping = new Dictionary<string, string>()
        {
            {"N", "W"},
            {"E", "N"},
            {"S", "E"},
            {"W", "S"},
        };

        public readonly int X;
        public readonly int Y;
        public readonly string Direction;

        public RoverState(int x, int y, string direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public RoverState CalculateNextPosition()
        {
            switch (Direction)
            {
                case "N":
                    return new RoverState(X, Y+1, Direction);
                case "E":
                    return new RoverState(X + 1, Y, Direction);
                case "S":
                    return new RoverState(X, Y - 1, Direction);
                case "W":
                    return new RoverState(X - 1, Y, Direction);
            }
            throw new InvalidOperationException("Invalid direction: " + Direction);
        }

        public RoverState TurnLeft()
        {
            return new RoverState(X, Y, lMapping[Direction]);
        }

        public RoverState TurnRight()
        {
            return new RoverState(X, Y, rMapping[Direction]);
        }

    }
}