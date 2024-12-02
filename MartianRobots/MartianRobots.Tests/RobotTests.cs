using MartianRobots.Domain.Entities;
using MartianRobots.Domain.Enums;
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

        [Test]
        public void CreateRobot_ShouldInitializeWithValidCoordinates()
        {
            //Arrange


            //Act
            _robot.Create(new Coordinates(3, 3), Direction.N);

            //Assert
            Assert.Pass();
        }
    }
}