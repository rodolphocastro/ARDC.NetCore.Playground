# ARDC NetCore Playground

## Status

### Master

[![Build Status](https://travis-ci.com/rodolphocastro/ARDC.NetCore.Playground.svg?branch=master)](https://travis-ci.com/rodolphocastro/ARDC.NetCore.Playground)

### Develop

[![Build Status](https://travis-ci.com/rodolphocastro/ARDC.NetCore.Playground.svg?branch=develop)](https://travis-ci.com/rodolphocastro/ARDC.NetCore.Playground)

### Changelogs

The changelogs for this API can be found in the `CHANGELOG.md` file.

## About this Repository

This repository aims to provide examples on how to build a RESTFul API using Microsoft's dotNet Core 2.2.

## Docker

This repository is available at [DockerHub](https://cloud.docker.com/repository/docker/rodolphoalves/ardcnetcoreplaygroundapi).

### Running the image

You can use the following command to run this API: `docker run -p 80:80 -it -v C:\dockervolumes\playground:/app/settings ardcnetcoreplaygroundapi`

Change `c:\dockervolumes\playground` to wherever you store you `appsettings.docker.json`. Failing to map the settings file will lead to an `ArgumentNullException`.

## Building

To build this project you'll need the DotNet 2.2 SDK.

Then use the following CLI commands:

+ Restore Nuget Packages: `dotnet restore .\src\ARDC.NetCore.Playground.sln`
+ Build: `dotnet build .\src\ARDC.NetCore.Playground.sln`
+ Publish: `dotnet publish .\src\ARDC.NetCore.Playground.sln -c Release`

## Running and Testing

To run all the tests run the CLI command `dotnet test .\src\ARDC.NetCore.Playground.sln`.

To run the API, without docker, use the command `dotnet run --project .\src\ARDC.NetCore.Playground.API\`

## Technologies

The following technologies and packages are used in this project:

+ Docker
+ Travis-ci
+ DotNet Core 2.2
    + API
        + AspNet.Security.OAuth.GitHub
        + AutoMapper
        + Swashbuckle
    + Persistence
        + EntityFramework Core InMemory
        + Bogus
+ xUnit
    + FluentAssertions
    + Moq

## Contributing

If you find a bug or wish to make a suggestion feel free to open a new Issue.
