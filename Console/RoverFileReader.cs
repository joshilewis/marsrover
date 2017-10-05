using System;
using System.IO;
using System.Linq;

namespace Console
{
    public class RoverFileReader
    {
        private readonly string inputFile;

        public RoverFileReader(string inputFile)
        {
            this.inputFile = inputFile;
        }

        public int MaxX { get; private set; }
        public int MaxY { get; private set; }
        public string Commands { get; private set; }
        public RoverState StartingState { get; private set; }

        public void ReadFile()
        {
            string[] fileContents = File.ReadAllLines(inputFile);
            InitialiseZoneBoundaries(fileContents[0]);
            InitialiseStartingState(fileContents[1]);
            Commands = fileContents[2];
        }
        private void InitialiseZoneBoundaries(string zoneString)
        {
            string[] zoneSizeContents = zoneString
                .Split(' ')
                .ToArray();
            MaxX = int.Parse(zoneSizeContents[0]);
            MaxY = int.Parse(zoneSizeContents[1]);
        }

        private void InitialiseStartingState(string stateString)
        {
            string[] startingStateContents = stateString
                .Split(' ')
                .ToArray();

            var startingX = int.Parse(startingStateContents[0]);
            if (startingX < 0) throw new ArgumentException("Negative starting X");
            if (startingX == MaxX) throw new RoverOutOfBoundsException("East");

            var startingY = int.Parse(startingStateContents[1]);
            if (startingY < 0) throw new ArgumentException("Negative starting Y");
            if (startingY == MaxY) throw new RoverOutOfBoundsException("North");

            string startingDirection = startingStateContents[2];
            if (!new[] { "N", "E", "S", "W" }.Contains(startingDirection))
                throw new InvalidStartingDirectionException(startingDirection);

            StartingState = new RoverState(startingX, startingY, startingDirection);
        }

    }
}