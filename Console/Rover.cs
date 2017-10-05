using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Console
{
    public class Rover
    {
        private readonly string inputFile;
        private int maxX;
        private int maxY;
        private string commands;
        private RoverState state;

        public Rover(string inputFile)
        {
            this.inputFile = inputFile;
        }

        public string Run()
        {
            InitialiseFromFile();

            foreach (char command in commands)
            {
                ProcessCommand(command);
            }

            return $"{state.X} {state.Y} {state.Direction}";
        }

        private void InitialiseFromFile()
        {
            string[] fileContents = File.ReadAllLines(inputFile);
            InitialiseZoneBoundaries(fileContents[0]);
            InitialiseStartingState(fileContents[1]);
            commands = fileContents[2];
        }

        private void InitialiseZoneBoundaries(string zoneString)
        {
            string[] zoneSizeContents = zoneString
                .Split(' ')
                .ToArray();
            maxX = int.Parse(zoneSizeContents[0]);
            maxY = int.Parse(zoneSizeContents[1]);
        }

        private void InitialiseStartingState(string stateString)
        {
            string[] startingStateContents = stateString
                .Split(' ')
                .ToArray();

            var startingX = int.Parse(startingStateContents[0]);
            if (startingX < 0) throw new ArgumentException("Negative starting X");
            if (startingX == maxX) throw new RoverOutOfBoundsException("East");

            var startingY = int.Parse(startingStateContents[1]);
            if (startingY < 0) throw new ArgumentException("Negative starting Y");
            if (startingY == maxY) throw new RoverOutOfBoundsException("North");

            string startingDirection = startingStateContents[2];
            if (!new[] {"N", "E", "S", "W"}.Contains(startingDirection))
                throw new ArgumentException("Invalid starting direction: " + startingDirection);

            state = new RoverState(startingX, startingY, startingDirection);
        }

        private void ProcessCommand(char command)
        {
            switch (command)
            {
                default: throw new ArgumentException("Invalid command: " + command);
                case 'R':
                    state = state.TurnRight();
                    break;
                case 'L':
                    state = state.TurnLeft();
                    break;
                case 'M':
                    MoveRover();

                    break;
            }
        }

        private void MoveRover()
        {
            RoverState next = state.CalculateNextPosition();

            if (next.Y == maxY) throw new RoverOutOfBoundsException("North");
            if (next.X == maxX) throw new RoverOutOfBoundsException("East");
            if (next.Y < 0) throw new RoverOutOfBoundsException("South");
            if (next.X < 0) throw new RoverOutOfBoundsException("West");

            state = next;
        }
    }

}