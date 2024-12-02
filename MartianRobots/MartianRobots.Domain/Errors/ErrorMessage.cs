using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Domain.Errors
{
    public static class ErrorMessage
    {
        public const string InvalidBoundaryCoOrdinateRange = "Invalid boundary co-ordinate range. Values have to be greater than 0 and not more than 50";

        public const string InvalidRobotStartingCoOrdinatesRange = "Invalid Martian Robot start position co-ordinate range. Values have to be greater than 0 and not more than 50";

        public const string InvalidDirection = "Invalid direction. Valid values are N, E, S, W.";
    }
}
