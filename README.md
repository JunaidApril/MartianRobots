# MartianRobots

Welcome to the Martian Robots Exploration! This application simulates the exploration of Mars by robotic vehicles, where the robots follow a set of instructions to explore a defined grid. The program accounts for the robot's boundaries, its movement, and whether it encounters any "scent marks" (indicating previous robots' lost positions).

	## Tech Stack
 
		.NET 8 : The application is built on .NET 8 to leverage modern C# features and performance improvements.
		Dependency Injection (DI): The project uses DI for managing dependencies, promoting loose coupling and making it easier to test.
		Unit Testing (NUnit): I used NUnit for unit testing, with mocking for interfaces to isolate the logic in tests.

	## Running Instructions

		### Prerequisites
  
			.NET SDK: You need to have the .NET SDK installed on your machine to run the application.
			Install .NET from the official website: Download .NET SDK
			
		### Steps to Run the Application

			Clone the repository: git clone https://github.com/your-repo/martian-robots.git
			
			Run the application: 
   
   				To start the program and simulate the Mars exploration
				In the program.cs file - you will find a method GetSampleInput() which returns an input string
				Replace this value for different input tests
				
		### Sample Input
  
			The input should be in the following format:
			
				5 3
				1 1 E
				RFRFRFRF

				3 2 N
				FRRFLLFFRRFLL

				0 3 W
				LLFFFLFLFL
				
			Each section represents:

				The boundary of the Mars grid (e.g., 5 3).
				The starting position of the robot (1 1 E).
				A series of instructions for the robot (RFRFRFRF).
			
			Expected Output:
			
				1 1 E
				3 3 N LOST
				0 3 W LOST
	
	## Project Architecture

		### Clean Architecture

			This project follows a Clean Architecture approach to maintain clear separation of concerns, making it scalable and easy to maintain. 
			The main components of the architecture are as follows:

			#### Domain Layer:

				##### Entities: 
					This contains the core entities such as Robot, Mars, Coordinates, etc., with their respective business logic.
			
				##### Interfaces: 
					Defines the contracts for services like IMarsRobotExploration, IMars, IRobot etc.
			
				##### Application Layer:
					Contains the service MarsRobotExploration, which handles the orchestration of the robot exploration process by interacting with the domain layer.
					Contains implementations for input validation and converting to required values for the entities.

				##### Presentation Layer:
					The Program.cs acts as the entry point, managing the user interface (i.e., reading input and showing results).
					
		### Dependency Injection (DI)
			DI is used to manage dependencies between the different layers. This is done in the Program.cs file where all dependencies are registered in the DI container:
			By using DI, components such as IMarsRobotExploration, IMars, and IInputValidatorAndConverter can be injected into each other, which helps to decouple the system and promote testability.
			
		### Mocking and Unit Testing
			Unit tests for various components have been written using NUnit and Moq. 
			For example, the IMars interface is mocked to simulate different conditions (such as when a robot moves out of bounds). This allows the behavior of the application to be tested in isolation.
		
		
	## Technical Choices
	
		### Why Clean Architecture?
			I chose Clean Architecture because it provides several key benefits:

			#### Separation of Concerns: 
				It clearly separates business logic, data access, and user interface concerns. This makes it easier to test and maintain.
			
			#### Scalability: 
				The architecture supports the addition of new features (e.g., new types of robots, different terrains) without tightly coupling new functionality to the existing codebase.
			
			#### Testability: 
				With Clean Architecture, unit tests are easier to write because dependencies are injected, and the core logic is isolated from external dependencies.
				
		### Why Dependency Injection?
			
			Dependency Injection (DI) helps achieve:

				#### Loose Coupling: 
					The classes are loosely coupled, making it easier to change or extend functionality without affecting other parts of the system.
				
				#### Testability: 
					It allows easier testing because we can inject mock implementations of the dependencies during testing.
					
		### Why Mocking?
		
			We use Moq for mocking interfaces to simulate different behaviors during testing. This allows us to isolate specific parts of the code and test them in various scenarios (e.g., robot moving out of bounds, scent encountered, etc.).
	

	## Conclusion
	
		This project implements a simulation for Mars robot exploration using Clean Architecture principles. 
		The use of Dependency Injection, Unit testing, and Mocking ensures that the application is maintainable, testable, and easy to scale. 
		With a simple yet effective user interface, the application can be easily extended to support more advanced features in the future.	


# Next steps I did not manage to find time to do:

	Add logging
	Add more unit tests for MarsRobotExplorationTests.cs
	Improve the UI - Create a front web front end where a user can input the data in textboxes according to Mars boundary, list of Robots with starting positions and instructions. For the console app - add ability to paste input sample data instead of adding it in program.cs 
 	Add configuration file - for values of the max co-ordiante range of 50 or the max input character string of 100 etc

