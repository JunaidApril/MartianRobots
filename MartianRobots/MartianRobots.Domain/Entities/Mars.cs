using MartianRobots.Domain.Errors;
using MartianRobots.Domain.Interfaces;
using MartianRobots.Domain.ValueObjects;

namespace MartianRobots.Domain.Entities
{
    public class Mars : IMars
    {
        private Coordinates _boundaryCoordinates;
        private List<Coordinates> _scentCoOrdinates = new List<Coordinates>();

        public Mars() { }

        public Coordinates BoundaryCoordinates => _boundaryCoordinates;

        public List<Coordinates> ScentCoordinates => _scentCoOrdinates;

        public void Create(Coordinates coordinates)
        {
            if (coordinates.X < 0 || coordinates.X > 50 || coordinates.Y < 0 || coordinates.Y > 50)
                throw new ArgumentException(ErrorMessage.InvalidBoundaryCoOrdinateRange);

            _boundaryCoordinates = coordinates;
        }

        public bool IsRobotInbounds(Coordinates coordinates)
        {
            if (coordinates.X <= _boundaryCoordinates.X && coordinates.X >= 0 && coordinates.Y <= _boundaryCoordinates.Y && coordinates.Y >= 0)
                return true;

            _scentCoOrdinates.Add(new Coordinates(coordinates.X, coordinates.Y));

            return false;
        }
    }
}
