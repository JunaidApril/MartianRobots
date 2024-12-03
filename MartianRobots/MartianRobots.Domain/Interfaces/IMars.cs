using MartianRobots.Domain.ValueObjects;

namespace MartianRobots.Domain.Interfaces
{
    /// <summary>
    /// Interface representing the Mars environment where robots operate.
    /// </summary>
    public interface IMars
    {
        /// <summary>
        /// Initializes the Mars grid with specified boundary coordinates.
        /// </summary>
        /// <param name="coordinates">
        /// A <see cref="Coordinates"/> object representing the upper-right boundary of the Mars grid.
        /// </param>
        void Create(Coordinates coordinates);

        /// <summary>
        /// Checks if a given set of coordinates is within the boundaries of the Mars grid.
        /// </summary>
        /// <param name="coordinates">
        /// A <see cref="Coordinates"/> object representing the current position of the robot.
        /// </param>
        /// <returns>
        /// <c>true</c> if the coordinates are within the grid boundaries; otherwise, <c>false</c>.
        /// If the robot goes out of bounds, the position is recorded as a scent to prevent future losses at the same point.
        /// </returns>
        bool IsRobotInbounds(Coordinates coordinates);

        /// <summary>
        /// Gets the boundary coordinates of the Mars grid.
        /// </summary>
        /// <value>
        /// A <see cref="Coordinates"/> object representing the upper-right boundary of the grid.
        /// </value>
        Coordinates BoundaryCoordinates {  get; }

        /// <summary>
        /// Gets a list of scent coordinates left by robots that have fallen off the grid.
        /// </summary>
        /// <value>
        /// A list of <see cref="Coordinates"/> objects representing the positions where robots were lost.
        /// Future robots will avoid moving off the grid at these positions.
        /// </value>
        List<Coordinates> ScentCoordinates { get; }
    }
}
