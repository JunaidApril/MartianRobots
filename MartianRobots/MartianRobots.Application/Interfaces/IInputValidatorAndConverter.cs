using MartianRobots.Domain.Enums;
using MartianRobots.Domain.ValueObjects;

namespace MartianRobots.Application.Interfaces
{
    /// <summary>
    /// Interface for validating and converting input data related to the Mars exploration process.
    /// </summary>
    public interface IInputValidatorAndConverter
    {
        /// <summary>
        /// Validates and converts a string representing the boundary coordinates of the Mars grid.
        /// </summary>
        /// <param name="boundary">
        /// A string containing the upper-right coordinates of the Mars grid, e.g., "5 3".
        /// </param>
        /// <returns>
        /// A <see cref="Coordinates"/> object representing the validated boundary coordinates.
        /// </returns>
        Coordinates ValidateAndConvertBoundaryCoordinates(string boundary);

        /// <summary>
        /// Validates and converts a string representing the starting position and direction of a robot.
        /// </summary>
        /// <param name="startingPosition">
        /// A string containing the robot's starting coordinates and direction, e.g., "1 1 E".
        /// </param>
        /// <returns>
        /// A <see cref="StartingPosition"/> object containing the validated coordinates and direction.
        /// </returns>
        StartingPosition ValidateAndConvertStartingPosition(string startingPosition);

        /// <summary>
        /// Validates and converts a string containing movement instructions for a robot.
        /// </summary>
        /// <param name="instructions">
        /// A string consisting of movement instructions, e.g., "LFRF".
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{string}"/> representing a sequence of validated instructions.
        /// Each instruction corresponds to a single character command.
        /// </returns>
        IEnumerable<string> ValidateAndConvertInstructions(string instructions);
    }
}
