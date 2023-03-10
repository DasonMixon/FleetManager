#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/FleetManager.csproj", "."]
RUN dotnet restore "./FleetManager.csproj"
COPY src/ .
WORKDIR "/src/."
RUN dotnet build "FleetManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FleetManager.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FleetManager.dll"]