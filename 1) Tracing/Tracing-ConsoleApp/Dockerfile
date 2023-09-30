#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["1) Tracing/Tracing-ConsoleApp/Tracing-ConsoleApp.csproj", "1) Tracing/Tracing-ConsoleApp/"]
RUN dotnet restore "1) Tracing/Tracing-ConsoleApp/Tracing-ConsoleApp.csproj"
COPY . .
WORKDIR "/src/1) Tracing/Tracing-ConsoleApp"
RUN dotnet build "Tracing-ConsoleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tracing-ConsoleApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tracing-ConsoleApp.dll"]