using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Services;
using MartianRobots.Domain.Entities;
using MartianRobots.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<IMars, Mars>();
services.AddSingleton<IInputValidatorAndConverter, InputValidatorAndConverter>();
services.AddSingleton<IRobot, Robot>();
services.AddSingleton<IMarsRobotExploration, MarsRobotExploration>();

var serviceProvider = services.BuildServiceProvider();

var exploration = serviceProvider.GetRequiredService<IMarsRobotExploration>();

Console.WriteLine("Welcome to Mars Martian Robots Exploration!");

Console.WriteLine("Provide Input");

//var input = Console.ReadLine();

// Sample input string
var input = "5 3\r\n1 1 E\r\nRFRFRFRF\r\n\r\n3 2 N\r\nFRRFLLFFRRFLL\r\n\r\n0 3 W\r\nLLFFFLFLFL";

if (!string.IsNullOrEmpty(input))
{
    exploration.StartExploration(input);

    var robotOutcomes = exploration.GetRobotsResultsOnMars();

    foreach (var outcome in robotOutcomes)
    {
        Console.WriteLine(outcome.ToString());
    }
}
