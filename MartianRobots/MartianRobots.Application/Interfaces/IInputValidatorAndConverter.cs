﻿using MartianRobots.Domain.Enums;
using MartianRobots.Domain.ValueObjects;

namespace MartianRobots.Application.Interfaces
{
    public interface IInputValidatorAndConverter
    {
        Coordinates ValidateAndConvertBoundaryCoordinates(string boundary);

        (Coordinates, Direction) ValidateAndConvertStartingPosition(string startingPosition);

        IEnumerable<string> ValidateAndConvertInstructions(string instructions);
    }
}