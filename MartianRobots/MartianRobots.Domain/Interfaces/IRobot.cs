using MartianRobots.Domain.Enums;
using MartianRobots.Domain.ValueObjects;

namespace MartianRobots.Domain.Interfaces
{
    /// <summary>
    /// Interface representing the core functionality of a robot exploring Mars.
    /// </summary>
    public interface IRobot
    {
        /// <summary>
        /// Initializes the robot with specified starting coordinates and direction.
        /// </summary>
        /// <param name="coordinates">
        /// A <see cref="Coordinates"/> object representing the robot's initial position.
        /// </param>
        /// <param name="direction">
        /// The initial <see cref="Direction"/> the robot is facing.
        /// </param>
        void Create(Coordinates coordinates, Direction direction);

        /// <summary>
        /// Rotates the robot 90 degrees to the left (counterclockwise) without changing its position.
        /// </summary>
        void TurnLeft();

        /// <summary>
        /// Rotates the robot 90 degrees to the right (clockwise) without changing its position.
        /// </summary>
        void TurnRight();

        /// <summary>
        /// Moves the robot one step forward in the direction it is currently facing.
        /// </summary>
        void MoveForward();

        /// <summary>
        /// Gets the current direction the robot is facing.
        /// </summary>
        /// <value>
        /// The current <see cref="Direction"/> of the robot.
        /// </value>
        Direction Direction { get; }

        /// <summary>
        /// Gets the current coordinates of the robot on the Mars grid.
        /// </summary>
        /// <value>
        /// A <see cref="Coordinates"/> object representing the robot's current position.
        /// </value>
        Coordinates Coordinates { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the robot is lost (has fallen off the grid).
        /// </summary>
        /// <value>
        /// <c>true</c> if the robot is lost; otherwise, <c>false</c>.
        /// </value>
        bool IsLost { get; set; }

        /// <summary>
        /// Calculates the next set of coordinates if the robot moves forward in its current direction.
        /// This method does not change the robot's actual position.
        /// </summary>
        /// <returns>
        /// A <see cref="Coordinates"/> object representing the robot's potential next position.
        /// </returns>
        Coordinates GetNextCoordinates();
    }
}
