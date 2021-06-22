# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY Product.API/*.csproj Product.API/
COPY Product.Application/*.csproj Product.Application/
COPY Product.Application.Command/*.csproj Product.Application.Command/
COPY Product.Application.Event/*.csproj Product.Application.Event/
COPY Product.Application.Query/*.csproj Product.Application.Query/
COPY Product.Infrastructure.Database.Abstractions/*.csproj Product.Infrastructure.Database.Abstractions/
COPY Product.Infrastructure.Database.MongoDB/*.csproj Product.Infrastructure.Database.MongoDB/
COPY Product.Models/*.csproj Product.Models/
RUN dotnet restore Product.API/Product.API.csproj

# copy and build app and libraries
COPY Product.API/ Product.API/
COPY Product.Application/ Product.Application/
COPY Product.Application.Command/ Product.Application.Command/
COPY Product.Application.Event/ Product.Application.Event/
COPY Product.Application.Query/ Product.Application.Query/
COPY Product.Infrastructure.Database.Abstractions/ Product.Infrastructure.Database.Abstractions/
COPY Product.Infrastructure.Database.MongoDB/ Product.Infrastructure.Database.MongoDB/
COPY Product.Models/ Product.Models/
WORKDIR /source/Product.API
RUN dotnet build -c release --no-restore

FROM build AS publish
RUN dotnet publish -c release --no-build -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/runtime:5.0
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Product.API.dll"]
