using MartianRobots.Domain.Entities;
using MartianRobots.Domain.Enums;
using MartianRobots.Domain.Errors;
using MartianRobots.Domain.Interfaces;
using MartianRobots.Domain.ValueObjects;
using Moq;

namespace MartianRobots.Tests
{
    public class RobotTests
    {
        private IRobot _robot;
        private Mock<IMars> _marsMock;
        private Coordinates _marsBoundaryCoordinates;
        private Coordinates _robotCoordinates;
        private Direction _direction;

        public RobotTests()
        {
            _marsBoundaryCoordinates = new Coordinates(25, 25);
            _robotCoordinates = new Coordinates(10, 10);
            _direction = Direction.N;
        }

        [SetUp]
        public void Setup()
        {
            _marsMock = new Mock<IMars>();
            _marsMock.Setup(m => m.BoundaryCoordinates).Returns(new Coordinates(_marsBoundaryCoordinates.X, _marsBoundaryCoordinates.Y));
            // Mock in-bound check to simulate different conditions
            _marsMock.Setup(m => m.IsRobotInbounds(It.IsAny<Coordinates>()))
                .Returns((Coordinates coordinates) => coordinates.X >= 0 && coordinates.Y >= 0 && coordinates.X <= _marsBoundaryCoordinates.X && coordinates.Y <= _marsBoundaryCoordinates.Y);

            _marsMock.Setup(m => m.ScentCoordinates)
                .Returns(new HashSet<Coordinates>());

            _robot = new Robot();

            _robot.Create(_robotCoordinates, _direction);
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

        [TestCase(-5, 0, Direction.S, ErrorMessage.InvalidRobotStartingCoordinatesRange)]
        [TestCase(2, -3, Direction.S, ErrorMessage.InvalidRobotStartingCoordinatesRange)]
        [TestCase(51, 10, Direction.S, ErrorMessage.InvalidRobotStartingCoordinatesRange)]
        [TestCase(10, 75, Direction.S, ErrorMessage.InvalidRobotStartingCoordinatesRange)]
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

        [TestCase(1, 1, Direction.E, 1, 1, Direction.E)]
        public void ExecuteMultipleInstructions_ShouldMoveCorrectlyThroughSequence(int x, int y, Direction direction, int expectedX, int expectedY, Direction expectedDirection)
        {
            // Arrange: Initialize the robot at the given starting coordinates and direction
            _robot.Create(new Coordinates(x, y), direction);

            // Act: Perform a series of movements and turns
            _robot.TurnRight(); // Turn the robot 90 degrees clockwise (to South)
            _robot.MoveForward(); // Move forward, should move 1 step down (South)
            _robot.TurnRight(); // Turn the robot 90 degrees clockwise (to West)
            _robot.MoveForward(); // Move forward, should move 1 step down (West)
            _robot.TurnRight(); // Turn the robot 90 degrees clockwise (to North)
            _robot.MoveForward(); // Move forward, should move 1 step up (North)
            _robot.TurnRight(); // Turn the robot 90 degrees clockwise (to East)
            _robot.MoveForward(); // Move forward, should move 1 step right (East)

            // Assert: Verify the robot's final position and direction
            Assert.That(_robot.Coordinates.X, Is.EqualTo(expectedX));
            Assert.That(_robot.Coordinates.Y, Is.EqualTo(expectedY));
            Assert.IsFalse(_robot.IsLost);
        }
    }
}