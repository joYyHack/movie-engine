
# Movie Fetcher

> [!CAUTION]
> **❗❗The app is still in beta and running in development mode, so you may notice bugs and see errors in the console.**

Implementation of OMDB Api for searching movie. The app consists of the UI part that written uing React and Api written using DOTNET 6 and MSSQL as database.

UI allows user to look for desired movies and see the detailed information of the movie.

The API also Swagger for API documentation and testing.

## Table of Contents
- [Table of Contents](#table-of-contents)
- [Overview](#overview)
- [Installation](#installation)
  - [Prerequisites](#prerequisites)
  - [Clone](#clone)  
  - [Docker](#docker) 
  - [Start](#start)
  - [API Local](#api-local)
  - [Test](#test)
- [Features and Technologies Used](#features-and-technologies-used)
- [Examples](#examples)
  - [UI](#ui)
  - [Swagger](#swagger) 

## Overview

Implementation of OMDB API for searching movies. The app consists of a UI part written using React and an API written using .NET 6, with MSSQL as the database.

The UI allows users to search for desired movies and see detailed information about each movie.

The API supports Swagger for API documentation and testing.

## Installation

### Prerequisites
- Docker installed
- .NET 6
- Visual Studio or Visual Studio Code for local development
- Node.js and npm for the React frontend (if running outside of Docker)

### Clone

```bash
  git clone https://github.com/joYyHack/movie-engine.git
```
Move to the repo directory

```bash
  cd movie-engine
```

### Docker

Build and run the Docker containers:

```bash
  docker compose up --build
```
This will take a couple of minutes.

> [!CAUTION]
> If you run docker-compose next time, the API will likely fail because of migrations and database issues. There are three options to overcome this:
> 1. Run `docker-compose up --build` again, but in a separate terminal under the same path without terminating the first one.
> 2. First, run docker rm $(docker ps -a -q), and then run docker-compose up --build.
> 3. If you are using the Docker UI, you will notice that the API container has failed. You will be able to run it again.

### Start

- UI: http://localhost:3000/
- Swagger: http://localhost:5142/swagger/index.html

> The app is still in beta and is running in development mode, so you can notice bugs and  see the errors in the console.

### Api local

To run an API locally you need to run 

```bash
  docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Admin1234" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```
Then open the .sln file and run the .NET app as you usually do through Visual Studio or Visual Studio Code.e

> [!NOTE]
> Configuration data is located in the `appsettings.json` file, including the DbConnectionString, OMDb API key, Latest search return count, etc.

### Test

To run tests
```bash
  dotnet test ./Api
```

## Features and Technologies Used

- .NET 6
- EF Core 6
- AutoMapper
- Unit of Work pattern
- Custom attributes
- Custom middleware
- Implementation of the base repository
- Implementation of the base response object
- Swagger support
- Implementation of the third-party API - OMDb API
- Docker Compose usage
- Infinite scroll on the UI
- 3-Tier Architecture
- Github Action

## Examples
### UI
![ScreenRecording2024-06-27at00 41 01-ezgif com-video-to-gif-converter](https://github.com/joYyHack/movie-engine/assets/94608729/c7199549-a9c3-41d4-abca-96cd5915df3b)
### Swagger
https://github.com/joYyHack/movie-engine/assets/94608729/cef3e9f8-2bcd-463a-83ed-593543318506
