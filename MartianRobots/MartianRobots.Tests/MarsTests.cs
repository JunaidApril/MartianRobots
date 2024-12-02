using MartianRobots.Domain.Entities;
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

        [Test]
        public void CreateMars_ShouldInitializeWithValidBoundaryCoordinates()
        {
            //Arrange
            _mars.Create(new Coordinates(5, 5));

            //Act
            var result = _mars.BoundaryCoordinates;

            //Assert
            Assert.That(_mars.BoundaryCoordinates.X, Is.EqualTo(5));
        }

        [Test]
        public void IsRobotWithinBounds_ValidCoordinates_ReturnsTrue()
        {
            //Arrange

            //Act

            //Assert
            Assert.Pass();
        }
    }
}
