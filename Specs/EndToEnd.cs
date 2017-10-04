using System;
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

        [Test]
        public void OutOfBoundsEast()
        {
            var inputFile = "OutOfBoundsEast.txt";
            Assert.That(() => RunRover(inputFile), Throws.InstanceOf<RoverOutOfBoundsException>()
                .With.Message.EqualTo("East"));
        }


        private string RunRover(string inputFile)
        {
            string[] fileContents = File.ReadAllLines(inputFile);

            string[] zoneSizeContents = fileContents[0]
                .Split(' ')
                .ToArray();
            int maxX = int.Parse(zoneSizeContents[0]);

            string[] currentStateContents = fileContents[1]
                .Split(' ')
                .ToArray();

            int currentX = int.Parse(currentStateContents[0]);
            int currentY = int.Parse(currentStateContents[1]);
            string currentDirection = currentStateContents[2];

            foreach (char command in fileContents[2])
            {
                switch (command)
                {
                    case 'R':
                        switch (currentDirection)
                        {
                            case "N":
                                currentDirection = "E";
                                break;
                            case "E":
                                currentDirection = "S";
                                break;
                            case "S":
                                currentDirection = "W";
                                break;
                            case "W":
                                currentDirection = "N";
                                break;
                        }
                        break;
                    case 'L':
                        switch (currentDirection)
                        {
                            case "N":
                                currentDirection = "W";
                                break;
                            case "E":
                                currentDirection = "N";
                                break;
                            case "S":
                                currentDirection = "E";
                                break;
                            case "W":
                                currentDirection = "S";
                                break;
                        }
                        break;
                    case 'M':
                        switch (currentDirection)
                        {
                            case "N":
                                currentY++;
                                break;
                            case "E":
                                if (currentX == maxX - 1) throw new RoverOutOfBoundsException("East");
                                currentX++;
                                break;
                            case "S":
                                currentY--;
                                break;
                            case "W":
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
