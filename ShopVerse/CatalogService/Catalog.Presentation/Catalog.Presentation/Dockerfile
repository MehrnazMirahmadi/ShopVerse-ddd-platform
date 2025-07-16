# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CatalogService/Catalog.Presentation/Catalog.Presentation/Catalog.Presentation.csproj", "CatalogService/Catalog.Presentation/Catalog.Presentation/"]
COPY ["CatalogService/Catalog.Application/Catalog.Application/Catalog.Application.csproj", "CatalogService/Catalog.Application/Catalog.Application/"]
COPY ["ShopVerse.BuildingBlocks.Messaging/ShopVerse.BuildingBlocks.Messaging/ShopVerse.BuildingBlocks.Messaging.csproj", "ShopVerse.BuildingBlocks.Messaging/ShopVerse.BuildingBlocks.Messaging/"]
COPY ["CatalogService/CatalogService.Domain/CatalogService.Domain/Catalog.Domain.csproj", "CatalogService/CatalogService.Domain/CatalogService.Domain/"]
COPY ["ShopVerse.BuildingBlocks/ShopVerse.BuildingBlocks.csproj", "ShopVerse.BuildingBlocks/"]
COPY ["CatalogService/Catalog.Infrastructure/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "CatalogService/Catalog.Infrastructure/Catalog.Infrastructure/"]
RUN dotnet restore "./CatalogService/Catalog.Presentation/Catalog.Presentation/Catalog.Presentation.csproj"
COPY . .
WORKDIR "/src/CatalogService/Catalog.Presentation/Catalog.Presentation"
RUN dotnet build "./Catalog.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Catalog.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Presentation.dll"]