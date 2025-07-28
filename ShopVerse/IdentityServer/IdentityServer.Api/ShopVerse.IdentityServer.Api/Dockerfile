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
COPY ["IdentityServer/IdentityServer.Api/ShopVerse.IdentityServer.Api/ShopVerse.IdentityServer.Api.csproj", "IdentityServer/IdentityServer.Api/ShopVerse.IdentityServer.Api/"]
COPY ["InventoryService/IdentityServer.Infrastructure/ShopVerse.IdentityServer.Infrastructure/ShopVerse.IdentityServer.Infrastructure.csproj", "InventoryService/IdentityServer.Infrastructure/ShopVerse.IdentityServer.Infrastructure/"]
COPY ["IdentityServer/IdentityServer.Application/ShopVerse.IdentityServer.Application/ShopVerse.IdentityServer.Application.csproj", "IdentityServer/IdentityServer.Application/ShopVerse.IdentityServer.Application/"]
COPY ["IdentityServer/IdentityServer.Domain/ShopVerse.IdentityServer.Domain/ShopVerse.IdentityServer.Domain.csproj", "IdentityServer/IdentityServer.Domain/ShopVerse.IdentityServer.Domain/"]
RUN dotnet restore "./IdentityServer/IdentityServer.Api/ShopVerse.IdentityServer.Api/ShopVerse.IdentityServer.Api.csproj"
COPY . .
WORKDIR "/src/IdentityServer/IdentityServer.Api/ShopVerse.IdentityServer.Api"
RUN dotnet build "./ShopVerse.IdentityServer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ShopVerse.IdentityServer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopVerse.IdentityServer.Api.dll"]