using MartianRobots.Domain.ValueObjects;

namespace MartianRobots.Domain.Interfaces
{
    public interface IMars
    {
        void Create(Coordinates coordinates);

        bool IsRobotInbounds(Coordinates coordinates);

        Coordinates BoundaryCoordinates {  get; }

        List<Coordinates> ScentCoordinates { get; }
    }
}
