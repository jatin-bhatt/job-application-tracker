# job-application-tracker

# React UI and .NET Web API Backend Project

This project consists of two main components:
1. **JobApplicationTracker.UI**: A React application that interacts with the backend API.
2. **JobApplicationTracker**: A .NET Web API serving as the backend for the React application.

## Prerequisites

Before running the project, ensure you have the following tools installed:

- [Node.js](https://nodejs.org/) (for frontend development)
- [npm](https://www.npmjs.com/) or [yarn](https://yarnpkg.com/) (for managing frontend dependencies)
- [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) (for backend development)
- [.NET SDK](https://dotnet.microsoft.com/download) (for backend development)


## Architecture

This application is built with the idea of Clean Architecture using mediator pattern. We try to separate the complicated business logic into command handlers.

### Project Structure
- Domain 
	- This is the heart of the application. All other layers depend on this, where domain experts and engineers define the business models and rules. 
	- This layer is persistent ignorant and independent of other layer
- Application
	- This layer is mainly for defining application logic and command handlers. This layer depends on the Domain layer as a dependency.
- Infrastructure
	- The Infrastructure layer defines the persistence technology and interactions with external applications.

## Project Setup

### Backend Setup

1. Navigate to the `JobApplicationTracker` directory:
    cd JobApplicationTracker
2. DB Migrations
    It is using SQlite and database file is created JobApplicationTracker.db. 
    For refreshing migration, remove database file "JobApplicationTracker.db", then Open Package Manager Console and select default project as Infrastructure. Run 1) "Remove-Migration" 2) "Add-Migration InitialCreate" 3) Update-Database 
3. Build Solution:
    Open JobApplicationTracker.sln  in Visual Studio and click Build/Rebuild Solution
    Select IIS Express and run (hit F5)

The Web API should now be running on http://localhost:53320

Swagger URL - http://localhost:53320/index.html 

### Frontend Setup

1. Navigate to the `JobApplicationTracker.UI` directory:
    cd JobApplicationTracker.UI
2. Install dependencies:
    npm install
3. Run the React app:
    npm run build
    npm run dev

The React app should now be running on http://localhost:5173


### Assumption
1. For simiplicity, Job Application Status is kept under Enumeration instead of user entered dynamic values
2. Authentication/Authorization is ignored.
3. Basic Input Validations are applied