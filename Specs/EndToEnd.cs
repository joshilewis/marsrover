using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console;
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
            Assert.That(new RoverRunner(inputFile).Run(), Is.EqualTo(expected));
        }

        [Test]
        public void FileDoesntExist()
        {
            var inputFile = "this file doesn't exist.txt";
            Assert.That(() => new RoverRunner(inputFile).Run(), Throws.InstanceOf<FileNotFoundException>());
        }

        [TestCase("MoveEastOfBounds.txt", typeof(RoverOutOfBoundsException), "East", TestName = "MoveEastOfBounds")]
        [TestCase("MoveWestOfBounds.txt", typeof(RoverOutOfBoundsException), "West", TestName = "MoveWestOfBounds")]
        [TestCase("MoveNorthOfBounds.txt", typeof(RoverOutOfBoundsException), "North", TestName = "MoveNorthOfBounds")]
        [TestCase("MoveSouthOfBounds.txt", typeof(RoverOutOfBoundsException), "South", TestName = "MoveSouthOfBounds")]
        [TestCase("StartingEastOfBounds.txt", typeof(RoverOutOfBoundsException), "East", TestName = "StartingEastOfBounds")]
        [TestCase("StartingNorthOfBounds.txt", typeof(RoverOutOfBoundsException), "North", TestName = "StartingNorthOfBounds")]
        [TestCase("InvalidStartingDirection.txt", typeof(ArgumentException), "Invalid starting direction: X", TestName = "InvalidStartingDirection")]
        [TestCase("InvalidCommand.txt", typeof(ArgumentException), "Invalid command: X", TestName = "InvalidCommand")]
        [TestCase("NegativeStartingX.txt", typeof(ArgumentException), "Negative starting X", TestName = "NegativeStartingX")]
        [TestCase("NegativeStartingY.txt", typeof(ArgumentException), "Negative starting Y", TestName = "NegativeStartingY")]
        public void OutOfBounds(string inputFile, Type exceptionType, string message)
        {
            Assert.That(() => new RoverRunner(inputFile).Run(), Throws.InstanceOf(exceptionType)
                .With.Message.EqualTo(message));
        }

    }
}
