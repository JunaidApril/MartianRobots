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
        private readonly Mock<IRobot> _robotMock;
        private MarsRobotExploration _marsRobotExploration;

        public MarsRobotExplorationTests()
        {
            _marsMock = new Mock<IMars>();
            _inputValidatorMock = new Mock<IInputValidatorAndConverter>();
            _robotMock = new Mock<IRobot>();
            _marsRobotExploration = new MarsRobotExploration(_marsMock.Object, _inputValidatorMock.Object, _robotMock.Object);
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
    }
}
