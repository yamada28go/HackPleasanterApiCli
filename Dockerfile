#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# --- net core のビルド用定義

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ["Generator/", "/Generator/"]
WORKDIR /Generator/Command/EntranceCommand
RUN dotnet restore "EntranceCommand.csproj"

# ビルド
WORKDIR "/Generator/Command/EntranceCommand"
RUN dotnet build "EntranceCommand.csproj" -c Release -o /app/build
RUN dotnet publish "EntranceCommand.csproj" -c Release -o /app/publish

# 実行対象にコピーする
FROM base AS publish
WORKDIR /app/publish
COPY --from=build /app/publish /app/publish

# --- 後工程の整形処理用に外部コマンドを使用できるようにする

# prettierを使えるようにする
# Node.jsのインストール
# NodeSourceから最新のNode.jsをインストールするためのコマンドを実行
RUN apt-get update && apt-get install -y curl 
RUN curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs
RUN npm install --global prettier
RUN npm install --global sql-formatter
RUN rm -rf /var/lib/apt/lists/*    

# 実行ファイルを指定
ENTRYPOINT ["dotnet", "/app/publish/EntranceCommand.dll"]
#CMD ["--help"]
#CMD ["while true; do sheep 1; done"]
#/bin/sh -c "while true; do sleep 1; done"

#デバッグ用　無限待ち
#ENTRYPOINT ["tail", "-f", "/dev/null"]