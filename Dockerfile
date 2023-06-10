#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# --- net core のビルド用定義

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ["Generator/", "/Generator/"]
WORKDIR /Generator/EntranceCommand
RUN dotnet restore "EntranceCommand.csproj"

# ビルド
WORKDIR "/Generator/EntranceCommand"
RUN dotnet build "EntranceCommand.csproj" -c Release -o /app/build
RUN dotnet publish "EntranceCommand.csproj" -c Release -o /app/publish

# 実行対象にコピーする
FROM base AS publish
WORKDIR /app/publish
COPY --from=build /app/publish /app/publish

# 実行ファイルを指定
ENTRYPOINT ["dotnet", "/app/publish/EntranceCommand.dll"]
CMD ["--help"]

