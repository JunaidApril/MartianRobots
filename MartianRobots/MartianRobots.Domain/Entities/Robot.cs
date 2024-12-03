using MartianRobots.Domain.Enums;
using MartianRobots.Domain.Errors;
using MartianRobots.Domain.Interfaces;
using MartianRobots.Domain.ValueObjects;

namespace MartianRobots.Domain.Entities
{
    public class Robot : IRobot
    {
        private int _xCoordinate;
        private int _yCoordinate;
        private Direction _direction;

        public Robot() {}

        public Direction Direction => _direction;

        public Coordinates Coordinates => new Coordinates(_xCoordinate, _yCoordinate);

        public bool IsLost { get; set; }

        public void Create(Coordinates coordinates, Direction direction)
        {
            if (coordinates.X < 0 || coordinates.X > 50 || coordinates.Y < 0 || coordinates.Y > 50)
                throw new ArgumentException(ErrorMessage.InvalidRobotStartingCoordinatesRange);

            _xCoordinate = coordinates.X;
            _yCoordinate = coordinates.Y;
            _direction = direction;
            IsLost = false;
        }

        public void MoveForward()
        {
            var newCoordinates = GetNextCoordinates();

            _xCoordinate = newCoordinates.X;
            _yCoordinate = newCoordinates.Y;
        }

        public Coordinates GetNextCoordinates()
        {
            return _direction switch
            {
                Direction.N => new Coordinates(_xCoordinate, _yCoordinate + 1),
                Direction.S => new Coordinates(_xCoordinate, _yCoordinate - 1),
                Direction.E => new Coordinates(_xCoordinate + 1, _yCoordinate),
                Direction.W => new Coordinates(_xCoordinate - 1, _yCoordinate),
                _ => throw new ArgumentException(ErrorMessage.InvalidDirection),
            };
        }

        public void TurnLeft()
        {
            _direction = _direction switch
            {
                Direction.N => Direction.W,
                Direction.W => Direction.S,
                Direction.S => Direction.E,
                Direction.E => Direction.N,
                _ => throw new ArgumentException(ErrorMessage.InvalidDirection),
            };
        }

        public void TurnRight()
        {
            _direction = _direction switch
            {
                Direction.N => Direction.E,
                Direction.E => Direction.S,
                Direction.S => Direction.W,
                Direction.W => Direction.N,
                _ => throw new ArgumentException(ErrorMessage.InvalidDirection),
            };
        }
    }
}
