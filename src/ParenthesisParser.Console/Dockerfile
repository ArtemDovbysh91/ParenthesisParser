#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["tests/ParenthesisParser.UnitTests/ParenthesisParser.UnitTests.csproj", "tests/ParenthesisParser.UnitTests/"]
COPY ["src/ParenthesisParser.Console/ParenthesisParser.Console.csproj", "src/ParenthesisParser.Console/"]
COPY ["src/ParenthesisParser.Application.FS/ParenthesisParser.Application.FS.fsproj", "src/ParenthesisParser.Application.FS/"]
COPY ["src/ParenthesisParser.Application/ParenthesisParser.Application.csproj", "src/ParenthesisParser.Application/"]
RUN dotnet restore "tests/ParenthesisParser.UnitTests/ParenthesisParser.UnitTests.csproj"
RUN dotnet restore "src/ParenthesisParser.Console/ParenthesisParser.Console.csproj"
COPY . .
WORKDIR "/src/src/ParenthesisParser.Console"
RUN dotnet build "ParenthesisParser.Console.csproj" -c Release -o /app/build

WORKDIR "/src/tests/ParenthesisParser.UnitTests/"
RUN dotnet test -c Release --logger:trx

FROM build AS publish
WORKDIR "/src/src/ParenthesisParser.Console"
RUN dotnet publish "ParenthesisParser.Console.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ParenthesisParser.Console.dll"]