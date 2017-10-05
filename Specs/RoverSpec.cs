using Model;
using NUnit.Framework;

namespace Specs
{
    public abstract class RoverSpec
    {
        public void CompareRoverToSpec(string inputFile, string expectedOutput)
        {
            Assert.That(new Rover(inputFile).Run(), Is.EqualTo(expectedOutput));
        }
    }
}