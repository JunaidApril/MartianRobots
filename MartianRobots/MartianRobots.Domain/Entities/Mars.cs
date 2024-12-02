using MartianRobots.Domain.Interfaces;
using MartianRobots.Domain.ValueObjects;

namespace MartianRobots.Domain.Entities
{
    public class Mars : IMars
    {
        public Coordinates BoundaryCoordinates => throw new NotImplementedException();

        public List<Coordinates> ScentCoordinates => throw new NotImplementedException();

        public void Create(Coordinates coordinates)
        {
            throw new NotImplementedException();
        }

        public bool IsRobotInbounds(Coordinates coordinates)
        {
            throw new NotImplementedException();
        }
    }
}
