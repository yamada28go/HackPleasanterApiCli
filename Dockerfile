# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:10.0-noble AS build
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

FROM node:24-bookworm-slim AS node-tools
ENV NPM_CONFIG_PREFIX=/opt/npm-global
ENV PATH="${NPM_CONFIG_PREFIX}/bin:${PATH}"
RUN npm install --global prettier@3.8.1 sql-formatter@15.7.3 \
    && npm cache clean --force

FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble AS final
WORKDIR /app
ENV NPM_CONFIG_PREFIX=/opt/npm-global
ENV PATH="${NPM_CONFIG_PREFIX}/bin:/usr/local/bin:${PATH}"

COPY --from=node-tools /usr/local/bin/node /usr/local/bin/node
COPY --from=node-tools /usr/local/lib/node_modules /usr/local/lib/node_modules
COPY --from=node-tools /usr/local/bin/npm /usr/local/bin/npm
COPY --from=node-tools /usr/local/bin/npx /usr/local/bin/npx
COPY --from=node-tools /opt/npm-global /opt/npm-global
COPY --from=build --chown=1000:1000 /app/publish/ ./

USER app

ENTRYPOINT ["dotnet", "/app/EntranceCommand.dll"]
