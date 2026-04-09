# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["HackPleasanterApi.sln", "./"]
COPY ["Directory.Build.props", "./"]
COPY ["global.json", "./"]
COPY ["Generator/Command/EntranceCommand/EntranceCommand.csproj", "Generator/Command/EntranceCommand/"]
COPY ["Generator/Command/DebugCommand/DebugCommand.csproj", "Generator/Command/DebugCommand/"]
COPY ["Generator/Command/GenerationCommand/GenerationCommand.csproj", "Generator/Command/GenerationCommand/"]
COPY ["Generator/Libs/HackPleasanterApi.Generator.CodeGenerator/HackPleasanterApi.Generator.CodeGenerator.csproj", "Generator/Libs/HackPleasanterApi.Generator.CodeGenerator/"]
COPY ["Generator/Libs/HackPleasanterApi.Generator.JsonDefinitionExtractor/HackPleasanterApi.Generator.JsonDefinitionExtractor.csproj", "Generator/Libs/HackPleasanterApi.Generator.JsonDefinitionExtractor/"]
COPY ["Generator/Libs/HackPleasanterApi.Generator.Library/HackPleasanterApi.Generator.Library.csproj", "Generator/Libs/HackPleasanterApi.Generator.Library/"]

RUN dotnet restore "Generator/Command/EntranceCommand/EntranceCommand.csproj" -r linux-x64

COPY ["Generator/", "Generator/"]

RUN dotnet publish "Generator/Command/EntranceCommand/EntranceCommand.csproj" \
    -c Release \
    -o /app/publish \
    /p:DebugType=None \
    /p:DebugSymbols=false \
    /p:TreatWarningsAsErrors=false \
    /p:CodeAnalysisTreatWarningsAsErrors=false

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app

RUN apt-get update \
    && apt-get install -y --no-install-recommends curl ca-certificates \
    && curl -fsSL https://deb.nodesource.com/setup_20.x | bash - \
    && apt-get install -y --no-install-recommends nodejs \
    && npm install --global prettier sql-formatter \
    && npm cache clean --force \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish/ ./

ENTRYPOINT ["dotnet", "/app/EntranceCommand.dll"]
