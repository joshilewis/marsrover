using System;
using System.Collections.Generic;
using System.IO;

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
            try
            {
                ReadFileAndInitialise();
            }
            catch (RoverOutOfBoundsException roobe)
            {
                return $"Rover would start {roobe.Message} of zone";
            }
            catch (InvalidStartingDirectionException isde)
            {
                return $"Invalid starting direction: {isde.Message}";
            }
            catch (ArgumentException ae)
            {
                return ae.Message;
            }
            catch (FileNotFoundException)
            {
                return "The specified file can't be found";
            }

            foreach (char command in commands)
            {
                try
                {
                    ProcessCommand(command);
                }
                catch (RoverOutOfBoundsException roobe)
                {
                    return $"Rover would move {roobe.Message} out of the zone";
                }
                catch (ArgumentException ae)
                {
                    return ae.Message;
                }
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