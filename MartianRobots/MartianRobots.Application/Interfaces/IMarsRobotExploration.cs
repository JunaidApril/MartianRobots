using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Application.Interfaces
{
    /// <summary>
    /// Interface for managing the exploration process of robots roaming Mars.
    /// </summary>
    public interface IMarsRobotExploration
    {
        /// <summary>
        /// Initiates the exploration process using the given input data.
        /// </summary>
        /// <param name="inputs">
        /// A string containing exploration data, including Mars grid boundaries, 
        /// initial robot positions, and movement instructions.
        /// </param>
        void StartExploration(string inputs);

        /// <summary>
        /// Retrieves the final results of the robot exploration, including their final positions 
        /// and status (e.g., whether a robot is lost).
        /// </summary>
        /// <returns>
        /// A list of strings representing the outcomes of each robot's exploration.
        /// Each string contains the final coordinates, direction, and an optional "LOST" status.
        /// </returns>
        List<string> GetRobotsResultsOnMars();
    }
}
