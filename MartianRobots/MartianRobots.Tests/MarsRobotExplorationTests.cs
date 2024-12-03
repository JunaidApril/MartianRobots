using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Services;
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
        private MarsRobotExploration _marsRobotExploration;

        public MarsRobotExplorationTests()
        {
            _marsMock = new Mock<IMars>();
            _inputValidatorMock = new Mock<IInputValidatorAndConverter>();
            _marsRobotExploration = new MarsRobotExploration(_marsMock.Object, _inputValidatorMock.Object);
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
        public void RobotMovesOutOfBounds_MarkedAsLost()
        {
            // Arrange
            var marsBoundary = new Coordinates(3, 3);
            _inputValidatorMock.Setup(x => x.ValidateAndConvertBoundaryCoordinates(It.IsAny<string>()))
                .Returns(marsBoundary);

            var robotStartingPosition = new StartingPosition(new Coordinates(1, 1), Direction.N);
            _inputValidatorMock.Setup(x => x.ValidateAndConvertStartingPosition(It.IsAny<string>()))
                .Returns(robotStartingPosition);

            var instructions = "FFF";
            var instructionsArray = instructions.Select(c => c.ToString()).ToList();
            _inputValidatorMock.Setup(x => x.ValidateAndConvertInstructions(It.IsAny<string>()))
                .Returns(instructionsArray);

            var scentCoordinates = new List<Coordinates> { };
            _marsMock.Setup(m => m.ScentCoordinates).Returns(scentCoordinates);

            _marsMock.SetupSequence(m => m.IsRobotInbounds(It.IsAny<Coordinates>()))
                .Returns(true)
                .Returns(true)
                .Returns(false);

            // Act
            _marsRobotExploration.StartExploration("3 3\r\n1 1 N\r\nFFF");

            // Assert
            var outcome = _marsRobotExploration.GetRobotsResultsOnMars();
            Assert.Contains("1 4 N LOST", outcome);
        }

        [Test]
        public void RobotAvoidsScentedLocation_DoesNotMove()
        {
            // Arrange
            var marsBoundary = new Coordinates(3, 3);
            _inputValidatorMock.Setup(x => x.ValidateAndConvertBoundaryCoordinates(It.IsAny<string>()))
                .Returns(marsBoundary);

            var robotStartingPosition = new StartingPosition(new Coordinates(1, 1), Direction.N);
            _inputValidatorMock.Setup(x => x.ValidateAndConvertStartingPosition(It.IsAny<string>()))
                .Returns(robotStartingPosition);

            var instructions = "FFF";
            var instructionsArray = instructions.Select(c => c.ToString()).ToList();
            _inputValidatorMock.Setup(x => x.ValidateAndConvertInstructions(It.IsAny<string>()))
                .Returns(instructionsArray);

            var scentCoordinates = new List<Coordinates> { new Coordinates(1, 4) };
            _marsMock.Setup(m => m.ScentCoordinates).Returns(scentCoordinates);

            _marsMock.SetupSequence(m => m.IsRobotInbounds(It.IsAny<Coordinates>()))
                 .Returns(true)
                 .Returns(true)
                 .Returns(true)
                 .Returns(true);

            //Act
            _marsRobotExploration.StartExploration("3 3\r\n1 1 N\r\nFFF");

            // Assert
            var outcome = _marsRobotExploration.GetRobotsResultsOnMars();
            Assert.Contains("1 3 N ", outcome);
        }
    }
}
