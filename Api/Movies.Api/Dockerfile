#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Api/Movies.Api/Movies.Api.csproj", "Movies.Api/"]
COPY ["Api/Movies.BL/Movies.BL.csproj", "Movies.BL/"]
COPY ["Api/Movies.DAL/Movies.DAL.csproj", "Movies.DAL/"]

RUN dotnet restore "Movies.Api/Movies.Api.csproj"

COPY Api/ .

WORKDIR "/src/Movies.Api"
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./Movies.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Movies.Api.dll"]