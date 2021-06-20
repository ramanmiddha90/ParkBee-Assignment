# ParkBee-Assignment - CQRS + MediatR ASP.NET Core API and SQL Server EF Core - Docker Containers

Garage API built using Asp.Net Core 3.1 and MediaR integrated with CQRS pattern in .Net Application

This API is exposing following Endpoints 

**Get Requests**

- api/v1/Garages/{garageId}
- pi/v1/Garages/{garageId}/doors

**Post Requests**

- api/v1/Garages/RefreshDoorStatus
- /token

internally using MediatR to handle request and response

# Frameworks and Libraries
* [Asp.Net Core 3.1](https://docs.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-3.1 )
* [MediatR](https://github.com/jbogard/MediatR)
* [CQRS](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
* [Ef Core](https://docs.microsoft.com/en-us/ef/core/)
* [SwashBuckle](https://github.com/domaindrivendev/Swashbuckle.WebApi)
* [FluentAssertion](https://github.com/fluentassertions/fluentassertions)
* [FluentValidator](https://github.com/FluentValidation/FluentValidation)
* [Moq](https://github.com/Moq/moq4/wiki/Quickstart)
* [MSTest](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)

# How to Test

## Pre-requisites

1.  [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
2.  [Docker Desktop](https://www.docker.com/products/docker-desktop) - Make sure its running ( Intall and reboot machine)

### Steps

1.  Launch ParkBee.sln in visual studio
2.  Set "docker-compose" as startup project
3.  Start the containers. Press "Run" button on visual studio
4.  Swagger URL will be launched https://localhost:63303/index.html



#### Sample Data in Database 

[SampleData](https://github.com/ramanmiddha90/ParkBee-Assignment/blob/main/SampleData.md)

Test below APIS 
```
https://localhost:63303/token

{
  "userName": "string",
  "password": "string"
}

```

Use this token in swagger authorize or in request header ( Authorization : Bearer {token} )

```
https://localhost:63303/api/v1/Garages/3D4D7FBC-08CC-4D4D-B7DA-B88A9EBA511D

```

```
https://localhost:63303/api/v1/Garages/3D4D7FBC-08CC-4D4D-B7DA-B88A9EBA511D\doors
```

```
https://localhost:63303/api/v1/Garages/RefreshDoorStatus

{
  "gargeId": "3d4d7fbc-08cc-4d4d-b7da-b88a9eba511d",
  "status": false,
  "doorId": "717decdd-69ba-43af-b5b5-94877f6d0ee4",
  "ipAddress": "garage1Door2IPId"
}
```

# Technical Questions

## What architectures or patterns are you using currently or have worked on recently?
Following architectures i am familiar and used

### Architecturs

- MVC
- Clent-Server
- CQRS + MediatR using DDD
- Onion Layer Architecture
- DDD
- MVVM

### Desgin Patterns

- Creational Design 
  - Factory
  - Abstract Factory
  - Singleton
  
- Structural Design
  - Adapter
  - Bridge
  - Facade
  
- Behavioral Pattern
  - Mediator
  - State
  - Strategy
 
## What do you think of them and would you want to implement it again?

Yes, I would always look for the right spot and place in code to integrate design pattern. These give us facility to make application structured and manageable.

## What version control system do you use or prefer?

I have used many versions control system in career.

- TFS
- Github ( For local)
- Azure Devops

## What is your favorite language feature and can you give a short snippet on how you use it?

My favourite language feature are : 

- Tuple
- Generics
- Delegates
- Nullable Types

**Tuple ** 

The tuples feature provides concise syntax to group multiple data elements in a lightweight data structure

One of the most common use cases of tuples is as a method return type. That is, instead of defining out method parameters, you can group method results in a tuple return type.

```
(int min, int max) FindMinMax(int[] input)
{
    if (input is null || input.Length == 0)
    {
        throw new ArgumentException("Cannot find minimum and maximum of a null or empty array.");
    }

    var min = int.MaxValue;
    var max = int.MinValue;
    foreach (var i in input)
    {
        if (i < min)
        {
            min = i;
        }
        if (i > max)
        {
            max = i;
        }
    }
    return (min, max);
}

var (minimum, maximum) = FindMinMax(ys);
Console.WriteLine($"Limits of [{string.Join(" ", ys)}] are {minimum} and {maximum}");
```

## What future or current technology do you look forward to the most or want to use and why?

I am pursuing my masters in Cyber security. So, I would love to use best cyber security principles in project. Also, I am passionate about azure architecture and design principle. So I am looking forward to use cyber security knowledge in Azure solutions.

My passion is to learn new technology concepts so I am also spending time to get my hands dirty on "React"

## How would you find a production bug/performance issue? Have you done this before?

Not frequently but in couple of instances, we were majorly dependent on logging of the system which helped us to track the issues.

## How would you improve the sample API (bug fixes, security, performance, etc.)?

I will make sure at least following things are implemented in API

- Caching Mechanism ( I recommend distributed cache like Redis) -Bug Diagnostic
- Async APIs to handle more requsts - Performance
- Database stored procedure and queries  Performance
- Logging ( Centralized logging like "Kafka", "Application Insight") - Bug Diagnostics
- Versioning 
- All endpoints are secured (CORS request, Token Authentication , Role authorization , owasp security guidlines implemented) - Security
- API deployed under scalable model  - Performance
