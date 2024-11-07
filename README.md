# Rock, Paper, Scissors, Spock, Lizard game - sample Web Api

This repository contains a sample .NET 8 Web Api project, supporting [**Rock, Paper, Scissors, Spock, Lizard**](http://www.samkass.com/theories/RPSSL.html) game and built using Clean Architecture principles. Project also includes unit, integration, architecture and E2E tests.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [PostgreSQL](https://www.postgresql.org/).

## Project Structure
The project follows Clean Architecture. Codebase is organized into layers, with domain layer in the center.
The project consists of following layers:

- **Domain layer**: Contains core business logic and domain entities.
- **Application layer**: Implements use cases. It includes commands, queries, handlers, and DTOs. Mediates between the domain and other layers.
- **Infrastructure layer**: Implements database access and external services.
- **Presentation layer**: Includes api controllers and global exception handler.

## Main Features
- Clean Architecture design (Domain, Application, Presentation, Infrastructure, Web API)
- **.NET 8** with **Entity Framework Core**
- **Docker** support with **PostgreSQL** integration
- Healthcheck `https://localhost:5001/health`
- Logging with **Serilog**
- Unit, Integration, E2E and Architecture tests with **Xunit**, **Mock**, **Testcontainers**, **FluentAssertions** and **NetArchTest.Rules**.
- Global Exception Handler
- **FluentValidation** integrated in the **MeadiatR** pipeline
- **CQRS** with **MediatR**
- Typed Http Client with retries (**Polly**)
- **Result/Error** pattern
- **Unit of Work (UOW)** pattern

## API
### Endpoints
Method | URI | Purpose
------ |  ------------ | -------
GET | /choices | Get a list of choice names and their numerical values.
GET | /choice | Get a random choice from the server.
POST | /play | Submit a choice to play a round against computer.
GET | /scores/getLatestScores | Get 10 most recent scores.
DELETE | /scores/resetScoreboard | Reset scoreboard.

#### GET /choices
Response (application/json):
```
[
  {
    “id": integer [1-5],
    "name": string [12] (rock, paper, scissors, lizard, spock)
  }
]
```

#### GET /choice
Response (application/json):
```
{
  "id": integer [1-5],
  "name" : string [12] (rock, paper, scissors, lizard, spock)
}
```

#### POST /play

POST data (application/json):
```
{
  “player”: integer [1-5] (choice_id)
}
```

Response (application/json):
``` 
{
  "results": string (win, lose, tie),
  “player”: integer [1-5] (choice_id),
  “computer”: integer [1-5] (choice_id)
}
```
#### GET /scores/getLatestScores
Response (application/json):
```
{
  "playerOne": "string",
  "playerTwo": "string",
  "result": string (win, lose, tie),
  "time": "2024-11-04T08:27:20.609Z"
}
```
#### DELETE /scores/resetScoreboard

## Running the Project

1. Clone the repository:
   ```bash
   git clone https://github.com/IvanSule/gameapidemo.git
   ```
2. Run project from terminal (solution folder):
   - Start container with Docker-compose:
     ```bash
     docker-compose up
     ```
     Delete container with:
     ```bash
     docker-compose down
     ```
   - Local: PostgreSQL instance must be running and reachable. Update the connection string in `appsettings.Development.json` to match PostgreSQL's host and username/password, and run:
     ```bash
     dotnet run --project src\RPSSL.WebApi\RPSSL.WebApi.csproj --launch-profile "https"
     ```
     After that, Api will be accessible on `https://localhost:5001`. Swagger page will be accessible on `https://localhost:5001/swagger/index.html`.
3. Run the tests:
   ```bash
   dotnet test RPSSL.sln
   ```
## Credits
- Game rules - http://www.samkass.com/theories/RPSSL.html
- Random numbers - https://codechallenge.boohma.com/random
- Result/Error pattern - https://www.milanjovanovic.tech/blog/functional-error-handling-in-dotnet-with-the-result-pattern
