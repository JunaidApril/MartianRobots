namespace MartianRobots.Domain.Errors
{
    public static class ErrorMessage
    {
        public const string InvalidBoundaryCoordinateRange = "Invalid boundary co-ordinate range. Values have to be greater than 0 and not more than 50";

        public const string InvalidRobotStartingCoordinatesRange = "Invalid Martian Robot start position co-ordinate range. Values have to be greater than 0 and not more than 50";

        public const string InvalidDirection = "Invalid direction. Valid values are N, E, S, W.";

        public const string InvalidInputs = "There was no valid inputs";

        public const string UserFriendlyForException = "The program was force to close due to unforeseen circumstances. Please try again later.";

        public const string InvalidBoundaryCoordinates = "Invalid boundary co-ordinates format. Expected format: 'x y'";

        public const string InvalidCoordinates = "Coordinates must be integers.";

        public const string InvalidRobotStartingCoordinates = "Martian Robot starting co-ordinates and starting direction is invalid. Expected format: 'x y N'";

        public const string InstructionLengthExceeded = "Instruction has to be less than 100 characters in length.";

        public const string NoInstructionProvided = "No instructions was provided. Valid values are LRF";

        public const string InvalidInstructionProvided = "Invalid instruction was provided. Valid values are LRF";
    }
}
