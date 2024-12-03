using MartianRobots.Domain.Entities;
using MartianRobots.Domain.Enums;
using MartianRobots.Domain.Errors;
using MartianRobots.Domain.Interfaces;
using MartianRobots.Domain.ValueObjects;

namespace MartianRobots.Tests
{
    public class MarsTests
    {
        private IMars _mars;

        [SetUp]
        public void Setup()
        {
            _mars = new Mars();
        }

        [TestCase(5, 5)]
        [TestCase(0, 0)]
        [TestCase(50, 50)]
        public void CreateMars_ShouldInitializeWithValidBoundaryCoordinates(int x, int y)
        {
            //Arrange
            _mars.Create(new Coordinates(x, y));

            //Act
            var result = _mars.BoundaryCoordinates;

            //Assert
            Assert.That(_mars.BoundaryCoordinates.X, Is.EqualTo(x));
            Assert.That(_mars.BoundaryCoordinates.Y, Is.EqualTo(y));
        }

        [TestCase(49, 51, ErrorMessage.InvalidBoundaryCoordinateRange)]
        [TestCase(51, 49, ErrorMessage.InvalidBoundaryCoordinateRange)]
        [TestCase(0, -1, ErrorMessage.InvalidBoundaryCoordinateRange)]
        [TestCase(-1, 0, ErrorMessage.InvalidBoundaryCoordinateRange)]
        public void CreateMars_ShouldNotInitializeWithInvalidBoundaryCoordinates_ShouldReturnErrorMessage(int x, int y, string errorMessage)
        {
            //Act
            var result = Assert.Throws<ArgumentException>(() => _mars.Create(new Coordinates(x, y)));

            //Assert
            Assert.That(result.Message, Does.Contain(errorMessage));
        }

        [TestCase(5, 3, 4, 2)]
        [TestCase(50, 50, 50, 50)]
        [TestCase(25, 25, 25, 24)]
        [TestCase(0, 2, 0, 1)]
        public void IsRobotWithinBounds_ValidCoordinates_ReturnsTrue(int boundaryX, int boundaryY, int x, int y)
        {
            //Arrange
            _mars.Create(new Coordinates(boundaryX, boundaryY));

            //Act
            var inBounds = _mars.IsRobotInbounds(new Coordinates(x, y));

            //Assert
            Assert.IsTrue(inBounds);
            Assert.IsFalse(_mars.ScentCoordinates.Any());
        }

        [TestCase(5, 3, 6, 2)]
        [TestCase(50, 50, 50, 51)]
        [TestCase(25, 25, 0, 26)]
        [TestCase(0, 2, -1, 1)]
        public void IsRobotWithinBounds_ValidCoordinates_ReturnsFalse(int boundaryX, int boundaryY, int x, int y)
        {
            //Arrange
            _mars.Create(new Coordinates(boundaryX, boundaryY));

            //Act
            var inBounds = _mars.IsRobotInbounds(new Coordinates(x, y));
            var scents = _mars.ScentCoordinates;

            //Assert
            Assert.IsFalse(inBounds);
            Assert.IsTrue(_mars.ScentCoordinates.Where(coordinates => coordinates.X == x && coordinates.Y == y).Any());
        }
    }
}
