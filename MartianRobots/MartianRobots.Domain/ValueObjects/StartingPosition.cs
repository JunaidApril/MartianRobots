using MartianRobots.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Domain.ValueObjects
{
    public class StartingPosition
    {
        public StartingPosition(Coordinates coordinates, Direction direction)
        {
            Coordinates = coordinates;
            Direction = direction;
        }

        public Coordinates Coordinates { get; set; }

        public Direction Direction { get; set; }
    }
}
