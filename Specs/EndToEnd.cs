﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Specs
{
    [TestFixture]
    public class EndToEnd
    {
        [Test]
        public void RealWorld1()
        {
            var inputFile = "RealWorld1.txt";
            var expected = "3 3 S";
            Assert.That(RunRover(inputFile), Is.EqualTo(expected));
        }

        [Test]
        public void FileDoesntExist()
        {
            var inputFile = "this file doesn't exist.txt";
            Assert.That(() => RunRover(inputFile), Throws.InstanceOf<FileNotFoundException>());
        }

        [TestCase("OutOfBoundsEast.txt", typeof(RoverOutOfBoundsException), "East", TestName = "OutOfBoundsEast")]
        [TestCase("OutOfBoundsWest.txt", typeof(RoverOutOfBoundsException), "West", TestName = "OutOfBoundsWest")]
        [TestCase("OutOfBoundsNorth.txt", typeof(RoverOutOfBoundsException), "North", TestName = "OutOfBoundsNorth")]
        [TestCase("OutOfBoundsSouth.txt", typeof(RoverOutOfBoundsException), "South", TestName = "OutOfBoundsSouth")]
        [TestCase("InvalidStartingDirection.txt", typeof(ArgumentException), "Invalid starting direction: X", TestName = "InvalidStartingDirection")]
        [TestCase("InvalidCommand.txt", typeof(ArgumentException), "Invalid command: X", TestName = "InvalidCommand")]
        [TestCase("StartingEastOfBounds.txt", typeof(RoverOutOfBoundsException), "East", TestName = "StartingEastOfBounds")]
        [TestCase("StartingNorthOfBounds.txt", typeof(RoverOutOfBoundsException), "North", TestName = "StartingNorthOfBounds")]
        public void OutOfBounds(string inputFile, Type exceptionType, string message)
        {
            Assert.That(() => RunRover(inputFile), Throws.InstanceOf(exceptionType)
                .With.Message.EqualTo(message));
        }

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

        private string RunRover(string inputFile)
        {
            

            string[] fileContents = File.ReadAllLines(inputFile);

            string[] zoneSizeContents = fileContents[0]
                .Split(' ')
                .ToArray();
            int maxX = int.Parse(zoneSizeContents[0]);
            int maxY = int.Parse(zoneSizeContents[1]);

            string[] currentStateContents = fileContents[1]
                .Split(' ')
                .ToArray();

            int currentX = int.Parse(currentStateContents[0]);
            int currentY = int.Parse(currentStateContents[1]);
            if (currentX == maxX) throw new RoverOutOfBoundsException("East");
            if (currentY == maxY) throw new RoverOutOfBoundsException("North");
            string currentDirection = currentStateContents[2];
            if (!new[] { "N", "E", "S", "W" }.Contains(currentDirection))
                throw new ArgumentException("Invalid starting direction: " + currentDirection);
            foreach (char command in fileContents[2])
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

                        break;
                }
            }

            return $"{currentX} {currentY} {currentDirection}";
        }
    }

    public class RoverOutOfBoundsException : Exception
    {
        public RoverOutOfBoundsException(string message)
            : base(message)
        {
        }
    }
}
