using MartianRobots.Domain.Enums;
using MartianRobots.Domain.Interfaces;
using MartianRobots.Domain.ValueObjects;

namespace MartianRobots.Domain.Entities
{
    public class Robot : IRobot
    {
        public Direction Direction => throw new NotImplementedException();

        public Coordinates CoOrdinates => throw new NotImplementedException();

        public bool IsLost => throw new NotImplementedException();

        public void Create(Coordinates coordinates, Direction direction)
        {
            throw new NotImplementedException();
        }

        public void MoveForward()
        {
            throw new NotImplementedException();
        }

        public void TurnLeft()
        {
            throw new NotImplementedException();
        }

        public void TurnRight()
        {
            throw new NotImplementedException();
        }
    }
}
