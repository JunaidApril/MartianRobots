using MartianRobots.Domain.Enums;
using MartianRobots.Domain.ValueObjects;

namespace MartianRobots.Domain.Interfaces
{
    public interface IRobot
    {
        void Create(Coordinates coordinates, Direction direction);

        void TurnLeft();

        void TurnRight();

        void MoveForward();
        Direction Direction { get; }

        Coordinates CoOrdinates { get; }

        bool IsLost { get; }
    }
}
