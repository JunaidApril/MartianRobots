using MartianRobots.Domain.Enums;
using MartianRobots.Domain.Errors;
using MartianRobots.Domain.Interfaces;
using MartianRobots.Domain.ValueObjects;

namespace MartianRobots.Domain.Entities
{
    public class Robot : IRobot
    {
        private readonly IMars _mars;
        private int _xCoordinate;
        private int _yCoordinate;
        private Direction _direction;

        public Robot(IMars mars) 
        {
            _mars = mars;
        }

        public Direction Direction => _direction;

        public Coordinates Coordinates => new Coordinates(_xCoordinate, _yCoordinate);

        public bool IsLost { get; private set; }

        public void Create(Coordinates coordinates, Direction direction)
        {
            if (coordinates.X < 0 || coordinates.X > 50 || coordinates.Y < 0 || coordinates.Y > 50)
                throw new ArgumentException(ErrorMessage.InvalidRobotStartingCoOrdinatesRange);

            _xCoordinate = coordinates.X;
            _yCoordinate = coordinates.Y;
            _direction = direction;
        }

        public void MoveForward()
        {
            var newCoordinates = GetNextCoordinates();

            if (CheckForScents(newCoordinates.X, newCoordinates.Y))
                return;

            _xCoordinate = newCoordinates.X;
            _yCoordinate = newCoordinates.Y;

            CheckRobotLocation();
        }

        private Coordinates GetNextCoordinates()
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

        private bool CheckForScents(int xCoOrdinate, int yCoOrdinate)
        {
            if (_mars.ScentCoordinates.Where(x => x.X == xCoOrdinate && x.Y == yCoOrdinate).Any())
                return true;

            return false;
        }

        private void CheckRobotLocation()
        {
            var inbounds = _mars.IsRobotInbounds(new Coordinates(_xCoordinate, _yCoordinate));

            if (!inbounds)
                IsLost = true;
        }
    }
}
