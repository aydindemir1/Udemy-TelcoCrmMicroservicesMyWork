FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 9500

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ./Packages /Packages
RUN dotnet nuget add source /Packages --name LocalPackages

# Context root olduğu için klasör adıyla birlikte yaz
COPY ApiGateway/ApiGateway.csproj ApiGateway/

RUN dotnet restore "ApiGateway/ApiGateway.csproj"

COPY . .

WORKDIR /src/ApiGateway

RUN dotnet build "ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.dll"]