using MartianRobots.Application.Interfaces;
using MartianRobots.Domain.Entities;
using MartianRobots.Domain.Enums;
using MartianRobots.Domain.Errors;
using MartianRobots.Domain.Interfaces;

namespace MartianRobots.Application.Services
{
    public class MarsRobotExploration : IMarsRobotExploration
    {
        private IMars _mars;
        private IInputValidatorAndConverter _inputConverterAndValidator;
        private IRobot _robot;
        private List<string> _martianRobotsOutcome = new List<string>();

        public MarsRobotExploration(IMars mars, IInputValidatorAndConverter inputValidator)
        {
            _inputConverterAndValidator = inputValidator;
            _mars = mars;
        }

        public void StartExploration(string inputs)
        {
            try
            {
                bool marsCreated = false;
                var robotCreated = false;

                if(string.IsNullOrEmpty(inputs))
                    throw new ArgumentException(ErrorMessage.InvalidInputs);

                var inputsArray = inputs.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                if (inputsArray.Length == 0)
                    throw new ArgumentException(ErrorMessage.InvalidInputs);

                foreach (var input in inputsArray)
                {
                    if (!marsCreated) 
                    {
                        CreateMars(input);
                        marsCreated = true;
                        continue;
                    }

                    if (input == "")
                    {
                        robotCreated = false;
                        continue;
                    }

                    if (!robotCreated) 
                    {
                        CreateRobot(input);
                        robotCreated = true;
                        continue;
                    }

                    var instructionsArray = _inputConverterAndValidator.ValidateAndConvertInstructions(input);
                    InstructRobot(instructionsArray);
                    AddRobotOutcome();

                }

                GetRobotsResultsOnMars();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                Console.WriteLine(ErrorMessage.UserFriendlyForException);
            }
        }

        private void CreateMars(string input)
        {
            _mars.Create(_inputConverterAndValidator.ValidateAndConvertBoundaryCoordinates(input));
        }

        private void CreateRobot(string input)
        {
            var startingPositionConverted = _inputConverterAndValidator.ValidateAndConvertStartingPosition(input);
            _robot = new Robot(startingPositionConverted.Coordinates, startingPositionConverted.Direction);
        }

        private void InstructRobot(IEnumerable<string> instructionsArray)
        {
            foreach (var instruct in instructionsArray)
            {
                Enum.TryParse(instruct, out Instruction instruction);
                switch (instruction)
                {
                    case Instruction.L:
                        _robot.TurnLeft();
                        break;

                    case Instruction.R:
                        _robot.TurnRight();
                        break;

                    case Instruction.F:
                        MoveRobot();
                        CheckRobotLocation();
                        break;

                    default:
                        break;
                }
            }
        }

        private void MoveRobot()
        {
            var newCoordinates = _robot.GetNextCoordinates();

            if (CheckForScents(newCoordinates.X, newCoordinates.Y))
                return;

            _robot.MoveForward();
        }

        bool CheckForScents(int xCoOrdinate, int yCoOrdinate)
        {
            if (_mars.ScentCoordinates.Where(x => x.X == xCoOrdinate && x.Y == yCoOrdinate).Any())
                return true;

            return false;
        }

        void CheckRobotLocation()
        {
            var inbounds = _mars.IsRobotInbounds(_robot.Coordinates);

            if (!inbounds)
                _robot.IsLost = true;
        }

        private void AddRobotOutcome()
        {
            var robotDirection = _robot.Direction;
            var robotCoOrdinates = _robot.Coordinates;
            var outbounds = _robot.IsLost ? "LOST" : "";
            _martianRobotsOutcome.Add($"{robotCoOrdinates.X} {robotCoOrdinates.Y} {robotDirection} {outbounds.Trim()}");
        }

        public List<string> GetRobotsResultsOnMars()
        {
            return _martianRobotsOutcome;
        }
    }
}
