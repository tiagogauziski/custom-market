# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY Stock.API/*.csproj Stock.API/
COPY Stock.Application/*.csproj Stock.Application/
COPY Stock.Domain/*.csproj Stock.Domain/
COPY Stock.Infrastructure.Database.Abstractions/*.csproj Stock.Infrastructure.Database.Abstractions/
COPY Stock.Infrastructure.Database.MongoDb/*.csproj Stock.Infrastructure.Database.MongoDb/
RUN dotnet restore Stock.API/Stock.API.csproj

# copy and build app and libraries
COPY Stock.API/ Stock.API/
COPY Stock.Application/ Stock.Application/
COPY Stock.Domain/ Stock.Domain/
COPY Stock.Infrastructure.Database.Abstractions/ Stock.Infrastructure.Database.Abstractions/
COPY Stock.Infrastructure.Database.MongoDb/ Stock.Infrastructure.Database.MongoDb/
WORKDIR /source/Stock.API
RUN dotnet build -c release --no-restore

FROM build AS publish
RUN dotnet publish -c release --no-build -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/runtime:5.0
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Stock.API.dll"]
