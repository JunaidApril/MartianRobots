using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Services;
using MartianRobots.Domain.Entities;
using MartianRobots.Domain.Enums;
using MartianRobots.Domain.Interfaces;
using MartianRobots.Domain.ValueObjects;
using Moq;

namespace MartianRobots.Tests
{
    public class MarsRobotExplorationTests
    {
        private readonly Mock<IMars> _marsMock;
        private readonly Mock<IInputValidatorAndConverter> _inputValidatorMock;
        private readonly Mock<IRobot> _robotMock;
        private MarsRobotExploration _marsRobotExploration;

        public MarsRobotExplorationTests()
        {
            _marsMock = new Mock<IMars>();
            _inputValidatorMock = new Mock<IInputValidatorAndConverter>();
            _robotMock = new Mock<IRobot>();
            _marsRobotExploration = new MarsRobotExploration(_marsMock.Object, _inputValidatorMock.Object, _robotMock.Object);
        }

        [SetUp]
        public void Setup()
        {
            //_robotMock.Object.Create(new Coordinates(1, 1), Direction.N);
           // _marsRobotExploration = new MarsRobotExploration(_marsMock.Object, _inputValidatorMock.Object, _robotMock.Object);
        }

        [Test]
        public void CreateMars_ValidInput_CreatesMarsSuccessfully()
        {
            // Arrange
            var marsBoundary = new Coordinates(5, 5);
            _inputValidatorMock.Setup(x => x.ValidateAndConvertBoundaryCoordinates(It.IsAny<string>()))
                .Returns(marsBoundary);

            // Act
            _marsRobotExploration.StartExploration("5 5");

            // Assert
            _marsMock.Verify(m => m.Create(It.Is<Coordinates>(c => c.X == 5 && c.Y == 5)), Times.Once);
        }

        [Test]
        public void CreateRobot_ValidInput_CreatesRobotSuccessfully()
        {
            // Arrange
            var startingPosition = new StartingPosition(new Coordinates(1, 1), Direction.E);
            _inputValidatorMock.Setup(x => x.ValidateAndConvertStartingPosition(It.IsAny<string>()))
                .Returns(startingPosition);

            // Act
            _marsRobotExploration.StartExploration("5 3\r\n1 1 E");

            // Assert
            _robotMock.Verify(m => m.Create(It.Is<Coordinates>(c => c.X == 1 && c.Y == 1), Direction.E), Times.Once);
        }

        [Test]
        public void RobotMovesCorrectlyAndStopsBeforeScent()
        {
            // Arrange
            var currentCoordinates = new Coordinates(1, 1); // Initial position
            var newCoordinatesSequence = new Queue<Coordinates>(new[]
            {
                new Coordinates(1, 2),
                new Coordinates(1, 3),
                new Coordinates(1, 4) // Scented location
            });

            // Setup robot mock to return updated coordinates
            _robotMock.SetupGet(r => r.Coordinates).Returns(() => currentCoordinates);

            var startingPosition = new StartingPosition(new Coordinates(1, 1), Direction.E);
            _inputValidatorMock.Setup(x => x.ValidateAndConvertStartingPosition(It.IsAny<string>()))
                .Returns(startingPosition);

            _robotMock.Setup(x => x.Create(startingPosition.Coordinates, startingPosition.Direction));

            _robotMock.Setup(r => r.MoveForward()).Callback(() =>
            {
                currentCoordinates = newCoordinatesSequence.Dequeue(); // Move to next position
            });

            _robotMock.Setup(r => r.IsLost).Returns(false);

            var instructionsArray = new List<string> { "F", "F", "F" };
            _inputValidatorMock.Setup(x => x.ValidateAndConvertInstructions(It.IsAny<string>()))
                .Returns(instructionsArray);

            _robotMock.SetupSequence(r => r.GetNextCoordinates())
                .Returns(new Coordinates(1, 2))
                .Returns(new Coordinates(1, 3))                 
                .Returns(new Coordinates(1, 4));

            _marsMock.Setup(m => m.ScentCoordinates).Returns(new HashSet<Coordinates> { new Coordinates(1, 4) });
            _marsMock.Setup(m => m.IsRobotInbounds(It.IsAny<Coordinates>())).Returns((Coordinates c) => c.Y <= 3); // Scented location is treated as 'out of bounds'

            // Act
            _marsRobotExploration.StartExploration("3 3\r\n1 1 N\r\nFFF");

            // Assert
            var outcome = _marsRobotExploration.GetRobotsResultsOnMars();
            Assert.IsTrue(outcome.Any(o => o.Trim() == "1 3 N"));
        }

        [Test]
        public void RobotGoesOutOfBounds_IsMarkedAsLost()
        {
            // Arrange
            var boundary = new Coordinates(3, 3);
            _marsMock.Setup(m => m.BoundaryCoordinates).Returns(boundary);
            _marsMock.Setup(m => m.ScentCoordinates).Returns(new HashSet<Coordinates>());

            var startingPosition = new StartingPosition(new Coordinates(3, 3), Direction.N);
            _inputValidatorMock.Setup(v => v.ValidateAndConvertBoundaryCoordinates(It.IsAny<string>())).Returns(boundary);
            _inputValidatorMock.Setup(v => v.ValidateAndConvertStartingPosition(It.IsAny<string>())).Returns(startingPosition);

            var instructions = new List<string> { "F" }; // Move forward into out-of-bounds
            _inputValidatorMock.Setup(v => v.ValidateAndConvertInstructions(It.IsAny<string>())).Returns(instructions);

            //_robotMock.Setup(r => r.Coordinates).Returns(startingPosition.Coordinates);
            _robotMock.SetupGet(r => r.Coordinates).Returns(() => startingPosition.Coordinates);
            _robotMock.Setup(r => r.Direction).Returns(startingPosition.Direction);
            _robotMock.SetupSequence(r => r.GetNextCoordinates())
                      .Returns(new Coordinates(3, 4)); // Simulate out-of-bounds coordinates

            _robotMock.Setup(r => r.MoveForward()).Callback(() =>
            {
                startingPosition.Coordinates = new Coordinates(3, 4); // Move to next position
            });

            _robotMock.Setup(r => r.IsLost).Returns(true);

            _marsMock.Setup(m => m.IsRobotInbounds(It.IsAny<Coordinates>())).Returns(false);

            // Act
            _marsRobotExploration.StartExploration("3 3\n3 3 N\nF");

            // Assert
            var outcome = _marsRobotExploration.GetRobotsResultsOnMars();
            Assert.Contains("3 4 N LOST", outcome);
        }
    }
}
