using System;
using System.Collections.Generic;

namespace Console
{
    public class Rover
    {
        private readonly string inputFile;
        private readonly RoverFileReader fileReader;
        private int maxX;
        private int maxY;
        private string commands;
        private RoverState state;

        public Rover(string inputFile)
        {
            fileReader = new RoverFileReader(inputFile);
        }

        public string Run()
        {
            ReadFileAndInitialise();

            foreach (char command in commands)
            {
                ProcessCommand(command);
            }

            return $"{state.X} {state.Y} {state.Direction}";
        }

        private void ReadFileAndInitialise()
        {
            fileReader.ReadFile();
            maxX = fileReader.MaxX;
            maxY = fileReader.MaxY;
            commands = fileReader.Commands;
            state = fileReader.StartingState;
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