using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Services;
using MartianRobots.Domain.Enums;
using MartianRobots.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Tests
{
    public class InputValidatorAndConverterTests
    {
        private IInputValidatorAndConverter _inputValidator;

        [SetUp]
        public void Setup()
        {
            _inputValidator = new InputValidatorAndConverter();
        }

        [TestCase("5 3", 5, 3)]
        [TestCase("25 20", 25, 20)]
        [TestCase("50 50", 50, 50)]
        [TestCase("0 0", 0, 0)]
        [TestCase(" 5 3", 5, 3)]
        [TestCase("5 3 ", 5, 3)]
        [TestCase("5  3", 5, 3)]
        public void ValidateAndConvertBoundaryCoordinates_ValidString_ShouldReturnCorrectCoordinates(string boundaryCoOrdinates, int x, int y)
        {
            //Act
            var coOrdinates = _inputValidator.ValidateAndConvertBoundaryCoordinates(boundaryCoOrdinates);

            //Assert
            Assert.That(coOrdinates.X, Is.EqualTo(x));
            Assert.That(coOrdinates.Y, Is.EqualTo(y));
        }

        [TestCase("2", ErrorMessage.InvalidBoundaryCoordinates)]
        [TestCase("2 4 6", ErrorMessage.InvalidBoundaryCoordinates)]
        [TestCase("R 4", ErrorMessage.InvalidCoordinates)]
        [TestCase("24 Z", ErrorMessage.InvalidCoordinates)]
        public void ValidateAndConvertBoundaryCoordinates_InvalidString_ShouldThrowArgumentException(string coOrdinates, string expectedErrorMessage)
        {
            //Act
            var result = Assert.Throws<ArgumentException>(() => _inputValidator.ValidateAndConvertBoundaryCoordinates(coOrdinates));

            //Assert
            Assert.That(result.Message, Does.Contain(expectedErrorMessage));
        }

        [TestCase("5 3 N", 5, 3, Direction.N, 6, 4)]
        [TestCase("24 19 S", 24, 19, Direction.S, 25, 20)]
        [TestCase("50 50 E", 50, 50, Direction.E, 50, 50)]
        [TestCase("5 3 n", 5, 3, Direction.N, 6, 4)]
        [TestCase(" 5 3 n", 5, 3, Direction.N, 6, 4)]
        [TestCase("5 3 n ", 5, 3, Direction.N, 6, 4)]
        [TestCase("5  3  n ", 5, 3, Direction.N, 6, 4)]
        public void ValidateStartingPosition_ValidInput_ShouldReturnCorrectCoordinatesAndDirection(string startingPosition, int startingXCoOrdinate,
            int startingYCoOrdinate, Direction direction, int boundaryXCoOrdinate, int boundaryYCoOrdinate)
        {
            //Act
            var result = _inputValidator.ValidateAndConvertStartingPosition(startingPosition);

            //Assert
            Assert.That(result.Coordinates.X, Is.EqualTo(startingXCoOrdinate));
            Assert.That(result.Coordinates.Y, Is.EqualTo(startingYCoOrdinate));
            Assert.That(result.Direction, Is.EqualTo(direction));
        }

        [TestCase("7 N", 6, 4, ErrorMessage.InvalidRobotStartingCoordinates)]
        [TestCase("ONE FIVE N", 6, 4, ErrorMessage.InvalidCoordinates)]
        [TestCase("50 50 R", 50, 50, ErrorMessage.InvalidDirection)]
        public void ValidateStartingPosition_InvalidInput_ShouldThrowArgumentException(string startingPosition, int boundaryXCoOrdinate, int boundaryYCoOrdinate, string expectedErrorMessage)
        {
            //Act
            var result = Assert.Throws<ArgumentException>(() => _inputValidator.ValidateAndConvertStartingPosition(startingPosition));

            //Assert
            Assert.That(result.Message, Does.Contain(expectedErrorMessage));
        }

        [TestCase("RFRFRFRF")]
        [TestCase("FRRFLLFFRRFLL")]
        [TestCase("LLFFFLFLFL")]
        [TestCase("F")]
        public void ValidateInstructions_ValidString_ShouldReturnCorrectInstructionsList(string instructions)
        {
            //Act
            var result = _inputValidator.ValidateAndConvertInstructions(instructions);

            //Assert
            Assert.That(result, Is.EqualTo(instructions.Select(c => c.ToString()).ToList()));
        }

        [TestCase("RFRWRFRF", ErrorMessage.InvalidInstructionProvided)]
        [TestCase("ZRRFLLFFRRFLL", ErrorMessage.InvalidInstructionProvided)]
        [TestCase("QQ", ErrorMessage.InvalidInstructionProvided)]
        [TestCase("A", ErrorMessage.InvalidInstructionProvided)]
        [TestCase("T3$T@", ErrorMessage.InvalidInstructionProvided)]
        [TestCase("", ErrorMessage.NoInstructionProvided)]
        public void ValidateInstructions_InvalidString_ShouldThrowArgumentException(string instructions, string expectedErrorMessage)
        {
            //Act
            var result = Assert.Throws<ArgumentException>(() => _inputValidator.ValidateAndConvertInstructions(instructions));

            //Assert
            Assert.That(result.Message, Does.Contain(expectedErrorMessage));
        }

        [TestCase("LRF", 101, ErrorMessage.InstructionLengthExceeded)]
        public void ValidateInstructions_LongString_ShouldThrowArgumentException(string pattern, int length, string expectedErrorMessage)
        {
            //Arrange
            string longInstruction = string.Concat(Enumerable.Repeat(pattern, length / pattern.Length)) + pattern.Substring(0, length % pattern.Length);

            //Act
            var result = Assert.Throws<ArgumentException>(() => _inputValidator.ValidateAndConvertInstructions(longInstruction));

            //Assert
            Assert.That(result.Message, Does.Contain(expectedErrorMessage));
        }

        [TestCase("LRF", 100, ErrorMessage.InstructionLengthExceeded)]
        [TestCase("LRF", 99, ErrorMessage.InstructionLengthExceeded)]
        public void ValidateInstructions_LongStringLessThan100Characters_ShouldNotThrowException(string pattern, int length, string expectedErrorMessage)
        {
            //Arrange
            string longInstruction = string.Concat(Enumerable.Repeat(pattern, length / pattern.Length)) + pattern.Substring(0, length % pattern.Length);
            var longInstructionList = longInstruction.Select(c => c.ToString()).ToList();

            //Act
            var result = _inputValidator.ValidateAndConvertInstructions(longInstruction);

            //Assert
            Assert.That(result, Is.EqualTo(longInstructionList));
        }
    }
}
