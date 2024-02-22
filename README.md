# Match Odds Service

A REST Web API for managing sports matches and their betting odds, developed as a technical assignment in the span of 3 days.

## Requirements

### Infrastructure
For the development of this project, the following requirements were requested:

* Use of [ASP.NET Core 3.1](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1)
* Use of [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
* Use of [Microsoft SQL Server](https://learn.microsoft.com/en-us/sql/sql-server/)
* Creation of an initial EF migration

### Entities

For the representation of sports matches and their betting odds, the following entity models were to be created:

**Match**, with properties:

* ID
* Description
* MatchDate
* MatchTime
* TeamA
* TeamB
* Sport *(enum with values 1 for Football and 2 for Basketball)*

**MatchOdds**, with properties:

* ID
* MatchId
* Specifier *(1, X, 2)*
* Odd

#### Examples

**Match**
|ID|Description|MatchDate |MatchTime|TeamA|TeamB|Sport|
|--|-----------|----------|---------|-----|-----|-----|
|1 |OSFP-PAO   |2021-03-19|12:00    |OSFP |PAO  |1    |

**MatchOdds**
|ID|MatchId|Specifier|Odd|
|--|-------|---------|---|
|1 |1      |X        |1.5|

### Service

The deliverable would be a functional web service that can run autonomously (or in a container as a plus), with the following features:

* An API controller that implements CRUD functions through the proper use of HTML verbs
* A design / implementation document in the assignee's desired format (Markdown, Word document, [Swagger](https://swagger.io/))
* Source code is to be hosted on a public repository from one of the popular version control online services (e.g. GitHub, BitBucket).

## Implementation

Two API controllers have been implemented for the service:

* ``MatchesController``, which is being served on ``base_url/api/matches``
* ``MatchOddsController``, which is being served on ``base_url/api/matchodds``

Both of these controllers support ``GET``, ``POST``, ``PUT`` and ``DELETE`` operations.

The design / implementation document has been implemented as a Swagger document through [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore), which can be viewed on ``base_url/index.html`` upon running the project successfully.

### Build & Run (Docker)

Ensure you have the required dependencies installed:

* Visual Studio *(latest version preferred)*
* .NET Core 3.1 Runtime
* MS SQL Server *(latest version preferred)*

To build and run the project, open the solution in Visual Studio and run the "Container (Dockerfile)" project.

The service should be available at http://localhost:8001.

### Build & Run (Manual)

Ensure you have the required dependencies installed:

* .NET Core 3.1 Runtime
* MS SQL Server *(latest version preferred)*

To build and run the project, run:

```
dotnet restore
dotnet run --project MatchOddsService
```

The service should be available at https://localhost:5001.
