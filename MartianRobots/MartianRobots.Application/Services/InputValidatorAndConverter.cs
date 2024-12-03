using MartianRobots.Application.Interfaces;
using MartianRobots.Domain.Enums;
using MartianRobots.Domain.Errors;
using MartianRobots.Domain.ValueObjects;

namespace MartianRobots.Application.Services
{
    public class InputValidatorAndConverter : IInputValidatorAndConverter
    {
        public Coordinates ValidateAndConvertBoundaryCoordinates(string boundary)
        {
            var coOrdinates = boundary.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (coOrdinates.Length != 2)
                throw new ArgumentException(ErrorMessage.InvalidBoundaryCoordinates);

            if (!int.TryParse(coOrdinates[0], out int boundaryXCoOrdinate) || !int.TryParse(coOrdinates[1], out int boundaryYCoOrdinate))
                throw new ArgumentException(ErrorMessage.InvalidCoordinates);

            return new Coordinates(boundaryXCoOrdinate, boundaryYCoOrdinate);
        }

        public StartingPosition ValidateAndConvertStartingPosition(string startingPosition)
        {
            var startPos = startingPosition.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (startPos.Length != 3)
                throw new ArgumentException(ErrorMessage.InvalidRobotStartingCoordinates);

            if (!int.TryParse(startPos[0], out int startXPosition) || !int.TryParse(startPos[1], out int startYPosition))
                throw new ArgumentException(ErrorMessage.InvalidCoordinates);

            if (!Enum.TryParse(startPos[2], true, out Direction direction))
                throw new ArgumentException(ErrorMessage.InvalidDirection);

            return new StartingPosition(new Coordinates(startXPosition, startYPosition), direction);
        }

        public IEnumerable<string> ValidateAndConvertInstructions(string instructions)
        {
            if (instructions.Length > 100)
                throw new ArgumentException(ErrorMessage.InstructionLengthExceeded);

            var instructionsArray = instructions.Select(c => c.ToString()).ToList();

            if (instructionsArray.Count == 0)
                throw new ArgumentException(ErrorMessage.NoInstructionProvided);

            if (!instructionsArray.All(c => Enum.IsDefined(typeof(Instruction), c.ToString())))
                throw new ArgumentException(ErrorMessage.InvalidInstructionProvided);

            return instructionsArray;
        }
    }
}
