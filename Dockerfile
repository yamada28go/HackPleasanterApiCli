#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# --- net core のビルド用定義

FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
COPY ["Generator/", "/Generator/"]
WORKDIR /Generator/EntranceCommand
RUN dotnet restore "EntranceCommand.csproj"

# ビルド
WORKDIR "/Generator/EntranceCommand"
RUN dotnet build "EntranceCommand.csproj" -c Release -o /app/build


FROM build AS publish
WORKDIR "/Generator/EntranceCommand"
RUN dotnet publish "EntranceCommand.csproj" -c Release -o /app/publish
WORKDIR /app/publish

# 実行ファイルを指定
ENTRYPOINT ["dotnet", "/app/publish/EntranceCommand.dll"]
CMD ["--help"]

