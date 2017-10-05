using System;
using System.IO;
using Console;
using NUnit.Framework;

namespace Specs
{
    [TestFixture]
    public class FileSpecs : RoverSpec
    {

        [TestCase("this file doesn't exist.txt", "The specified file can't be found", TestName = "Non existent file")]
        [TestCase("StartingEastOfBounds.txt", "Rover would start East of zone", TestName = "StartingEastOfBounds")]
        [TestCase("StartingNorthOfBounds.txt", "Rover would start North of zone", TestName = "StartingNorthOfBounds")]
        [TestCase("InvalidStartingDirection.txt", "Invalid starting direction: X", TestName = "InvalidStartingDirection")]
        [TestCase("NegativeStartingX.txt", "Negative starting X", TestName = "NegativeStartingX")]
        [TestCase("NegativeStartingY.txt", "Negative starting Y", TestName = "NegativeStartingY")]
        [TestCase("InvalidCommand.txt", "Invalid command: X", TestName = "InvalidCommand")]
        public void VerifyFileSpecs(string inputFile, string expectedOutput)
        {
            CompareRoverToSpec(inputFile, expectedOutput);
        }

    }
}