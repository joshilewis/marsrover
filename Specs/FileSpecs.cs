using System;
using System.IO;
using Console;
using NUnit.Framework;

namespace Specs
{
    [TestFixture]
    public class FileSpecs
    {
        [TestCase("StartingEastOfBounds.txt", typeof(RoverOutOfBoundsException), "East", TestName = "StartingEastOfBounds")]
        [TestCase("StartingNorthOfBounds.txt", typeof(RoverOutOfBoundsException), "North", TestName = "StartingNorthOfBounds")]
        [TestCase("InvalidStartingDirection.txt", typeof(ArgumentException), "Invalid starting direction: X", TestName = "InvalidStartingDirection")]
        [TestCase("InvalidCommand.txt", typeof(ArgumentException), "Invalid command: X", TestName = "InvalidCommand")]
        [TestCase("NegativeStartingX.txt", typeof(ArgumentException), "Negative starting X", TestName = "NegativeStartingX")]
        [TestCase("NegativeStartingY.txt", typeof(ArgumentException), "Negative starting Y", TestName = "NegativeStartingY")]
        public void FileProblems(string inputFile, Type exceptionType, string message)
        {
            Assert.That(() => new Rover(inputFile).Run(), Throws.InstanceOf(exceptionType)
                .With.Message.EqualTo(message));
        }

        [Test]
        public void FileDoesntExist()
        {
            var inputFile = "this file doesn't exist.txt";
            Assert.That(() => new Rover(inputFile).Run(), Throws.InstanceOf<FileNotFoundException>());
        }
    }
}