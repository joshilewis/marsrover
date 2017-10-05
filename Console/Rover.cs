﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Console
{
    public class Rover
    {
        private readonly Dictionary<string, string> rMapping = new Dictionary<string, string>()
        {
            { "N", "E" },
            { "E", "S" },
            { "S", "W" },
            { "W", "N" },
        };

        private readonly Dictionary<string, string> lMapping = new Dictionary<string, string>()
        {
            { "N", "W" },
            { "E", "N" },
            { "S", "E" },
            { "W", "S" },
        };

        private readonly string inputFile;
        private int maxX;
        private int maxY;
        private int currentX;
        private int currentY;
        private string currentDirection;
        private string commands;

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

            return $"{currentX} {currentY} {currentDirection}";
        }

        private void InitialiseFromFile()
        {
            string[] fileContents = File.ReadAllLines(inputFile);
            InitialiseZoneBoundaries(fileContents[0]);
            InitialiseCurrentState(fileContents[1]);
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

        private void InitialiseCurrentState(string stateString)
        {
            string[] currentStateContents = stateString
                .Split(' ')
                .ToArray();

            currentX = int.Parse(currentStateContents[0]);
            if (currentX < 0) throw new ArgumentException("Negative starting X");
            if (currentX == maxX) throw new RoverOutOfBoundsException("East");

            currentY = int.Parse(currentStateContents[1]);
            if (currentY < 0) throw new ArgumentException("Negative starting Y");
            if (currentY == maxY) throw new RoverOutOfBoundsException("North");

            currentDirection = currentStateContents[2];
            if (!new[] { "N", "E", "S", "W" }.Contains(currentDirection))
                throw new ArgumentException("Invalid starting direction: " + currentDirection);
        }

        private void ProcessCommand(char command)
        {
            switch (command)
            {
                default: throw new ArgumentException("Invalid command: " + command);
                case 'R':
                    currentDirection = rMapping[currentDirection];
                    break;
                case 'L':
                    currentDirection = lMapping[currentDirection];
                    break;
                case 'M':
                    MoveRover();

                    break;
            }
        }

        private void MoveRover()
        {
            switch (currentDirection)
            {
                case "N":
                    if (currentY == maxY - 1) throw new RoverOutOfBoundsException("North");
                    currentY++;
                    break;
                case "E":
                    if (currentX == maxX - 1) throw new RoverOutOfBoundsException("East");
                    currentX++;
                    break;
                case "S":
                    if (currentY == 0) throw new RoverOutOfBoundsException("South");
                    currentY--;
                    break;
                case "W":
                    if (currentX == 0) throw new RoverOutOfBoundsException("West");
                    currentX--;
                    break;
            }
        }

    }
}