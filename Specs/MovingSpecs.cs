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
    public class MovingSpecs
    {
        [TestCase("RealWorld1.txt", "3 3 S", TestName = "RealWorld1")]
        [TestCase("MoveEastOfBounds.txt", "Rover would move East out of the zone", TestName = "MoveEastOfBounds")]
        [TestCase("MoveWestOfBounds.txt", "Rover would move West out of the zone", TestName = "MoveWestOfBounds")]
        [TestCase("MoveNorthOfBounds.txt", "Rover would move North out of the zone", TestName = "MoveNorthOfBounds")]
        [TestCase("MoveSouthOfBounds.txt", "Rover would move South out of the zone", TestName = "MoveSouthOfBounds")]
        public void MoveOutOfBounds(string inputFile, string expectedOutput)
        {
            Assert.That(new Rover(inputFile).Run(), Is.EqualTo(expectedOutput));
        }
    }
}
