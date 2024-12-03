using MartianRobots.Domain.Errors;
using MartianRobots.Domain.Interfaces;
using MartianRobots.Domain.ValueObjects;

namespace MartianRobots.Domain.Entities
{
    public class Mars : IMars
    {
        private Coordinates _boundaryCoordinates;
        private HashSet<Coordinates> _scentCoOrdinates = new HashSet<Coordinates>();
        private const int MaxBoundaryLimit = 50;
        private const int MinBoundaryLimit = 0;

        public Mars() { }

        public Coordinates BoundaryCoordinates => _boundaryCoordinates;

        public HashSet<Coordinates> ScentCoordinates => _scentCoOrdinates;

        public void Create(Coordinates coordinates)
        {
            if (coordinates.X < MinBoundaryLimit || coordinates.X > MaxBoundaryLimit || coordinates.Y < MinBoundaryLimit || coordinates.Y > MaxBoundaryLimit)
                throw new ArgumentException(ErrorMessage.InvalidBoundaryCoordinateRange);

            _boundaryCoordinates = coordinates;
        }

        public bool IsRobotInbounds(Coordinates coordinates)
        {
            if (coordinates.X <= _boundaryCoordinates.X && coordinates.X >= MinBoundaryLimit && coordinates.Y <= _boundaryCoordinates.Y && coordinates.Y >= MinBoundaryLimit)
                return true;

            _scentCoOrdinates.Add(new Coordinates(coordinates.X, coordinates.Y));

            return false;
        }
    }
}
