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
            Assert.That(new Rover(inputFile).Run(), Is.EqualTo(expected));
        }

        [TestCase("MoveEastOfBounds.txt", "East", TestName = "MoveEastOfBounds")]
        [TestCase("MoveWestOfBounds.txt", "West", TestName = "MoveWestOfBounds")]
        [TestCase("MoveNorthOfBounds.txt", "North", TestName = "MoveNorthOfBounds")]
        [TestCase("MoveSouthOfBounds.txt", "South", TestName = "MoveSouthOfBounds")]
        public void MoveOutOfBounds(string inputFile, string message)
        {
            Assert.That(() => new Rover(inputFile).Run(), Throws.InstanceOf(typeof(RoverOutOfBoundsException))
                .With.Message.EqualTo(message));
        }
    }
}
