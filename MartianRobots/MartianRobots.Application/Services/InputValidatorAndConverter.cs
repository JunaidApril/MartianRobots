using MartianRobots.Application.Interfaces;
using MartianRobots.Domain.Enums;
using MartianRobots.Domain.ValueObjects;

namespace MartianRobots.Application.Services
{
    public class InputValidatorAndConverter : IInputValidatorAndConverter
    {
        public Coordinates ValidateAndConvertBoundaryCoordinates(string boundary)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ValidateAndConvertInstructions(string instructions)
        {
            throw new NotImplementedException();
        }

        public (Coordinates, Direction) ValidateAndConvertStartingPosition(string startingPosition)
        {
            throw new NotImplementedException();
        }
    }
}
