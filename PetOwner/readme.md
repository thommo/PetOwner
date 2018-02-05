AGL Coding Test Submission by Michael Thompson

The application in this repository is a .NET Core 2 Console application. It was developed 
using Visual Studio 17 and requires C# 7.1 or greater. It can be run from Visual Studio or 
by running "dotnet PetOwner.dll" from the command line. It will output the desired results
as a JSON string to the console.

Configuration for the application is contained in config.json. This file contains the url for 
the pet owner web service and logging configuration.

The PetOwnerService class implements both synchronous and asynchronous methods. A real world
application would likely use the asynchronous version for performance. For a small console app
such as this it makes little difference, but I included both implementations for completeness.

Unit tests are contained in a separate project, PetOwnerTests, and can be run from the 
Visual Studio Test Explorer.
