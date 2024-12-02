using MartianRobots.Domain.Enums;
using MartianRobots.Domain.Errors;
using MartianRobots.Domain.Interfaces;
using MartianRobots.Domain.ValueObjects;

namespace MartianRobots.Domain.Entities
{
    public class Robot : IRobot
    {
        private Coordinates _coordinates;
        private int _xCoOrdinate;
        private int _yCoOrdinate;
        private Direction _direction;

        public Robot() { }

        public Direction Direction => _direction;

        public Coordinates Coordinates => new Coordinates(_xCoOrdinate, _yCoOrdinate);

        public bool IsLost => throw new NotImplementedException();

        public void Create(Coordinates coordinates, Direction direction)
        {
            if (coordinates.X < 0 || coordinates.X > 50 || coordinates.Y < 0 || coordinates.Y > 50)
                throw new ArgumentException(ErrorMessage.InvalidRobotStartingCoOrdinatesRange);

            _xCoOrdinate = coordinates.X;
            _yCoOrdinate = coordinates.Y;
            _direction = direction;
        }

        public void MoveForward()
        {
            switch (_direction)
            {
                case Direction.N:
                    _yCoOrdinate += 1;
                    break;

                case Direction.S:
                    _yCoOrdinate -= 1;
                    break;

                case Direction.E:
                    _xCoOrdinate += 1;
                    break;

                case Direction.W:
                    _xCoOrdinate -= 1;
                    break;

                default:
                    throw new ArgumentException(ErrorMessage.InvalidDirection);
            }
        }

        public void TurnLeft()
        {
            switch (_direction)
            {
                case Direction.N:
                    _direction = Direction.W;
                    break;

                case Direction.W:
                    _direction = Direction.S;
                    break;

                case Direction.S:
                    _direction = Direction.E;
                    break;

                case Direction.E:
                    _direction = Direction.N;
                    break;

                default:
                    throw new ArgumentException(ErrorMessage.InvalidDirection);
            }
        }

        public void TurnRight()
        {
            switch (_direction)
            {
                case Direction.N:
                    _direction = Direction.E;
                    break;

                case Direction.E:
                    _direction = Direction.S;
                    break;

                case Direction.S:
                    _direction = Direction.W;
                    break;

                case Direction.W:
                    _direction = Direction.N;
                    break;

                default:
                    throw new ArgumentException(ErrorMessage.InvalidDirection);
            }
        }
    }
}
