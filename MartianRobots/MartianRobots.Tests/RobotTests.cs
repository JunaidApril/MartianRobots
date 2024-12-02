using MartianRobots.Domain.Entities;
using MartianRobots.Domain.Enums;
using MartianRobots.Domain.Errors;
using MartianRobots.Domain.Interfaces;
using MartianRobots.Domain.ValueObjects;

namespace MartianRobots.Tests
{
    public class RobotTests
    {
        private IRobot _robot;

        [SetUp]
        public void Setup()
        {
            _robot = new Robot();
        }

        [TestCase(2, 4, Direction.N)]
        public void CreateRobot_ShouldInitializeWithValidCoordinates(int x, int y, Direction direction)
        {
            //Arrange
            _robot.Create(new Coordinates(x, y), direction);

            //Assert
            Assert.That(_robot.Direction, Is.EqualTo(direction));
            Assert.That(_robot.Coordinates.X, Is.EqualTo(x));
            Assert.That(_robot.Coordinates.Y, Is.EqualTo(y));
        }

        [TestCase(-5, 0, Direction.S, ErrorMessage.InvalidRobotStartingCoOrdinatesRange)]
        [TestCase(2, -3, Direction.S, ErrorMessage.InvalidRobotStartingCoOrdinatesRange)]
        [TestCase(51, 10, Direction.S, ErrorMessage.InvalidRobotStartingCoOrdinatesRange)]
        [TestCase(10, 75, Direction.S, ErrorMessage.InvalidRobotStartingCoOrdinatesRange)]
        public void CreateRobot_ShouldNotInitializeWithInvalidCoordinates(int x, int y, Direction direction, string expectedErrorMessage)
        {
            // Arrange
            var result = Assert.Throws<ArgumentException>(() => _robot.Create(new Coordinates(x, y), direction));

            // Assert
            Assert.That(result.Message, Does.Contain(expectedErrorMessage));
        }

        [TestCase(2, 4, Direction.N)]
        [TestCase(2, 4, Direction.W)]
        [TestCase(2, 4, Direction.E)]
        [TestCase(2, 4, Direction.S)]
        public void GetDirection_ShouldReturnProvidedDirection(int x, int y, Direction direction)
        {
            // Arrange
            _robot.Create(new Coordinates(x, y), direction);

            // Assert
            Assert.That(_robot.Direction, Is.EqualTo(direction));
        }

        [TestCase(2, 4, Direction.N, Direction.W)]
        [TestCase(2, 4, Direction.W, Direction.S)]
        [TestCase(2, 4, Direction.S, Direction.E)]
        [TestCase(2, 4, Direction.E, Direction.N)]
        public void TurnLeft_ShouldUpdateDirectionCorrectly(int x, int y, Direction direction, Direction expectedDirection)
        {
            // Arrange
            _robot.Create(new Coordinates(x, y), direction);

            // Act
            _robot.TurnLeft();

            // Assert
            Assert.That(_robot.Direction, Is.EqualTo(expectedDirection));
        }

        [TestCase(2, 4, Direction.N, Direction.E)]
        [TestCase(2, 4, Direction.E, Direction.S)]
        [TestCase(2, 4, Direction.S, Direction.W)]
        [TestCase(2, 4, Direction.W, Direction.N)]
        public void TurnRight_ShouldUpdateDirectionCorrectly(int x, int y, Direction direction, Direction expectedDirection)
        {
            // Arrange
            _robot.Create(new Coordinates(x, y), direction);

            // Act
            _robot.TurnRight();

            // Assert
            Assert.That(_robot.Direction, Is.EqualTo(expectedDirection));
        }

        [TestCase(2, 4, Direction.N)]
        [TestCase(50, 50, Direction.E)]
        [TestCase(12, 10, Direction.S)]
        [TestCase(1, 1, Direction.W)]
        public void GetCoOrdinates_ShouldReturnProvideLocation(int x, int y, Direction direction)
        {
            // Arrange
            _robot.Create(new Coordinates(x, y), direction);

            // Assert
            Assert.That(_robot.Coordinates.X, Is.EqualTo(x));
            Assert.That(_robot.Coordinates.Y, Is.EqualTo(y));
        }

        [TestCase(2, 4, Direction.E, 3, 4)]
        [TestCase(2, 4, Direction.S, 2, 3)]
        [TestCase(2, 4, Direction.W, 1, 4)]
        public void MoveForward_ShouldUpdateCoordinates_WhenWithinBounds(int x, int y, Direction direction, int expectedX, int expectedY)
        {
            // Arrange
            _robot.Create(new Coordinates(x, y), direction);

            // Act
            _robot.MoveForward();

            // Assert
            Assert.That(_robot.Coordinates.X, Is.EqualTo(expectedX));
            Assert.That(_robot.Coordinates.Y, Is.EqualTo(expectedY));
        }
    }
}