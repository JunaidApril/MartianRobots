using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Services;
using MartianRobots.Domain.Entities;
using MartianRobots.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

// Configure services and build the service provider
var serviceProvider = ConfigureServices();

// Run the application
RunApplication(serviceProvider);


/// <summary>
/// Configures the dependency injection container and returns a service provider.
/// </summary>
static ServiceProvider ConfigureServices()
{
    var services = new ServiceCollection();

    // Register services
    services.AddSingleton<IMars, Mars>();
    services.AddSingleton<IInputValidatorAndConverter, InputValidatorAndConverter>();
    services.AddSingleton<IRobot, Robot>();
    services.AddSingleton<IMarsRobotExploration, MarsRobotExploration>();

    return services.BuildServiceProvider();
}

/// <summary>
/// Runs the Mars Robot Exploration application.
/// </summary>
static void RunApplication(ServiceProvider serviceProvider)
{
    var exploration = serviceProvider.GetRequiredService<IMarsRobotExploration>();

    DisplayConsoleMessage("Welcome to Martian Robots Exploration!");

    // Replace with var input = Console.ReadLine(); to enter input via console else replace input in GetSampleInput()
    var input = GetSampleInput();

    if (!string.IsNullOrWhiteSpace(input))
    {
        DisplayInput(input);
        DisplayConsoleMessage("The robots are roaming...");
        exploration.StartExploration(input);
        DisplayConsoleMessage("The Exploration is complete with the following outcomes...");
        DisplayResults(exploration.GetRobotsResultsOnMars());
    }
    else
    {
        DisplayConsoleMessage("No input provided. Exiting.");
    }

    DisplayConsoleMessage("Thank you for joining us on this voyage. Goodbye");
}

/// <summary>
/// Displays a message on the console and pauses for 2 seconds.
/// </summary>
/// <param name="message">The message to be displayed on the console.</param>
static void DisplayConsoleMessage(string message)
{
    Console.WriteLine(message);
    Console.WriteLine();
    Thread.Sleep(2000);
}

/// <summary>
/// Displays a message indicating that the input for exploration is being displayed, followed by the actual input.
/// </summary>
/// <param name="input">The input string that will be displayed for the exploration.</param>
static void DisplayInput(string input)
{
    DisplayConsoleMessage($"Displaying input for the exploration...");
    DisplayConsoleMessage(input);
}

/// <summary>
/// Retrieves a sample input string. Replace or extend to test any input.
/// </summary>
static string GetSampleInput()
{
    return "5 3\r\n1 1 E\r\nRFRFRFRF\r\n\r\n3 2 N\r\nFRRFLLFFRRFLL\r\n\r\n0 3 W\r\nLLFFFLFLFL";
}

/// <summary>
/// Displays the exploration results.
/// </summary>
static void DisplayResults(List<string> robotOutcomes)
{
    foreach (var outcome in robotOutcomes)
    {
        DisplayConsoleMessage(outcome);
    }
}
